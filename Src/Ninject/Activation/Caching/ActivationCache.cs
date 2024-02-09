// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Caching.ActivationCache
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Activation.Caching
{
  /// <summary>Stores the objects that were activated</summary>
  public class ActivationCache : 
    NinjectComponent,
    IActivationCache,
    INinjectComponent,
    IDisposable,
    IPruneable
  {
    /// <summary>
    /// The objects that were activated as reference equal weak references.
    /// </summary>
    private readonly HashSet<object> activatedObjects = new HashSet<object>((IEqualityComparer<object>) new WeakReferenceEqualityComparer());
    /// <summary>
    /// The objects that were activated as reference equal weak references.
    /// </summary>
    private readonly HashSet<object> deactivatedObjects = new HashSet<object>((IEqualityComparer<object>) new WeakReferenceEqualityComparer());

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Caching.ActivationCache" /> class.
    /// </summary>
    /// <param name="cachePruner">The cache pruner.</param>
    public ActivationCache(ICachePruner cachePruner) => cachePruner.Start((IPruneable) this);

    /// <summary>Gets the activated object count.</summary>
    /// <value>The activated object count.</value>
    public int ActivatedObjectCount => this.activatedObjects.Count;

    /// <summary>Gets the deactivated object count.</summary>
    /// <value>The deactivated object count.</value>
    public int DeactivatedObjectCount => this.deactivatedObjects.Count;

    /// <summary>Clears the cache.</summary>
    public void Clear()
    {
      lock (this.activatedObjects)
        this.activatedObjects.Clear();
      lock (this.deactivatedObjects)
        this.deactivatedObjects.Clear();
    }

    /// <summary>Adds an activated instance.</summary>
    /// <param name="instance">The instance to be added.</param>
    public void AddActivatedInstance(object instance)
    {
      lock (this.activatedObjects)
        this.activatedObjects.Add((object) new ReferenceEqualWeakReference(instance));
    }

    /// <summary>Adds an deactivated instance.</summary>
    /// <param name="instance">The instance to be added.</param>
    public void AddDeactivatedInstance(object instance)
    {
      lock (this.deactivatedObjects)
        this.deactivatedObjects.Add((object) new ReferenceEqualWeakReference(instance));
    }

    /// <summary>
    /// Determines whether the specified instance is activated.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified instance is activated; otherwise, <c>false</c>.
    /// </returns>
    public bool IsActivated(object instance) => this.activatedObjects.Contains(instance);

    /// <summary>
    /// Determines whether the specified instance is deactivated.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified instance is deactivated; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDeactivated(object instance) => this.deactivatedObjects.Contains(instance);

    /// <summary>Prunes this instance.</summary>
    public void Prune()
    {
      lock (this.activatedObjects)
        ActivationCache.RemoveDeadObjects(this.activatedObjects);
      lock (this.deactivatedObjects)
        ActivationCache.RemoveDeadObjects(this.deactivatedObjects);
    }

    /// <summary>Removes all dead objects.</summary>
    /// <param name="objects">The objects collection to be freed of dead objects.</param>
    private static void RemoveDeadObjects(HashSet<object> objects)
    {
      objects.RemoveWhere((Predicate<object>) (reference => !((WeakReference) reference).IsAlive));
    }
  }
}
