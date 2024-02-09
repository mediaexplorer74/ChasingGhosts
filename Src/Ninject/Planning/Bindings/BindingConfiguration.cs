// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.BindingConfiguration
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Parameters;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>The configuration of a binding.</summary>
  public class BindingConfiguration : IBindingConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.BindingConfiguration" /> class.
    /// </summary>
    public BindingConfiguration()
    {
      this.Metadata = (IBindingMetadata) new BindingMetadata();
      this.Parameters = (ICollection<IParameter>) new List<IParameter>();
      this.ActivationActions = (ICollection<Action<IContext, object>>) new List<Action<IContext, object>>();
      this.DeactivationActions = (ICollection<Action<IContext, object>>) new List<Action<IContext, object>>();
      this.ScopeCallback = StandardScopeCallbacks.Transient;
    }

    /// <summary>Gets the binding's metadata.</summary>
    public IBindingMetadata Metadata { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the binding was implicitly registered.
    /// </summary>
    public bool IsImplicit { get; set; }

    /// <summary>
    /// Gets a value indicating whether the binding has a condition associated with it.
    /// </summary>
    public bool IsConditional => this.Condition != null;

    /// <summary>Gets or sets the type of target for the binding.</summary>
    public BindingTarget Target { get; set; }

    /// <summary>Gets or sets the condition defined for the binding.</summary>
    public Func<IRequest, bool> Condition { get; set; }

    /// <summary>
    /// Gets or sets the callback that returns the provider that should be used by the binding.
    /// </summary>
    public Func<IContext, IProvider> ProviderCallback { get; set; }

    /// <summary>
    /// Gets or sets the callback that returns the object that will act as the binding's scope.
    /// </summary>
    public Func<IContext, object> ScopeCallback { get; set; }

    /// <summary>Gets the parameters defined for the binding.</summary>
    public ICollection<IParameter> Parameters { get; private set; }

    /// <summary>
    /// Gets the actions that should be called after instances are activated via the binding.
    /// </summary>
    public ICollection<Action<IContext, object>> ActivationActions { get; private set; }

    /// <summary>
    /// Gets the actions that should be called before instances are deactivated via the binding.
    /// </summary>
    public ICollection<Action<IContext, object>> DeactivationActions { get; private set; }

    /// <summary>Gets the provider for the binding.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The provider to use.</returns>
    public IProvider GetProvider(IContext context)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      return this.ProviderCallback(context);
    }

    /// <summary>Gets the scope for the binding, if any.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The object that will act as the scope, or <see langword="null" /> if the service is transient.</returns>
    public object GetScope(IContext context)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      return this.ScopeCallback(context);
    }

    /// <summary>
    /// Determines whether the specified request satisfies the conditions defined on this binding.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>True</c> if the request satisfies the conditions; otherwise <c>false</c>.</returns>
    public bool Matches(IRequest request)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      return this.Condition == null || this.Condition(request);
    }
  }
}
