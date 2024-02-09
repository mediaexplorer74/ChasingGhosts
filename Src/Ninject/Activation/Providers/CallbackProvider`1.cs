// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Providers.CallbackProvider`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using System;

#nullable disable
namespace Ninject.Activation.Providers
{
  /// <summary>
  /// A provider that delegates to a callback method to create instances.
  /// </summary>
  /// <typeparam name="T">The type of instances the provider creates.</typeparam>
  public class CallbackProvider<T> : Provider<T>
  {
    /// <summary>Gets the callback method used by the provider.</summary>
    public Func<IContext, T> Method { get; private set; }

    /// <summary>
    /// Initializes a new instance of the CallbackProvider&lt;T&gt; class.
    /// </summary>
    /// <param name="method">The callback method that will be called to create instances.</param>
    public CallbackProvider(Func<IContext, T> method)
    {
      Ensure.ArgumentNotNull((object) method, nameof (method));
      this.Method = method;
    }

    /// <summary>Invokes the callback method to create an instance.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The created instance.</returns>
    protected override T CreateInstance(IContext context) => this.Method(context);
  }
}
