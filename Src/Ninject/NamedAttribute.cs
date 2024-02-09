// Decompiled with JetBrains decompiler
// Type: Ninject.NamedAttribute
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Planning.Bindings;

#nullable disable
namespace Ninject
{
  /// <summary>
  /// Indicates that the decorated member should only be injected using binding(s) registered
  /// with the specified name.
  /// </summary>
  public class NamedAttribute : ConstraintAttribute
  {
    /// <summary>Gets the binding name.</summary>
    public string Name { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.NamedAttribute" /> class.
    /// </summary>
    /// <param name="name">The name of the binding(s) to use.</param>
    public NamedAttribute(string name)
    {
      Ensure.ArgumentNotNullOrEmpty(name, nameof (name));
      this.Name = name;
    }

    /// <summary>
    /// Determines whether the specified binding metadata matches the constraint.
    /// </summary>
    /// <param name="metadata">The metadata in question.</param>
    /// <returns><c>True</c> if the metadata matches; otherwise <c>false</c>.</returns>
    public override bool Matches(IBindingMetadata metadata)
    {
      Ensure.ArgumentNotNull((object) metadata, nameof (metadata));
      return metadata.Name == this.Name;
    }
  }
}
