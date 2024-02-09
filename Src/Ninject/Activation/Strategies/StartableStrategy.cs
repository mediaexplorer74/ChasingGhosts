// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.StartableStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>
  /// Starts instances that implement <see cref="T:Ninject.IStartable" /> during activation,
  /// and stops them during deactivation.
  /// </summary>
  public class StartableStrategy : ActivationStrategy
  {
    /// <summary>Starts the specified instance.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public override void Activate(IContext context, InstanceReference reference)
    {
      reference.IfInstanceIs<IStartable>((Action<IStartable>) (x => x.Start()));
    }

    /// <summary>Stops the specified instance.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being deactivated.</param>
    public override void Deactivate(IContext context, InstanceReference reference)
    {
      reference.IfInstanceIs<IStartable>((Action<IStartable>) (x => x.Stop()));
    }
  }
}
