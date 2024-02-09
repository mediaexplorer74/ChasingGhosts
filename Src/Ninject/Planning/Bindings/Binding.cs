// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.Binding
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
  /// <summary>Contains information about a service registration.</summary>
  public class Binding : IBinding, IBindingConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.Binding" /> class.
    /// </summary>
    /// <param name="service">The service that is controlled by the binding.</param>
    public Binding(Type service)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      this.Service = service;
      this.BindingConfiguration = (IBindingConfiguration) new Ninject.Planning.Bindings.BindingConfiguration();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.Binding" /> class.
    /// </summary>
    /// <param name="service">The service that is controlled by the binding.</param>
    /// <param name="configuration">The binding configuration.</param>
    public Binding(Type service, IBindingConfiguration configuration)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) configuration, nameof (configuration));
      this.Service = service;
      this.BindingConfiguration = configuration;
    }

    /// <summary>Gets or sets the binding configuration.</summary>
    /// <value>The binding configuration.</value>
    public IBindingConfiguration BindingConfiguration { get; private set; }

    /// <summary>
    /// Gets the service type that is controlled by the binding.
    /// </summary>
    public Type Service { get; private set; }

    /// <summary>Gets the binding's metadata.</summary>
    /// <value></value>
    public IBindingMetadata Metadata => this.BindingConfiguration.Metadata;

    /// <summary>Gets or sets the type of target for the binding.</summary>
    /// <value></value>
    public BindingTarget Target
    {
      get => this.BindingConfiguration.Target;
      set => this.BindingConfiguration.Target = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the binding was implicitly registered.
    /// </summary>
    /// <value></value>
    public bool IsImplicit
    {
      get => this.BindingConfiguration.IsImplicit;
      set => this.BindingConfiguration.IsImplicit = value;
    }

    /// <summary>
    /// Gets a value indicating whether the binding has a condition associated with it.
    /// </summary>
    /// <value></value>
    public bool IsConditional => this.BindingConfiguration.IsConditional;

    /// <summary>Gets or sets the condition defined for the binding.</summary>
    /// <value></value>
    public Func<IRequest, bool> Condition
    {
      get => this.BindingConfiguration.Condition;
      set => this.BindingConfiguration.Condition = value;
    }

    /// <summary>
    /// Gets or sets the callback that returns the provider that should be used by the binding.
    /// </summary>
    /// <value></value>
    public Func<IContext, IProvider> ProviderCallback
    {
      get => this.BindingConfiguration.ProviderCallback;
      set => this.BindingConfiguration.ProviderCallback = value;
    }

    /// <summary>
    /// Gets or sets the callback that returns the object that will act as the binding's scope.
    /// </summary>
    /// <value></value>
    public Func<IContext, object> ScopeCallback
    {
      get => this.BindingConfiguration.ScopeCallback;
      set => this.BindingConfiguration.ScopeCallback = value;
    }

    /// <summary>Gets the parameters defined for the binding.</summary>
    /// <value></value>
    public ICollection<IParameter> Parameters => this.BindingConfiguration.Parameters;

    /// <summary>
    /// Gets the actions that should be called after instances are activated via the binding.
    /// </summary>
    /// <value></value>
    public ICollection<Action<IContext, object>> ActivationActions
    {
      get => this.BindingConfiguration.ActivationActions;
    }

    /// <summary>
    /// Gets the actions that should be called before instances are deactivated via the binding.
    /// </summary>
    /// <value></value>
    public ICollection<Action<IContext, object>> DeactivationActions
    {
      get => this.BindingConfiguration.DeactivationActions;
    }

    /// <summary>Gets the provider for the binding.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The provider to use.</returns>
    public IProvider GetProvider(IContext context)
    {
      return this.BindingConfiguration.GetProvider(context);
    }

    /// <summary>Gets the scope for the binding, if any.</summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// The object that will act as the scope, or <see langword="null" /> if the service is transient.
    /// </returns>
    public object GetScope(IContext context) => this.BindingConfiguration.GetScope(context);

    /// <summary>
    /// Determines whether the specified request satisfies the condition defined on the binding,
    /// if one was defined.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     <c>True</c> if the request satisfies the condition; otherwise <c>false</c>.
    /// </returns>
    public bool Matches(IRequest request) => this.BindingConfiguration.Matches(request);
  }
}
