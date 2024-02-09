// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Bootstrapping.DefaultGameBootstrapper
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Bootstrapping
{
  /// <summary>
  /// A default GameBootstrapper that does absolutely nothing regarding platform registration. Probably not a class you'd use for anything but PoCs.
  /// </summary>
  /// <seealso cref="!:GameBootstrapper" />
  public class DefaultGameBootstrapper
  {
    /// <summary>Registers all the platform-dependant services.</summary>
    public virtual void RegisterServices()
    {
    }
  }
}
