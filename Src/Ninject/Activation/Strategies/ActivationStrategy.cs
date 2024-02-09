// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.ActivationStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>
  /// Contributes to a <see cref="T:Ninject.Activation.IPipeline" />, and is called during the activation
  /// and deactivation of an instance.
  /// </summary>
  public abstract class ActivationStrategy : 
    NinjectComponent,
    IActivationStrategy,
    INinjectComponent,
    IDisposable
  {
    /// <summary>
    /// Contributes to the activation of the instance in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public virtual void Activate(IContext context, InstanceReference reference)
    {
    }

    /// <summary>
    /// Contributes to the deactivation of the instance in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being deactivated.</param>
    public virtual void Deactivate(IContext context, InstanceReference reference)
    {
    }
  }
}
