// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.Heuristics.SpecificConstructorSelector
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Components;
using Ninject.Planning.Directives;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Selection.Heuristics
{
  /// <summary>
  /// Constructor selector that selects the constructor matching the one passed to the constructor.
  /// </summary>
  public class SpecificConstructorSelector : 
    NinjectComponent,
    IConstructorScorer,
    INinjectComponent,
    IDisposable
  {
    private readonly ConstructorInfo constructorInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Selection.Heuristics.SpecificConstructorSelector" /> class.
    /// </summary>
    /// <param name="constructorInfo">The constructor info of the constructor that shall be selected.</param>
    public SpecificConstructorSelector(ConstructorInfo constructorInfo)
    {
      this.constructorInfo = constructorInfo;
    }

    /// <summary>Gets the score for the specified constructor.</summary>
    /// <param name="context">The injection context.</param>
    /// <param name="directive">The constructor.</param>
    /// <returns>The constructor's score.</returns>
    public virtual int Score(IContext context, ConstructorInjectionDirective directive)
    {
      return !directive.Constructor.Equals((object) this.constructorInfo) ? 0 : 1;
    }
  }
}
