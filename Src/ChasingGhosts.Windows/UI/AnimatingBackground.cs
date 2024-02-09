// ChasingGhosts.Windows.UI.AnimatingBackground

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.Components.Animations;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using System;

#nullable disable
namespace ChasingGhosts.Windows.UI
{
  public class AnimatingBackground : GameObject
  {
    private readonly Color color;
    private float transparency;
    private Texture2D text;

    public AnimatingBackground(Color color, float transparency)
    {
      this.color = color;
      this.transparency = transparency;
    }

    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.text = new Texture2D(resolver.Resolve<GraphicsDevice>(), 1, 1);
      this.text.SetData<Color>(new Color[1]{ Color.White });
      ValueAnimator.PlayAnimation((GameObject) this, (Action<float>) (val => this.transparency = val * 0.85f), TimeSpan.FromSeconds(1.0)).Easing = AnimationEase.CubicEaseOut;
    }

    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      Vector2 virtualScreen = Resolution.VirtualScreen;
      batch.Draw(this.text, new Rectangle(0, 0, (int) virtualScreen.X, (int) virtualScreen.Y), this.color * this.transparency);
      base.Draw(batch, time);
    }
  }
}
