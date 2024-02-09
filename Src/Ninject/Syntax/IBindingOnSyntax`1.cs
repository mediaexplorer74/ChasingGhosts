// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingOnSyntax`1
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
  /// Used to add additional actions to be performed during activation or deactivation of instances via a binding.
  /// </summary>
  /// <typeparam name="T">The service being bound.</typeparam>
  public interface IBindingOnSyntax<T> : 
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnActivation(Action<T> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnActivation<TImplementation>(Action<TImplementation> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnActivation(Action<IContext, T> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnActivation<TImplementation>(Action<IContext, TImplementation> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnDeactivation(Action<T> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnDeactivation<TImplementation>(Action<TImplementation> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnDeactivation(Action<IContext, T> action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingOnSyntax<T> OnDeactivation<TImplementation>(Action<IContext, TImplementation> action);
  }
}
