// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Caching.GarbageCollectionCachePruner
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Ninject.Activation.Caching
{
  /// <summary>
  /// Uses a <see cref="T:System.Threading.Timer" /> and some <see cref="T:System.WeakReference" /> magic to poll
  /// the garbage collector to see if it has run.
  /// </summary>
  public class GarbageCollectionCachePruner : 
    NinjectComponent,
    ICachePruner,
    INinjectComponent,
    IDisposable
  {
    /// <summary>indicator for if GC has been run.</summary>
    private readonly WeakReference indicator = new WeakReference(new object());
    /// <summary>The caches that are being pruned.</summary>
    private readonly List<IPruneable> caches = new List<IPruneable>();
    /// <summary>The timer used to trigger the cache pruning</summary>
    private Timer timer;
    private bool stop;

    /// <summary>Releases resources held by the object.</summary>
    public override void Dispose(bool disposing)
    {
      if (disposing && !this.IsDisposed && this.timer != null)
        this.Stop();
      base.Dispose(disposing);
    }

    /// <summary>
    /// Starts pruning the specified pruneable based on the rules of the pruner.
    /// </summary>
    /// <param name="pruneable">The pruneable that will be pruned.</param>
    public void Start(IPruneable pruneable)
    {
      Ensure.ArgumentNotNull((object) pruneable, nameof (pruneable));
      this.caches.Add(pruneable);
      if (this.timer != null)
        return;
      this.timer = new Timer(new TimerCallback(this.PruneCacheIfGarbageCollectorHasRun), (object) null, this.GetTimeoutInMilliseconds(), -1);
    }

    /// <summary>Stops pruning.</summary>
    public void Stop()
    {
      lock (this)
        this.stop = true;
      using (ManualResetEvent notifyObject = new ManualResetEvent(false))
      {
        this.timer.Dispose((WaitHandle) notifyObject);
        notifyObject.WaitOne();
        this.timer = (Timer) null;
        this.caches.Clear();
      }
    }

    private void PruneCacheIfGarbageCollectorHasRun(object state)
    {
      lock (this)
      {
        if (this.stop)
          return;
        try
        {
          if (this.indicator.IsAlive)
            return;
          this.caches.Map<IPruneable>((Action<IPruneable>) (cache => cache.Prune()));
          this.indicator.Target = new object();
        }
        finally
        {
          this.timer.Change(this.GetTimeoutInMilliseconds(), -1);
        }
      }
    }

    private int GetTimeoutInMilliseconds()
    {
      TimeSpan cachePruningInterval = this.Settings.CachePruningInterval;
      return !(cachePruningInterval == TimeSpan.MaxValue) ? (int) cachePruningInterval.TotalMilliseconds : -1;
    }
  }
}
