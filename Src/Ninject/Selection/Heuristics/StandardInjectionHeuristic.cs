// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.Heuristics.StandardInjectionHeuristic
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Selection.Heuristics
{
  /// <summary>
  /// Determines whether members should be injected during activation by checking
  /// if they are decorated with an injection marker attribute.
  /// </summary>
  public class StandardInjectionHeuristic : 
    NinjectComponent,
    IInjectionHeuristic,
    INinjectComponent,
    IDisposable
  {
    /// <summary>
    /// Returns a value indicating whether the specified member should be injected.
    /// </summary>
    /// <param name="member">The member in question.</param>
    /// <returns><c>True</c> if the member should be injected; otherwise <c>false</c>.</returns>
    public virtual bool ShouldInject(MemberInfo member)
    {
      Ensure.ArgumentNotNull((object) member, nameof (member));
      PropertyInfo propertyInfo = member as PropertyInfo;
      if (!(propertyInfo != (PropertyInfo) null))
        return ExtensionsForMemberInfo.HasAttribute(member, this.Settings.InjectAttribute);
      bool injectNonPublic = this.Settings.InjectNonPublic;
      MethodInfo setMethod = propertyInfo.GetSetMethod(injectNonPublic);
      return ExtensionsForMemberInfo.HasAttribute(member, this.Settings.InjectAttribute) && setMethod != (MethodInfo) null;
    }
  }
}
