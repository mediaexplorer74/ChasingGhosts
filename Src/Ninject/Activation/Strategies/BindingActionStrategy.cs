// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.BindingActionStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>
  /// Executes actions defined on the binding during activation and deactivation.
  /// </summary>
  public class BindingActionStrategy : ActivationStrategy
  {
    /// <summary>Calls the activation actions defined on the binding.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public override void Activate(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      context.Binding.ActivationActions.Map<Action<IContext, object>>((Action<Action<IContext, object>>) (action => action(context, reference.Instance)));
    }

    /// <summary>
    /// Calls the deactivation actions defined on the binding.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being deactivated.</param>
    public override void Deactivate(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      context.Binding.DeactivationActions.Map<Action<IContext, object>>((Action<Action<IContext, object>>) (action => action(context, reference.Instance)));
    }
  }
}
