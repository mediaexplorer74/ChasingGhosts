// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.Predefined.TransparencyAnimator
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Helper;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations.Predefined
{
  /// <summary>
  /// Animates the transparency of a specific texture between the two values specified.
  /// Optionally specify a bounds, if you wish to stretch the texture.
  /// </summary>
  /// <seealso cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" />
  public class TransparencyAnimator : ValueAnimator
  {
    /// <summary>The texture</summary>
    private readonly Texture2D texture;
    /// <summary>The minimum transparency</summary>
    private readonly float minTransparency;
    /// <summary>The maximum transparency</summary>
    private readonly float maxTransparency;
    /// <summary>The region</summary>
    private readonly Rectanglef region;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.Predefined.TransparencyAnimator" /> class.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <param name="texture">The texture.</param>
    /// <param name="minTransparency">The minimum transparency.</param>
    /// <param name="maxTransparency">The maximum transparency.</param>
    /// <param name="region">The region.</param>
    public TransparencyAnimator(
      TimeSpan time,
      Texture2D texture,
      float minTransparency,
      float maxTransparency,
      Rectanglef? region = null)
      : base(time)
    {
      this.texture = texture;
      this.minTransparency = minTransparency;
      this.maxTransparency = maxTransparency;
      this.region = region ?? new Rectanglef(texture.Bounds);
    }

    /// <summary>
    /// Draws this object to the screen.
    /// <para>
    /// Does nothing for a value animator.
    /// </para>
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      base.Draw(batch, time);
      float num;
      SharpMathHelper.SetPercentage(this.Percentage, this.minTransparency, this.maxTransparency, out num);
      batch.Draw(this.texture, this.region.ToRect(), Color.White * num);
    }

    /// <summary>Updates the value.</summary>
    /// <param name="value">The value.</param>
    protected override void UpdateValue(float value)
    {
    }
  }
}
