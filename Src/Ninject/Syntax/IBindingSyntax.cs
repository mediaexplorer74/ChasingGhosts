// Decompiled with JetBrains decompiler
// Type: Ninject.Syntax.IBindingSyntax
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure;

#nullable disable
namespace Ninject.Syntax
{
  /// <summary>Used to define a basic binding syntax builder.</summary>
  public interface IBindingSyntax : IHaveBindingConfiguration, IHaveKernel, IFluentSyntax
  {
  }
}
