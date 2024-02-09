// Decompiled with JetBrains decompiler
// Type: Ninject.Components.INinjectComponent
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Components
{
  /// <summary>
  /// A component that contributes to the internals of Ninject.
  /// </summary>
  public interface INinjectComponent : IDisposable
  {
    /// <summary>Gets or sets the settings.</summary>
    INinjectSettings Settings { get; set; }
  }
}
