// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.INinjectModule
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>
  /// A pluggable unit that can be loaded into an <see cref="T:Ninject.IKernel" />.
  /// </summary>
  public interface INinjectModule : IHaveKernel
  {
    /// <summary>Gets the module's name.</summary>
    string Name { get; }

    /// <summary>Called when the module is loaded into a kernel.</summary>
    /// <param name="kernel">The kernel that is loading the module.</param>
    void OnLoad(IKernel kernel);

    /// <summary>Called when the module is unloaded from a kernel.</summary>
    /// <param name="kernel">The kernel that is unloading the module.</param>
    void OnUnload(IKernel kernel);

    /// <summary>
    /// Called after loading the modules. A module can verify here if all other required modules are loaded.
    /// </summary>
    void OnVerifyRequiredModules();
  }
}
