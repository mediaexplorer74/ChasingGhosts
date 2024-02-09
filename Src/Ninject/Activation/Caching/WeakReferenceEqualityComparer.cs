// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Caching.WeakReferenceEqualityComparer
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Ninject.Activation.Caching
{
  /// <summary>Compares ReferenceEqualWeakReferences to objects</summary>
  public class WeakReferenceEqualityComparer : IEqualityComparer<object>
  {
    /// <summary>Returns if the specifed objects are equal.</summary>
    /// <param name="x">The first object.</param>
    /// <param name="y">The second object.</param>
    /// <returns>True if the objects are equal; otherwise false</returns>
    public bool Equals(object x, object y) => x.Equals(y);

    /// <summary>Returns the hash code of the specified object.</summary>
    /// <param name="obj">The object for which the hash code is calculated.</param>
    /// <returns>The hash code of the specified object.</returns>
    public int GetHashCode(object obj)
    {
      return !(obj is ReferenceEqualWeakReference equalWeakReference) ? RuntimeHelpers.GetHashCode(obj) : equalWeakReference.GetHashCode();
    }
  }
}
