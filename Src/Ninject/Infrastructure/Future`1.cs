// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Future`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Infrastructure
{
  /// <summary>Represents a future value.</summary>
  /// <typeparam name="T">The type of value.</typeparam>
  public class Future<T>
  {
    private bool _hasValue;
    private T _value;

    /// <summary>Gets the value, resolving it if necessary.</summary>
    public T Value
    {
      get
      {
        if (!this._hasValue)
        {
          this._value = this.Callback();
          this._hasValue = true;
        }
        return this._value;
      }
    }

    /// <summary>
    /// Gets the callback that will be called to resolve the value.
    /// </summary>
    public Func<T> Callback { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Future&lt;T&gt; class.
    /// </summary>
    /// <param name="callback">The callback that will be triggered to read the value.</param>
    public Future(Func<T> callback) => this.Callback = callback;

    /// <summary>Gets the value from the future.</summary>
    /// <param name="future">The future.</param>
    /// <returns>The future value.</returns>
    public static implicit operator T(Future<T> future) => future.Value;
  }
}
