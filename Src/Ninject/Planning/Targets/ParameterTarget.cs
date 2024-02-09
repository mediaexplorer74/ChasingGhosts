// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Targets.ParameterTarget
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Targets
{
  /// <summary>
  /// Represents an injection target for a <see cref="T:System.Reflection.ParameterInfo" />.
  /// </summary>
  public class ParameterTarget : Target<ParameterInfo>
  {
    private readonly Future<object> defaultValue;

    /// <summary>Gets the name of the target.</summary>
    public override string Name => this.Site.Name;

    /// <summary>Gets the type of the target.</summary>
    public override Type Type => this.Site.ParameterType;

    /// <summary>
    /// Gets a value indicating whether the target has a default value.
    /// </summary>
    public override bool HasDefaultValue => this.defaultValue.Value != DBNull.Value;

    /// <summary>Gets the default value for the target.</summary>
    /// <exception cref="T:System.InvalidOperationException">If the item does not have a default value.</exception>
    public override object DefaultValue
    {
      get => !this.HasDefaultValue ? base.DefaultValue : this.defaultValue.Value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Targets.ParameterTarget" /> class.
    /// </summary>
    /// <param name="method">The method that defines the parameter.</param>
    /// <param name="site">The parameter that this target represents.</param>
    public ParameterTarget(MethodBase method, ParameterInfo site)
      : base((MemberInfo) method, site)
    {
      this.defaultValue = new Future<object>((Func<object>) (() => site.DefaultValue));
    }
  }
}
