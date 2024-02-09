// Decompiled with JetBrains decompiler
// Type: Ninject.ResolutionExtensions
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject
{
  /// <summary>Extensions that enhance resolution of services.</summary>
  public static class ResolutionExtensions
  {
    /// <summary>Gets an instance of the specified service.</summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static T Get<T>(this IResolutionRoot root, params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, false, true).Cast<T>().Single<T>();
    }

    /// <summary>
    /// Gets an instance of the specified service by using the first binding with the specified name.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static T Get<T>(this IResolutionRoot root, string name, params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, false, true).Cast<T>().Single<T>();
    }

    /// <summary>
    /// Gets an instance of the specified service by using the first binding that matches the specified constraint.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static T Get<T>(
      this IResolutionRoot root,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), constraint, (IEnumerable<IParameter>) parameters, false, true).Cast<T>().Single<T>();
    }

    /// <summary>Tries to get an instance of the specified service.</summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static T TryGet<T>(this IResolutionRoot root, params IParameter[] parameters)
    {
      return ResolutionExtensions.TryGet<T>((Func<IEnumerable<T>>) (() => ResolutionExtensions.GetResolutionIterator(root, typeof (T), (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, true, true).Cast<T>()));
    }

    /// <summary>
    /// Tries to get an instance of the specified service by using the first binding with the specified name.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static T TryGet<T>(
      this IResolutionRoot root,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.TryGet<T>((Func<IEnumerable<T>>) (() => ResolutionExtensions.GetResolutionIterator(root, typeof (T), (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, true, true).Cast<T>()));
    }

    /// <summary>
    /// Tries to get an instance of the specified service by using the first binding that matches the specified constraint.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static T TryGet<T>(
      this IResolutionRoot root,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.TryGet<T>((Func<IEnumerable<T>>) (() => ResolutionExtensions.GetResolutionIterator(root, typeof (T), constraint, (IEnumerable<IParameter>) parameters, true, true).Cast<T>()));
    }

    /// <summary>Tries to get an instance of the specified service.</summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static T TryGetAndThrowOnInvalidBinding<T>(
      this IResolutionRoot root,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.DoTryGetAndThrowOnInvalidBinding<T>(root, (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters);
    }

    /// <summary>
    /// Tries to get an instance of the specified service by using the first binding with the specified name.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static T TryGetAndThrowOnInvalidBinding<T>(
      this IResolutionRoot root,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.DoTryGetAndThrowOnInvalidBinding<T>(root, (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters);
    }

    /// <summary>
    /// Tries to get an instance of the specified service by using the first binding that matches the specified constraint.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static T TryGetAndThrowOnInvalidBinding<T>(
      this IResolutionRoot root,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.DoTryGetAndThrowOnInvalidBinding<T>(root, constraint, (IEnumerable<IParameter>) parameters);
    }

    /// <summary>
    /// Gets all available instances of the specified service.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>A series of instances of the service.</returns>
    public static IEnumerable<T> GetAll<T>(
      this IResolutionRoot root,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, true, false).Cast<T>();
    }

    /// <summary>
    /// Gets all instances of the specified service using bindings registered with the specified name.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>A series of instances of the service.</returns>
    public static IEnumerable<T> GetAll<T>(
      this IResolutionRoot root,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, true, false).Cast<T>();
    }

    /// <summary>
    /// Gets all instances of the specified service by using the bindings that match the specified constraint.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="constraint">The constraint to apply to the bindings.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>A series of instances of the service.</returns>
    public static IEnumerable<T> GetAll<T>(
      this IResolutionRoot root,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), constraint, (IEnumerable<IParameter>) parameters, true, false).Cast<T>();
    }

    /// <summary>Gets an instance of the specified service.</summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static object Get(
      this IResolutionRoot root,
      Type service,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, service, (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, false, true).Single<object>();
    }

    /// <summary>
    /// Gets an instance of the specified service by using the first binding with the specified name.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static object Get(
      this IResolutionRoot root,
      Type service,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, service, (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, false, true).Single<object>();
    }

    /// <summary>
    /// Gets an instance of the specified service by using the first binding that matches the specified constraint.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static object Get(
      this IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, service, constraint, (IEnumerable<IParameter>) parameters, false, true).Single<object>();
    }

    /// <summary>Tries to get an instance of the specified service.</summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static object TryGet(
      this IResolutionRoot root,
      Type service,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.TryGet<object>((Func<IEnumerable<object>>) (() => ResolutionExtensions.GetResolutionIterator(root, service, (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, true, true)));
    }

    /// <summary>
    /// Tries to get an instance of the specified service by using the first binding with the specified name.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static object TryGet(
      this IResolutionRoot root,
      Type service,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.TryGet<object>((Func<IEnumerable<object>>) (() => ResolutionExtensions.GetResolutionIterator(root, service, (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, true, false)));
    }

    /// <summary>
    /// Tries to get an instance of the specified service by using the first binding that matches the specified constraint.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service, or <see langword="null" /> if no implementation was available.</returns>
    public static object TryGet(
      this IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.TryGet<object>((Func<IEnumerable<object>>) (() => ResolutionExtensions.GetResolutionIterator(root, service, constraint, (IEnumerable<IParameter>) parameters, true, false)));
    }

    /// <summary>
    /// Gets all available instances of the specified service.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>A series of instances of the service.</returns>
    public static IEnumerable<object> GetAll(
      this IResolutionRoot root,
      Type service,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, service, (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, true, false);
    }

    /// <summary>
    /// Gets all instances of the specified service using bindings registered with the specified name.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>A series of instances of the service.</returns>
    public static IEnumerable<object> GetAll(
      this IResolutionRoot root,
      Type service,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, service, (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, true, false);
    }

    /// <summary>
    /// Gets all instances of the specified service by using the bindings that match the specified constraint.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="constraint">The constraint to apply to the bindings.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>A series of instances of the service.</returns>
    public static IEnumerable<object> GetAll(
      this IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, service, constraint, (IEnumerable<IParameter>) parameters, true, false);
    }

    /// <summary>
    /// Evaluates if an instance of the specified service can be resolved.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static bool CanResolve<T>(this IResolutionRoot root, params IParameter[] parameters)
    {
      return ResolutionExtensions.CanResolve(root, typeof (T), (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, false, true);
    }

    /// <summary>
    /// Evaluates if  an instance of the specified service by using the first binding with the specified name can be resolved.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static bool CanResolve<T>(
      this IResolutionRoot root,
      string name,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.CanResolve(root, typeof (T), (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, false, true);
    }

    /// <summary>
    /// Evaluates if  an instance of the specified service by using the first binding that matches the specified constraint can be resolved.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <param name="root">The resolution root.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static bool CanResolve<T>(
      this IResolutionRoot root,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return ResolutionExtensions.CanResolve(root, typeof (T), constraint, (IEnumerable<IParameter>) parameters, false, true);
    }

    /// <summary>Gets an instance of the specified service.</summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static object CanResolve(
      this IResolutionRoot root,
      Type service,
      params IParameter[] parameters)
    {
      return (object) ResolutionExtensions.CanResolve(root, service, (Func<IBindingMetadata, bool>) null, (IEnumerable<IParameter>) parameters, false, true);
    }

    /// <summary>
    /// Gets an instance of the specified service by using the first binding with the specified name.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="name">The name of the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static object CanResolve(
      this IResolutionRoot root,
      Type service,
      string name,
      params IParameter[] parameters)
    {
      return (object) ResolutionExtensions.CanResolve(root, service, (Func<IBindingMetadata, bool>) (b => b.Name == name), (IEnumerable<IParameter>) parameters, false, true);
    }

    /// <summary>
    /// Gets an instance of the specified service by using the first binding that matches the specified constraint.
    /// </summary>
    /// <param name="root">The resolution root.</param>
    /// <param name="service">The service to resolve.</param>
    /// <param name="constraint">The constraint to apply to the binding.</param>
    /// <param name="parameters">The parameters to pass to the request.</param>
    /// <returns>An instance of the service.</returns>
    public static object CanResolve(
      this IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      params IParameter[] parameters)
    {
      return (object) ResolutionExtensions.CanResolve(root, service, constraint, (IEnumerable<IParameter>) parameters, false, true);
    }

    private static bool CanResolve(
      IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters,
      bool isOptional,
      bool isUnique)
    {
      Ensure.ArgumentNotNull((object) root, nameof (root));
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parameters, nameof (parameters));
      IRequest request = root.CreateRequest(service, constraint, parameters, isOptional, isUnique);
      return root.CanResolve(request);
    }

    private static IEnumerable<object> GetResolutionIterator(
      IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters,
      bool isOptional,
      bool isUnique)
    {
      Ensure.ArgumentNotNull((object) root, nameof (root));
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parameters, nameof (parameters));
      IRequest request = root.CreateRequest(service, constraint, parameters, isOptional, isUnique);
      return root.Resolve(request);
    }

    private static IEnumerable<object> GetResolutionIterator(
      IResolutionRoot root,
      Type service,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters,
      bool isOptional,
      bool isUnique,
      bool forceUnique)
    {
      Ensure.ArgumentNotNull((object) root, nameof (root));
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parameters, nameof (parameters));
      IRequest request = root.CreateRequest(service, constraint, parameters, isOptional, isUnique);
      request.ForceUnique = forceUnique;
      return root.Resolve(request);
    }

    private static T TryGet<T>(Func<IEnumerable<T>> iterator)
    {
      try
      {
        return iterator().SingleOrDefault<T>();
      }
      catch (ActivationException ex)
      {
        return default (T);
      }
    }

    private static T DoTryGetAndThrowOnInvalidBinding<T>(
      IResolutionRoot root,
      Func<IBindingMetadata, bool> constraint,
      IEnumerable<IParameter> parameters)
    {
      return ResolutionExtensions.GetResolutionIterator(root, typeof (T), constraint, parameters, true, true, true).Cast<T>().SingleOrDefault<T>();
    }
  }
}
