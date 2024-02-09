// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.AssemblyNameRetriever
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>
  /// Retrieves assembly names from file names using a temporary app domain.
  /// </summary>
  public class AssemblyNameRetriever : 
    NinjectComponent,
    IAssemblyNameRetriever,
    INinjectComponent,
    IDisposable
  {
    /// <summary>
    /// Gets all assembly names of the assemblies in the given files that match the filter.
    /// </summary>
    /// <param name="filenames">The filenames.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>All assembly names of the assemblies in the given files that match the filter.</returns>
    public IEnumerable<AssemblyName> GetAssemblyNames(
      IEnumerable<string> filenames,
      Predicate<Assembly> filter)
    {
      Type type = typeof (AssemblyNameRetriever.AssemblyChecker);
      AppDomain temporaryAppDomain = AssemblyNameRetriever.CreateTemporaryAppDomain();
      try
      {
        return ((AssemblyNameRetriever.AssemblyChecker) temporaryAppDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName ?? string.Empty)).GetAssemblyNames((IEnumerable<string>) filenames.ToArray<string>(), filter);
      }
      finally
      {
        AppDomain.Unload(temporaryAppDomain);
      }
    }

    /// <summary>Creates a temporary app domain.</summary>
    /// <returns>The created app domain.</returns>
    private static AppDomain CreateTemporaryAppDomain()
    {
      return AppDomain.CreateDomain("NinjectModuleLoader", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation);
    }

    /// <summary>
    /// This class is loaded into the temporary appdomain to load and check if the assemblies match the filter.
    /// </summary>
    private class AssemblyChecker : MarshalByRefObject
    {
      /// <summary>
      /// Gets the assembly names of the assemblies matching the filter.
      /// </summary>
      /// <param name="filenames">The filenames.</param>
      /// <param name="filter">The filter.</param>
      /// <returns>All assembly names of the assemblies matching the filter.</returns>
      public IEnumerable<AssemblyName> GetAssemblyNames(
        IEnumerable<string> filenames,
        Predicate<Assembly> filter)
      {
        List<AssemblyName> assemblyNames = new List<AssemblyName>();
        foreach (string filename in filenames)
        {
          Assembly assembly;
          if (File.Exists(filename))
          {
            try
            {
              assembly = Assembly.LoadFrom(filename);
            }
            catch (BadImageFormatException ex)
            {
              continue;
            }
          }
          else
          {
            try
            {
              assembly = Assembly.Load(filename);
            }
            catch (FileNotFoundException ex)
            {
              continue;
            }
          }
          if (filter(assembly))
            assemblyNames.Add(assembly.GetName(false));
        }
        return (IEnumerable<AssemblyName>) assemblyNames;
      }
    }
  }
}
