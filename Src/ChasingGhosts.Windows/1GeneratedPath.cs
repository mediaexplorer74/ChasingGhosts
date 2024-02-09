// ChasingGhosts.Windows.ShoePrint

using ChasingGhosts.Windows.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Animations;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using System;

#nullable disable
namespace ChasingGhosts.Windows
{
  public class ShoePrint : GameObject, IShoePrint
  {
    private readonly Sprite shoeSprite;

    public ShoePrint(Vector2 localPosition, float turnDegrees, ShoeFoot foot)
    {
      this.LocalPosition = localPosition;
      this.LocalRotation = turnDegrees;
      this.shoeSprite = Sprite.Load("shoe");
      this.shoeSprite.Scale = new Vector2(0.8f);
      this.shoeSprite.CenterObject();
      this.shoeSprite.SpriteEffect = foot == ShoeFoot.Right ? SpriteEffects.None : SpriteEffects.FlipVertically;
      this.Add(new GameObject()
      {
        LocalPosition = new Vector2(0.0f, foot == ShoeFoot.Right ? 10f : -10f),
        Components = {
          (Component) this.shoeSprite
        }
      });
    }

    public bool IsActive { get; private set; } = true;

    public Color Tint
    {
      get => this.shoeSprite.Tint;
      set => this.shoeSprite.Tint = value;
    }

    public int Level { get; set; }

    public void Dismiss()
    {
      this.IsActive = false;
      ValueAnimator valueAnimator = ValueAnimator.PlayAnimation((GameObject) this, (Action<float>) (val => this.shoeSprite.Tint = Color.White * val), TimeSpan.FromSeconds(0.5));
      valueAnimator.Easing = AnimationEase.CubicEaseOut;
      valueAnimator.Loop = false;
      valueAnimator.Inverse = true;
    }
  }
}
