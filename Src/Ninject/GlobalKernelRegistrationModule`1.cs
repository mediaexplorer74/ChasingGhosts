// Decompiled with JetBrains decompiler
// Type: Ninject.GlobalKernelRegistrationModule`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Modules;

#nullable disable
namespace Ninject
{
  /// <summary>
  /// Registers the kernel into which the module is loaded on the GlobalKernelRegistry using the
  /// type specified by TGlobalKernelRegistry.
  /// </summary>
  /// <typeparam name="TGlobalKernelRegistry">The type that is used to register the kernel.</typeparam>
  public abstract class GlobalKernelRegistrationModule<TGlobalKernelRegistry> : NinjectModule where TGlobalKernelRegistry : GlobalKernelRegistration
  {
    /// <summary>Loads the module into the kernel.</summary>
    public override void Load()
    {
      GlobalKernelRegistration.RegisterKernelForType(this.Kernel, typeof (TGlobalKernelRegistry));
    }

    /// <summary>Unloads the module from the kernel.</summary>
    public override void Unload()
    {
      GlobalKernelRegistration.UnregisterKernelForType(this.Kernel, typeof (TGlobalKernelRegistry));
    }
  }
}
