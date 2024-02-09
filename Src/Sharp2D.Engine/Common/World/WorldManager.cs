// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.World.WorldManager
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;

#nullable disable
namespace Sharp2D.Engine.Common.World
{
  /// <summary>The world manager.</summary>
  public class WorldManager : GameObject, IRootObject
  {
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.World.WorldManager" /> class.
    /// </summary>
    public WorldManager() => this.IsGenerated = true;

    /// <summary>The initialize.</summary>
    /// <param name="resolver"></param>
    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.Name = "World";
    }

    /// <summary>Updates the world.</summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      if (this.IsPaused)
        return;
      base.Update(time);
    }
  }
}
