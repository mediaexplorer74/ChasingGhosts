// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Strategies.IPlanningStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;

#nullable disable
namespace Ninject.Planning.Strategies
{
  /// <summary>
  /// Contributes to the generation of a <see cref="T:Ninject.Planning.IPlan" />.
  /// </summary>
  public interface IPlanningStrategy : INinjectComponent, IDisposable
  {
    /// <summary>Contributes to the specified plan.</summary>
    /// <param name="plan">The plan that is being generated.</param>
    void Execute(IPlan plan);
  }
}
