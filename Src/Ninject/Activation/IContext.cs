// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.IContext
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>
  /// Contains information about the activation of a single instance.
  /// </summary>
  public interface IContext
  {
    /// <summary>Gets the kernel that is driving the activation.</summary>
    IKernel Kernel { get; }

    /// <summary>Gets the request.</summary>
    IRequest Request { get; }

    /// <summary>Gets the binding.</summary>
    IBinding Binding { get; }

    /// <summary>Gets or sets the activation plan.</summary>
    IPlan Plan { get; set; }

    /// <summary>
    /// Gets the parameters that were passed to manipulate the activation process.
    /// </summary>
    ICollection<IParameter> Parameters { get; }

    /// <summary>Gets the generic arguments for the request, if any.</summary>
    Type[] GenericArguments { get; }

    /// <summary>
    /// Gets a value indicating whether the request involves inferred generic arguments.
    /// </summary>
    bool HasInferredGenericArguments { get; }

    /// <summary>
    /// Gets the provider that should be used to create the instance for this context.
    /// </summary>
    /// <returns>The provider that should be used.</returns>
    IProvider GetProvider();

    /// <summary>
    /// Gets the scope for the context that "owns" the instance activated therein.
    /// </summary>
    /// <returns>The object that acts as the scope.</returns>
    object GetScope();

    /// <summary>Resolves this instance for this context.</summary>
    /// <returns>The resolved instance.</returns>
    object Resolve();
  }
}
