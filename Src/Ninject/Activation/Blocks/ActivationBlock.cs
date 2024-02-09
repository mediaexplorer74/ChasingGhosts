// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Blocks.ActivationBlock
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Activation.Blocks
{
  /// <summary>
  /// A block used for deterministic disposal of activated instances. When the block is
  /// disposed, all instances activated via it will be deactivated.
  /// </summary>
  public class ActivationBlock : 
    DisposableObject,
    IActivationBlock,
    IResolutionRoot,
    IFluentSyntax,
    INotifyWhenDisposed,
    IDisposableObject,
    IDisposable
  {
    /// <summary>
    /// Gets or sets the parent resolution root (usually the kernel).
    /// </summary>
    public IResolutionRoot Parent { get; private set; }

    /// <summary>Occurs when the object is disposed.</summary>
    public event EventHandler Disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.Activation.Blocks.ActivationBlock" /> class.
    /// </summary>
    /// <param name="parent">The parent resolution root.</param>
    public ActivationBlock(IResolutionRoot parent)
    {
      Ensure.ArgumentNotNull((object) parent, nameof (parent));
      this.Parent = parent;
    }

    /// <summary>Releases resources held by the object.</summary>
    public override void Dispose(bool disposing)
    {
      lock (this)
      {
        if (disposing && !this.IsDisposed)
        {
          EventHandler disposed = this.Disposed;
          if (disposed != null)
            disposed((object) this, EventArgs.Empty);
          this.Disposed = (EventHandler) null;
        }
        base.Dispose(disposing);
      }
    }

    /// <summary>
    /// Determines whether the specified request can be resolved.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>True</c> if the request can be resolved; otherwise, <c>false</c>.</returns>
    public bool CanResolve(IRequest request)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      return this.Parent.CanResolve(request);
    }

    /// <summary>
    /// Determines whether the specified request can be resolved.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="ignoreImplicitBindings">if set to <c>true</c> implicit bindings are ignored.</param>
    /// <returns>
    ///     <c>True</c> if the request can be resolved; otherwise, <c>false</c>.
    /// </returns>
    public bool CanResolve(IRequest request, bool ignoreImplicitBindings)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      return this.Parent.CanResolve(request, ignoreImplicitBindings);
    }

    /// <summary>
    /// Resolves instances for the specified request. The instances are not actually resolved
    /// until a consumer iterates over the enumerator.
    /// </summary>
    /// <param name="request">The request to resolve.</param>
    /// <returns>An enumerator of instances that match the request.</returns>
    public IEnumerable<object> Resolve(IRequest request)
    {
      Ensure.ArgumentNotNull((object) request, nameof (request));
      return this.Parent.Resolve(request);
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
      return (IRequest) new Request(service, constraint, parameters, (Func<object>) (() => (object) this), isOptional, isUnique);
    }

    /// <summary>
    /// Deactivates and releases the specified instance if it is currently managed by Ninject.
    /// </summary>
    /// <param name="instance">The instance to release.</param>
    /// <returns><see langword="True" /> if the instance was found and released; otherwise <see langword="false" />.</returns>
    /// <remarks></remarks>
    public bool Release(object instance) => this.Parent.Release(instance);

    Type IFluentSyntax.GetType() => this.GetType();
  }
}
