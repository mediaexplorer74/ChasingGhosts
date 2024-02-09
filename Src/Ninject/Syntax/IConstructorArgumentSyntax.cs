// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IConstructorArgumentSyntax
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>
  /// Passed to ToConstructor to specify that a constructor value is Injected.
  /// </summary>
  public interface IConstructorArgumentSyntax : IFluentSyntax
  {
    /// <summary>Gets the context.</summary>
    /// <value>The context.</value>
    IContext Context { get; }

    /// <summary>Specifies that the argument is injected.</summary>
    /// <typeparam name="T">The type of the parameter</typeparam>
    /// <returns>Not used. This interface has no implementation.</returns>
    T Inject<T>();
  }
}
