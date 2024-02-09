// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.StandardScopeCallbacks
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using System;

#nullable disable
namespace Ninject.Infrastructure
{
  /// <summary>Scope callbacks for standard scopes.</summary>
  public class StandardScopeCallbacks
  {
    /// <summary>Gets the callback for transient scope.</summary>
    public static readonly Func<IContext, object> Transient = (Func<IContext, object>) (ctx => (object) null);
    /// <summary>Gets the callback for singleton scope.</summary>
    public static readonly Func<IContext, object> Singleton = (Func<IContext, object>) (ctx => (object) ctx.Kernel);
    /// <summary>Gets the callback for thread scope.</summary>
    public static readonly Func<IContext, object> Thread = (Func<IContext, object>) (ctx => (object) System.Threading.Thread.CurrentThread);
  }
}
