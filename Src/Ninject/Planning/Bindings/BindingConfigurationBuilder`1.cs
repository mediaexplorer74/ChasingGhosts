// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.BindingConfigurationBuilder`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using Ninject.Parameters;
using Ninject.Planning.Targets;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>
  /// Provides a root for the fluent syntax associated with an <see cref="P:Ninject.Planning.Bindings.BindingConfigurationBuilder`1.BindingConfiguration" />.
  /// </summary>
  /// <typeparam name="T">The implementation type of the built binding.</typeparam>
  public class BindingConfigurationBuilder<T> : 
    IBindingConfigurationSyntax<T>,
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
    /// <summary>The names of the services added to the exceptions.</summary>
    private readonly string serviceNames;

    /// <summary>Gets the binding being built.</summary>
    public IBindingConfiguration BindingConfiguration { get; private set; }

    /// <summary>Gets the kernel.</summary>
    public IKernel Kernel { get; private set; }

    /// <summary>
    /// Initializes a new instance of the BindingBuilder&lt;T&gt; class.
    /// </summary>
    /// <param name="bindingConfiguration">The binding configuration to build.</param>
    /// <param name="serviceNames">The names of the configured services.</param>
    /// <param name="kernel">The kernel.</param>
    public BindingConfigurationBuilder(
      IBindingConfiguration bindingConfiguration,
      string serviceNames,
      IKernel kernel)
    {
      Ensure.ArgumentNotNull((object) bindingConfiguration, nameof (bindingConfiguration));
      Ensure.ArgumentNotNull((object) kernel, nameof (kernel));
      this.BindingConfiguration = bindingConfiguration;
      this.Kernel = kernel;
      this.serviceNames = serviceNames;
    }

    /// <summary>
    /// Indicates that the binding should be used only for requests that support the specified condition.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> When(Func<IRequest, bool> condition)
    {
      this.BindingConfiguration.Condition = condition;
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only for injections on the specified type.
    /// Types that derive from the specified type are considered as valid targets.
    /// </summary>
    /// <typeparam name="TParent">The type.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto<TParent>()
    {
      return this.WhenInjectedInto(typeof (TParent));
    }

    /// <summary>
    /// Indicates that the binding should be used only for injections on the specified type.
    /// Types that derive from the specified type are considered as valid targets.
    /// </summary>
    /// <param name="parent">The type.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto(Type parent)
    {
      this.BindingConfiguration.Condition = !parent.IsGenericTypeDefinition ? (Func<IRequest, bool>) (r => r.Target != null && parent.IsAssignableFrom(r.Target.Member.ReflectedType)) : (!parent.IsInterface ? (Func<IRequest, bool>) (r => r.Target != null && r.Target.Member.ReflectedType.GetAllBaseTypes().Any<Type>((Func<Type, bool>) (i => i.IsGenericType && i.GetGenericTypeDefinition() == parent))) : (Func<IRequest, bool>) (r => r.Target != null && ((IEnumerable<Type>) r.Target.Member.ReflectedType.GetInterfaces()).Any<Type>((Func<Type, bool>) (i => i.IsGenericType && i.GetGenericTypeDefinition() == parent))));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only for injections on the specified type.
    /// Types that derive from the specified type are considered as valid targets.
    /// </summary>
    /// <param name="parents">The type.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto(params Type[] parents)
    {
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r =>
      {
        foreach (Type parent1 in parents)
        {
          Type parent = parent1;
          if (!parent.IsGenericTypeDefinition ? r.Target != null && parent.IsAssignableFrom(r.Target.Member.ReflectedType) : (!parent.IsInterface ? r.Target != null && r.Target.Member.ReflectedType.GetAllBaseTypes().Any<Type>((Func<Type, bool>) (i => i.IsGenericType && i.GetGenericTypeDefinition() == parent)) : r.Target != null && ((IEnumerable<Type>) r.Target.Member.ReflectedType.GetInterfaces()).Any<Type>((Func<Type, bool>) (i => i.IsGenericType && i.GetGenericTypeDefinition() == parent))))
            return true;
        }
        return false;
      });
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only for injections on the specified type.
    /// The type must match exactly the specified type. Types that derive from the specified type
    /// will not be considered as valid target.
    /// </summary>
    /// <typeparam name="TParent">The type.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenInjectedExactlyInto<TParent>()
    {
      return this.WhenInjectedExactlyInto(typeof (TParent));
    }

    /// <summary>
    /// Indicates that the binding should be used only for injections on the specified type.
    /// The type must match exactly the specified type. Types that derive from the specified type
    /// will not be considered as valid target.
    /// </summary>
    /// <param name="parent">The type.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenInjectedExactlyInto(Type parent)
    {
      this.BindingConfiguration.Condition = !parent.IsGenericTypeDefinition ? (Func<IRequest, bool>) (r => r.Target != null && r.Target.Member.ReflectedType == parent) : (Func<IRequest, bool>) (r => r.Target != null && r.Target.Member.ReflectedType.IsGenericType && parent == r.Target.Member.ReflectedType.GetGenericTypeDefinition());
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only for injections on the specified type.
    /// The type must match exactly the specified type. Types that derive from the specified type
    /// will not be considered as valid target.
    /// Should match at least one of the specified targets
    /// </summary>
    /// <param name="parents">The types.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenInjectedExactlyInto(params Type[] parents)
    {
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r =>
      {
        foreach (Type parent in parents)
        {
          if (!parent.IsGenericTypeDefinition ? r.Target != null && r.Target.Member.ReflectedType == parent : r.Target != null && r.Target.Member.ReflectedType.IsGenericType && parent == r.Target.Member.ReflectedType.GetGenericTypeDefinition())
            return true;
        }
        return false;
      });
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only when the class being injected has
    /// an attribute of the specified type.
    /// </summary>
    /// <typeparam name="TAttribute">The type of attribute.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenClassHas<TAttribute>() where TAttribute : Attribute
    {
      return this.WhenClassHas(typeof (TAttribute));
    }

    /// <summary>
    /// Indicates that the binding should be used only when the member being injected has
    /// an attribute of the specified type.
    /// </summary>
    /// <typeparam name="TAttribute">The type of attribute.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenMemberHas<TAttribute>() where TAttribute : Attribute
    {
      return this.WhenMemberHas(typeof (TAttribute));
    }

    /// <summary>
    /// Indicates that the binding should be used only when the target being injected has
    /// an attribute of the specified type.
    /// </summary>
    /// <typeparam name="TAttribute">The type of attribute.</typeparam>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenTargetHas<TAttribute>() where TAttribute : Attribute
    {
      return this.WhenTargetHas(typeof (TAttribute));
    }

    /// <summary>
    /// Indicates that the binding should be used only when the class being injected has
    /// an attribute of the specified type.
    /// </summary>
    /// <param name="attributeType">The type of attribute.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenClassHas(Type attributeType)
    {
      if (!typeof (Attribute).IsAssignableFrom(attributeType))
        throw new InvalidOperationException(ExceptionFormatter.InvalidAttributeTypeUsedInBindingCondition(this.serviceNames, nameof (WhenClassHas), attributeType));
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r => r.Target != null && r.Target.IsDefinedOnParent(attributeType, r.Target.Member.ReflectedType));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only when the member being injected has
    /// an attribute of the specified type.
    /// </summary>
    /// <param name="attributeType">The type of attribute.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenMemberHas(Type attributeType)
    {
      if (!typeof (Attribute).IsAssignableFrom(attributeType))
        throw new InvalidOperationException(ExceptionFormatter.InvalidAttributeTypeUsedInBindingCondition(this.serviceNames, nameof (WhenMemberHas), attributeType));
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r => r.Target != null && r.Target.IsDefined(attributeType, true));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only when the target being injected has
    /// an attribute of the specified type.
    /// </summary>
    /// <param name="attributeType">The type of attribute.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenTargetHas(Type attributeType)
    {
      if (!typeof (Attribute).IsAssignableFrom(attributeType))
        throw new InvalidOperationException(ExceptionFormatter.InvalidAttributeTypeUsedInBindingCondition(this.serviceNames, nameof (WhenTargetHas), attributeType));
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r => r.Target != null && r.Target.IsDefined(attributeType, true));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only when the service is being requested
    /// by a service bound with the specified name.
    /// </summary>
    /// <param name="name">The name to expect.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenParentNamed(string name)
    {
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r => r.ParentContext != null && string.Equals(r.ParentContext.Binding.Metadata.Name, name, StringComparison.Ordinal));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only when any ancestor is bound with the specified name.
    /// </summary>
    /// <param name="name">The name to expect.</param>
    /// <returns>The fluent syntax.</returns>
    [Obsolete("Use WhenAnyAncestorNamed(string name)")]
    public IBindingInNamedWithOrOnSyntax<T> WhenAnyAnchestorNamed(string name)
    {
      return this.WhenAnyAncestorNamed(name);
    }

    /// <summary>
    /// Indicates that the binding should be used only when any ancestor is bound with the specified name.
    /// </summary>
    /// <param name="name">The name to expect.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenAnyAncestorNamed(string name)
    {
      return this.WhenAnyAncestorMatches((Predicate<IContext>) (ctx => ctx.Binding.Metadata.Name == name));
    }

    /// <summary>
    /// Indicates that the binding should be used only when no ancestor is bound with the specified name.
    /// </summary>
    /// <param name="name">The name to expect.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenNoAncestorNamed(string name)
    {
      return this.WhenNoAncestorMatches((Predicate<IContext>) (ctx => ctx.Binding.Metadata.Name == name));
    }

    /// <summary>
    /// Indicates that the binding should be used only when any ancestor matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenAnyAncestorMatches(Predicate<IContext> predicate)
    {
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r => BindingConfigurationBuilder<T>.DoesAnyAncestorMatch(r, predicate));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be used only when no ancestor matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingInNamedWithOrOnSyntax<T> WhenNoAncestorMatches(Predicate<IContext> predicate)
    {
      this.BindingConfiguration.Condition = (Func<IRequest, bool>) (r => !BindingConfigurationBuilder<T>.DoesAnyAncestorMatch(r, predicate));
      return (IBindingInNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the binding should be registered with the specified name. Names are not
    /// necessarily unique; multiple bindings for a given service may be registered with the same name.
    /// </summary>
    /// <param name="name">The name to give the binding.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> Named(string name)
    {
      this.BindingConfiguration.Metadata.Name = name;
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that only a single instance of the binding should be created, and then
    /// should be re-used for all subsequent requests.
    /// </summary>
    /// <returns>The fluent syntax.</returns>
    public IBindingNamedWithOrOnSyntax<T> InSingletonScope()
    {
      this.BindingConfiguration.ScopeCallback = StandardScopeCallbacks.Singleton;
      return (IBindingNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that instances activated via the binding should not be re-used, nor have
    /// their lifecycle managed by Ninject.
    /// </summary>
    /// <returns>The fluent syntax.</returns>
    public IBindingNamedWithOrOnSyntax<T> InTransientScope()
    {
      this.BindingConfiguration.ScopeCallback = StandardScopeCallbacks.Transient;
      return (IBindingNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that instances activated via the binding should be re-used within the same thread.
    /// </summary>
    /// <returns>The fluent syntax.</returns>
    public IBindingNamedWithOrOnSyntax<T> InThreadScope()
    {
      this.BindingConfiguration.ScopeCallback = StandardScopeCallbacks.Thread;
      return (IBindingNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that instances activated via the binding should be re-used as long as the object
    /// returned by the provided callback remains alive (that is, has not been garbage collected).
    /// </summary>
    /// <param name="scope">The callback that returns the scope.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, object> scope)
    {
      this.BindingConfiguration.ScopeCallback = scope;
      return (IBindingNamedWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="value">The value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, object value)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new ConstructorArgument(name, value));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument(
      string name,
      Func<IContext, object> callback)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new ConstructorArgument(name, callback));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument(
      string name,
      Func<IContext, ITarget, object> callback)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new ConstructorArgument(name, callback));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <typeparam name="TValue">Specifies the argument type to override.</typeparam>
    /// <param name="value">The value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument<TValue>(TValue value)
    {
      return this.WithConstructorArgument(typeof (TValue), (Func<IContext, ITarget, object>) ((context, target) => (object) (TValue) value));
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="type">The type of the argument to override.</param>
    /// <param name="value">The value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument(Type type, object value)
    {
      return this.WithConstructorArgument(type, (Func<IContext, ITarget, object>) ((context, target) => value));
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="type">The type of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument(
      Type type,
      Func<IContext, object> callback)
    {
      return this.WithConstructorArgument(type, (Func<IContext, ITarget, object>) ((context, target) => callback(context)));
    }

    /// <summary>
    /// Indicates that the specified constructor argument should be overridden with the specified value.
    /// </summary>
    /// <param name="type">The type of the argument to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the argument.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithConstructorArgument(
      Type type,
      Func<IContext, ITarget, object> callback)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new TypeMatchingConstructorArgument(type, callback));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified property should be injected with the specified value.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="value">The value for the property.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithPropertyValue(string name, object value)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new PropertyValue(name, value));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified property should be injected with the specified value.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the property.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithPropertyValue(string name, Func<IContext, object> callback)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new PropertyValue(name, callback));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified property should be injected with the specified value.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="callback">The callback to invoke to get the value for the property.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithPropertyValue(
      string name,
      Func<IContext, ITarget, object> callback)
    {
      this.BindingConfiguration.Parameters.Add((IParameter) new PropertyValue(name, callback));
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>Adds a custom parameter to the binding.</summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithParameter(IParameter parameter)
    {
      this.BindingConfiguration.Parameters.Add(parameter);
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>Sets the value of a piece of metadata on the binding.</summary>
    /// <param name="key">The metadata key.</param>
    /// <param name="value">The metadata value.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingWithOrOnSyntax<T> WithMetadata(string key, object value)
    {
      this.BindingConfiguration.Metadata.Set(key, value);
      return (IBindingWithOrOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnActivation(Action<T> action) => this.OnActivation<T>(action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnActivation<TImplementation>(Action<TImplementation> action)
    {
      this.BindingConfiguration.ActivationActions.Add((Action<IContext, object>) ((context, instance) => action((TImplementation) instance)));
      return (IBindingOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnActivation(Action<IContext, T> action)
    {
      return this.OnActivation<T>(action);
    }

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are activated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnActivation<TImplementation>(
      Action<IContext, TImplementation> action)
    {
      this.BindingConfiguration.ActivationActions.Add((Action<IContext, object>) ((context, instance) => action(context, (TImplementation) instance)));
      return (IBindingOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnDeactivation(Action<T> action) => this.OnDeactivation<T>(action);

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnDeactivation<TImplementation>(Action<TImplementation> action)
    {
      this.BindingConfiguration.DeactivationActions.Add((Action<IContext, object>) ((context, instance) => action((TImplementation) instance)));
      return (IBindingOnSyntax<T>) this;
    }

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnDeactivation(Action<IContext, T> action)
    {
      return this.OnDeactivation<T>(action);
    }

    /// <summary>
    /// Indicates that the specified callback should be invoked when instances are deactivated.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="action">The action callback.</param>
    /// <returns>The fluent syntax.</returns>
    public IBindingOnSyntax<T> OnDeactivation<TImplementation>(
      Action<IContext, TImplementation> action)
    {
      this.BindingConfiguration.DeactivationActions.Add((Action<IContext, object>) ((context, instance) => action(context, (TImplementation) instance)));
      return (IBindingOnSyntax<T>) this;
    }

    private static bool DoesAnyAncestorMatch(IRequest request, Predicate<IContext> predicate)
    {
      IContext parentContext = request.ParentContext;
      if (parentContext == null)
        return false;
      return predicate(parentContext) || BindingConfigurationBuilder<T>.DoesAnyAncestorMatch(parentContext.Request, predicate);
    }

    Type IFluentSyntax.GetType() => this.GetType();
  }
}
