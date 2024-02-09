// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.ConstructorArgument
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
  /// <summary>
  /// Overrides the injected value of a constructor argument.
  /// </summary>
  public class ConstructorArgument : 
    Parameter,
    IConstructorArgument,
    IParameter,
    IEquatable<IParameter>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="value">The value to inject into the property.</param>
    public ConstructorArgument(string name, object value)
      : base(name, value, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="valueCallback">The callback to invoke to get the value that should be injected.</param>
    public ConstructorArgument(string name, Func<IContext, object> valueCallback)
      : base(name, valueCallback, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="valueCallback">The callback to invoke to get the value that should be injected.</param>
    public ConstructorArgument(string name, Func<IContext, ITarget, object> valueCallback)
      : base(name, valueCallback, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="value">The value to inject into the property.</param>
    /// <param name="shouldInherit">Whether the parameter should be inherited into child requests.</param>
    public ConstructorArgument(string name, object value, bool shouldInherit)
      : base(name, value, shouldInherit)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="valueCallback">The callback to invoke to get the value that should be injected.</param>
    /// <param name="shouldInherit">if set to <c>true</c> [should inherit].</param>
    public ConstructorArgument(
      string name,
      Func<IContext, object> valueCallback,
      bool shouldInherit)
      : base(name, valueCallback, shouldInherit)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="valueCallback">The callback to invoke to get the value that should be injected.</param>
    /// <param name="shouldInherit">if set to <c>true</c> [should inherit].</param>
    public ConstructorArgument(
      string name,
      Func<IContext, ITarget, object> valueCallback,
      bool shouldInherit)
      : base(name, valueCallback, shouldInherit)
    {
    }

    /// <summary>
    /// Determines if the parameter applies to the given target.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>
    /// True if the parameter applies in the specified context to the specified target.
    /// </returns>
    /// <remarks>Only one parameter may return true.</remarks>
    public bool AppliesToTarget(IContext context, ITarget target)
    {
      return string.Equals(this.Name, target.Name);
    }
  }
}
