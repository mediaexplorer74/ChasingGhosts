// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.DisposableStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>
  /// During deactivation, disposes instances that implement <see cref="T:System.IDisposable" />.
  /// </summary>
  public class DisposableStrategy : ActivationStrategy
  {
    /// <summary>Disposes the specified instance.</summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being deactivated.</param>
    public override void Deactivate(IContext context, InstanceReference reference)
    {
      reference.IfInstanceIs<IDisposable>((Action<IDisposable>) (x => x.Dispose()));
    }
  }
}
