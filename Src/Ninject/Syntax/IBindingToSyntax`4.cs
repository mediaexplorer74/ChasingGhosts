﻿// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingToSyntax`4
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>Used to define the target of a binding.</summary>
  /// <typeparam name="T1">The first service type to be bound.</typeparam>
  /// <typeparam name="T2">The second service type to be bound.</typeparam>
  /// <typeparam name="T3">The third service type to be bound.</typeparam>
  /// <typeparam name="T4">The fourth service type to be bound.</typeparam>
  public interface IBindingToSyntax<T1, T2, T3, T4> : 
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T1, T2, T3, T4;

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <param name="implementation">The implementation type.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<object> To(Type implementation);

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="TProvider">The type of provider to activate.</typeparam>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<object> ToProvider<TProvider>() where TProvider : IProvider;

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="TProvider">The type of provider to activate.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToProvider<TProvider, TImplementation>()
      where TProvider : IProvider<TImplementation>
      where TImplementation : T1, T2, T3, T4;

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <param name="providerType">The type of provider to activate.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<object> ToProvider(Type providerType);

    /// <summary>
    /// Indicates that the service should be bound to the specified provider.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="provider">The provider.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToProvider<TImplementation>(
      IProvider<TImplementation> provider)
      where TImplementation : T1, T2, T3, T4;

    /// <summary>
    /// Indicates that the service should be bound to the specified callback method.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToMethod<TImplementation>(
      Func<IContext, TImplementation> method)
      where TImplementation : T1, T2, T3;

    /// <summary>
    /// Indicates that the service should be bound to the specified constant value.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="value">The constant value.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstant<TImplementation>(
      TImplementation value)
      where TImplementation : T1, T2, T3, T4;

    /// <summary>
    /// Indicates that the service should be bound to the specified constructor.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="newExpression">The expression that specifies the constructor.</param>
    /// <returns>The fluent syntax.</returns>
    IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstructor<TImplementation>(
      Expression<Func<IConstructorArgumentSyntax, TImplementation>> newExpression)
      where TImplementation : T1, T2, T3, T4;
  }
}
