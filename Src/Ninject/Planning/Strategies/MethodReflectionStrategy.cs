// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Strategies.MethodReflectionStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Injection;
using Ninject.Planning.Directives;
using Ninject.Selection;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Strategies
{
  /// <summary>
  /// Adds directives to plans indicating which methods should be injected during activation.
  /// </summary>
  public class MethodReflectionStrategy : 
    NinjectComponent,
    IPlanningStrategy,
    INinjectComponent,
    IDisposable
  {
    /// <summary>Gets the selector component.</summary>
    public ISelector Selector { get; private set; }

    /// <summary>Gets the injector factory component.</summary>
    public IInjectorFactory InjectorFactory { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Strategies.MethodReflectionStrategy" /> class.
    /// </summary>
    /// <param name="selector">The selector component.</param>
    /// <param name="injectorFactory">The injector factory component.</param>
    public MethodReflectionStrategy(ISelector selector, IInjectorFactory injectorFactory)
    {
      Ensure.ArgumentNotNull((object) selector, nameof (selector));
      Ensure.ArgumentNotNull((object) injectorFactory, nameof (injectorFactory));
      this.Selector = selector;
      this.InjectorFactory = injectorFactory;
    }

    /// <summary>
    /// Adds a <see cref="T:Ninject.Planning.Directives.MethodInjectionDirective" /> to the plan for each method
    /// that should be injected.
    /// </summary>
    /// <param name="plan">The plan that is being generated.</param>
    public void Execute(IPlan plan)
    {
      Ensure.ArgumentNotNull((object) plan, nameof (plan));
      foreach (MethodInfo method in this.Selector.SelectMethodsForInjection(plan.Type))
        plan.Add((IDirective) new MethodInjectionDirective(method, this.InjectorFactory.Create(method)));
    }
  }
}
