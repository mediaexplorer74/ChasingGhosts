// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.IModuleLoaderPlugin
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
  /// <summary>Loads modules at runtime by searching external files.</summary>
  public interface IModuleLoaderPlugin : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Gets the file extensions that the plugin understands how to load.
    /// </summary>
    IEnumerable<string> SupportedExtensions { get; }

    /// <summary>Loads modules from the specified files.</summary>
    /// <param name="filenames">The names of the files to load modules from.</param>
    void LoadModules(IEnumerable<string> filenames);
  }
}
