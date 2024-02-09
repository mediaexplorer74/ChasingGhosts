// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Targets.Target`1
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Targets
{
  /// <summary>
  /// Represents a site on a type where a value can be injected.
  /// </summary>
  /// <typeparam name="T">The type of site this represents.</typeparam>
  public abstract class Target<T> : ITarget where T : ICustomAttributeProvider
  {
    private readonly Future<Func<IBindingMetadata, bool>> _constraint;
    private readonly Future<bool> _isOptional;

    /// <summary>Gets the member that contains the target.</summary>
    public MemberInfo Member { get; private set; }

    /// <summary>
    /// Gets or sets the site (property, parameter, etc.) represented by the target.
    /// </summary>
    public T Site { get; private set; }

    /// <summary>Gets the name of the target.</summary>
    public abstract string Name { get; }

    /// <summary>Gets the type of the target.</summary>
    public abstract Type Type { get; }

    /// <summary>Gets the constraint defined on the target.</summary>
    public Func<IBindingMetadata, bool> Constraint
    {
      get => (Func<IBindingMetadata, bool>) this._constraint;
    }

    /// <summary>
    /// Gets a value indicating whether the target represents an optional dependency.
    /// </summary>
    public bool IsOptional => (bool) this._isOptional;

    /// <summary>
    /// Gets a value indicating whether the target has a default value.
    /// </summary>
    public virtual bool HasDefaultValue => false;

    /// <summary>Gets the default value for the target.</summary>
    /// <exception cref="T:System.InvalidOperationException">If the item does not have a default value.</exception>
    public virtual object DefaultValue
    {
      get
      {
        throw new InvalidOperationException(ExceptionFormatter.TargetDoesNotHaveADefaultValue((ITarget) this));
      }
    }

    /// <summary>
    /// Initializes a new instance of the Target&lt;T&gt; class.
    /// </summary>
    /// <param name="member">The member that contains the target.</param>
    /// <param name="site">The site represented by the target.</param>
    protected Target(MemberInfo member, T site)
    {
      Ensure.ArgumentNotNull((object) member, nameof (member));
      Ensure.ArgumentNotNull((object) site, nameof (site));
      this.Member = member;
      this.Site = site;
      this._constraint = new Future<Func<IBindingMetadata, bool>>(new Func<Func<IBindingMetadata, bool>>(this.ReadConstraintFromTarget));
      this._isOptional = new Future<bool>(new Func<bool>(this.ReadOptionalFromTarget));
    }

    /// <summary>
    /// Returns an array of custom attributes of a specified type defined on the target.
    /// </summary>
    /// <param name="attributeType">The type of attribute to search for.</param>
    /// <param name="inherit">Whether to look up the hierarchy chain for inherited custom attributes.</param>
    /// <returns>An array of custom attributes of the specified type.</returns>
    public object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      Ensure.ArgumentNotNull((object) attributeType, nameof (attributeType));
      return this.Site.GetCustomAttributesExtended(attributeType, inherit);
    }

    /// <summary>
    /// Returns an array of custom attributes defined on the target.
    /// </summary>
    /// <param name="inherit">Whether to look up the hierarchy chain for inherited custom attributes.</param>
    /// <returns>An array of custom attributes.</returns>
    public object[] GetCustomAttributes(bool inherit) => this.Site.GetCustomAttributes(inherit);

    /// <summary>
    /// Returns a value indicating whether an attribute of the specified type is defined on the target.
    /// </summary>
    /// <param name="attributeType">The type of attribute to search for.</param>
    /// <param name="inherit">Whether to look up the hierarchy chain for inherited custom attributes.</param>
    /// <returns><c>True</c> if such an attribute is defined; otherwise <c>false</c>.</returns>
    public bool IsDefined(Type attributeType, bool inherit)
    {
      Ensure.ArgumentNotNull((object) attributeType, nameof (attributeType));
      return this.Site.IsDefined(attributeType, inherit);
    }

    /// <summary>Determines whether the parent has attribute.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="attributeType">The type of the attribute.</param>
    /// <returns>
    /// 	<c>true</c> if the specified member has attribute; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDefinedOnParent(Type attributeType, Type parent)
    {
      return ExtensionsForMemberInfo.HasAttribute(parent, attributeType);
    }

    /// <summary>
    /// Resolves a value for the target within the specified parent context.
    /// </summary>
    /// <param name="parent">The parent context.</param>
    /// <returns>The resolved value.</returns>
    public object ResolveWithin(IContext parent)
    {
      Ensure.ArgumentNotNull((object) parent, nameof (parent));
      if (this.Type.IsArray)
      {
        Type elementType = this.Type.GetElementType();
        return (object) this.GetValues(elementType, parent).CastSlow(elementType).ToArraySlow(elementType);
      }
      if (this.Type.IsGenericType)
      {
        Type genericTypeDefinition = this.Type.GetGenericTypeDefinition();
        Type genericArgument = this.Type.GetGenericArguments()[0];
        if (genericTypeDefinition == typeof (List<>) || genericTypeDefinition == typeof (IList<>) || genericTypeDefinition == typeof (ICollection<>))
          return (object) this.GetValues(genericArgument, parent).CastSlow(genericArgument).ToListSlow(genericArgument);
        if (genericTypeDefinition == typeof (IEnumerable<>))
          return (object) this.GetValues(genericArgument, parent).CastSlow(genericArgument);
      }
      return this.GetValue(this.Type, parent);
    }

    /// <summary>
    /// Gets the value(s) that should be injected into the target.
    /// </summary>
    /// <param name="service">The service that the target is requesting.</param>
    /// <param name="parent">The parent context in which the target is being injected.</param>
    /// <returns>A series of values that are available for injection.</returns>
    protected virtual IEnumerable<object> GetValues(Type service, IContext parent)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parent, nameof (parent));
      IRequest child = parent.Request.CreateChild(service, parent, (ITarget) this);
      child.IsOptional = true;
      return parent.Kernel.Resolve(child);
    }

    /// <summary>
    /// Gets the value that should be injected into the target.
    /// </summary>
    /// <param name="service">The service that the target is requesting.</param>
    /// <param name="parent">The parent context in which the target is being injected.</param>
    /// <returns>The value that is to be injected.</returns>
    protected virtual object GetValue(Type service, IContext parent)
    {
      Ensure.ArgumentNotNull((object) service, nameof (service));
      Ensure.ArgumentNotNull((object) parent, nameof (parent));
      IRequest child = parent.Request.CreateChild(service, parent, (ITarget) this);
      child.IsUnique = true;
      return parent.Kernel.Resolve(child).SingleOrDefault<object>();
    }

    /// <summary>
    /// Reads whether the target represents an optional dependency.
    /// </summary>
    /// <returns><see langword="True" /> if it is optional; otherwise <see langword="false" />.</returns>
    protected virtual bool ReadOptionalFromTarget()
    {
      return this.Site.HasAttribute(typeof (OptionalAttribute));
    }

    /// <summary>Reads the resolution constraint from target.</summary>
    /// <returns>The resolution constraint.</returns>
    protected virtual Func<IBindingMetadata, bool> ReadConstraintFromTarget()
    {
      ConstraintAttribute[] attributes = this.GetCustomAttributes(typeof (ConstraintAttribute), true) as ConstraintAttribute[];
      if (attributes == null || attributes.Length == 0)
        return (Func<IBindingMetadata, bool>) null;
      return attributes.Length == 1 ? new Func<IBindingMetadata, bool>(attributes[0].Matches) : (Func<IBindingMetadata, bool>) (metadata => ((IEnumerable<ConstraintAttribute>) attributes).All<ConstraintAttribute>((Func<ConstraintAttribute, bool>) (attribute => attribute.Matches(metadata))));
    }
  }
}
