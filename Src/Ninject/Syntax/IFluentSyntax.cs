// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IFluentSyntax
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>
  /// A hack to hide methods defined on <see cref="T:System.Object" /> for IntelliSense
  /// on fluent interfaces. Credit to Daniel Cazzulino.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IFluentSyntax
  {
    /// <summary>Gets the type of this instance.</summary>
    /// <returns>The type of this instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetType();

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    string ToString();

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="T:System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object other);
  }
}
