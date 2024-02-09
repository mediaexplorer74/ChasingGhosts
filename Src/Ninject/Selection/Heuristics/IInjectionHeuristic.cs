// Decompiled with JetBrains decompiler
// Type: Ninject.Selection.Heuristics.IInjectionHeuristic
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Selection.Heuristics
{
  /// <summary>
  /// Determines whether members should be injected during activation.
  /// </summary>
  public interface IInjectionHeuristic : INinjectComponent, IDisposable
  {
    /// <summary>
    /// Returns a value indicating whether the specified member should be injected.
    /// </summary>
    /// <param name="member">The member in question.</param>
    /// <returns><c>True</c> if the member should be injected; otherwise <c>false</c>.</returns>
    bool ShouldInject(MemberInfo member);
  }
}
