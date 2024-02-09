// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.Resolvers.IBindingResolver
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Planning.Bindings.Resolvers
{
  /// <summary>
  /// Contains logic about which bindings to use for a given service request.
  /// </summary>
  public interface IBindingResolver : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Returns any bindings from the specified collection that match the specified service.
    /// </summary>
    /// <param name="bindings">The multimap of all registered bindings.</param>
    /// <param name="service">The service in question.</param>
    /// <returns>The series of matching bindings.</returns>
    IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, Type service);
  }
}
