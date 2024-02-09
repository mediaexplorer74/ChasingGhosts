// ChasingGhosts.Windows.World.ShoePowerup

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Utility;

#nullable disable
namespace ChasingGhosts.Windows.World
{
  public class ShoePowerup : GameObject
  {
    public ShoeType Powerup { get; set; }

    public override void Initialize(IResolver resolver)
    {
      SpriteSheet<int> spriteSheet1 = new SpriteSheet<int>();
      SpriteRegions<int> spriteRegions = new SpriteRegions<int>();
      spriteRegions.Add(0, new SpriteFrame("shoes_icon", new Rectangle(0, 0, 16, 16)));
      spriteRegions.Add(1, new SpriteFrame("shoes_icon", new Rectangle(16, 0, 16, 16)));
      spriteRegions.Add(2, new SpriteFrame("shoes_icon", new Rectangle(32, 0, 16, 16)));
      spriteSheet1.Regions = spriteRegions;
      spriteSheet1.Scale = new Vector2(3f);
      SpriteSheet<int> spriteSheet2 = spriteSheet1;
      spriteSheet2.CenterObject();
      switch (this.Powerup)
      {
        case ShoeType.Sneakers:
          spriteSheet2.RegionKey = 0;
          break;
        case ShoeType.Rollerblades:
          spriteSheet2.RegionKey = 1;
          break;
        case ShoeType.FlipFlops:
          spriteSheet2.RegionKey = 2;
          break;
      }
      this.Components.Add((Component) spriteSheet2);
      base.Initialize(resolver);
    }

    public void Dismiss() => this.Parent.Remove((GameObject) this);
  }
}
