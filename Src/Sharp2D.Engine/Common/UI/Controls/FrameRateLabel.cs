// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.FrameRateLabel
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Utility;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>
  ///     Label that displays the current frame rate. Set the <see cref="P:Sharp2D.Engine.Common.UI.Controls.FrameRateLabel.Text" />
  ///     yourself, but use {0} for average frame rate, and {1} for actual frame rate.
  ///     Actual frame rate updates once per second.
  /// </summary>
  public class FrameRateLabel : Label
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.FrameRateLabel" /> class.
    /// </summary>
    /// <param name="fontPath">The font path.</param>
    /// <param name="position">The position.</param>
    public FrameRateLabel(string fontPath, Vector2 position = default (Vector2))
      : base(fontPath, position)
    {
      this.Tint = Color.Yellow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.FrameRateLabel" /> class.
    /// </summary>
    /// <param name="fontPath">The font path.</param>
    /// <param name="text">The text.</param>
    /// <param name="position">The position.</param>
    public FrameRateLabel(string fontPath, string text, Vector2 position)
      : this(fontPath, position)
    {
      this.Text = text;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.FrameRateLabel" /> class.
    /// </summary>
    /// <param name="fontDefinition">The font definition.</param>
    /// <param name="position">The position.</param>
    public FrameRateLabel(FontDefinition fontDefinition, Vector2 position = default (Vector2))
      : base(fontDefinition, position)
    {
      this.Tint = Color.Yellow;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.FrameRateLabel" /> class.
    /// </summary>
    public FrameRateLabel() => this.Tint = Color.Yellow;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.FrameRateLabel" /> class.
    /// </summary>
    /// <param name="fontDefinition">The font definition.</param>
    /// <param name="avgFpsActualFps">The avg fps actual fps.</param>
    /// <param name="position">The position.</param>
    public FrameRateLabel(FontDefinition fontDefinition, string avgFpsActualFps, Vector2 position)
      : this(fontDefinition.FontPath, position)
    {
      this.Text = avgFpsActualFps;
    }

    /// <summary>
    ///     Gets or sets the text of the label, should include the formats for the FPS data.
    /// </summary>
    /// <value>The text.</value>
    public new string Text { get; set; }

    /// <summary>Draws the string and calls base.Draw.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (this.IsHidden)
        return;
      FrameCounter.Update(time);
      base.Text = string.Format(this.Text, new object[2]
      {
        (object) Math.Round((double) FrameCounter.GetAverageFramesPerSecond(), MidpointRounding.ToEven),
        (object) FrameCounter.GetRealFramesPerSecond()
      });
      base.Draw(batch, time);
    }
  }
}
