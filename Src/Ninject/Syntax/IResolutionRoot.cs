// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IResolutionRoot
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>Provides a path to resolve instances.</summary>
  public interface IResolutionRoot : IFluentSyntax
  {
    /// <summary>
    /// Determines whether the specified request can be resolved.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>True</c> if the request can be resolved; otherwise, <c>false</c>.</returns>
    bool CanResolve(IRequest request);

    /// <summary>
    /// Determines whether the specified request can be resolved.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="ignoreImplicitBindings">if set to <c>true</c> implicit bindings are ignored.</param>
    /// <returns>
    ///     <c>True</c> if the request can be resolved; otherwise, <c>false</c>.
    /// </returns>
    bool CanResolve(IRequest request, bool ignoreImplicitBindings);

    /// <summary>
    /// Resolves instances for the specified request. The instances are not actually resolved
    /// until a consumer iterates over the enumerator.
    /// </summary>
    /// <param name="request">The request to resolve.</param>
    /// <returns>An enumerator of instances that match the request.</returns>
    IEnumerable<object> Resolve(IRequest request);

    /// <summary>Creates a request for the specified service.</summary>
    /// <param name="service">The service that is being requested.</param>
    /// <param name="constraint">The constraint to apply to the bindings to determine if they match the request.</param>
    /// <param name="parameters">The parameters to pass to the resolution.</param>
    /// <param name="isOptional"><c>True</c> if the request is optional; otherwise, <c>false</c>.</param>
    /// <param name="isUnique"><c>True</c> if the request should return a unique result; otherwise, <c>false</c>.</param>
    /// <returns>The created request.</returns>
    IRequest CreateRequest(
      Type service,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters,
      bool isOptional,
      bool isUnique);

    /// <summary>
    /// Deactivates and releases the specified instance if it is currently managed by Ninject.
    /// </summary>
    /// <param name="instance">The instance to release.</param>
    /// <returns><see langword="True" /> if the instance was found and released; otherwise <see langword="false" />.</returns>
    bool Release(object instance);
  }
}
