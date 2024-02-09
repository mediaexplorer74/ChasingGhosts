// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.Predefined.ScaleAnimation
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Helper;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations.Predefined
{
  /// <summary>
  /// A default scale animation that animates the parents scale between 2 values.
  /// </summary>
  /// <seealso cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" />
  public class ScaleAnimation : ValueAnimator
  {
    /// <summary>The destination scale</summary>
    private readonly Vector2 destinationScale;
    /// <summary>The start scale</summary>
    private readonly Vector2 startScale;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.Predefined.ScaleAnimation" /> class.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <param name="startScale">The start scale.</param>
    /// <param name="destinationScale">The destination scale.</param>
    public ScaleAnimation(TimeSpan time, Vector2 startScale, Vector2 destinationScale)
      : base(time)
    {
      this.startScale = startScale;
      this.destinationScale = destinationScale;
    }

    /// <summary>Stops this value animator and resets all values.</summary>
    public override void Stop()
    {
      base.Stop();
      this.Parent.Sprite.Scale = this.Inverse ? this.startScale : this.destinationScale;
    }

    /// <summary>Updates the value.</summary>
    /// <param name="value">The value.</param>
    protected override void UpdateValue(float value)
    {
      bool flag1 = (double) this.destinationScale.X > (double) this.startScale.X;
      bool flag2 = (double) this.destinationScale.Y > (double) this.startScale.Y;
      float x;
      SharpMathHelper.SetPercentage(flag1 ? value : 1f - value, flag1 ? this.startScale.X : this.destinationScale.X, flag1 ? this.destinationScale.X : this.startScale.X, out x);
      float y;
      SharpMathHelper.SetPercentage(flag2 ? value : 1f - value, flag2 ? this.startScale.Y : this.destinationScale.Y, flag2 ? this.destinationScale.Y : this.startScale.Y, out y);
      this.Parent.Sprite.Scale = new Vector2(x, y);
    }
  }
}
