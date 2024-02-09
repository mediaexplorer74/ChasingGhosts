// Decompiled with JetBrains decompiler
// Type: Ninject.KernelBase
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Activation.Blocks;
using Ninject.Activation.Caching;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject
{
  /// <summary>
  /// The base implementation of an <see cref="T:Ninject.IKernel" />.
  /// </summary>
  public abstract class KernelBase : 
    BindingRoot,
    IKernel,
    IBindingRoot,
    IResolutionRoot,
    IFluentSyntax,
    IServiceProvider,
    IDisposableObject,
    IDisposable
  {
    /// <summary>Lock used when adding missing bindings.</summary>
    protected readonly object HandleMissingBindingLockObject = new object();
    private readonly Multimap<Type, IBinding> bindings = new Multimap<Type, IBinding>();
    private readonly Dictionary<Type, List<IBinding>> bindingCache = new Dictionary<Type, List<IBinding>>();
    private readonly Dictionary<string, INinjectModule> modules = new Dictionary<string, INinjectModule>();
    private readonly IComparer<IBinding> bindingPrecedenceComparer;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.KernelBase" /> class.
    /// </summary>
    protected KernelBase()
      : this((IComponentContainer) new ComponentContainer(), (INinjectSettings) new NinjectSettings())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.KernelBase" /> class.
    /// </summary>
    /// <param name="modules">The modules to load into the kernel.</param>
    protected KernelBase(params INinjectModule[] modules)
      : this((IComponentContainer) new ComponentContainer(), (INinjectSettings) new NinjectSettings(), modules)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.KernelBase" /> class.
    /// </summary>
    /// <param name="settings">The configuration to use.</param>
    /// <param name="modules">The modules to load into the kernel.</param>
    protected KernelBase(INinjectSettings settings, params INinjectModule[] modules)
      : this((IComponentContainer) new ComponentContainer(), settings, modules)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.KernelBase" /> class.
    /// </summary>
    /// <param name="components">The component container to use.</param>
    /// <param name="settings">The configuration to use.</param>
    /// <param name="modules">The modules to load into the kernel.</param>
    protected KernelBase(
      IComponentContainer components,
      INinjectSettings settings,
      params INinjectModule[] modules)
    {
      Ensure.ArgumentNotNull((object) components, nameof (components));
      Ensure.ArgumentNotNull((object) settings, nameof (settings));
      Ensure.ArgumentNotNull((object) modules, nameof (modules));
      this.Settings = settings;
      this.Components = components;
      components.Kernel = (IKernel) this;
      this.AddComponents();
      this.bindingPrecedenceComparer = this.GetBindingPrecedenceComparer();
      this.Bind<IKernel>().ToConstant<KernelBase>(this).InTransientScope();
      this.Bind<IResolutionRoot>().ToConstant<KernelBase>(this).InTransientScope();
      if (this.Settings.LoadExtensions)
        this.Load((IEnumerable<string>) this.Settings.ExtensionSearchPatterns);
      this.Load((IEnumerable<INinjectModule>) modules);
    }

    /// <summary>Gets the kernel settings.</summary>
    public INinjectSettings Settings { get; private set; }

    /// <summary>
    /// Gets the component container, which holds components that contribute to Ninject.
    /// </summary>
    public IComponentContainer Components { get; private set; }

    /// <summary>Releases resources held by the object.</summary>
    public override void Dispose(bool disposing)
    {
      if (disposing && !this.IsDisposed && this.Components != null)
      {
        this.Components.Get<ICache>().Clear();
        this.Components.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <summary>Unregisters all bindings for the specified service.</summary>
    /// <param name="service">The service to unbind.</param>
    public override void Unbind(Type service)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      this.bindings.RemoveAll(service);
      lock (this.bindingCache)
        this.bindingCache.Clear();
    }

    /// <summary>Registers the specified binding.</summary>
    /// <param name="binding">The binding to add.</param>
    public override void AddBinding(IBinding binding)
    {
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      this.AddBindings((IEnumerable<IBinding>) new IBinding[1]
      {
        binding
      });
    }

    /// <summary>Unregisters the specified binding.</summary>
    /// <param name="binding">The binding to remove.</param>
    public override void RemoveBinding(IBinding binding)
    {
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      this.bindings.Remove(binding.Service, binding);
      lock (this.bindingCache)
        this.bindingCache.Clear();
    }

    /// <summary>
    /// Determines whether a module with the specified name has been loaded in the kernel.
    /// </summary>
    /// <param name="name">The name of the module.</param>
    /// <returns><c>True</c> if the specified module has been loaded; otherwise, <c>false</c>.</returns>
    public bool HasModule(string name)
    {
      Ensure.ArgumentNotNullOrEmpty(name, nameof (name));
      return this.modules.ContainsKey(name);
    }

    /// <summary>
    /// Gets the modules that have been loaded into the kernel.
    /// </summary>
    /// <returns>A series of loaded modules.</returns>
    public IEnumerable<INinjectModule> GetModules()
    {
      return (IEnumerable<INinjectModule>) this.modules.Values.ToArray<INinjectModule>();
    }

    /// <summary>Loads the module(s) into the kernel.</summary>
    /// <param name="m">The modules to load.</param>
    public void Load(IEnumerable<INinjectModule> m)
    {
      Ensure.ArgumentNotNull((object) m, "modules");
      m = (IEnumerable<INinjectModule>) m.ToList<INinjectModule>();
      foreach (INinjectModule newModule in m)
      {
        if (string.IsNullOrEmpty(newModule.Name))
          throw new NotSupportedException(ExceptionFormatter.ModulesWithNullOrEmptyNamesAreNotSupported());
        INinjectModule existingModule;
        if (this.modules.TryGetValue(newModule.Name, out existingModule))
          throw new NotSupportedException(ExceptionFormatter.ModuleWithSameNameIsAlreadyLoaded(newModule, existingModule));
        newModule.OnLoad((IKernel) this);
        this.modules.Add(newModule.Name, newModule);
      }
      foreach (INinjectModule ninjectModule in m)
        ninjectModule.OnVerifyRequiredModules();
    }

    /// <summary>
    /// Loads modules from the files that match the specified pattern(s).
    /// </summary>
    /// <param name="filePatterns">The file patterns (i.e. "*.dll", "modules/*.rb") to match.</param>
    public void Load(IEnumerable<string> filePatterns)
    {
      this.Components.Get<IModuleLoader>().LoadModules(filePatterns);
    }

    /// <summary>Loads modules defined in the specified assemblies.</summary>
    /// <param name="assemblies">The assemblies to search.</param>
    public void Load(IEnumerable<Assembly> assemblies)
    {
      this.Load(assemblies.SelectMany<Assembly, INinjectModule>((Func<Assembly, IEnumerable<INinjectModule>>) (asm => asm.GetNinjectModules())));
    }

    /// <summary>Unloads the plugin with the specified name.</summary>
    /// <param name="name">The plugin's name.</param>
    public void Unload(string name)
    {
      Ensure.ArgumentNotNullOrEmpty(name, nameof (name));
      INinjectModule ninjectModule;
      if (!this.modules.TryGetValue(name, out ninjectModule))
        throw new NotSupportedException(ExceptionFormatter.NoModuleLoadedWithTheSpecifiedName(name));
      ninjectModule.OnUnload((IKernel) this);
      this.modules.Remove(name);
    }

    /// <summary>
    /// Injects the specified existing instance, without managing its lifecycle.
    /// </summary>
    /// <param name="instance">The instance to inject.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    public virtual void Inject(object instance, params IParameter[] parameters)
    {
      Ensure.ArgumentNotNull(instance, nameof (instance));
      Ensure.ArgumentNotNull((object) parameters, nameof (parameters));
      Type type = instance.GetType();
      IPlanner planner = this.Components.Get<IPlanner>();
      IPipeline pipeline = this.Components.Get<IPipeline>();
      Binding binding = new Binding(type);
      IContext context = this.CreateContext(this.CreateRequest(type, (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, false, false), (IBinding) binding);
      context.Plan = planner.GetPlan(type);
      InstanceReference reference = new InstanceReference()
      {
        Instance = instance
      };
      pipeline.Activate(context, reference);
    }

    /// <summary>
    /// Deactivates and releases the specified instance if it is currently managed by Ninject.
    /// </summary>
    /// <param name="instance">The instance to release.</param>
    /// <returns><see langword="True" /> if the instance was found and released; otherwise <see langword="false" />.</returns>
    public virtual bool Release(object instance)
    {
      Ensure.ArgumentNotNull(instance, nameof (instance));
      return this.Components.Get<ICache>().Release(instance);
    }

    /// <summary>
    /// Determines whether the specified request can be resolved.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>True</c> if the request can be resolved; otherwise, <c>false</c>.</returns>
    public virtual bool CanResolve(IRequest request)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      return this.GetBindings(request.Service).Any<IBinding>(this.SatifiesRequest(request));
    }

    /// <summary>
    /// Determines whether the specified request can be resolved.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="ignoreImplicitBindings">if set to <c>true</c> implicit bindings are ignored.</param>
    /// <returns>
    ///     <c>True</c> if the request can be resolved; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool CanResolve(IRequest request, bool ignoreImplicitBindings)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      return this.GetBindings(request.Service).Any<IBinding>((Func<IBinding, bool>) (binding => (!ignoreImplicitBindings || !binding.IsImplicit) && this.SatifiesRequest(request)(binding)));
    }

    /// <summary>
    /// Resolves instances for the specified request. The instances are not actually resolved
    /// until a consumer iterates over the enumerator.
    /// </summary>
    /// <param name="request">The request to resolve.</param>
    /// <returns>An enumerator of instances that match the request.</returns>
    public virtual IEnumerable<object> Resolve(IRequest request) => this.Resolve(request, true);

    private IEnumerable<object> Resolve(IRequest request, bool handleMissingBindings)
    {
      IEnumerable<IBinding> source1 = this.GetBindings(request.Service).Where<IBinding>(this.SatifiesRequest(request));
      IEnumerator<IBinding> enumerator = source1.GetEnumerator();
      if (!enumerator.MoveNext())
      {
        if (handleMissingBindings && this.HandleMissingBinding(request))
          return this.Resolve(request, false);
        if (request.IsOptional)
          return Enumerable.Empty<object>();
        throw new ActivationException(ExceptionFormatter.CouldNotResolveBinding(request));
      }
      if (request.IsUnique)
      {
        IBinding current = enumerator.Current;
        if (enumerator.MoveNext() && this.bindingPrecedenceComparer.Compare(current, enumerator.Current) == 0)
        {
          if (request.IsOptional && !request.ForceUnique)
            return Enumerable.Empty<object>();
          IEnumerable<string> source2 = source1.Select(binding => new
          {
            binding = binding,
            context = this.CreateContext(request, binding)
          }).Select(_param0 => _param0.binding.Format(_param0.context));
          throw new ActivationException(ExceptionFormatter.CouldNotUniquelyResolveBinding(request, source2.ToArray<string>()));
        }
        return (IEnumerable<object>) new object[1]
        {
          this.CreateContext(request, current).Resolve()
        };
      }
      if (source1.Any<IBinding>((Func<IBinding, bool>) (binding => !binding.IsImplicit)))
        source1 = source1.Where<IBinding>((Func<IBinding, bool>) (binding => !binding.IsImplicit));
      return source1.Select<IBinding, object>((Func<IBinding, object>) (binding => this.CreateContext(request, binding).Resolve()));
    }

    /// <summary>Creates a request for the specified service.</summary>
    /// <param name="service">The service that is being requested.</param>
    /// <param name="constraint">The constraint to apply to the bindings to determine if they match the request.</param>
    /// <param name="parameters">The parameters to pass to the resolution.</param>
    /// <param name="isOptional"><c>True</c> if the request is optional; otherwise, <c>false</c>.</param>
    /// <param name="isUnique"><c>True</c> if the request should return a unique result; otherwise, <c>false</c>.</param>
    /// <returns>The created request.</returns>
    public virtual IRequest CreateRequest(
      Type service,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters,
      bool isOptional,
      bool isUnique)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parameters, nameof (parameters));
      return (IRequest) new Request(service, constraint, parameters, (Func<object>) null, isOptional, isUnique);
    }

    /// <summary>
    /// Begins a new activation block, which can be used to deterministically dispose resolved instances.
    /// </summary>
    /// <returns>The new activation block.</returns>
    public virtual IActivationBlock BeginBlock()
    {
      return (IActivationBlock) new ActivationBlock((IResolutionRoot) this);
    }

    /// <summary>
    /// Gets the bindings registered for the specified service.
    /// </summary>
    /// <param name="service">The service in question.</param>
    /// <returns>A series of bindings that are registered for the service.</returns>
    public virtual IEnumerable<IBinding> GetBindings(Type service)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      lock (this.bindingCache)
      {
        if (this.bindingCache.ContainsKey(service))
          return (IEnumerable<IBinding>) this.bindingCache[service];
        List<IBinding> list = this.Components.GetAll<IBindingResolver>().SelectMany<IBindingResolver, IBinding>((Func<IBindingResolver, IEnumerable<IBinding>>) (resolver => resolver.Resolve(this.bindings, service))).OrderByDescending<IBinding, IBinding>((Func<IBinding, IBinding>) (b => b), this.bindingPrecedenceComparer).ToList<IBinding>();
        this.bindingCache.Add(service, list);
        return (IEnumerable<IBinding>) list;
      }
    }

    /// <summary>
    /// Returns an IComparer that is used to determine resolution precedence.
    /// </summary>
    /// <returns>An IComparer that is used to determine resolution precedence.</returns>
    protected virtual IComparer<IBinding> GetBindingPrecedenceComparer()
    {
      return (IComparer<IBinding>) new KernelBase.BindingPrecedenceComparer();
    }

    /// <summary>
    /// Returns a predicate that can determine if a given IBinding matches the request.
    /// </summary>
    /// <param name="request">The request/</param>
    /// <returns>A predicate that can determine if a given IBinding matches the request.</returns>
    protected virtual Func<IBinding, bool> SatifiesRequest(IRequest request)
    {
      return (Func<IBinding, bool>) (binding => binding.Matches(request) && request.Matches(binding));
    }

    /// <summary>Adds components to the kernel during startup.</summary>
    protected abstract void AddComponents();

    /// <summary>Attempts to handle a missing binding for a service.</summary>
    /// <param name="service">The service.</param>
    /// <returns><c>True</c> if the missing binding can be handled; otherwise <c>false</c>.</returns>
    [Obsolete]
    protected virtual bool HandleMissingBinding(Type service) => false;

    /// <summary>Attempts to handle a missing binding for a request.</summary>
    /// <param name="request">The request.</param>
    /// <returns><c>True</c> if the missing binding can be handled; otherwise <c>false</c>.</returns>
    protected virtual bool HandleMissingBinding(IRequest request)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      if (this.HandleMissingBinding(request.Service))
        return true;
      List<IBinding> series = this.Components.GetAll<IMissingBindingResolver>().Select<IMissingBindingResolver, List<IBinding>>((Func<IMissingBindingResolver, List<IBinding>>) (c => c.Resolve(this.bindings, request).ToList<IBinding>())).FirstOrDefault<List<IBinding>>((Func<List<IBinding>, bool>) (b => b.Any<IBinding>()));
      if (series == null)
        return false;
      lock (this.HandleMissingBindingLockObject)
      {
        if (!this.CanResolve(request))
        {
          series.Map<IBinding>((Action<IBinding>) (binding => binding.IsImplicit = true));
          this.AddBindings((IEnumerable<IBinding>) series);
        }
      }
      return true;
    }

    /// <summary>
    /// Returns a value indicating whether the specified service is self-bindable.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns><see langword="True" /> if the type is self-bindable; otherwise <see langword="false" />.</returns>
    [Obsolete]
    protected virtual bool TypeIsSelfBindable(Type service)
    {
      return !service.IsInterface && !service.IsAbstract && !service.IsValueType && service != typeof (string) && !service.ContainsGenericParameters;
    }

    /// <summary>
    /// Creates a context for the specified request and binding.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="binding">The binding.</param>
    /// <returns>The created context.</returns>
    protected virtual IContext CreateContext(IRequest request, IBinding binding)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      Ensure.ArgumentNotNull((object) binding, nameof (binding));
      return (IContext) new Context((IKernel) this, request, binding, this.Components.Get<ICache>(), this.Components.Get<IPlanner>(), this.Components.Get<IPipeline>());
    }

    private void AddBindings(IEnumerable<IBinding> bindings)
    {
      bindings.Map<IBinding>((Action<IBinding>) (binding => this.bindings.Add(binding.Service, binding)));
      lock (this.bindingCache)
        this.bindingCache.Clear();
    }

    object IServiceProvider.GetService(Type service) => this.Get(service);

    private class BindingPrecedenceComparer : IComparer<IBinding>
    {
      public int Compare(IBinding x, IBinding y)
      {
        if (x == y)
          return 0;
        return new List<Func<IBinding, bool>>()
        {
          (Func<IBinding, bool>) (b => b != null),
          (Func<IBinding, bool>) (b => b.IsConditional),
          (Func<IBinding, bool>) (b => !b.Service.ContainsGenericParameters),
          (Func<IBinding, bool>) (b => !b.IsImplicit)
        }.Select(func => new{ func = func, xVal = func(x) }).Where(_param1 => _param1.xVal != _param1.func(y)).Select(_param0 => !_param0.xVal ? -1 : 1).FirstOrDefault<int>();
      }
    }
  }
}
