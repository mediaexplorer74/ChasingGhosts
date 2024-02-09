// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.BindingBuilder`2
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Syntax;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>
  /// Provides a root for the fluent syntax associated with an <see cref="P:Ninject.Planning.Bindings.BindingBuilder.BindingConfiguration" />.
  /// </summary>
  /// <typeparam name="T1">The first service type.</typeparam>
  /// <typeparam name="T2">The second service type.</typeparam>
  public class BindingBuilder<T1, T2> : 
    BindingBuilder,
    IBindingToSyntax<T1, T2>,
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.BindingBuilder`2" /> class.
    /// </summary>
    /// <param name="bindingConfigurationConfiguration">The binding to build.</param>
    /// <param name="kernel">The kernel.</param>
    /// <param name="serviceNames">The names of the services.</param>
    public BindingBuilder(
      IBindingConfiguration bindingConfigurationConfiguration,
      IKernel kernel,
      string serviceNames)
      : base(bindingConfigurationConfiguration, kernel, serviceNames)
    {
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T1, T2
    {
      return this.InternalTo<TImplementation>();
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <param name="implementation">The implementation type.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<object> To(Type implementation)
    {
      return this.InternalTo<object>(implementation);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified constructor.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="newExpression">The expression that specifies the constructor.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstructor<TImplementation>(
      Expression<Func<IConstructorArgumentSyntax, TImplementation>> newExpression)
      where TImplementation : T1, T2
    {
      return this.InternalToConstructor<TImplementation>(newExpression);
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="TProvider">The type of provider to activate.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<object> ToProvider<TProvider>() where TProvider : IProvider
    {
      return this.ToProviderInternal<TProvider, object>();
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="TProvider">The type of provider to activate.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToProvider<TProvider, TImplementation>()
      where TProvider : IProvider<TImplementation>
      where TImplementation : T1, T2
    {
      return this.ToProviderInternal<TProvider, TImplementation>();
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <param name="providerType">The type of provider to activate.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<object> ToProvider(Type providerType)
    {
      return this.ToProviderInternal<object>(providerType);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified provider.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="provider">The provider.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToProvider<TImplementation>(
      IProvider<TImplementation> provider)
      where TImplementation : T1, T2
    {
      return this.InternalToProvider<TImplementation>(provider);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified callback method.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToMethod<TImplementation>(
      Func<IContext, TImplementation> method)
      where TImplementation : T1, T2
    {
      return this.InternalToMethod<TImplementation>(method);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified constant value.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="value">The constant value.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstant<TImplementation>(
      TImplementation value)
      where TImplementation : T1, T2
    {
      return this.InternalToConfiguration<TImplementation>(value);
    }

    Type IFluentSyntax.GetType() => this.GetType();
  }
}
