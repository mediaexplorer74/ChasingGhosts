﻿// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.IRequest
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>Describes the request for a service resolution.</summary>
  public interface IRequest
  {
    /// <summary>Gets the service that was requested.</summary>
    Type Service { get; }

    /// <summary>Gets the parent request.</summary>
    IRequest ParentRequest { get; }

    /// <summary>Gets the parent context.</summary>
    IContext ParentContext { get; }

    /// <summary>
    /// Gets the target that will receive the injection, if any.
    /// </summary>
    ITarget Target { get; }

    /// <summary>
    /// Gets the constraint that will be applied to filter the bindings used for the request.
    /// </summary>
    Func<IBindingMetadata, bool> Constraint { get; }

    /// <summary>Gets the parameters that affect the resolution.</summary>
    ICollection<IParameter> Parameters { get; }

    /// <summary>
    /// Gets the stack of bindings which have been activated by either this request or its ancestors.
    /// </summary>
    Stack<IBinding> ActiveBindings { get; }

    /// <summary>
    /// Gets the recursive depth at which this request occurs.
    /// </summary>
    int Depth { get; }

    /// <summary>
    /// Gets or sets value indicating whether the request is optional.
    /// </summary>
    bool IsOptional { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether the request should return a unique result.
    /// </summary>
    bool IsUnique { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether the request should force to return a unique value even if the request is optional.
    /// If this value is set true the request will throw an ActivationException if there are multiple satisfying bindings rather
    /// than returning null for the request is optional. For none optional requests this parameter does not change anything.
    /// </summary>
    bool ForceUnique { get; set; }

    /// <summary>
    /// Determines whether the specified binding satisfies the constraint defined on this request.
    /// </summary>
    /// <param name="binding">The binding.</param>
    /// <returns><c>True</c> if the binding satisfies the constraint; otherwise <c>false</c>.</returns>
    bool Matches(IBinding binding);

    /// <summary>Gets the scope if one was specified in the request.</summary>
    /// <returns>The object that acts as the scope.</returns>
    object GetScope();

    /// <summary>Creates a child request.</summary>
    /// <param name="service">The service that is being requested.</param>
    /// <param name="parentContext">The context in which the request was made.</param>
    /// <param name="target">The target that will receive the injection.</param>
    /// <returns>The child request.</returns>
    IRequest CreateChild(Type service, IContext parentContext, ITarget target);
  }
}
