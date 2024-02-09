// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.Selector
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Selection
{
  /// <summary>Selects members for injection.</summary>
  public class Selector : NinjectComponent, ISelector, INinjectComponent, IDisposable
  {
    private const BindingFlags DefaultFlags = BindingFlags.Instance | BindingFlags.Public;

    /// <summary>Gets the default binding flags.</summary>
    protected virtual BindingFlags Flags
    {
      get
      {
        return !this.Settings.InjectNonPublic ? BindingFlags.Instance | BindingFlags.Public : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      }
    }

    /// <summary>Gets or sets the constructor scorer.</summary>
    public IConstructorScorer ConstructorScorer { get; set; }

    /// <summary>Gets the property injection heuristics.</summary>
    public ICollection<IInjectionHeuristic> InjectionHeuristics { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Selection.Selector" /> class.
    /// </summary>
    /// <param name="constructorScorer">The constructor scorer.</param>
    /// <param name="injectionHeuristics">The injection heuristics.</param>
    public Selector(
      IConstructorScorer constructorScorer,
      IEnumerable<IInjectionHeuristic> injectionHeuristics)
    {
      Ensure.ArgumentNotNull((object) constructorScorer, nameof (constructorScorer));
      Ensure.ArgumentNotNull((object) injectionHeuristics, nameof (injectionHeuristics));
      this.ConstructorScorer = constructorScorer;
      this.InjectionHeuristics = (ICollection<IInjectionHeuristic>) injectionHeuristics.ToList<IInjectionHeuristic>();
    }

    /// <summary>
    /// Selects the constructor to call on the specified type, by using the constructor scorer.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The selected constructor, or <see langword="null" /> if none were available.</returns>
    public virtual IEnumerable<ConstructorInfo> SelectConstructorsForInjection(Type type)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      ConstructorInfo[] constructors = type.GetConstructors(this.Flags);
      return constructors.Length != 0 ? (IEnumerable<ConstructorInfo>) constructors : (IEnumerable<ConstructorInfo>) null;
    }

    /// <summary>Selects properties that should be injected.</summary>
    /// <param name="type">The type.</param>
    /// <returns>A series of the selected properties.</returns>
    public virtual IEnumerable<PropertyInfo> SelectPropertiesForInjection(Type type)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();
      propertyInfoList.AddRange(((IEnumerable<PropertyInfo>) type.GetProperties(this.Flags)).Select<PropertyInfo, PropertyInfo>((Func<PropertyInfo, PropertyInfo>) (p => p.GetPropertyFromDeclaredType(p, this.Flags))).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => this.InjectionHeuristics.Any<IInjectionHeuristic>((Func<IInjectionHeuristic, bool>) (h => h.ShouldInject((MemberInfo) p))))));
      if (this.Settings.InjectParentPrivateProperties)
      {
        for (Type baseType = type.BaseType; baseType != (Type) null; baseType = baseType.BaseType)
          propertyInfoList.AddRange(this.GetPrivateProperties(type.BaseType));
      }
      return (IEnumerable<PropertyInfo>) propertyInfoList;
    }

    private IEnumerable<PropertyInfo> GetPrivateProperties(Type type)
    {
      return ((IEnumerable<PropertyInfo>) type.GetProperties(this.Flags)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.DeclaringType == type && p.IsPrivate())).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => this.InjectionHeuristics.Any<IInjectionHeuristic>((Func<IInjectionHeuristic, bool>) (h => h.ShouldInject((MemberInfo) p)))));
    }

    /// <summary>Selects methods that should be injected.</summary>
    /// <param name="type">The type.</param>
    /// <returns>A series of the selected methods.</returns>
    public virtual IEnumerable<MethodInfo> SelectMethodsForInjection(Type type)
    {
      Ensure.ArgumentNotNull((object) type, nameof (type));
      return ((IEnumerable<MethodInfo>) type.GetMethods(this.Flags)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => this.InjectionHeuristics.Any<IInjectionHeuristic>((Func<IInjectionHeuristic, bool>) (h => h.ShouldInject((MemberInfo) m)))));
    }
  }
}
