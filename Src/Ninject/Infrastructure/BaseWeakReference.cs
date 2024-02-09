// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.BaseWeakReference
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Infrastructure
{
  /// <summary>Inheritable weak reference base class for Silverlight</summary>
  public abstract class BaseWeakReference
  {
    private readonly WeakReference innerWeakReference;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Infrastructure.ReferenceEqualWeakReference" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    protected BaseWeakReference(object target)
    {
      this.innerWeakReference = new WeakReference(target);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Infrastructure.ReferenceEqualWeakReference" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="trackResurrection">if set to <c>true</c> [track resurrection].</param>
    protected BaseWeakReference(object target, bool trackResurrection)
    {
      this.innerWeakReference = new WeakReference(target, trackResurrection);
    }

    /// <summary>
    /// Gets a value indicating whether this instance is alive.
    /// </summary>
    /// <value><c>true</c> if this instance is alive; otherwise, <c>false</c>.</value>
    public bool IsAlive => this.innerWeakReference.IsAlive;

    /// <summary>Gets or sets the target of this weak reference.</summary>
    /// <value>The target of this weak reference.</value>
    public object Target
    {
      get => this.innerWeakReference.Target;
      set => this.innerWeakReference.Target = value;
    }
  }
}
