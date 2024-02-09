// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.IProvider
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Activation
{
  /// <summary>Creates instances of services.</summary>
  public interface IProvider
  {
    /// <summary>
    /// Gets the type (or prototype) of instances the provider creates.
    /// </summary>
    Type Type { get; }

    /// <summary>Creates an instance within the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The created instance.</returns>
    object Create(IContext context);
  }
}
