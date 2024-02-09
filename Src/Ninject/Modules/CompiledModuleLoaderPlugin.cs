// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.CompiledModuleLoaderPlugin
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>Loads modules from compiled assemblies.</summary>
  public class CompiledModuleLoaderPlugin : 
    NinjectComponent,
    IModuleLoaderPlugin,
    INinjectComponent,
    IDisposable
  {
    /// <summary>The assembly name retriever.</summary>
    private readonly IAssemblyNameRetriever assemblyNameRetriever;
    /// <summary>The file extensions that are supported.</summary>
    private static readonly string[] Extensions = new string[1]
    {
      ".dll"
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Modules.CompiledModuleLoaderPlugin" /> class.
    /// </summary>
    /// <param name="kernel">The kernel into which modules will be loaded.</param>
    /// <param name="assemblyNameRetriever">The assembly name retriever.</param>
    public CompiledModuleLoaderPlugin(IKernel kernel, IAssemblyNameRetriever assemblyNameRetriever)
    {
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.Kernel = kernel;
      this.assemblyNameRetriever = assemblyNameRetriever;
    }

    /// <summary>Gets the kernel into which modules will be loaded.</summary>
    public IKernel Kernel { get; private set; }

    /// <summary>
    /// Gets the file extensions that the plugin understands how to load.
    /// </summary>
    public IEnumerable<string> SupportedExtensions
    {
      get => (IEnumerable<string>) CompiledModuleLoaderPlugin.Extensions;
    }

    /// <summary>Loads modules from the specified files.</summary>
    /// <param name="filenames">The names of the files to load modules from.</param>
    public void LoadModules(IEnumerable<string> filenames)
    {
      this.Kernel.Load(this.assemblyNameRetriever.GetAssemblyNames(filenames, (Predicate<Assembly>) (asm => asm.HasNinjectModules())).Select<AssemblyName, Assembly>((Func<AssemblyName, Assembly>) (asm => Assembly.Load(asm))));
    }
  }
}
