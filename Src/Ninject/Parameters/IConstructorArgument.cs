// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.IConstructorArgument
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
  /// <summary>Defines the interface for constructor arguments.</summary>
  public interface IConstructorArgument : IParameter, IEquatable<IParameter>
  {
    /// <summary>
    /// Determines if the parameter applies to the given target.
    /// </summary>
    /// <remarks>Only one parameter may return true.</remarks>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>True if the parameter applies in the specified context to the specified target.</returns>
    bool AppliesToTarget(IContext context, ITarget target);
  }
}
