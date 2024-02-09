// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.BindingBuilder`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Activation.Providers;
using Ninject.Infrastructure;
using Ninject.Syntax;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>
  /// Provides a root for the fluent syntax associated with an <see cref="P:Ninject.Planning.Bindings.BindingBuilder`1.Binding" />.
  /// </summary>
  /// <typeparam name="T1">The service type.</typeparam>
  public class BindingBuilder<T1> : 
    BindingBuilder,
    IBindingToSyntax<T1>,
    IBindingSyntax,
    IHaveBindingConfiguration,
    IHaveKernel,
    IFluentSyntax
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.BindingBuilder`1" /> class.
    /// </summary>
    /// <param name="binding">The binding to build.</param>
    /// <param name="kernel">The kernel.</param>
    /// <param name="serviceNames">The names of the services.</param>
    public BindingBuilder(IBinding binding, IKernel kernel, string serviceNames)
      : base(binding.BindingConfiguration, kernel, serviceNames)
    {
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.Binding = binding;
    }

    /// <summary>Gets the binding being built.</summary>
    public IBinding Binding { get; private set; }

    /// <summary>Indicates that the service should be self-bound.</summary>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<T1> ToSelf()
    {
      this.Binding.ProviderCallback = StandardProvider.GetCreationCallback(this.Binding.Service);
      this.Binding.Target = BindingTarget.Self;
      return (IBindingWhenInNamedWithOrOnSyntax<T1>) new BindingConfigurationBuilder<T1>(this.Binding.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T1
    {
      return this.InternalTo<TImplementation>();
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <param name="implementation">The implementation type.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<T1> To(Type implementation)
    {
      return this.InternalTo<T1>(implementation);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified constructor.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="newExpression">The expression that specifies the constructor.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstructor<TImplementation>(
      Expression<Func<IConstructorArgumentSyntax, TImplementation>> newExpression)
      where TImplementation : T1
    {
      return this.InternalToConstructor<TImplementation>(newExpression);
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="TProvider">The type of provider to activate.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<T1> ToProvider<TProvider>() where TProvider : IProvider
    {
      return this.ToProviderInternal<TProvider, T1>();
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <param name="providerType">The type of provider to activate.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<T1> ToProvider(Type providerType)
    {
      return this.ToProviderInternal<T1>(providerType);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified provider.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="provider">The provider.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToProvider<TImplementation>(
      IProvider<TImplementation> provider)
      where TImplementation : T1
    {
      return this.InternalToProvider<TImplementation>(provider);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified callback method.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<T1> ToMethod(Func<IContext, T1> method)
    {
      return this.InternalToMethod<T1>(method);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified callback method.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToMethod<TImplementation>(
      Func<IContext, TImplementation> method)
      where TImplementation : T1
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
      where TImplementation : T1
    {
      return this.InternalToConfiguration<TImplementation>(value);
    }

    Type IFluentSyntax.GetType() => this.GetType();
  }
}
