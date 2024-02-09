// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.PropertyInjectionStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Injection;
using Ninject.Parameters;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>Injects properties on an instance during activation.</summary>
  public class PropertyInjectionStrategy : ActivationStrategy
  {
    private const BindingFlags DefaultFlags = BindingFlags.Instance | BindingFlags.Public;

    private BindingFlags Flags
    {
      get
      {
        return !this.Settings.InjectNonPublic ? BindingFlags.Instance | BindingFlags.Public : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      }
    }

    /// <summary>Gets the injector factory component.</summary>
    public IInjectorFactory InjectorFactory { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Strategies.PropertyInjectionStrategy" /> class.
    /// </summary>
    /// <param name="injectorFactory">The injector factory component.</param>
    public PropertyInjectionStrategy(IInjectorFactory injectorFactory)
    {
      this.InjectorFactory = injectorFactory;
    }

    /// <summary>
    /// Injects values into the properties as described by <see cref="T:Ninject.Planning.Directives.PropertyInjectionDirective" />s
    /// contained in the plan.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public override void Activate(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      Ensure.ArgumentNotNull((object) reference, nameof (reference));
      List<IPropertyValue> list = context.Parameters.OfType<IPropertyValue>().ToList<IPropertyValue>();
      foreach (PropertyInjectionDirective injectionDirective in context.Plan.GetAll<PropertyInjectionDirective>())
      {
        object obj = this.GetValue(context, injectionDirective.Target, (IEnumerable<IPropertyValue>) list);
        injectionDirective.Injector(reference.Instance, obj);
      }
      this.AssignPropertyOverrides(context, reference, (IList<IPropertyValue>) list);
    }

    /// <summary>
    /// Applies user supplied override values to instance properties.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    /// <param name="propertyValues">The parameter override value accessors.</param>
    private void AssignPropertyOverrides(
      IContext context,
      InstanceReference reference,
      IList<IPropertyValue> propertyValues)
    {
      PropertyInfo[] properties = reference.Instance.GetType().GetProperties(this.Flags);
      foreach (IPropertyValue propertyValue in (IEnumerable<IPropertyValue>) propertyValues)
      {
        string propertyName = propertyValue.Name;
        PropertyInfo propertyInfo = ((IEnumerable<PropertyInfo>) properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (property => string.Equals(property.Name, propertyName, StringComparison.Ordinal)));
        PropertyInjectionDirective injectionDirective = !(propertyInfo == (PropertyInfo) null) ? new PropertyInjectionDirective(propertyInfo, this.InjectorFactory.Create(propertyInfo)) : throw new ActivationException(ExceptionFormatter.CouldNotResolvePropertyForValueInjection(context.Request, propertyName));
        object obj = this.GetValue(context, injectionDirective.Target, (IEnumerable<IPropertyValue>) propertyValues);
        injectionDirective.Injector(reference.Instance, obj);
      }
    }

    /// <summary>Gets the value to inject into the specified target.</summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <param name="allPropertyValues">all property values of the current request.</param>
    /// <returns>The value to inject into the specified target.</returns>
    private object GetValue(
      IContext context,
      ITarget target,
      IEnumerable<IPropertyValue> allPropertyValues)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      Ensure.ArgumentNotNull((object) target, nameof (target));
      IPropertyValue propertyValue = allPropertyValues.SingleOrDefault<IPropertyValue>((Func<IPropertyValue, bool>) (p => p.Name == target.Name));
      return propertyValue == null ? target.ResolveWithin(context) : propertyValue.GetValue(context, target);
    }
  }
}
