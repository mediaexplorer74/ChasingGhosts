// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.IHaveKernel
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject.Infrastructure
{
  /// <summary>
  /// Indicates that the object has a reference to an <see cref="T:Ninject.IKernel" />.
  /// </summary>
  public interface IHaveKernel
  {
    /// <summary>Gets the kernel.</summary>
    IKernel Kernel { get; }
  }
}
