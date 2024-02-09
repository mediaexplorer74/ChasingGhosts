// Decompiled with JetBrains decompiler
// Type: Ninject.Components.IComponentContainer
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Components
{
  /// <summary>
  /// An internal container that manages and resolves components that contribute to Ninject.
  /// </summary>
  public interface IComponentContainer : IDisposable
  {
    /// <summary>
    /// Gets or sets the kernel that owns the component container.
    /// </summary>
    IKernel Kernel { get; set; }

    /// <summary>Registers a component in the container.</summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <typeparam name="TImplementation">The component's implementation type.</typeparam>
    void Add<TComponent, TImplementation>()
      where TComponent : INinjectComponent
      where TImplementation : TComponent, INinjectComponent;

    /// <summary>
    /// Removes all registrations for the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    void RemoveAll<T>() where T : INinjectComponent;

    /// <summary>
    /// Removes all registrations for the specified component.
    /// </summary>
    /// <param name="component">The component's type.</param>
    void RemoveAll(Type component);

    /// <summary>Removes the specified registration.</summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    void Remove<T, TImplementation>()
      where T : INinjectComponent
      where TImplementation : T;

    /// <summary>Gets one instance of the specified component.</summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The instance of the component.</returns>
    T Get<T>() where T : INinjectComponent;

    /// <summary>
    /// Gets all available instances of the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>A series of instances of the specified component.</returns>
    IEnumerable<T> GetAll<T>() where T : INinjectComponent;

    /// <summary>Gets one instance of the specified component.</summary>
    /// <param name="component">The component type.</param>
    /// <returns>The instance of the component.</returns>
    object Get(Type component);

    /// <summary>
    /// Gets all available instances of the specified component.
    /// </summary>
    /// <param name="component">The component type.</param>
    /// <returns>A series of instances of the specified component.</returns>
    IEnumerable<object> GetAll(Type component);

    /// <summary>Registers a transient component in the container.</summary>
    /// <typeparam name="TComponent">The component type.</typeparam>
    /// <typeparam name="TImplementation">The component's implementation type.</typeparam>
    void AddTransient<TComponent, TImplementation>()
      where TComponent : INinjectComponent
      where TImplementation : TComponent, INinjectComponent;
  }
}
