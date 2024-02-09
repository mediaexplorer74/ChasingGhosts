// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.IBinding
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>Contains information about a service registration.</summary>
  public interface IBinding : IBindingConfiguration
  {
    /// <summary>Gets the binding configuration.</summary>
    /// <value>The binding configuration.</value>
    IBindingConfiguration BindingConfiguration { get; }

    /// <summary>
    /// Gets the service type that is controlled by the binding.
    /// </summary>
    Type Service { get; }
  }
}
