// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.IBindingConfigurationSyntax`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Syntax;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>The syntax to define bindings.</summary>
  /// <typeparam name="T">The type of the service.</typeparam>
  public interface IBindingConfigurationSyntax<T> : 
    IBindingWhenInNamedWithOrOnSyntax<T>,
    IBindingWhenSyntax<T>,
    IBindingInNamedWithOrOnSyntax<T>,
    IBindingInSyntax<T>,
    IBindingNamedWithOrOnSyntax<T>,
    IBindingNamedSyntax<T>,
    IBindingWithOrOnSyntax<T>,
    IBindingWithSyntax<T>,
    IBindingOnSyntax<T>,
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
  }
}
