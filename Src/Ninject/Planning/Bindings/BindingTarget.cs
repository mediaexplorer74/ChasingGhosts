// Decompiled with JetBrains decompiler
// Type: Ninject.Planning.Bindings.BindingTarget
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject.Planning.Bindings
{
  /// <summary>Describes the target of a binding.</summary>
  public enum BindingTarget
  {
    /// <summary>Indicates that the binding is from a type to itself.</summary>
    Self,
    /// <summary>
    /// Indicates that the binding is from one type to another.
    /// </summary>
    Type,
    /// <summary>
    /// Indicates that the binding is from a type to a provider.
    /// </summary>
    Provider,
    /// <summary>
    /// Indicates that the binding is from a type to a callback method.
    /// </summary>
    Method,
    /// <summary>
    /// Indicates that the binding is from a type to a constant value.
    /// </summary>
    Constant,
  }
}
