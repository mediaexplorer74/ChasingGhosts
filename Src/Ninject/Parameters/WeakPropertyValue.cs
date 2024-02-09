// Decompiled with JetBrains decompiler
// Type: Ninject.Parameters.WeakPropertyValue
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
  /// Overrides the injected value of a property.
  /// Keeps a weak reference to the value.
  /// </summary>
  public class WeakPropertyValue : Parameter, IPropertyValue, IParameter, IEquatable<IParameter>
  {
    private readonly WeakReference weakReference;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Parameters.WeakPropertyValue" /> class.
    /// </summary>
    /// <param name="name">The name of the property to override.</param>
    /// <param name="value">The value to inject into the property.</param>
    public WeakPropertyValue(string name, object value)
      : base(name, (object) null, false)
    {
      this.weakReference = new WeakReference(value);
      this.ValueCallback = (Func<IContext, ITarget, object>) ((ctx, target) => this.weakReference.Target);
    }
  }
}
