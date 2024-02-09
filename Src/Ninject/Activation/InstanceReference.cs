// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.InstanceReference
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Security;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>
  /// Holds an instance during activation or after it has been cached.
  /// </summary>
  public class InstanceReference
  {
    /// <summary>Gets or sets the instance.</summary>
    public object Instance { get; set; }

    /// <summary>
    /// Returns a value indicating whether the instance is of the specified type.
    /// </summary>
    /// <typeparam name="T">The type in question.</typeparam>
    /// <returns><see langword="True" /> if the instance is of the specified type, otherwise <see langword="false" />.</returns>
    [SecuritySafeCritical]
    public bool Is<T>() => this.Instance is T;

    /// <summary>Returns the instance as the specified type.</summary>
    /// <typeparam name="T">The requested type.</typeparam>
    /// <returns>The instance.</returns>
    public T As<T>() => (T) this.Instance;

    /// <summary>
    /// Executes the specified action if the instance if of the specified type.
    /// </summary>
    /// <typeparam name="T">The type in question.</typeparam>
    /// <param name="action">The action to execute.</param>
    public void IfInstanceIs<T>(Action<T> action)
    {
      if (!this.Is<T>())
        return;
      action((T) this.Instance);
    }
  }
}
