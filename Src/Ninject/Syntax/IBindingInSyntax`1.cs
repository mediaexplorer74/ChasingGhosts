// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingInSyntax`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using System;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>
  /// Used to define the scope in which instances activated via a binding should be re-used.
  /// </summary>
  /// <typeparam name="T">The service being bound.</typeparam>
  public interface IBindingInSyntax<T> : 
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Indicates that only a single instance of the binding should be created, and then
    /// should be re-used for all subsequent requests.
    /// </summary>
    /// <returns>The fluent syntax.</returns>
    IBindingNamedWithOrOnSyntax<T> InSingletonScope();

    /// <summary>
    /// Indicates that instances activated via the binding should not be re-used, nor have
    /// their lifecycle managed by Ninject.
    /// </summary>
    /// <returns>The fluent syntax.</returns>
    IBindingNamedWithOrOnSyntax<T> InTransientScope();

    /// <summary>
    /// Indicates that instances activated via the binding should be re-used within the same thread.
    /// </summary>
    /// <returns>The fluent syntax.</returns>
    IBindingNamedWithOrOnSyntax<T> InThreadScope();

    /// <summary>
    /// Indicates that instances activated via the binding should be re-used as long as the object
    /// returned by the provided callback remains alive (that is, has not been garbage collected).
    /// </summary>
    /// <param name="scope">The callback that returns the scope.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, object> scope);
  }
}
