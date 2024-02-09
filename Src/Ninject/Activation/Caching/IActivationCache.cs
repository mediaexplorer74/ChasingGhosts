// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Caching.IActivationCache
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;

#nullable disable
namespace Ninject.Activation.Caching
{
  /// <summary>Stores the objects that were activated</summary>
  public interface IActivationCache : INinjectComponent, IDisposable
  {
    /// <summary>Clears the cache.</summary>
    void Clear();

    /// <summary>Adds an activated instance.</summary>
    /// <param name="instance">The instance to be added.</param>
    void AddActivatedInstance(object instance);

    /// <summary>Adds an deactivated instance.</summary>
    /// <param name="instance">The instance to be added.</param>
    void AddDeactivatedInstance(object instance);

    /// <summary>
    /// Determines whether the specified instance is activated.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified instance is activated; otherwise, <c>false</c>.
    /// </returns>
    bool IsActivated(object instance);

    /// <summary>
    /// Determines whether the specified instance is deactivated.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified instance is deactivated; otherwise, <c>false</c>.
    /// </returns>
    bool IsDeactivated(object instance);
  }
}
