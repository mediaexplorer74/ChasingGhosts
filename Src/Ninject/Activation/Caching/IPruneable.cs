// Decompiled with JetBrains decompiler
// Type: Ninject.Activation.Caching.IPruneable
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject.Activation.Caching
{
  /// <summary>An object that is pruneable.</summary>
  public interface IPruneable
  {
    /// <summary>
    /// Removes instances from the cache which should no longer be re-used.
    /// </summary>
    void Prune();
  }
}
