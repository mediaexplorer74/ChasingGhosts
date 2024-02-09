// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.NinjectResolver
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Ninject;
using Ninject.Modules;
using System;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  public class NinjectResolver : IResolver
  {
    private IKernel kernel;

    public NinjectResolver()
    {
      this.kernel = (IKernel) new StandardKernel(new INinjectModule[0]);
      this.kernel.Bind<IResolver>().ToConstant<NinjectResolver>(this);
    }

    public T Resolve<T>() where T : class => this.kernel.Get<T>();

    public T Resolve<T>(Type type) where T : class => (T) this.kernel.Get(type);

    public T TryResolve<T>() where T : class
    {
      try
      {
        return this.Resolve<T>();
      }
      catch (ActivationException ex)
      {
        return default (T);
      }
    }

    public void Register<T>(T instance) where T : class
    {
      this.kernel.Bind<T>().ToConstant<T>(instance).InSingletonScope();
    }

    public void Register<TInt, TImpl>()
      where TInt : class
      where TImpl : class, TInt
    {
      this.kernel.Bind<TInt>().To<TImpl>().InSingletonScope();
    }

    public void Unregister<T>() where T : class => this.kernel.Unbind<T>();

    public void ClearBindings()
    {
      this.kernel = (IKernel) new StandardKernel(new INinjectModule[0]);
      this.kernel.Bind<IResolver>().ToConstant<NinjectResolver>(this);
    }
  }
}
