// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.IModuleLoader
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>Finds modules defined in external files.</summary>
  public interface IModuleLoader : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Loads any modules found in the files that match the specified patterns.
    /// </summary>
    /// <param name="patterns">The patterns to search.</param>
    void LoadModules(IEnumerable<string> patterns);
  }
}
