// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Particles.Particle
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Helper;
using System;

#nullable disable
namespace Sharp2D.Engine.Particles
{
  /// <summary>The particle.</summary>
  public class Particle : GameObject
  {
    /// <summary>The _start duration.</summary>
    private TimeSpan startDuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Particles.Particle" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="texture">The texture.</param>
    public Particle(Vector2 position, Sprite texture)
      : base(position, texture)
    {
      this.Sprite.CenterObject();
    }

    /// <summary>Gets or sets the angle.</summary>
    public float Angle { get; set; }

    /// <summary>Gets or sets the angular velocity.</summary>
    public float AngularVelocity { get; set; }

    /// <summary>Gets the duration.</summary>
    public TimeSpan Duration { get; private set; }

    /// <summary>Gets or sets the end color.</summary>
    public Color EndColor { get; set; }

    /// <summary>Gets or sets the start color.</summary>
    public Color StartColor { get; set; }

    /// <summary>Gets or sets the start duration.</summary>
    public TimeSpan StartDuration
    {
      get => this.startDuration;
      set
      {
        this.startDuration = value;
        this.Duration = value;
      }
    }

    /// <summary>Gets or sets the velocity.</summary>
    public Vector2 Velocity { get; set; }

    /// <summary>The update.</summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      base.Update(time);
      this.Duration -= time.ElapsedGameTime;
      this.LocalPosition = this.LocalPosition + this.Velocity;
      this.Angle += this.AngularVelocity;
      this.LocalRotation = this.Angle;
      TimeSpan timeSpan = this.StartDuration;
      double totalSeconds1 = timeSpan.TotalSeconds;
      timeSpan = this.Duration;
      double totalSeconds2 = timeSpan.TotalSeconds;
      float percentage1 = SharpMathHelper.GetPercentage((float) totalSeconds1, (float) totalSeconds2, 0.0f);
      double percentage2 = (double) percentage1;
      Color color1 = this.StartColor;
      double r1 = (double) color1.R;
      color1 = this.EndColor;
      double r2 = (double) color1.R;
      float num1 = 0;
      ref float local1 = ref num1;
      SharpMathHelper.SetPercentage((float) percentage2, (float) r1, (float) r2, out local1);
      double percentage3 = (double) percentage1;
      Color color2 = this.StartColor;
      double g1 = (double) color2.G;
      color2 = this.EndColor;
      double g2 = (double) color2.G;
      float num2 = 0;
      ref float local2 = ref num2;
      SharpMathHelper.SetPercentage((float) percentage3, (float) g1, (float) g2, out local2);
      double percentage4 = (double) percentage1;
      Color color3 = this.StartColor;
      double b1 = (double) color3.B;
      color3 = this.EndColor;
      double b2 = (double) color3.B;
      float num3 = 0;
      ref float local3 = ref num3;
      SharpMathHelper.SetPercentage((float) percentage4, (float) b1, (float) b2, out local3);
      this.Sprite.Tint = new Color(num1 / (float) byte.MaxValue, num2 / (float) byte.MaxValue, num3 / (float) byte.MaxValue);
    }
  }
}
