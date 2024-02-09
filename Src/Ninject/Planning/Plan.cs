// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Plan
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Planning.Directives;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Planning
{
  /// <summary>
  /// Describes the means by which a type should be activated.
  /// </summary>
  public class Plan : IPlan
  {
    /// <summary>Gets the type that the plan describes.</summary>
    public Type Type { get; private set; }

    /// <summary>Gets the directives defined in the plan.</summary>
    public ICollection<IDirective> Directives { get; private set; }

    /// <summary>Gets the constructor injection directives.</summary>
    /// <value>The constructor injection directives.</value>
    public IList<ConstructorInjectionDirective> ConstructorInjectionDirectives { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Planning.Plan" /> class.
    /// </summary>
    /// <param name="type">The type the plan describes.</param>
    public Plan(Type type)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      this.Type = type;
      this.Directives = (ICollection<IDirective>) new List<IDirective>();
      this.ConstructorInjectionDirectives = (IList<ConstructorInjectionDirective>) new List<ConstructorInjectionDirective>();
    }

    /// <summary>Adds the specified directive to the plan.</summary>
    /// <param name="directive">The directive.</param>
    public void Add(IDirective directive)
    {
      Ensure.ArgumentNotNull((object) directive, nameof (directive));
      if (directive is ConstructorInjectionDirective injectionDirective)
        this.ConstructorInjectionDirectives.Add(injectionDirective);
      this.Directives.Add(directive);
    }

    /// <summary>
    /// Determines whether the plan contains one or more directives of the specified type.
    /// </summary>
    /// <typeparam name="TDirective">The type of directive.</typeparam>
    /// <returns><c>True</c> if the plan has one or more directives of the type; otherwise, <c>false</c>.</returns>
    public bool Has<TDirective>() where TDirective : IDirective
    {
      return this.GetAll<TDirective>().Any<TDirective>();
    }

    /// <summary>
    /// Gets the first directive of the specified type from the plan.
    /// </summary>
    /// <typeparam name="TDirective">The type of directive.</typeparam>
    /// <returns>The first directive, or <see langword="null" /> if no matching directives exist.</returns>
    public TDirective GetOne<TDirective>() where TDirective : IDirective
    {
      return this.GetAll<TDirective>().SingleOrDefault<TDirective>();
    }

    /// <summary>
    /// Gets all directives of the specified type that exist in the plan.
    /// </summary>
    /// <typeparam name="TDirective">The type of directive.</typeparam>
    /// <returns>A series of directives of the specified type.</returns>
    public IEnumerable<TDirective> GetAll<TDirective>() where TDirective : IDirective
    {
      return this.Directives.OfType<TDirective>();
    }
  }
}
