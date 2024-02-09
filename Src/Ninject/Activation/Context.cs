// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Context
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation.Caching;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>
  /// Contains information about the activation of a single instance.
  /// </summary>
  public class Context : IContext
  {
    private WeakReference cachedScope;

    /// <summary>Gets the kernel that is driving the activation.</summary>
    public IKernel Kernel { get; set; }

    /// <summary>Gets the request.</summary>
    public IRequest Request { get; set; }

    /// <summary>Gets the binding.</summary>
    public IBinding Binding { get; set; }

    /// <summary>Gets or sets the activation plan.</summary>
    public IPlan Plan { get; set; }

    /// <summary>
    /// Gets the parameters that were passed to manipulate the activation process.
    /// </summary>
    public ICollection<IParameter> Parameters { get; set; }

    /// <summary>Gets the generic arguments for the request, if any.</summary>
    public Type[] GenericArguments { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the request involves inferred generic arguments.
    /// </summary>
    public bool HasInferredGenericArguments { get; private set; }

    /// <summary>Gets or sets the cache component.</summary>
    public ICache Cache { get; private set; }

    /// <summary>Gets or sets the planner component.</summary>
    public IPlanner Planner { get; private set; }

    /// <summary>Gets or sets the pipeline component.</summary>
    public IPipeline Pipeline { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Context" /> class.
    /// </summary>
    /// <param name="kernel">The kernel managing the resolution.</param>
    /// <param name="request">The context's request.</param>
    /// <param name="binding">The context's binding.</param>
    /// <param name="cache">The cache component.</param>
    /// <param name="planner">The planner component.</param>
    /// <param name="pipeline">The pipeline component.</param>
    public Context(
      IKernel kernel,
      IRequest request,
      IBinding binding,
      ICache cache,
      IPlanner planner,
      IPipeline pipeline)
    {
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      Ensure.ArgumentNotNull((object) request, nameof (request));
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      Ensure.ArgumentNotNull((object) cache, nameof (cache));
      Ensure.ArgumentNotNull((object) planner, nameof (planner));
      Ensure.ArgumentNotNull((object) pipeline, nameof (pipeline));
      this.Kernel = kernel;
      this.Request = request;
      this.Binding = binding;
      this.Parameters = (ICollection<IParameter>) request.Parameters.Union<IParameter>((IEnumerable<IParameter>) binding.Parameters).ToList<IParameter>();
      this.Cache = cache;
      this.Planner = planner;
      this.Pipeline = pipeline;
      if (!binding.Service.IsGenericTypeDefinition)
        return;
      this.HasInferredGenericArguments = true;
      this.GenericArguments = request.Service.GetGenericArguments();
    }

    /// <summary>
    /// Gets the scope for the context that "owns" the instance activated therein.
    /// </summary>
    /// <returns>The object that acts as the scope.</returns>
    public object GetScope()
    {
      if (this.cachedScope == null)
        this.cachedScope = new WeakReference(this.Request.GetScope() ?? this.Binding.GetScope((IContext) this));
      return this.cachedScope.Target;
    }

    /// <summary>
    /// Gets the provider that should be used to create the instance for this context.
    /// </summary>
    /// <returns>The provider that should be used.</returns>
    public IProvider GetProvider() => this.Binding.GetProvider((IContext) this);

    /// <summary>Resolves the instance associated with this hook.</summary>
    /// <returns>The resolved instance.</returns>
    public object Resolve()
    {
      if (this.Request.ActiveBindings.Contains(this.Binding))
        throw new ActivationException(ExceptionFormatter.CyclicalDependenciesDetected((IContext) this));
      object scope = this.GetScope();
      if (scope == null)
        return this.ResolveInternal((object) null);
      lock (scope)
        return this.ResolveInternal(scope);
    }

    private object ResolveInternal(object scope)
    {
      object obj = this.Cache.TryGet((IContext) this);
      if (obj != null)
        return obj;
      this.Request.ActiveBindings.Push(this.Binding);
      InstanceReference reference = new InstanceReference()
      {
        Instance = this.GetProvider().Create((IContext) this)
      };
      this.Request.ActiveBindings.Pop();
      if (reference.Instance == null)
      {
        if (!this.Kernel.Settings.AllowNullInjection)
          throw new ActivationException(ExceptionFormatter.ProviderReturnedNull((IContext) this));
        if (this.Plan == null)
          this.Plan = this.Planner.GetPlan(this.Request.Service);
        return (object) null;
      }
      if (scope != null)
        this.Cache.Remember((IContext) this, reference);
      if (this.Plan == null)
        this.Plan = this.Planner.GetPlan(reference.Instance.GetType());
      this.Pipeline.Activate((IContext) this, reference);
      return reference.Instance;
    }
  }
}
