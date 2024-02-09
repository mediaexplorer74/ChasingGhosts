// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Providers.StandardProvider
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using Ninject.Selection;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Activation.Providers
{
  /// <summary>
  /// The standard provider for types, which activates instances via a <see cref="T:Ninject.Activation.IPipeline" />.
  /// </summary>
  public class StandardProvider : IProvider
  {
    /// <summary>
    /// Gets the type (or prototype) of instances the provider creates.
    /// </summary>
    public Type Type { get; private set; }

    /// <summary>Gets or sets the planner component.</summary>
    public IPlanner Planner { get; private set; }

    /// <summary>Gets or sets the selector component.</summary>
    public IConstructorScorer ConstructorScorer { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Providers.StandardProvider" /> class.
    /// </summary>
    /// <param name="type">The type (or prototype) of instances the provider creates.</param>
    /// <param name="planner">The planner component.</param>
    /// <param name="constructorScorer">The constructor scorer component.</param>
    public StandardProvider(Type type, IPlanner planner, IConstructorScorer constructorScorer)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      Ensure.ArgumentNotNull((object) planner, nameof (planner));
      Ensure.ArgumentNotNull((object) constructorScorer, nameof (constructorScorer));
      this.Type = type;
      this.Planner = planner;
      this.ConstructorScorer = constructorScorer;
    }

    /// <summary>Creates an instance within the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The created instance.</returns>
    public virtual object Create(IContext context)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      if (context.Plan == null)
        context.Plan = this.Planner.GetPlan(this.GetImplementationType(context.Request.Service));
      IList<ConstructorInjectionDirective> injectionDirectives = context.Plan.ConstructorInjectionDirectives;
      ConstructorInjectionDirective injectionDirective;
      if (injectionDirectives.Count == 1)
      {
        injectionDirective = injectionDirectives[0];
      }
      else
      {
        IGrouping<int, ConstructorInjectionDirective> grouping = injectionDirectives.GroupBy<ConstructorInjectionDirective, int>((Func<ConstructorInjectionDirective, int>) (option => this.ConstructorScorer.Score(context, option))).OrderByDescending<IGrouping<int, ConstructorInjectionDirective>, int>((Func<IGrouping<int, ConstructorInjectionDirective>, int>) (g => g.Key)).FirstOrDefault<IGrouping<int, ConstructorInjectionDirective>>();
        if (grouping == null)
          throw new ActivationException(ExceptionFormatter.NoConstructorsAvailable(context));
        injectionDirective = !grouping.Skip<ConstructorInjectionDirective>(1).Any<ConstructorInjectionDirective>() ? grouping.First<ConstructorInjectionDirective>() : throw new ActivationException(ExceptionFormatter.ConstructorsAmbiguous(context, grouping));
      }
      object[] array = ((IEnumerable<ITarget>) injectionDirective.Targets).Select<ITarget, object>((Func<ITarget, object>) (target => this.GetValue(context, target))).ToArray<object>();
      return injectionDirective.Injector(array);
    }

    /// <summary>Gets the value to inject into the specified target.</summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>The value to inject into the specified target.</returns>
    public object GetValue(IContext context, ITarget target)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      Ensure.ArgumentNotNull((object) target, nameof (target));
      IConstructorArgument constructorArgument = context.Parameters.OfType<IConstructorArgument>().Where<IConstructorArgument>((Func<IConstructorArgument, bool>) (p => p.AppliesToTarget(context, target))).SingleOrDefault<IConstructorArgument>();
      return constructorArgument == null ? target.ResolveWithin(context) : constructorArgument.GetValue(context, target);
    }

    /// <summary>
    /// Gets the implementation type that the provider will activate an instance of
    /// for the specified service.
    /// </summary>
    /// <param name="service">The service in question.</param>
    /// <returns>The implementation type that will be activated.</returns>
    public Type GetImplementationType(Type service)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      return !this.Type.ContainsGenericParameters ? this.Type : this.Type.MakeGenericType(service.GetGenericArguments());
    }

    /// <summary>
    /// Gets a callback that creates an instance of the <see cref="T:Ninject.Activation.Providers.StandardProvider" />
    /// for the specified type.
    /// </summary>
    /// <param name="prototype">The prototype the provider instance will create.</param>
    /// <returns>The created callback.</returns>
    public static Func<IContext, IProvider> GetCreationCallback(Type prototype)
    {
      Ensure.ArgumentNotNull((object) prototype, nameof (prototype));
      return (Func<IContext, IProvider>) (ctx => (IProvider) new StandardProvider(prototype, ctx.Kernel.Components.Get<IPlanner>(), ctx.Kernel.Components.Get<ISelector>().ConstructorScorer));
    }

    /// <summary>
    /// Gets a callback that creates an instance of the <see cref="T:Ninject.Activation.Providers.StandardProvider" />
    /// for the specified type and constructor.
    /// </summary>
    /// <param name="prototype">The prototype the provider instance will create.</param>
    /// <param name="constructor">The constructor.</param>
    /// <returns>The created callback.</returns>
    public static Func<IContext, IProvider> GetCreationCallback(
      Type prototype,
      ConstructorInfo constructor)
    {
      Ensure.ArgumentNotNull((object) prototype, nameof (prototype));
      return (Func<IContext, IProvider>) (ctx => (IProvider) new StandardProvider(prototype, ctx.Kernel.Components.Get<IPlanner>(), (IConstructorScorer) new SpecificConstructorSelector(constructor)));
    }
  }
}
