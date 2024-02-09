// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingWithOrOnSyntax`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>
  /// Used to add additional information or actions to a binding.
  /// </summary>
  /// <typeparam name="T">The service being bound.</typeparam>
  public interface IBindingWithOrOnSyntax<T> : 
    IBindingWithSyntax<T>,
    IBindingOnSyntax<T>,
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
  }
}
