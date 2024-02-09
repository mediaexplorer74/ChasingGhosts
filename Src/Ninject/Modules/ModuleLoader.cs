// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.ModuleLoader
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>
  /// Automatically finds and loads modules from assemblies.
  /// </summary>
  public class ModuleLoader : NinjectComponent, IModuleLoader, INinjectComponent, IDisposable
  {
    /// <summary>
    /// Gets or sets the kernel into which modules will be loaded.
    /// </summary>
    public IKernel Kernel { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Modules.ModuleLoader" /> class.
    /// </summary>
    /// <param name="kernel">The kernel into which modules will be loaded.</param>
    public ModuleLoader(IKernel kernel)
    {
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.Kernel = kernel;
    }

    /// <summary>
    /// Loads any modules found in the files that match the specified patterns.
    /// </summary>
    /// <param name="patterns">The patterns to search.</param>
    public void LoadModules(IEnumerable<string> patterns)
    {
      IEnumerable<IModuleLoaderPlugin> all = this.Kernel.Components.GetAll<IModuleLoaderPlugin>();
      foreach (IGrouping<string, string> filenames in patterns.SelectMany<string, string>((Func<string, IEnumerable<string>>) (pattern => ModuleLoader.GetFilesMatchingPattern(pattern))).GroupBy<string, string>((Func<string, string>) (filename => Path.GetExtension(filename).ToLowerInvariant())))
      {
        string extension = filenames.Key;
        all.Where<IModuleLoaderPlugin>((Func<IModuleLoaderPlugin, bool>) (p => p.SupportedExtensions.Contains<string>(extension))).FirstOrDefault<IModuleLoaderPlugin>()?.LoadModules((IEnumerable<string>) filenames);
      }
    }

    private static IEnumerable<string> GetFilesMatchingPattern(string pattern)
    {
      return ModuleLoader.NormalizePaths(Path.GetDirectoryName(pattern)).SelectMany<string, string>((Func<string, IEnumerable<string>>) (path => (IEnumerable<string>) Directory.GetFiles(path, Path.GetFileName(pattern))));
    }

    private static IEnumerable<string> NormalizePaths(string path)
    {
      if (!Path.IsPathRooted(path))
        return ModuleLoader.GetBaseDirectories().Select<string, string>((Func<string, string>) (baseDirectory => Path.Combine(baseDirectory, path)));
      return (IEnumerable<string>) new string[1]
      {
        Path.GetFullPath(path)
      };
    }

    private static IEnumerable<string> GetBaseDirectories()
    {
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
      return !string.IsNullOrEmpty(relativeSearchPath) ? ((IEnumerable<string>) relativeSearchPath.Split(new char[1]
      {
        Path.PathSeparator
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (path => Path.Combine(baseDirectory, path))) : (IEnumerable<string>) new string[1]
      {
        baseDirectory
      };
    }
  }
}
