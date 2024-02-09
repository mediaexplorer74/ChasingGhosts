// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.BindingBuilder
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Activation.Providers;
using Ninject.Infrastructure;
using Ninject.Parameters;
using Ninject.Syntax;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>
  /// Provides a root for the fluent syntax associated with an <see cref="P:Ninject.Planning.Bindings.BindingBuilder.BindingConfiguration" />.
  /// </summary>
  public class BindingBuilder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.BindingBuilder" /> class.
    /// </summary>
    /// <param name="bindingConfiguration">The binding to build.</param>
    /// <param name="kernel">The kernel.</param>
    /// <param name="serviceNames">The names of the services.</param>
    public BindingBuilder(
      IBindingConfiguration bindingConfiguration,
      IKernel kernel,
      string serviceNames)
    {
      Ensure.ArgumentNotNull((object) bindingConfiguration, "binding");
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.BindingConfiguration = bindingConfiguration;
      this.Kernel = kernel;
      this.ServiceNames = serviceNames;
      this.BindingConfiguration.ScopeCallback = kernel.Settings.DefaultScopeCallback;
    }

    /// <summary>Gets the binding being built.</summary>
    public IBindingConfiguration BindingConfiguration { get; private set; }

    /// <summary>Gets the kernel.</summary>
    public IKernel Kernel { get; private set; }

    /// <summary>Gets the names of the services.</summary>
    /// <value>The names of the services.</value>
    protected string ServiceNames { get; private set; }

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <typeparam name="TImplementation">The implementation type.</typeparam>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<TImplementation> InternalTo<TImplementation>()
    {
      return this.InternalTo<TImplementation>(typeof (TImplementation));
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified implementation type.
    /// </summary>
    /// <typeparam name="T">The type of the returned syntax.</typeparam>
    /// <param name="implementation">The implementation type.</param>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<T> InternalTo<T>(Type implementation)
    {
      this.BindingConfiguration.ProviderCallback = StandardProvider.GetCreationCallback(implementation);
      this.BindingConfiguration.Target = BindingTarget.Type;
      return (IBindingWhenInNamedWithOrOnSyntax<T>) new BindingConfigurationBuilder<T>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified constant value.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="value">The constant value.</param>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<TImplementation> InternalToConfiguration<TImplementation>(
      TImplementation value)
    {
      this.BindingConfiguration.ProviderCallback = (Func<IContext, IProvider>) (ctx => (IProvider) new ConstantProvider<TImplementation>(value));
      this.BindingConfiguration.Target = BindingTarget.Constant;
      this.BindingConfiguration.ScopeCallback = StandardScopeCallbacks.Singleton;
      return (IBindingWhenInNamedWithOrOnSyntax<TImplementation>) new BindingConfigurationBuilder<TImplementation>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified callback method.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<TImplementation> InternalToMethod<TImplementation>(
      Func<IContext, TImplementation> method)
    {
      this.BindingConfiguration.ProviderCallback = (Func<IContext, IProvider>) (ctx => (IProvider) new CallbackProvider<TImplementation>(method));
      this.BindingConfiguration.Target = BindingTarget.Method;
      return (IBindingWhenInNamedWithOrOnSyntax<TImplementation>) new BindingConfigurationBuilder<TImplementation>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified provider.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="provider">The provider.</param>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<TImplementation> InternalToProvider<TImplementation>(
      IProvider<TImplementation> provider)
    {
      this.BindingConfiguration.ProviderCallback = (Func<IContext, IProvider>) (ctx => (IProvider) provider);
      this.BindingConfiguration.Target = BindingTarget.Provider;
      return (IBindingWhenInNamedWithOrOnSyntax<TImplementation>) new BindingConfigurationBuilder<TImplementation>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="TProvider">The type of provider to activate.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToProviderInternal<TProvider, TImplementation>() where TProvider : IProvider
    {
      this.BindingConfiguration.ProviderCallback = (Func<IContext, IProvider>) (ctx => (IProvider) ctx.Kernel.Get<TProvider>());
      this.BindingConfiguration.Target = BindingTarget.Provider;
      return (IBindingWhenInNamedWithOrOnSyntax<TImplementation>) new BindingConfigurationBuilder<TImplementation>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to an instance of the specified provider type.
    /// The instance will be activated via the kernel when an instance of the service is activated.
    /// </summary>
    /// <typeparam name="T">The type of the returned fluent syntax</typeparam>
    /// <param name="providerType">The type of provider to activate.</param>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<T> ToProviderInternal<T>(Type providerType)
    {
      this.BindingConfiguration.ProviderCallback = (Func<IContext, IProvider>) (ctx => ctx.Kernel.Get(providerType) as IProvider);
      this.BindingConfiguration.Target = BindingTarget.Provider;
      return (IBindingWhenInNamedWithOrOnSyntax<T>) new BindingConfigurationBuilder<T>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Indicates that the service should be bound to the specified constructor.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="newExpression">The expression that specifies the constructor.</param>
    /// <returns>The fluent syntax.</returns>
    protected IBindingWhenInNamedWithOrOnSyntax<TImplementation> InternalToConstructor<TImplementation>(
      Expression<Func<IConstructorArgumentSyntax, TImplementation>> newExpression)
    {
      if (!(newExpression.Body is NewExpression body))
        throw new ArgumentException("The expression must be a constructor call.", nameof (newExpression));
      this.BindingConfiguration.ProviderCallback = StandardProvider.GetCreationCallback(body.Type, body.Constructor);
      this.BindingConfiguration.Target = BindingTarget.Type;
      this.AddConstructorArguments(body, newExpression.Parameters[0]);
      return (IBindingWhenInNamedWithOrOnSyntax<TImplementation>) new BindingConfigurationBuilder<TImplementation>(this.BindingConfiguration, this.ServiceNames, this.Kernel);
    }

    /// <summary>
    /// Adds the constructor arguments for the specified constructor expression.
    /// </summary>
    /// <param name="ctorExpression">The ctor expression.</param>
    /// <param name="constructorArgumentSyntaxParameterExpression">The constructor argument syntax parameter expression.</param>
    private void AddConstructorArguments(
      NewExpression ctorExpression,
      ParameterExpression constructorArgumentSyntaxParameterExpression)
    {
      ParameterInfo[] parameters = ctorExpression.Constructor.GetParameters();
      for (int index = 0; index < ctorExpression.Arguments.Count; ++index)
        this.AddConstructorArgument(ctorExpression.Arguments[index], parameters[index].Name, constructorArgumentSyntaxParameterExpression);
    }

    /// <summary>
    /// Adds a constructor argument for the specified argument expression.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="constructorArgumentSyntaxParameterExpression">The constructor argument syntax parameter expression.</param>
    private void AddConstructorArgument(
      Expression argument,
      string argumentName,
      ParameterExpression constructorArgumentSyntaxParameterExpression)
    {
      if (argument is MethodCallExpression methodCallExpression && methodCallExpression.Method.IsGenericMethod && !(methodCallExpression.Method.GetGenericMethodDefinition().DeclaringType != typeof (IConstructorArgumentSyntax)))
        return;
      Delegate compiledExpression = Expression.Lambda(argument, constructorArgumentSyntaxParameterExpression).Compile();
      this.BindingConfiguration.Parameters.Add((IParameter) new ConstructorArgument(argumentName, (Func<IContext, object>) (ctx => compiledExpression.DynamicInvoke((object) new BindingBuilder.ConstructorArgumentSyntax(ctx)))));
    }

    /// <summary>
    /// Passed to ToConstructor to specify that a constructor value is Injected.
    /// </summary>
    private class ConstructorArgumentSyntax : IConstructorArgumentSyntax, IFluentSyntax
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Ninject.Planning.Bindings.BindingBuilder.ConstructorArgumentSyntax" /> class.
      /// </summary>
      /// <param name="context">The context.</param>
      public ConstructorArgumentSyntax(IContext context) => this.Context = context;

      /// <summary>Gets the context.</summary>
      /// <value>The context.</value>
      public IContext Context { get; private set; }

      /// <summary>Specifies that the argument is injected.</summary>
      /// <typeparam name="T1">The type of the parameter</typeparam>
      /// <returns>Not used. This interface has no implementation.</returns>
      public T1 Inject<T1>()
      {
        throw new InvalidOperationException("This method is for declaration that a parameter shall be injected only! Never call it directly.");
      }

      Type IFluentSyntax.GetType() => this.GetType();
    }
  }
}
