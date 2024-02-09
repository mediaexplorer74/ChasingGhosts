// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.PropertyValue
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
  /// <summary>Overrides the injected value of a property.</summary>
  public class PropertyValue : Parameter, IPropertyValue, IParameter, IEquatable<IParameter>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.PropertyValue" /> class.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="value">The value to inject into the property.</param>
    public PropertyValue(string name, object value)
      : base(name, value, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.PropertyValue" /> class.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="valueCallback">The callback to invoke to get the value that should be injected.</param>
    public PropertyValue(string name, Func<IContext, object> valueCallback)
      : base(name, valueCallback, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.PropertyValue" /> class.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="valueCallback">The callback to invoke to get the value that should be injected.</param>
    public PropertyValue(string name, Func<IContext, ITarget, object> valueCallback)
      : base(name, valueCallback, false)
    {
    }
  }
}
