// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.Predefined.SpriteSheetAnimation
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations.Predefined
{
  /// <summary>
  /// Animates through all frames in a SpriteSheet over the duration specified.
  /// </summary>
  /// <seealso cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" />
  public class SpriteSheetAnimation : ValueAnimator
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.Predefined.SpriteSheetAnimation" /> class.
    /// </summary>
    /// <param name="assetName">Name of the asset.</param>
    /// <param name="size">The size.</param>
    /// <param name="count">The count.</param>
    /// <param name="duration">The duration.</param>
    public SpriteSheetAnimation(SpriteSheet<int> spritesheet, TimeSpan duration)
      : base(duration)
    {
      this.Sprite = spritesheet;
      this.IsVisible = false;
      this.StartIndex = 0;
      this.EndIndex = spritesheet.Regions.Count;
    }

    /// <summary>Gets or sets the end index.</summary>
    /// <value>The end index.</value>
    public int EndIndex { get; set; }

    /// <summary>Gets or sets the start index.</summary>
    /// <value>The start index.</value>
    public int StartIndex { get; set; }

    /// <summary>The Sprite</summary>
    public SpriteSheet<int> Sprite { get; set; }

    /// <summary>Updates the value.</summary>
    /// <param name="value">The value.</param>
    protected override void UpdateValue(float value)
    {
      SpriteRegions<int> regions = this.Sprite.Regions;
      SharpMathHelper.SetPercentage(value, (float) this.StartIndex, (float) this.EndIndex, out value);
      value = SharpMathHelper.Loop((float) this.StartIndex, (float) this.EndIndex, value);
      int newIndex = (int) Math.Round((double) value, MidpointRounding.ToEven);
      if (regions.All<KeyValuePair<int, SpriteFrame>>((Func<KeyValuePair<int, SpriteFrame>, bool>) (pair => pair.Key != newIndex)))
        newIndex = this.StartIndex;
      this.Sprite.RegionKey = newIndex;
    }
  }
}
