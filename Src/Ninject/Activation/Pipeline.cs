// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Pipeline
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation.Caching;
using Ninject.Activation.Strategies;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>
  /// Drives the activation (injection, etc.) of an instance.
  /// </summary>
  public class Pipeline : NinjectComponent, IPipeline, INinjectComponent, IDisposable
  {
    /// <summary>The activation cache.</summary>
    private readonly IActivationCache activationCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Pipeline" /> class.
    /// </summary>
    /// <param name="strategies">The strategies to execute during activation and deactivation.</param>
    /// <param name="activationCache">The activation cache.</param>
    public Pipeline(IEnumerable<IActivationStrategy> strategies, IActivationCache activationCache)
    {
      Ensure.ArgumentNotNull((object) strategies, nameof (strategies));
      this.Strategies = (IList<IActivationStrategy>) strategies.ToList<IActivationStrategy>();
      this.activationCache = activationCache;
    }

    /// <summary>
    /// Gets the strategies that contribute to the activation and deactivation processes.
    /// </summary>
    public IList<IActivationStrategy> Strategies { get; private set; }

    /// <summary>Activates the instance in the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">The instance reference.</param>
    public void Activate(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      if (this.activationCache.IsActivated(reference.Instance))
        return;
      this.Strategies.Map<IActivationStrategy>((Action<IActivationStrategy>) (s => s.Activate(context, reference)));
    }

    /// <summary>Deactivates the instance in the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">The instance reference.</param>
    public void Deactivate(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      if (this.activationCache.IsDeactivated(reference.Instance))
        return;
      this.Strategies.Map<IActivationStrategy>((Action<IActivationStrategy>) (s => s.Deactivate(context, reference)));
    }
  }
}
