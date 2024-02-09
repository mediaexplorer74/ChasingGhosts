// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Disposal.INotifyWhenDisposed
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Infrastructure.Disposal
{
  /// <summary>An object that fires an event when it is disposed.</summary>
  public interface INotifyWhenDisposed : IDisposableObject, IDisposable
  {
    /// <summary>Occurs when the object is disposed.</summary>
    event EventHandler Disposed;
  }
}
