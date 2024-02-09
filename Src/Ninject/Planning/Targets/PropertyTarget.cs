// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Targets.PropertyTarget
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Targets
{
  /// <summary>
  /// Represents an injection target for a <see cref="T:System.Reflection.PropertyInfo" />.
  /// </summary>
  public class PropertyTarget : Target<PropertyInfo>
  {
    /// <summary>Gets the name of the target.</summary>
    public override string Name => this.Site.Name;

    /// <summary>Gets the type of the target.</summary>
    public override Type Type => this.Site.PropertyType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Targets.PropertyTarget" /> class.
    /// </summary>
    /// <param name="site">The property that this target represents.</param>
    public PropertyTarget(PropertyInfo site)
      : base((MemberInfo) site, site)
    {
    }
  }
}
