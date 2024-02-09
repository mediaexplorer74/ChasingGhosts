// Decompiled with JetBrains decompiler
// Type: Ninject.ModuleLoadExtensions
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Modules;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Ninject
{
  /// <summary>Extension methods that enhance module loading.</summary>
  public static class ModuleLoadExtensions
  {
    /// <summary>
    /// Creates a new instance of the module and loads it into the kernel.
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <param name="kernel">The kernel.</param>
    public static void Load<TModule>(this IKernel kernel) where TModule : INinjectModule, new()
    {
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      kernel.Load((INinjectModule) new TModule());
    }

    /// <summary>Loads the module(s) into the kernel.</summary>
    /// <param name="kernel">The kernel.</param>
    /// <param name="modules">The modules to load.</param>
    public static void Load(this IKernel kernel, params INinjectModule[] modules)
    {
      kernel.Load((IEnumerable<INinjectModule>) modules);
    }

    /// <summary>
    /// Loads modules from the files that match the specified pattern(s).
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    /// <param name="filePatterns">The file patterns (i.e. "*.dll", "modules/*.rb") to match.</param>
    public static void Load(this IKernel kernel, params string[] filePatterns)
    {
      kernel.Load((IEnumerable<string>) filePatterns);
    }

    /// <summary>Loads modules defined in the specified assemblies.</summary>
    /// <param name="kernel">The kernel.</param>
    /// <param name="assemblies">The assemblies to search.</param>
    public static void Load(this IKernel kernel, params Assembly[] assemblies)
    {
      kernel.Load((IEnumerable<Assembly>) assemblies);
    }
  }
}
