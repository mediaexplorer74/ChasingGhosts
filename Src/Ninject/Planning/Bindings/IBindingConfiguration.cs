﻿// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.IBindingConfiguration
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Parameters;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>The configuration of a binding.</summary>
  public interface IBindingConfiguration
  {
    /// <summary>Gets the binding's metadata.</summary>
    IBindingMetadata Metadata { get; }

    /// <summary>Gets or sets the type of target for the binding.</summary>
    BindingTarget Target { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the binding was implicitly registered.
    /// </summary>
    bool IsImplicit { get; set; }

    /// <summary>
    /// Gets a value indicating whether the binding has a condition associated with it.
    /// </summary>
    bool IsConditional { get; }

    /// <summary>Gets or sets the condition defined for the binding.</summary>
    Func<IRequest, bool> Condition { get; set; }

    /// <summary>
    /// Gets or sets the callback that returns the provider that should be used by the binding.
    /// </summary>
    Func<IContext, IProvider> ProviderCallback { get; set; }

    /// <summary>
    /// Gets or sets the callback that returns the object that will act as the binding's scope.
    /// </summary>
    Func<IContext, object> ScopeCallback { get; set; }

    /// <summary>Gets the parameters defined for the binding.</summary>
    ICollection<IParameter> Parameters { get; }

    /// <summary>
    /// Gets the actions that should be called after instances are activated via the binding.
    /// </summary>
    ICollection<Action<IContext, object>> ActivationActions { get; }

    /// <summary>
    /// Gets the actions that should be called before instances are deactivated via the binding.
    /// </summary>
    ICollection<Action<IContext, object>> DeactivationActions { get; }

    /// <summary>Gets the provider for the binding.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The provider to use.</returns>
    IProvider GetProvider(IContext context);

    /// <summary>Gets the scope for the binding, if any.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The object that will act as the scope, or <see langword="null" /> if the service is transient.</returns>
    object GetScope(IContext context);

    /// <summary>
    /// Determines whether the specified request satisfies the condition defined on the binding,
    /// if one was defined.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>True</c> if the request satisfies the condition; otherwise <c>false</c>.</returns>
    bool Matches(IRequest request);
  }
}
