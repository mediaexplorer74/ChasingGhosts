// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.IParameter
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Planning.Targets;
using System;

#nullable disable
namespace Ninject.Parameters
{
  /// <summary>Modifies an activation process in some way.</summary>
  public interface IParameter : IEquatable<IParameter>
  {
    /// <summary>Gets the name of the parameter.</summary>
    string Name { get; }

    /// <summary>
    /// Gets a value indicating whether the parameter should be inherited into child requests.
    /// </summary>
    bool ShouldInherit { get; }

    /// <summary>
    /// Gets the value for the parameter within the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>The value for the parameter.</returns>
    object GetValue(IContext context, ITarget target);
  }
}
