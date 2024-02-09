// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Directives.PropertyInjectionDirective
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Injection;
using Ninject.Planning.Targets;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Directives
{
  /// <summary>Describes the injection of a property.</summary>
  public class PropertyInjectionDirective : IDirective
  {
    /// <summary>Gets or sets the injector that will be triggered.</summary>
    public PropertyInjector Injector { get; private set; }

    /// <summary>Gets or sets the injection target for the directive.</summary>
    public ITarget Target { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Directives.PropertyInjectionDirective" /> class.
    /// </summary>
    /// <param name="member">The member the directive describes.</param>
    /// <param name="injector">The injector that will be triggered.</param>
    public PropertyInjectionDirective(PropertyInfo member, PropertyInjector injector)
    {
      this.Injector = injector;
      this.Target = this.CreateTarget(member);
    }

    /// <summary>Creates a target for the property.</summary>
    /// <param name="propertyInfo">The property.</param>
    /// <returns>The target for the property.</returns>
    protected virtual ITarget CreateTarget(PropertyInfo propertyInfo)
    {
      return (ITarget) new PropertyTarget(propertyInfo);
    }
  }
}
