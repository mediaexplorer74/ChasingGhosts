// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.IPlan
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Planning.Directives;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Planning
{
  /// <summary>
  /// Describes the means by which a type should be activated.
  /// </summary>
  public interface IPlan
  {
    /// <summary>Gets the type that the plan describes.</summary>
    Type Type { get; }

    /// <summary>Gets the constructor injection directives.</summary>
    /// <value>The constructor injection directives.</value>
    IList<ConstructorInjectionDirective> ConstructorInjectionDirectives { get; }

    /// <summary>Adds the specified directive to the plan.</summary>
    /// <param name="directive">The directive.</param>
    void Add(IDirective directive);

    /// <summary>
    /// Determines whether the plan contains one or more directives of the specified type.
    /// </summary>
    /// <typeparam name="TDirective">The type of directive.</typeparam>
    /// <returns><c>True</c> if the plan has one or more directives of the type; otherwise, <c>false</c>.</returns>
    bool Has<TDirective>() where TDirective : IDirective;

    /// <summary>
    /// Gets the first directive of the specified type from the plan.
    /// </summary>
    /// <typeparam name="TDirective">The type of directive.</typeparam>
    /// <returns>The first directive, or <see langword="null" /> if no matching directives exist.</returns>
    TDirective GetOne<TDirective>() where TDirective : IDirective;

    /// <summary>
    /// Gets all directives of the specified type that exist in the plan.
    /// </summary>
    /// <typeparam name="TDirective">The type of directive.</typeparam>
    /// <returns>A series of directives of the specified type.</returns>
    IEnumerable<TDirective> GetAll<TDirective>() where TDirective : IDirective;
  }
}
