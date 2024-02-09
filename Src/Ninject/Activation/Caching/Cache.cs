// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Caching.Cache
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Activation.Caching
{
  /// <summary>Tracks instances for re-use in certain scopes.</summary>
  public class Cache : NinjectComponent, ICache, INinjectComponent, IDisposable, IPruneable
  {
    /// <summary>
    /// Contains all cached instances.
    /// This is a dictionary of scopes to a multimap for bindings to cache entries.
    /// </summary>
    private readonly IDictionary<object, Multimap<IBindingConfiguration, Cache.CacheEntry>> entries = (IDictionary<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>) new Dictionary<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>((IEqualityComparer<object>) new WeakReferenceEqualityComparer());

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Caching.Cache" /> class.
    /// </summary>
    /// <param name="pipeline">The pipeline component.</param>
    /// <param name="cachePruner">The cache pruner component.</param>
    public Cache(IPipeline pipeline, ICachePruner cachePruner)
    {
      Ensure.ArgumentNotNull((object) pipeline, nameof (pipeline));
      Ensure.ArgumentNotNull((object) cachePruner, nameof (cachePruner));
      this.Pipeline = pipeline;
      cachePruner.Start((IPruneable) this);
    }

    /// <summary>Gets the pipeline component.</summary>
    public IPipeline Pipeline { get; private set; }

    /// <summary>
    /// Gets the number of entries currently stored in the cache.
    /// </summary>
    public int Count => this.GetAllCacheEntries().Count<Cache.CacheEntry>();

    /// <summary>Releases resources held by the object.</summary>
    /// <param name="disposing"></param>
    public override void Dispose(bool disposing)
    {
      if (disposing && !this.IsDisposed)
        this.Clear();
      base.Dispose(disposing);
    }

    /// <summary>Stores the specified context in the cache.</summary>
    /// <param name="context">The context to store.</param>
    /// <param name="reference">The instance reference.</param>
    public void Remember(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      object scope = context.GetScope();
      Cache.CacheEntry cacheEntry = new Cache.CacheEntry(context, reference);
      lock (this.entries)
      {
        ReferenceEqualWeakReference weakScopeReference = new ReferenceEqualWeakReference(scope);
        if (!this.entries.ContainsKey((object) weakScopeReference))
        {
          this.entries[(object) weakScopeReference] = new Multimap<IBindingConfiguration, Cache.CacheEntry>();
          if (scope is INotifyWhenDisposed notifyWhenDisposed)
          {
            EventHandler eventHandler = (EventHandler) ((o, e) => this.Clear((object) weakScopeReference));
            notifyWhenDisposed.Disposed += eventHandler;
          }
        }
        this.entries[(object) weakScopeReference].Add(context.Binding.BindingConfiguration, cacheEntry);
      }
    }

    /// <summary>
    /// Tries to retrieve an instance to re-use in the specified context.
    /// </summary>
    /// <param name="context">The context that is being activated.</param>
    /// <returns>The instance for re-use, or <see langword="null" /> if none has been stored.</returns>
    public object TryGet(IContext context)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      object scope = context.GetScope();
      if (scope == null)
        return (object) null;
      lock (this.entries)
      {
        Multimap<IBindingConfiguration, Cache.CacheEntry> multimap;
        if (!this.entries.TryGetValue(scope, out multimap))
          return (object) null;
        foreach (Cache.CacheEntry cacheEntry in (IEnumerable<Cache.CacheEntry>) multimap[context.Binding.BindingConfiguration])
        {
          if (!context.HasInferredGenericArguments || ((IEnumerable<Type>) cacheEntry.Context.GenericArguments).SequenceEqual<Type>((IEnumerable<Type>) context.GenericArguments))
            return cacheEntry.Reference.Instance;
        }
        return (object) null;
      }
    }

    /// <summary>
    /// Deactivates and releases the specified instance from the cache.
    /// </summary>
    /// <param name="instance">The instance to release.</param>
    /// <returns><see langword="True" /> if the instance was found and released; otherwise <see langword="false" />.</returns>
    public bool Release(object instance)
    {
      lock (this.entries)
      {
        bool flag = false;
        foreach (ICollection<Cache.CacheEntry> source in this.entries.Values.SelectMany<Multimap<IBindingConfiguration, Cache.CacheEntry>, ICollection<Cache.CacheEntry>>((Func<Multimap<IBindingConfiguration, Cache.CacheEntry>, IEnumerable<ICollection<Cache.CacheEntry>>>) (bindingEntries => (IEnumerable<ICollection<Cache.CacheEntry>>) bindingEntries.Values)).ToList<ICollection<Cache.CacheEntry>>())
        {
          foreach (Cache.CacheEntry entry in source.Where<Cache.CacheEntry>((Func<Cache.CacheEntry, bool>) (cacheEntry => object.ReferenceEquals(instance, cacheEntry.Reference.Instance))).ToList<Cache.CacheEntry>())
          {
            this.Forget(entry);
            source.Remove(entry);
            flag = true;
          }
        }
        return flag;
      }
    }

    /// <summary>
    /// Removes instances from the cache which should no longer be re-used.
    /// </summary>
    public void Prune()
    {
      lock (this.entries)
      {
        foreach (KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>> keyValuePair in this.entries.Where<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>>((Func<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>, bool>) (scope => !((WeakReference) scope.Key).IsAlive)).Select<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>, KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>>((Func<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>, KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>>) (scope => scope)).ToList<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>>())
        {
          this.entries.Remove(keyValuePair.Key);
          this.Forget(Cache.GetAllBindingEntries((IEnumerable<KeyValuePair<IBindingConfiguration, ICollection<Cache.CacheEntry>>>) keyValuePair.Value));
        }
      }
    }

    /// <summary>
    /// Immediately deactivates and removes all instances in the cache that are owned by
    /// the specified scope.
    /// </summary>
    /// <param name="scope">The scope whose instances should be deactivated.</param>
    public void Clear(object scope)
    {
      lock (this.entries)
      {
        Multimap<IBindingConfiguration, Cache.CacheEntry> bindings;
        if (!this.entries.TryGetValue(scope, out bindings))
          return;
        this.entries.Remove(scope);
        this.Forget(Cache.GetAllBindingEntries((IEnumerable<KeyValuePair<IBindingConfiguration, ICollection<Cache.CacheEntry>>>) bindings));
      }
    }

    /// <summary>
    /// Immediately deactivates and removes all instances in the cache, regardless of scope.
    /// </summary>
    public void Clear()
    {
      lock (this.entries)
      {
        this.Forget(this.GetAllCacheEntries());
        this.entries.Clear();
      }
    }

    /// <summary>
    /// Gets all entries for a binding withing the selected scope.
    /// </summary>
    /// <param name="bindings">The bindings.</param>
    /// <returns>All bindings of a binding.</returns>
    private static IEnumerable<Cache.CacheEntry> GetAllBindingEntries(
      IEnumerable<KeyValuePair<IBindingConfiguration, ICollection<Cache.CacheEntry>>> bindings)
    {
      return bindings.SelectMany<KeyValuePair<IBindingConfiguration, ICollection<Cache.CacheEntry>>, Cache.CacheEntry>((Func<KeyValuePair<IBindingConfiguration, ICollection<Cache.CacheEntry>>, IEnumerable<Cache.CacheEntry>>) (bindingEntries => (IEnumerable<Cache.CacheEntry>) bindingEntries.Value));
    }

    /// <summary>Gets all cache entries.</summary>
    /// <returns>Returns all cache entries.</returns>
    private IEnumerable<Cache.CacheEntry> GetAllCacheEntries()
    {
      return this.entries.SelectMany<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>, Cache.CacheEntry>((Func<KeyValuePair<object, Multimap<IBindingConfiguration, Cache.CacheEntry>>, IEnumerable<Cache.CacheEntry>>) (scopeCache => Cache.GetAllBindingEntries((IEnumerable<KeyValuePair<IBindingConfiguration, ICollection<Cache.CacheEntry>>>) scopeCache.Value)));
    }

    /// <summary>Forgets the specified cache entries.</summary>
    /// <param name="cacheEntries">The cache entries.</param>
    private void Forget(IEnumerable<Cache.CacheEntry> cacheEntries)
    {
      foreach (Cache.CacheEntry entry in cacheEntries.ToList<Cache.CacheEntry>())
        this.Forget(entry);
    }

    /// <summary>Forgets the specified entry.</summary>
    /// <param name="entry">The entry.</param>
    private void Forget(Cache.CacheEntry entry)
    {
      this.Clear(entry.Reference.Instance);
      this.Pipeline.Deactivate(entry.Context, entry.Reference);
    }

    /// <summary>An entry in the cache.</summary>
    private class CacheEntry
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Ninject.Activation.Caching.Cache.CacheEntry" /> class.
      /// </summary>
      /// <param name="context">The context.</param>
      /// <param name="reference">The instance reference.</param>
      public CacheEntry(IContext context, InstanceReference reference)
      {
        this.Context = context;
        this.Reference = reference;
      }

      /// <summary>Gets the context of the instance.</summary>
      /// <value>The context.</value>
      public IContext Context { get; private set; }

      /// <summary>Gets the instance reference.</summary>
      /// <value>The instance reference.</value>
      public InstanceReference Reference { get; private set; }
    }
  }
}
