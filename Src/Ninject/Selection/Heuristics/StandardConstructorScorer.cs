// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.Heuristics.StandardConstructorScorer
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Selection.Heuristics
{
  /// <summary>
  /// Scores constructors by either looking for the existence of an injection marker
  /// attribute, or by counting the number of parameters.
  /// </summary>
  public class StandardConstructorScorer : 
    NinjectComponent,
    IConstructorScorer,
    INinjectComponent,
    IDisposable
  {
    /// <summary>Gets the score for the specified constructor.</summary>
    /// <param name="context">The injection context.</param>
    /// <param name="directive">The constructor.</param>
    /// <returns>The constructor's score.</returns>
    public virtual int Score(IContext context, ConstructorInjectionDirective directive)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      Ensure.ArgumentNotNull((object) directive, "constructor");
      if (directive.HasInjectAttribute)
        return int.MaxValue;
      int num = 1;
      foreach (ITarget target in directive.Targets)
      {
        if (this.ParameterExists(context, target))
          ++num;
        else if (this.BindingExists(context, target))
        {
          ++num;
        }
        else
        {
          ++num;
          if (num > 0)
            num += int.MinValue;
        }
      }
      return num;
    }

    /// <summary>Checkes whether a binding exists for a given target.</summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>Whether a binding exists for the target in the given context.</returns>
    protected virtual bool BindingExists(IContext context, ITarget target)
    {
      return this.BindingExists(context.Kernel, context, target);
    }

    /// <summary>
    /// Checkes whether a binding exists for a given target on the specified kernel.
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>Whether a binding exists for the target in the given context.</returns>
    protected virtual bool BindingExists(IKernel kernel, IContext context, ITarget target)
    {
      Type targetType = this.GetTargetType(target);
      return kernel.GetBindings(targetType).Any<IBinding>((Func<IBinding, bool>) (b => !b.IsImplicit)) || target.HasDefaultValue;
    }

    private Type GetTargetType(ITarget target)
    {
      Type targetType = target.Type;
      if (targetType.IsArray)
        targetType = targetType.GetElementType();
      if (targetType.IsGenericType && ((IEnumerable<Type>) targetType.GetInterfaces()).Any<Type>((Func<Type, bool>) (type => type == typeof (IEnumerable))))
        targetType = targetType.GetGenericArguments()[0];
      return targetType;
    }

    /// <summary>
    /// Checks whether any parameters exist for the given target..
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>Whether a parameter exists for the target in the given context.</returns>
    protected virtual bool ParameterExists(IContext context, ITarget target)
    {
      return context.Parameters.OfType<IConstructorArgument>().Any<IConstructorArgument>((Func<IConstructorArgument, bool>) (parameter => parameter.AppliesToTarget(context, target)));
    }
  }
}
