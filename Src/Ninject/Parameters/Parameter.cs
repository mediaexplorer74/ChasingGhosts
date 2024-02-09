// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.Parameter
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Planning.Targets;
using System;

#nullable disable
namespace Ninject.Parameters
{
  /// <summary>Modifies an activation process in some way.</summary>
  public class Parameter : IParameter, IEquatable<IParameter>
  {
    /// <summary>Gets the name of the parameter.</summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the parameter should be inherited into child requests.
    /// </summary>
    public bool ShouldInherit { get; private set; }

    /// <summary>
    /// Gets or sets the callback that will be triggered to get the parameter's value.
    /// </summary>
    public Func<IContext, ITarget, object> ValueCallback { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.Parameter" /> class.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="shouldInherit">Whether the parameter should be inherited into child requests.</param>
    public Parameter(string name, object value, bool shouldInherit)
      : this(name, (Func<IContext, ITarget, object>) ((ctx, target) => value), shouldInherit)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.Parameter" /> class.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="valueCallback">The callback that will be triggered to get the parameter's value.</param>
    /// <param name="shouldInherit">Whether the parameter should be inherited into child requests.</param>
    public Parameter(string name, Func<IContext, object> valueCallback, bool shouldInherit)
    {
      Ensure.ArgumentNotNullOrEmpty(name, nameof (name));
      Ensure.ArgumentNotNull((object) valueCallback, nameof (valueCallback));
      this.Name = name;
      this.ValueCallback = (Func<IContext, ITarget, object>) ((ctx, target) => valueCallback(ctx));
      this.ShouldInherit = shouldInherit;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.Parameter" /> class.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="valueCallback">The callback that will be triggered to get the parameter's value.</param>
    /// <param name="shouldInherit">Whether the parameter should be inherited into child requests.</param>
    public Parameter(
      string name,
      Func<IContext, ITarget, object> valueCallback,
      bool shouldInherit)
    {
      Ensure.ArgumentNotNullOrEmpty(name, nameof (name));
      Ensure.ArgumentNotNull((object) valueCallback, nameof (valueCallback));
      this.Name = name;
      this.ValueCallback = valueCallback;
      this.ShouldInherit = shouldInherit;
    }

    /// <summary>
    /// Gets the value for the parameter within the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="target">The target.</param>
    /// <returns>The value for the parameter.</returns>
    public object GetValue(IContext context, ITarget target)
    {
      Ensure.ArgumentNotNull((object) context, nameof (context));
      return this.ValueCallback(context, target);
    }

    /// <summary>
    /// Determines whether the object equals the specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this object.</param>
    /// <returns><c>True</c> if the objects are equal; otherwise <c>false</c></returns>
    public override bool Equals(object obj)
    {
      return !(obj is IParameter other) ? base.Equals(obj) : this.Equals(other);
    }

    /// <summary>Serves as a hash function for a particular type.</summary>
    /// <returns>A hash code for the object.</returns>
    public override int GetHashCode() => this.GetType().GetHashCode() ^ this.Name.GetHashCode();

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>True</c> if the objects are equal; otherwise <c>false</c></returns>
    public bool Equals(IParameter other)
    {
      return other.GetType() == this.GetType() && other.Name.Equals(this.Name);
    }
  }
}
