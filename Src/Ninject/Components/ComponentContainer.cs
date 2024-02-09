// Decompiled with JetBrains decompiler
// Type: Ninject.Components.ComponentContainer
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Components
{
  /// <summary>
  /// An internal container that manages and resolves components that contribute to Ninject.
  /// </summary>
  public class ComponentContainer : DisposableObject, IComponentContainer, IDisposable
  {
    private readonly Multimap<Type, Type> _mappings = new Multimap<Type, Type>();
    private readonly Dictionary<Type, INinjectComponent> _instances = new Dictionary<Type, INinjectComponent>();
    private readonly HashSet<KeyValuePair<Type, Type>> transients = new HashSet<KeyValuePair<Type, Type>>();

    /// <summary>
    /// Gets or sets the kernel that owns the component container.
    /// </summary>
    public IKernel Kernel { get; set; }

    /// <summary>Releases resources held by the object.</summary>
    public override void Dispose(bool disposing)
    {
      if (disposing && !this.IsDisposed)
      {
        foreach (IDisposable disposable in this._instances.Values)
          disposable.Dispose();
        this._mappings.Clear();
        this._instances.Clear();
      }
      base.Dispose(disposing);
    }

    /// <summary>Registers a component in the container.</summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <typeparam name="TImplementation">The component's implementation type.</typeparam>
    public void Add<TComponent, TImplementation>()
      where TComponent : INinjectComponent
      where TImplementation : TComponent, INinjectComponent
    {
      this._mappings.Add(typeof (TComponent), typeof (TImplementation));
    }

    /// <summary>Registers a transient component in the container.</summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <typeparam name="TImplementation">The component's implementation type.</typeparam>
    public void AddTransient<TComponent, TImplementation>()
      where TComponent : INinjectComponent
      where TImplementation : TComponent, INinjectComponent
    {
      this.Add<TComponent, TImplementation>();
      this.transients.Add(new KeyValuePair<Type, Type>(typeof (TComponent), typeof (TImplementation)));
    }

    /// <summary>
    /// Removes all registrations for the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    public void RemoveAll<T>() where T : INinjectComponent => this.RemoveAll(typeof (T));

    /// <summary>Removes the specified registration.</summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    public void Remove<T, TImplementation>()
      where T : INinjectComponent
      where TImplementation : T
    {
      Type key = typeof (TImplementation);
      if (this._instances.ContainsKey(key))
        this._instances[key].Dispose();
      this._instances.Remove(key);
      this._mappings[typeof (T)].Remove(typeof (TImplementation));
    }

    /// <summary>
    /// Removes all registrations for the specified component.
    /// </summary>
    /// <param name="component">The component type.</param>
    public void RemoveAll(Type component)
    {
      Ensure.ArgumentNotNull((object) component, nameof (component));
      foreach (Type key in (IEnumerable<Type>) this._mappings[component])
      {
        if (this._instances.ContainsKey(key))
          this._instances[key].Dispose();
        this._instances.Remove(key);
      }
      this._mappings.RemoveAll(component);
    }

    /// <summary>Gets one instance of the specified component.</summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The instance of the component.</returns>
    public T Get<T>() where T : INinjectComponent => (T) this.Get(typeof (T));

    /// <summary>
    /// Gets all available instances of the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>A series of instances of the specified component.</returns>
    public IEnumerable<T> GetAll<T>() where T : INinjectComponent
    {
      return this.GetAll(typeof (T)).Cast<T>();
    }

    /// <summary>Gets one instance of the specified component.</summary>
    /// <param name="component">The component type.</param>
    /// <returns>The instance of the component.</returns>
    public object Get(Type component)
    {
      Ensure.ArgumentNotNull((object) component, nameof (component));
      if (component == typeof (IKernel))
        return (object) this.Kernel;
      if (component.IsGenericType)
      {
        Type genericTypeDefinition = component.GetGenericTypeDefinition();
        Type genericArgument = component.GetGenericArguments()[0];
        if (genericTypeDefinition.IsInterface && typeof (IEnumerable<>).IsAssignableFrom(genericTypeDefinition))
          return (object) this.GetAll(genericArgument).CastSlow(genericArgument);
      }
      Type implementation = this._mappings[component].FirstOrDefault<Type>();
      return !(implementation == (Type) null) ? this.ResolveInstance(component, implementation) : throw new InvalidOperationException(ExceptionFormatter.NoSuchComponentRegistered(component));
    }

    /// <summary>
    /// Gets all available instances of the specified component.
    /// </summary>
    /// <param name="component">The component type.</param>
    /// <returns>A series of instances of the specified component.</returns>
    public IEnumerable<object> GetAll(Type component)
    {
      Ensure.ArgumentNotNull((object) component, nameof (component));
      return this._mappings[component].Select<Type, object>((Func<Type, object>) (implementation => this.ResolveInstance(component, implementation)));
    }

    private object ResolveInstance(Type component, Type implementation)
    {
      lock (this._instances)
        return this._instances.ContainsKey(implementation) ? (object) this._instances[implementation] : this.CreateNewInstance(component, implementation);
    }

    private object CreateNewInstance(Type component, Type implementation)
    {
      ConstructorInfo constructorInfo = ComponentContainer.SelectConstructor(component, implementation);
      object[] array = ((IEnumerable<ParameterInfo>) constructorInfo.GetParameters()).Select<ParameterInfo, object>((Func<ParameterInfo, object>) (parameter => this.Get(parameter.ParameterType))).ToArray<object>();
      try
      {
        INinjectComponent newInstance = constructorInfo.Invoke(array) as INinjectComponent;
        newInstance.Settings = this.Kernel.Settings;
        if (!this.transients.Contains(new KeyValuePair<Type, Type>(component, implementation)))
          this._instances.Add(implementation, newInstance);
        return (object) newInstance;
      }
      catch (TargetInvocationException ex)
      {
        ex.RethrowInnerException();
        return (object) null;
      }
    }

    private static ConstructorInfo SelectConstructor(Type component, Type implementation)
    {
      ConstructorInfo constructorInfo = ((IEnumerable<ConstructorInfo>) implementation.GetConstructors()).OrderByDescending<ConstructorInfo, int>((Func<ConstructorInfo, int>) (c => c.GetParameters().Length)).FirstOrDefault<ConstructorInfo>();
      return !(constructorInfo == (ConstructorInfo) null) ? constructorInfo : throw new InvalidOperationException(ExceptionFormatter.NoConstructorsAvailableForComponent(component, implementation));
    }
  }
}
