// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Provider`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using System;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>
  /// A simple abstract provider for instances of a specific type.
  /// </summary>
  /// <typeparam name="T">The type of instances the provider creates.</typeparam>
  public abstract class Provider<T> : IProvider<T>, IProvider
  {
    /// <summary>
    /// Gets the type (or prototype) of instances the provider creates.
    /// </summary>
    public virtual Type Type => typeof (T);

    /// <summary>Creates an instance within the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The created instance.</returns>
    public object Create(IContext context)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      return (object) this.CreateInstance(context);
    }

    /// <summary>Creates an instance within the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The created instance.</returns>
    protected abstract T CreateInstance(IContext context);
  }
}
