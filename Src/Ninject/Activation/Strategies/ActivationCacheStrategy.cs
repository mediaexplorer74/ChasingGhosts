// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.ActivationCacheStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation.Caching;
using Ninject.Components;
using System;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>Adds all activated instances to the activation cache.</summary>
  public class ActivationCacheStrategy : IActivationStrategy, INinjectComponent, IDisposable
  {
    /// <summary>The activation cache.</summary>
    private readonly IActivationCache activationCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Strategies.ActivationCacheStrategy" /> class.
    /// </summary>
    /// <param name="activationCache">The activation cache.</param>
    public ActivationCacheStrategy(IActivationCache activationCache)
    {
      this.activationCache = activationCache;
    }

    /// <summary>Gets or sets the settings.</summary>
    /// <value>The ninject settings.</value>
    public INinjectSettings Settings { get; set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
    }

    /// <summary>
    /// Contributes to the activation of the instance in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public void Activate(IContext context, InstanceReference reference)
    {
      this.activationCache.AddActivatedInstance(reference.Instance);
    }

    /// <summary>
    /// Contributes to the deactivation of the instance in the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being deactivated.</param>
    public void Deactivate(IContext context, InstanceReference reference)
    {
      this.activationCache.AddDeactivatedInstance(reference.Instance);
    }
  }
}
