// Decompiled with JetBrains decompiler
// Type: Ninject.Modules.NinjectModule
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Modules
{
  /// <summary>
  /// A loadable unit that defines bindings for your application.
  /// </summary>
  public abstract class NinjectModule : BindingRoot, INinjectModule, IHaveKernel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Modules.NinjectModule" /> class.
    /// </summary>
    protected NinjectModule() => this.Bindings = (ICollection<IBinding>) new List<IBinding>();

    /// <summary>Gets the kernel that the module is loaded into.</summary>
    public IKernel Kernel { get; private set; }

    /// <summary>
    /// Gets the module's name. Only a single module with a given name can be loaded at one time.
    /// </summary>
    public virtual string Name => this.GetType().FullName;

    /// <summary>Gets the bindings that were registered by the module.</summary>
    public ICollection<IBinding> Bindings { get; private set; }

    /// <summary>Gets the kernel.</summary>
    /// <value>The kernel.</value>
    protected override IKernel KernelInstance => this.Kernel;

    /// <summary>Called when the module is loaded into a kernel.</summary>
    /// <param name="kernel">The kernel that is loading the module.</param>
    public void OnLoad(IKernel kernel)
    {
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.Kernel = kernel;
      this.Load();
    }

    /// <summary>Called when the module is unloaded from a kernel.</summary>
    /// <param name="kernel">The kernel that is unloading the module.</param>
    public void OnUnload(IKernel kernel)
    {
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.Unload();
      this.Bindings.Map<IBinding>(new Action<IBinding>(((IBindingRoot) this.Kernel).RemoveBinding));
      this.Kernel = (IKernel) null;
    }

    /// <summary>
    /// Called after loading the modules. A module can verify here if all other required modules are loaded.
    /// </summary>
    public void OnVerifyRequiredModules() => this.VerifyRequiredModulesAreLoaded();

    /// <summary>Loads the module into the kernel.</summary>
    public abstract void Load();

    /// <summary>Unloads the module from the kernel.</summary>
    public virtual void Unload()
    {
    }

    /// <summary>
    /// Called after loading the modules. A module can verify here if all other required modules are loaded.
    /// </summary>
    public virtual void VerifyRequiredModulesAreLoaded()
    {
    }

    /// <summary>Unregisters all bindings for the specified service.</summary>
    /// <param name="service">The service to unbind.</param>
    public override void Unbind(Type service) => this.Kernel.Unbind(service);

    /// <summary>Registers the specified binding.</summary>
    /// <param name="binding">The binding to add.</param>
    public override void AddBinding(IBinding binding)
    {
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      this.Kernel.AddBinding(binding);
      this.Bindings.Add(binding);
    }

    /// <summary>Unregisters the specified binding.</summary>
    /// <param name="binding">The binding to remove.</param>
    public override void RemoveBinding(IBinding binding)
    {
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      this.Kernel.RemoveBinding(binding);
      this.Bindings.Remove(binding);
    }
  }
}
