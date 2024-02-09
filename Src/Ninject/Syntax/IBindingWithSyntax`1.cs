// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingWithSyntax`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Parameters;
using Ninject.Planning.Targets;
using System;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>Used to add additional information to a binding.</summary>
  /// <typeparam name="T">The service being bound.</typeparam>
  public interface IBindingWithSyntax<T> : 
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="value">The value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, object value);

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, Func<IContext, object> callback);

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument(
      string name,
      Func<IContext, ITarget, object> callback);

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <typeparam name="TValue">Specifies the argument type to override.</typeparam>
    /// <param name="value">The value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument<TValue>(TValue value);

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="type">The type of the argument to override.</param>
    /// <param name="value">The value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument(Type type, object value);

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="type">The type of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument(Type type, Func<IContext, object> callback);

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="type">The type of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithConstructorArgument(
      Type type,
      Func<IContext, ITarget, object> callback);

    /// <summary>
    /// Indicates that the specified property should be injected with the specified value.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="value">The value for the property.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithPropertyValue(string name, object value);

    /// <summary>
    /// Indicates that the specified property should be injected with the specified value.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the property.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithPropertyValue(string name, Func<IContext, object> callback);

    /// <summary>
    /// Indicates that the specified property should be injected with the specified value.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the property.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithPropertyValue(
      string name,
      Func<IContext, ITarget, object> callback);

    /// <summary>Adds a custom parameter to the binding.</summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithParameter(IParameter parameter);

    /// <summary>Sets the value of a piece of metadata on the binding.</summary>
    /// <param name="key">The metadata key.</param>
    /// <param name="value">The metadata value.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWithOrOnSyntax<T> WithMetadata(string key, object value);
  }
}
