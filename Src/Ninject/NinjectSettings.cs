// Decompiled with JetBrains decompiler
// Type: Ninject.NinjectSettings
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject
{
  /// <summary>Contains configuration options for Ninject.</summary>
  public class NinjectSettings : INinjectSettings
  {
    private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

    /// <summary>
    /// Gets or sets the attribute that indicates that a member should be injected.
    /// </summary>
    public Type InjectAttribute
    {
      get => this.Get<Type>(nameof (InjectAttribute), typeof (Ninject.InjectAttribute));
      set => this.Set(nameof (InjectAttribute), (object) value);
    }

    /// <summary>
    /// Gets or sets the interval at which the GC should be polled.
    /// </summary>
    public TimeSpan CachePruningInterval
    {
      get => this.Get<TimeSpan>(nameof (CachePruningInterval), TimeSpan.FromSeconds(30.0));
      set => this.Set(nameof (CachePruningInterval), (object) value);
    }

    /// <summary>Gets or sets the default scope callback.</summary>
    public Func<IContext, object> DefaultScopeCallback
    {
      get
      {
        return this.Get<Func<IContext, object>>(nameof (DefaultScopeCallback), StandardScopeCallbacks.Transient);
      }
      set => this.Set(nameof (DefaultScopeCallback), (object) value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the kernel should automatically load extensions at startup.
    /// </summary>
    public bool LoadExtensions
    {
      get => this.Get<bool>(nameof (LoadExtensions), true);
      set => this.Set(nameof (LoadExtensions), (object) value);
    }

    /// <summary>
    /// Gets or sets the paths that should be searched for extensions.
    /// </summary>
    public string[] ExtensionSearchPatterns
    {
      get
      {
        return this.Get<string[]>(nameof (ExtensionSearchPatterns), new string[2]
        {
          "Ninject.Extensions.*.dll",
          "Ninject.Web*.dll"
        });
      }
      set => this.Set(nameof (ExtensionSearchPatterns), (object) value);
    }

    /// <summary>
    /// Gets a value indicating whether Ninject should use reflection-based injection instead of
    /// the (usually faster) lightweight code generation system.
    /// </summary>
    public bool UseReflectionBasedInjection
    {
      get => this.Get<bool>(nameof (UseReflectionBasedInjection), false);
      set => this.Set(nameof (UseReflectionBasedInjection), (object) value);
    }

    /// <summary>
    /// Gets a value indicating whether Ninject should inject non public members.
    /// </summary>
    public bool InjectNonPublic
    {
      get => this.Get<bool>(nameof (InjectNonPublic), false);
      set => this.Set(nameof (InjectNonPublic), (object) value);
    }

    /// <summary>
    /// Gets a value indicating whether Ninject should inject private properties of base classes.
    /// </summary>
    /// <remarks>
    /// Activating this setting has an impact on the performance. It is recommended not
    /// to use this feature and use constructor injection instead.
    /// </remarks>
    public bool InjectParentPrivateProperties
    {
      get => this.Get<bool>(nameof (InjectParentPrivateProperties), false);
      set => this.Set(nameof (InjectParentPrivateProperties), (object) value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the activation cache is disabled.
    /// If the activation cache is disabled less memory is used. But in some cases
    /// instances are activated or deactivated multiple times. e.g. in the following scenario:
    /// Bind{A}().ToSelf();
    /// Bind{IA}().ToMethod(ctx =&gt; kernel.Get{IA}();
    /// </summary>
    /// <value>
    /// 	<c>true</c> if activation cache is disabled; otherwise, <c>false</c>.
    /// </value>
    public bool ActivationCacheDisabled
    {
      get => this.Get<bool>(nameof (ActivationCacheDisabled), false);
      set => this.Set(nameof (ActivationCacheDisabled), (object) value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether Null is a valid value for injection.
    /// By default this is disabled and whenever a provider returns null an exception is thrown.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if null is allowed as injected value otherwise false.
    /// </value>
    public bool AllowNullInjection
    {
      get => this.Get<bool>(nameof (AllowNullInjection), false);
      set => this.Set(nameof (AllowNullInjection), (object) value);
    }

    /// <summary>Gets the value for the specified key.</summary>
    /// <typeparam name="T">The type of value to return.</typeparam>
    /// <param name="key">The setting's key.</param>
    /// <param name="defaultValue">The value to return if no setting is available.</param>
    /// <returns>The value, or the default value if none was found.</returns>
    public T Get<T>(string key, T defaultValue)
    {
      object obj;
      return !this._values.TryGetValue(key, out obj) ? defaultValue : (T) obj;
    }

    /// <summary>Sets the value for the specified key.</summary>
    /// <param name="key">The setting's key.</param>
    /// <param name="value">The setting's value.</param>
    public void Set(string key, object value) => this._values[key] = value;
  }
}
