// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.ISelector
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Ninject.Selection
{
  /// <summary>Selects members for injection.</summary>
  public interface ISelector : INinjectComponent, IDisposable
  {
    /// <summary>Gets or sets the constructor scorer.</summary>
    IConstructorScorer ConstructorScorer { get; set; }

    /// <summary>
    /// Gets the heuristics used to determine which members should be injected.
    /// </summary>
    ICollection<IInjectionHeuristic> InjectionHeuristics { get; }

    /// <summary>
    /// Selects the constructor to call on the specified type, by using the constructor scorer.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The selected constructor, or <see langword="null" /> if none were available.</returns>
    IEnumerable<ConstructorInfo> SelectConstructorsForInjection(Type type);

    /// <summary>Selects properties that should be injected.</summary>
    /// <param name="type">The type.</param>
    /// <returns>A series of the selected properties.</returns>
    IEnumerable<PropertyInfo> SelectPropertiesForInjection(Type type);

    /// <summary>Selects methods that should be injected.</summary>
    /// <param name="type">The type.</param>
    /// <returns>A series of the selected methods.</returns>
    IEnumerable<MethodInfo> SelectMethodsForInjection(Type type);
  }
}
