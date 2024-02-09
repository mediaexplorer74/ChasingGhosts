// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Directives.MethodInjectionDirective
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Injection;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Directives
{
  /// <summary>Describes the injection of a method.</summary>
  public class MethodInjectionDirective : MethodInjectionDirectiveBase<MethodInfo, MethodInjector>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Directives.MethodInjectionDirective" /> class.
    /// </summary>
    /// <param name="method">The method described by the directive.</param>
    /// <param name="injector">The injector that will be triggered.</param>
    public MethodInjectionDirective(MethodInfo method, MethodInjector injector)
      : base(method, injector)
    {
    }
  }
}
