// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Disposal.DisposableObject
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Infrastructure.Disposal
{
  /// <summary>An object that notifies when it is disposed.</summary>
  public abstract class DisposableObject : IDisposableObject, IDisposable
  {
    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.Dispose(true);

    /// <summary>Releases resources held by the object.</summary>
    public virtual void Dispose(bool disposing)
    {
      lock (this)
      {
        if (!disposing || this.IsDisposed)
          return;
        this.IsDisposed = true;
        GC.SuppressFinalize((object) this);
      }
    }

    /// <summary>
    /// Releases resources before the object is reclaimed by garbage collection.
    /// </summary>
    ~DisposableObject() => this.Dispose(false);
  }
}
