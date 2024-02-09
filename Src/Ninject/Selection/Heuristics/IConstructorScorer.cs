// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.Heuristics.IConstructorScorer
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Components;
using Ninject.Planning.Directives;
using System;

#nullable disable
namespace Ninject.Selection.Heuristics
{
  /// <summary>
  /// Generates scores for constructors, to determine which is the best one to call during activation.
  /// </summary>
  public interface IConstructorScorer : INinjectComponent, IDisposable
  {
    /// <summary>Gets the score for the specified constructor.</summary>
    /// <param name="context">The injection context.</param>
    /// <param name="directive">The constructor.</param>
    /// <returns>The constructor's score.</returns>
    int Score(IContext context, ConstructorInjectionDirective directive);
  }
}
