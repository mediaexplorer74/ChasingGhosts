// Decompiled with JetBrains decompiler
// Type: Ninject.GlobalKernelRegistration
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace Ninject
{
  /// <summary>
  /// Allows to register kernel globally to perform some tasks on all kernels.
  /// The registration is done by loading the GlobalKernelRegistrationModule to the kernel.
  /// </summary>
  public abstract class GlobalKernelRegistration
  {
    private static readonly ReaderWriterLock kernelRegistrationsLock = new ReaderWriterLock();
    private static readonly IDictionary<Type, GlobalKernelRegistration.Registration> kernelRegistrations = (IDictionary<Type, GlobalKernelRegistration.Registration>) new Dictionary<Type, GlobalKernelRegistration.Registration>();

    internal static void RegisterKernelForType(IKernel kernel, Type type)
    {
      GlobalKernelRegistration.Registration registrationForType = GlobalKernelRegistration.GetRegistrationForType(type);
      registrationForType.KernelLock.AcquireReaderLock(-1);
      try
      {
        registrationForType.Kernels.Add(new WeakReference((object) kernel));
      }
      finally
      {
        registrationForType.KernelLock.ReleaseReaderLock();
      }
    }

    internal static void UnregisterKernelForType(IKernel kernel, Type type)
    {
      GlobalKernelRegistration.Registration registrationForType = GlobalKernelRegistration.GetRegistrationForType(type);
      GlobalKernelRegistration.RemoveKernels(registrationForType, registrationForType.Kernels.Where<WeakReference>((Func<WeakReference, bool>) (reference => reference.Target == kernel || !reference.IsAlive)));
    }

    /// <summary>Performs an action on all registered kernels.</summary>
    /// <param name="action">The action.</param>
    protected void MapKernels(Action<IKernel> action)
    {
      bool flag = false;
      GlobalKernelRegistration.Registration registrationForType = GlobalKernelRegistration.GetRegistrationForType(this.GetType());
      registrationForType.KernelLock.AcquireReaderLock(-1);
      try
      {
        foreach (WeakReference kernel in (IEnumerable<WeakReference>) registrationForType.Kernels)
        {
          if (kernel.Target is IKernel target)
            action(target);
          else
            flag = true;
        }
      }
      finally
      {
        registrationForType.KernelLock.ReleaseReaderLock();
      }
      if (!flag)
        return;
      GlobalKernelRegistration.RemoveKernels(registrationForType, registrationForType.Kernels.Where<WeakReference>((Func<WeakReference, bool>) (reference => !reference.IsAlive)));
    }

    private static void RemoveKernels(
      GlobalKernelRegistration.Registration registration,
      IEnumerable<WeakReference> references)
    {
      registration.KernelLock.ReleaseReaderLock();
      try
      {
        foreach (WeakReference weakReference in references.ToArray<WeakReference>())
          registration.Kernels.Remove(weakReference);
      }
      finally
      {
        registration.KernelLock.ReleaseReaderLock();
      }
    }

    private static GlobalKernelRegistration.Registration GetRegistrationForType(Type type)
    {
      GlobalKernelRegistration.kernelRegistrationsLock.AcquireReaderLock(-1);
      try
      {
        GlobalKernelRegistration.Registration registration;
        return GlobalKernelRegistration.kernelRegistrations.TryGetValue(type, out registration) ? registration : GlobalKernelRegistration.CreateNewRegistration(type);
      }
      finally
      {
        GlobalKernelRegistration.kernelRegistrationsLock.ReleaseReaderLock();
      }
    }

    private static GlobalKernelRegistration.Registration CreateNewRegistration(Type type)
    {
      LockCookie writerLock = GlobalKernelRegistration.kernelRegistrationsLock.UpgradeToWriterLock(-1);
      try
      {
        GlobalKernelRegistration.Registration newRegistration1;
        if (GlobalKernelRegistration.kernelRegistrations.TryGetValue(type, out newRegistration1))
          return newRegistration1;
        GlobalKernelRegistration.Registration newRegistration2 = new GlobalKernelRegistration.Registration();
        GlobalKernelRegistration.kernelRegistrations.Add(type, newRegistration2);
        return newRegistration2;
      }
      finally
      {
        GlobalKernelRegistration.kernelRegistrationsLock.DowngradeFromWriterLock(ref writerLock);
      }
    }

    private class Registration
    {
      public Registration()
      {
        this.KernelLock = new ReaderWriterLock();
        this.Kernels = (IList<WeakReference>) new List<WeakReference>();
      }

      public ReaderWriterLock KernelLock { get; private set; }

      public IList<WeakReference> Kernels { get; private set; }
    }
  }
}
