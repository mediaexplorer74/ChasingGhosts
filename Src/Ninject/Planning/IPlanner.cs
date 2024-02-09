// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.IPlanner
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Planning.Strategies;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Planning
{
  /// <summary>Generates plans for how to activate instances.</summary>
  public interface IPlanner : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Gets the strategies that contribute to the planning process.
    /// </summary>
    IList<IPlanningStrategy> Strategies { get; }

    /// <summary>
    /// Gets or creates an activation plan for the specified type.
    /// </summary>
    /// <param name="type">The type for which a plan should be created.</param>
    /// <returns>The type's activation plan.</returns>
    IPlan GetPlan(Type type);
  }
}
