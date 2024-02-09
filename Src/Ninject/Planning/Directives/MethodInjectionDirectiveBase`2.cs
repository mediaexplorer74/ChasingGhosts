// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Directives.MethodInjectionDirectiveBase`2
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Directives
{
  /// <summary>Describes the injection of a method or constructor.</summary>
  public abstract class MethodInjectionDirectiveBase<TMethod, TInjector> : IDirective where TMethod : MethodBase
  {
    /// <summary>Gets or sets the injector that will be triggered.</summary>
    public TInjector Injector { get; private set; }

    /// <summary>Gets or sets the targets for the directive.</summary>
    public ITarget[] Targets { get; private set; }

    /// <summary>
    /// Initializes a new instance of the MethodInjectionDirectiveBase&lt;TMethod, TInjector&gt; class.
    /// </summary>
    /// <param name="method">The method this directive represents.</param>
    /// <param name="injector">The injector that will be triggered.</param>
    protected MethodInjectionDirectiveBase(TMethod method, TInjector injector)
    {
      Ensure.ArgumentNotNull((object) method, nameof (method));
      Ensure.ArgumentNotNull((object) injector, nameof (injector));
      this.Injector = injector;
      this.Targets = this.CreateTargetsFromParameters(method);
    }

    /// <summary>Creates targets for the parameters of the method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>The targets for the method's parameters.</returns>
    protected virtual ITarget[] CreateTargetsFromParameters(TMethod method)
    {
      return (ITarget[]) ((IEnumerable<ParameterInfo>) method.GetParameters()).Select<ParameterInfo, ParameterTarget>((Func<ParameterInfo, ParameterTarget>) (parameter => new ParameterTarget((MethodBase) method, parameter))).ToArray<ParameterTarget>();
    }
  }
}
