// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Planner
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace Ninject.Planning
{
  /// <summary>Generates plans for how to activate instances.</summary>
  public class Planner : NinjectComponent, IPlanner, INinjectComponent, IDisposable
  {
    private readonly ReaderWriterLock plannerLock = new ReaderWriterLock();
    private readonly Dictionary<Type, IPlan> plans = new Dictionary<Type, IPlan>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Planner" /> class.
    /// </summary>
    /// <param name="strategies">The strategies to execute during planning.</param>
    public Planner(IEnumerable<IPlanningStrategy> strategies)
    {
      Ensure.ArgumentNotNull((object) strategies, nameof (strategies));
      this.Strategies = (IList<IPlanningStrategy>) strategies.ToList<IPlanningStrategy>();
    }

    /// <summary>
    /// Gets the strategies that contribute to the planning process.
    /// </summary>
    public IList<IPlanningStrategy> Strategies { get; private set; }

    /// <summary>
    /// Gets or creates an activation plan for the specified type.
    /// </summary>
    /// <param name="type">The type for which a plan should be created.</param>
    /// <returns>The type's activation plan.</returns>
    public IPlan GetPlan(Type type)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      this.plannerLock.AcquireReaderLock(-1);
      try
      {
        IPlan plan;
        return this.plans.TryGetValue(type, out plan) ? plan : this.CreateNewPlan(type);
      }
      finally
      {
        this.plannerLock.ReleaseReaderLock();
      }
    }

    /// <summary>Creates an empty plan for the specified type.</summary>
    /// <param name="type">The type for which a plan should be created.</param>
    /// <returns>The created plan.</returns>
    protected virtual IPlan CreateEmptyPlan(Type type)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      return (IPlan) new Plan(type);
    }

    /// <summary>
    /// Creates a new plan for the specified type.
    /// This method requires an active reader lock!
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The newly created plan.</returns>
    private IPlan CreateNewPlan(Type type)
    {
      LockCookie writerLock = this.plannerLock.UpgradeToWriterLock(-1);
      try
      {
        IPlan plan;
        if (this.plans.TryGetValue(type, out plan))
          return plan;
        plan = this.CreateEmptyPlan(type);
        this.plans.Add(type, plan);
        this.Strategies.Map<IPlanningStrategy>((Action<IPlanningStrategy>) (s => s.Execute(plan)));
        return plan;
      }
      finally
      {
        this.plannerLock.DowngradeFromWriterLock(ref writerLock);
      }
    }
  }
}
