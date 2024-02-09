// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.Resolvers.SelfBindingResolver
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Activation.Providers;
using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Planning.Bindings.Resolvers
{
  /// <summary>
  /// </summary>
  public class SelfBindingResolver : 
    NinjectComponent,
    IMissingBindingResolver,
    INinjectComponent,
    IDisposable
  {
    /// <summary>
    /// Returns any bindings from the specified collection that match the specified service.
    /// </summary>
    /// <param name="bindings">The multimap of all registered bindings.</param>
    /// <param name="request">The service in question.</param>
    /// <returns>The series of matching bindings.</returns>
    public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, IRequest request)
    {
      Type service = request.Service;
      if (!this.TypeIsSelfBindable(service))
        return Enumerable.Empty<IBinding>();
      return (IEnumerable<IBinding>) new Binding[1]
      {
        new Binding(service)
        {
          ProviderCallback = StandardProvider.GetCreationCallback(service)
        }
      };
    }

    /// <summary>
    /// Returns a value indicating whether the specified service is self-bindable.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns><see langword="True" /> if the type is self-bindable; otherwise <see langword="false" />.</returns>
    protected virtual bool TypeIsSelfBindable(Type service)
    {
      return !service.IsInterface && !service.IsAbstract && !service.IsValueType && service != typeof (string) && !service.ContainsGenericParameters;
    }
  }
}
