// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Directives.ConstructorInjectionDirective
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Injection;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Directives
{
  /// <summary>Describes the injection of a constructor.</summary>
  public class ConstructorInjectionDirective : 
    MethodInjectionDirectiveBase<ConstructorInfo, ConstructorInjector>
  {
    /// <summary>The base .ctor definition.</summary>
    public ConstructorInfo Constructor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this constructor has an inject attribute.
    /// </summary>
    /// <value><c>true</c> if this constructor has an inject attribute; otherwise, <c>false</c>.</value>
    public bool HasInjectAttribute { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Directives.ConstructorInjectionDirective" /> class.
    /// </summary>
    /// <param name="constructor">The constructor described by the directive.</param>
    /// <param name="injector">The injector that will be triggered.</param>
    public ConstructorInjectionDirective(ConstructorInfo constructor, ConstructorInjector injector)
      : base(constructor, injector)
    {
      this.Constructor = constructor;
    }
  }
}
