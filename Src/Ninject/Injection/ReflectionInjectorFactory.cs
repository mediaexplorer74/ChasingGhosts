// Decompiled with JetBrains decompiler
// Type: Ninject.Injection.ReflectionInjectorFactory
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Injection
{
  /// <summary>
  /// Creates injectors from members via reflective invocation.
  /// </summary>
  public class ReflectionInjectorFactory : 
    NinjectComponent,
    IInjectorFactory,
    INinjectComponent,
    IDisposable
  {
    /// <summary>
    /// Gets or creates an injector for the specified constructor.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <returns>The created injector.</returns>
    public ConstructorInjector Create(ConstructorInfo constructor)
    {
      return (ConstructorInjector) (args => constructor.Invoke(args));
    }

    /// <summary>
    /// Gets or creates an injector for the specified property.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The created injector.</returns>
    public PropertyInjector Create(PropertyInfo property)
    {
      return (PropertyInjector) ((target, value) => property.SetValue(target, value, (object[]) null));
    }

    /// <summary>Gets or creates an injector for the specified method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>The created injector.</returns>
    public MethodInjector Create(MethodInfo method)
    {
      return (MethodInjector) ((target, args) => method.Invoke(target, args));
    }
  }
}
