// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.ReferenceEqualWeakReference
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Ninject.Infrastructure
{
  /// <summary>
  /// Weak reference that can be used in collections. It is equal to the
  /// object it references and has the same hash code.
  /// </summary>
  public class ReferenceEqualWeakReference : WeakReference
  {
    private readonly int cashedHashCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Infrastructure.ReferenceEqualWeakReference" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    public ReferenceEqualWeakReference(object target)
      : base(target)
    {
      this.cashedHashCode = RuntimeHelpers.GetHashCode(target);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Infrastructure.ReferenceEqualWeakReference" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="trackResurrection">if set to <c>true</c> [track resurrection].</param>
    public ReferenceEqualWeakReference(object target, bool trackResurrection)
      : base(target, trackResurrection)
    {
      this.cashedHashCode = RuntimeHelpers.GetHashCode(target);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj" /> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
      object objA = this.IsAlive ? this.Target : (object) this;
      if (obj is WeakReference weakReference && weakReference.IsAlive)
        obj = weakReference.Target;
      return object.ReferenceEquals(objA, obj);
    }

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode() => this.cashedHashCode;
  }
}
