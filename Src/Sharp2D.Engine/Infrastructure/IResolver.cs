// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.IResolver
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  /// <summary>Service Locator</summary>
  public interface IResolver
  {
    /// <summary>Resolves this instance.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T Resolve<T>() where T : class;

    /// <summary>Resolves the specified type.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    T Resolve<T>(Type type) where T : class;

    /// <summary>Tries to resolves this instance.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T TryResolve<T>() where T : class;

    /// <summary>Registers the specified instance.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance">The instance.</param>
    void Register<T>(T instance) where T : class;

    /// <summary>Registers the specified instance.</summary>
    /// <typeparam name="TInt">The type of the int.</typeparam>
    /// <typeparam name="TImpl">The type of the implementation.</typeparam>
    void Register<TInt, TImpl>()
      where TInt : class
      where TImpl : class, TInt;

    /// <summary>Unregisters the binding for the specified type.</summary>
    /// <typeparam name="T"></typeparam>
    void Unregister<T>() where T : class;

    /// <summary>Clears the services.</summary>
    void ClearBindings();
  }
}
