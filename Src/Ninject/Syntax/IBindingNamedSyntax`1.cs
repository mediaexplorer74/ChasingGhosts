// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingNamedSyntax`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>Used to define the name of a binding.</summary>
  /// <typeparam name="T">The service being bound.</typeparam>
  public interface IBindingNamedSyntax<T> : 
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Indicates that the binding should be registered with the specified name. Names are not
    /// necessarily unique; multiple bindings for a given service may be registered with the same name.
    /// </summary>
    /// <param name="name">The name to give the binding.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> Named(string name);
  }
}
