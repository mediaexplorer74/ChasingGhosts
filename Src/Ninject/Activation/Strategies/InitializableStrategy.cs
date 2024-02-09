// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.InitializableStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>
  /// During activation, initializes instances that implement <see cref="T:Ninject.IInitializable" />.
  /// </summary>
  public class InitializableStrategy : ActivationStrategy
  {
    /// <summary>Initializes the specified instance.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public override void Activate(IContext context, InstanceReference reference)
    {
      reference.IfInstanceIs<IInitializable>((Action<IInitializable>) (x => x.Initialize()));
    }
  }
}
