// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Blocks.IActivationBlock
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Infrastructure.Disposal;
using Ninject.Syntax;
using System;

#nullable disable
namespace Ninject.Activation.Blocks
{
  /// <summary>
  /// A block used for deterministic disposal of activated instances. When the block is
  /// disposed, all instances activated via it will be deactivated.
  /// </summary>
  public interface IActivationBlock : 
    IResolutionRoot,
    IFluentSyntax,
    INotifyWhenDisposed,
    IDisposableObject,
    IDisposable
  {
  }
}
