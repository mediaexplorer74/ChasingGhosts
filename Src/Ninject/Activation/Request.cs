// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Request
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>Describes the request for a service resolution.</summary>
  public class Request : IRequest
  {
    /// <summary>Gets the service that was requested.</summary>
    public Type Service { get; private set; }

    /// <summary>Gets the parent request.</summary>
    public IRequest ParentRequest { get; private set; }

    /// <summary>Gets the parent context.</summary>
    public IContext ParentContext { get; private set; }

    /// <summary>
    /// Gets the target that will receive the injection, if any.
    /// </summary>
    public ITarget Target { get; private set; }

    /// <summary>
    /// Gets the constraint that will be applied to filter the bindings used for the request.
    /// </summary>
    public Func<IBindingMetadata, bool> Constraint { get; private set; }

    /// <summary>Gets the parameters that affect the resolution.</summary>
    public ICollection<IParameter> Parameters { get; private set; }

    /// <summary>
    /// Gets the stack of bindings which have been activated by either this request or its ancestors.
    /// </summary>
    public Stack<IBinding> ActiveBindings { get; private set; }

    /// <summary>
    /// Gets the recursive depth at which this request occurs.
    /// </summary>
    public int Depth { get; private set; }

    /// <summary>
    /// Gets or sets value indicating whether the request is optional.
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether the request is for a single service.
    /// </summary>
    public bool IsUnique { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether the request should force to return a unique value even if the request is optional.
    /// If this value is set true the request will throw an ActivationException if there are multiple satisfying bindings rather
    /// than returning null for the request is optional. For none optional requests this parameter does not change anything.
    /// </summary>
    public bool ForceUnique { get; set; }

    /// <summary>
    /// Gets the callback that resolves the scope for the request, if an external scope was provided.
    /// </summary>
    public Func<object> ScopeCallback { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Request" /> class.
    /// </summary>
    /// <param name="service">The service that was requested.</param>
    /// <param name="constraint">The constraint that will be applied to filter the bindings used for the request.</param>
    /// <param name="parameters">The parameters that affect the resolution.</param>
    /// <param name="scopeCallback">The scope callback, if an external scope was specified.</param>
    /// <param name="isOptional"><c>True</c> if the request is optional; otherwise, <c>false</c>.</param>
    /// <param name="isUnique"><c>True</c> if the request should return a unique result; otherwise, <c>false</c>.</param>
    public Request(
      Type service,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters,
      Func<object> scopeCallback,
      bool isOptional,
      bool isUnique)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parameters, nameof (parameters));
      this.Service = service;
      this.Constraint = constraint;
      this.Parameters = (ICollection<IParameter>) parameters.ToList<IParameter>();
      this.ScopeCallback = scopeCallback;
      this.ActiveBindings = new Stack<IBinding>();
      this.Depth = 0;
      this.IsOptional = isOptional;
      this.IsUnique = isUnique;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Request" /> class.
    /// </summary>
    /// <param name="parentContext">The parent context.</param>
    /// <param name="service">The service that was requested.</param>
    /// <param name="target">The target that will receive the injection.</param>
    /// <param name="scopeCallback">The scope callback, if an external scope was specified.</param>
    public Request(
      IContext parentContext,
      Type service,
      ITarget target,
      Func<object> scopeCallback)
    {
      Ensure.ArgumentNotNull((object) parentContext, nameof (parentContext));
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) target, nameof (target));
      this.ParentContext = parentContext;
      this.ParentRequest = parentContext.Request;
      this.Service = service;
      this.Target = target;
      this.Constraint = target.Constraint;
      this.IsOptional = target.IsOptional;
      this.Parameters = (ICollection<IParameter>) parentContext.Parameters.Where<IParameter>((Func<IParameter, bool>) (p => p.ShouldInherit)).ToList<IParameter>();
      this.ScopeCallback = scopeCallback;
      this.ActiveBindings = new Stack<IBinding>((IEnumerable<IBinding>) this.ParentRequest.ActiveBindings);
      this.Depth = this.ParentRequest.Depth + 1;
    }

    /// <summary>
    /// Determines whether the specified binding satisfies the constraints defined on this request.
    /// </summary>
    /// <param name="binding">The binding.</param>
    /// <returns><c>True</c> if the binding satisfies the constraints; otherwise <c>false</c>.</returns>
    public bool Matches(IBinding binding)
    {
      return this.Constraint == null || this.Constraint(binding.Metadata);
    }

    /// <summary>Gets the scope if one was specified in the request.</summary>
    /// <returns>The object that acts as the scope.</returns>
    public object GetScope() => this.ScopeCallback != null ? this.ScopeCallback() : (object) null;

    /// <summary>Creates a child request.</summary>
    /// <param name="service">The service that is being requested.</param>
    /// <param name="parentContext">The context in which the request was made.</param>
    /// <param name="target">The target that will receive the injection.</param>
    /// <returns>The child request.</returns>
    public IRequest CreateChild(Type service, IContext parentContext, ITarget target)
    {
      return (IRequest) new Request(parentContext, service, target, this.ScopeCallback);
    }

    /// <summary>
    /// Formats this object into a meaningful string representation.
    /// </summary>
    /// <returns>The request formatted as string.</returns>
    public override string ToString() => this.Format();
  }
}
