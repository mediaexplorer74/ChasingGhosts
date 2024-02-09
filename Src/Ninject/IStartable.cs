// Decompiled with JetBrains decompiler
// Type: Ninject.IStartable
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject
{
  /// <summary>
  /// A service that is started when activated, and stopped when deactivated.
  /// </summary>
  public interface IStartable
  {
    /// <summary>Starts this instance. Called during activation.</summary>
    void Start();

    /// <summary>Stops this instance. Called during deactivation.</summary>
    void Stop();
  }
}
