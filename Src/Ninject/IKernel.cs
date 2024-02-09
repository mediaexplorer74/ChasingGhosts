﻿// Decompiled with JetBrains decompiler
// Type: Ninject.IKernel
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation.Blocks;
using Ninject.Components;
using Ninject.Infrastructure.Disposal;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Ninject
{
  /// <summary>
  /// A super-factory that can create objects of all kinds, following hints provided by <see cref="T:Ninject.Planning.Bindings.IBinding" />s.
  /// </summary>
  public interface IKernel : 
    IBindingRoot,
    IResolutionRoot,
    IFluentSyntax,
    IServiceProvider,
    IDisposableObject,
    IDisposable
  {
    /// <summary>Gets the kernel settings.</summary>
    INinjectSettings Settings { get; }

    /// <summary>
    /// Gets the component container, which holds components that contribute to Ninject.
    /// </summary>
    IComponentContainer Components { get; }

    /// <summary>
    /// Gets the modules that have been loaded into the kernel.
    /// </summary>
    /// <returns>A series of loaded modules.</returns>
    IEnumerable<INinjectModule> GetModules();

    /// <summary>
    /// Determines whether a module with the specified name has been loaded in the kernel.
    /// </summary>
    /// <param name="name">The name of the module.</param>
    /// <returns><c>True</c> if the specified module has been loaded; otherwise, <c>false</c>.</returns>
    bool HasModule(string name);

    /// <summary>Loads the module(s) into the kernel.</summary>
    /// <param name="m">The modules to load.</param>
    void Load(IEnumerable<INinjectModule> m);

    /// <summary>
    /// Loads modules from the files that match the specified pattern(s).
    /// </summary>
    /// <param name="filePatterns">The file patterns (i.e. "*.dll", "modules/*.rb") to match.</param>
    void Load(IEnumerable<string> filePatterns);

    /// <summary>Loads modules defined in the specified assemblies.</summary>
    /// <param name="assemblies">The assemblies to search.</param>
    void Load(IEnumerable<Assembly> assemblies);

    /// <summary>Unloads the plugin with the specified name.</summary>
    /// <param name="name">The plugin's name.</param>
    void Unload(string name);

    /// <summary>
    /// Injects the specified existing instance, without managing its lifecycle.
    /// </summary>
    /// <param name="instance">The instance to inject.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    void Inject(object instance, params IParameter[] parameters);

    /// <summary>
    /// Gets the bindings registered for the specified service.
    /// </summary>
    /// <param name="service">The service in question.</param>
    /// <returns>A series of bindings that are registered for the service.</returns>
    IEnumerable<IBinding> GetBindings(Type service);

    /// <summary>
    /// Begins a new activation block, which can be used to deterministically dispose resolved instances.
    /// </summary>
    /// <returns>The new activation block.</returns>
    IActivationBlock BeginBlock();
  }
}
