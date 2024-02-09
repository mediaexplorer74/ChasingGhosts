// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.WeakConstructorArgument
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
  public class WeakConstructorArgument : 
    Parameter,
    IConstructorArgument,
    IParameter,
    IEquatable<IParameter>
  {
    /// <summary>A weak reference to the constructor argument value.</summary>
    private readonly WeakReference weakReference;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="value">The value to inject into the property.</param>
    public WeakConstructorArgument(string name, object value)
      : this(name, value, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.ConstructorArgument" /> class.
    /// </summary>
    /// <param name="name">The name of the argument to override.</param>
    /// <param name="value">The value to inject into the property.</param>
    /// <param name="shouldInherit">Whether the parameter should be inherited into child requests.</param>
    public WeakConstructorArgument(string name, object value, bool shouldInherit)
      : base(name, value, shouldInherit)
    {
      this.weakReference = new WeakReference(value);
      this.ValueCallback = (Func<IContext, ITarget, object>) ((ctx, target) => this.weakReference.Target);
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
