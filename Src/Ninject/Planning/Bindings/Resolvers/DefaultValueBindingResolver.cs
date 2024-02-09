// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.Resolvers.DefaultValueBindingResolver
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Planning.Bindings.Resolvers
{
  /// <summary>
  /// </summary>
  public class DefaultValueBindingResolver : 
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
      if (!DefaultValueBindingResolver.HasDefaultValue(request.Target))
        return Enumerable.Empty<IBinding>();
      return (IEnumerable<IBinding>) new Binding[1]
      {
        new Binding(service)
        {
          Condition = (Func<IRequest, bool>) (r => DefaultValueBindingResolver.HasDefaultValue(r.Target)),
          ProviderCallback = (Func<IContext, IProvider>) (_ => (IProvider) new DefaultValueBindingResolver.DefaultParameterValueProvider(service))
        }
      };
    }

    private static bool HasDefaultValue(ITarget target) => target != null && target.HasDefaultValue;

    private class DefaultParameterValueProvider : IProvider
    {
      public DefaultParameterValueProvider(Type type) => this.Type = type;

      public Type Type { get; private set; }

      public object Create(IContext context) => context.Request.Target?.DefaultValue;
    }
  }
}
