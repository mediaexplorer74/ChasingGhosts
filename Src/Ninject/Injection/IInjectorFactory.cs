// Decompiled with JetBrains decompiler
// Type: Ninject.Injection.IInjectorFactory
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
  /// <summary>Creates injectors from members.</summary>
  public interface IInjectorFactory : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Gets or creates an injector for the specified constructor.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <returns>The created injector.</returns>
    ConstructorInjector Create(ConstructorInfo constructor);

    /// <summary>
    /// Gets or creates an injector for the specified property.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The created injector.</returns>
    PropertyInjector Create(PropertyInfo property);

    /// <summary>Gets or creates an injector for the specified method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>The created injector.</returns>
    MethodInjector Create(MethodInfo method);
  }
}
