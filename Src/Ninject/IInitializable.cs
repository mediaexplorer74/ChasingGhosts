// Decompiled with JetBrains decompiler
// Type: Ninject.IInitializable
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject
{
  /// <summary>
  /// A service that requires initialization after it is activated.
  /// </summary>
  public interface IInitializable
  {
    /// <summary>Initializes the instance. Called during activation.</summary>
    void Initialize();
  }
}
