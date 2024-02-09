// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Providers.ConstantProvider`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject.Activation.Providers
{
  /// <summary>
  /// A provider that always returns the same constant value.
  /// </summary>
  /// <typeparam name="T">The type of value that is returned.</typeparam>
  public class ConstantProvider<T> : Provider<T>
  {
    /// <summary>Gets the value that the provider will return.</summary>
    public T Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the ConstantProvider&lt;T&gt; class.
    /// </summary>
    /// <param name="value">The value that the provider should return.</param>
    public ConstantProvider(T value) => this.Value = value;

    /// <summary>Creates an instance within the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The constant value this provider returns.</returns>
    protected override T CreateInstance(IContext context) => this.Value;
  }
}
