// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Strategies.MethodInjectionStrategy
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Activation.Strategies
{
  /// <summary>Injects methods on an instance during activation.</summary>
  public class MethodInjectionStrategy : ActivationStrategy
  {
    /// <summary>
    /// Injects values into the properties as described by <see cref="T:Ninject.Planning.Directives.MethodInjectionDirective" />s
    /// contained in the plan.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reference">A reference to the instance being activated.</param>
    public override void Activate(IContext context, InstanceReference reference)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      Ensure.ArgumentNotNull((object) reference, nameof (reference));
      foreach (MethodInjectionDirective injectionDirective in context.Plan.GetAll<MethodInjectionDirective>())
      {
        IEnumerable<object> source = ((IEnumerable<ITarget>) injectionDirective.Targets).Select<ITarget, object>((Func<ITarget, object>) (target => target.ResolveWithin(context)));
        injectionDirective.Injector(reference.Instance, source.ToArray<object>());
      }
    }
  }
}
