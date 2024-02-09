// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.IPipeline
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation.Strategies;
using Ninject.Components;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>
  /// Drives the activation (injection, etc.) of an instance.
  /// </summary>
  public interface IPipeline : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Gets the strategies that contribute to the activation and deactivation processes.
    /// </summary>
    IList<IActivationStrategy> Strategies { get; }

    /// <summary>Activates the instance in the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">The instance reference.</param>
    void Activate(IContext context, InstanceReference reference);

    /// <summary>Deactivates the instance in the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">The instance reference.</param>
    void Deactivate(IContext context, InstanceReference reference);
  }
}
