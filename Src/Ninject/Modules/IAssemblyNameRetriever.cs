// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.IAssemblyNameRetriever
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>
  /// Retrieves assembly names from file names using a temporary app domain.
  /// </summary>
  public interface IAssemblyNameRetriever : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Gets all assembly names of the assemblies in the given files that match the filter.
    /// </summary>
    /// <param name="filenames">The filenames.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>All assembly names of the assemblies in the given files that match the filter.</returns>
    IEnumerable<AssemblyName> GetAssemblyNames(
      IEnumerable<string> filenames,
      Predicate<Assembly> filter);
  }
}
