// Decompiled with JetBrains decompiler
// Type: Ninject.ConstraintAttribute
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Planning.Bindings;
using System;

#nullable disable
namespace Ninject
{
  /// <summary>Defines a constraint on the decorated member.</summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public abstract class ConstraintAttribute : Attribute
  {
    /// <summary>
    /// Determines whether the specified binding metadata matches the constraint.
    /// </summary>
    /// <param name="metadata">The metadata in question.</param>
    /// <returns><c>True</c> if the metadata matches; otherwise <c>false</c>.</returns>
    public abstract bool Matches(IBindingMetadata metadata);
  }
}
