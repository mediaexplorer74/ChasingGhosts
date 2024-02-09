// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Targets.ITarget
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Planning.Bindings;
using System;
using System.Reflection;

#nullable disable
namespace Ninject.Planning.Targets
{
  /// <summary>
  /// Represents a site on a type where a value will be injected.
  /// </summary>
  public interface ITarget
  {
    /// <summary>Gets the type of the target.</summary>
    Type Type { get; }

    /// <summary>Gets the name of the target.</summary>
    string Name { get; }

    /// <summary>Gets the member that contains the target.</summary>
    MemberInfo Member { get; }

    /// <summary>Gets the constraint defined on the target.</summary>
    Func<IBindingMetadata, bool> Constraint { get; }

    /// <summary>
    /// Gets a value indicating whether the target represents an optional dependency.
    /// </summary>
    bool IsOptional { get; }

    /// <summary>
    /// Gets a value indicating whether the target has a default value.
    /// </summary>
    bool HasDefaultValue { get; }

    /// <summary>Gets the default value for the target.</summary>
    /// <exception cref="T:System.InvalidOperationException">If the item does not have a default value.</exception>
    object DefaultValue { get; }

    /// <summary>
    /// Resolves a value for the target within the specified parent context.
    /// </summary>
    /// <param name="parent">The parent context.</param>
    /// <returns>The resolved value.</returns>
    object ResolveWithin(IContext parent);

    /// <summary>Determines if an attribute is defined on the target</summary>
    /// <param name="attributeType">Type of attribute</param>
    /// <param name="inherit">Check base types</param>
    /// <returns></returns>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    /// Determines if an attribute is defined on the target's parent
    /// </summary>
    /// <param name="attributeType">Type of attribute</param>
    /// <param name="parent">Parent type to check</param>
    /// <returns></returns>
    bool IsDefinedOnParent(Type attributeType, Type parent);
  }
}
