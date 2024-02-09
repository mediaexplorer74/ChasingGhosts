// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.World.Camera.CameraTracker
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Utility;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.World.Camera
{
  /// <summary>
  ///     Camera tracker component.
  ///     A Camera Tracker is a component added to the Camera, and every update it will move the Camera closer to the players
  ///     position, until reaching the exact position.
  /// </summary>
  public class CameraTracker : Component
  {
    /// <summary>The _target position.</summary>
    private Vector2 targetPosition;
    /// <summary>The _target rotation.</summary>
    private float targetRotation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.World.Camera.CameraTracker" /> class.
    /// </summary>
    public CameraTracker()
    {
      this.EnablePositionTracking = true;
      this.EnableRotationTracking = false;
    }

    /// <summary>Gets the current position.</summary>
    /// <value>The current position.</value>
    public Vector2 CurrentPosition { get; private set; }

    /// <summary>Gets the current rotation.</summary>
    /// <value>The current rotation.</value>
    public float CurrentRotation { get; private set; }

    /// <summary>
    ///     Gets or sets a value indicating whether tracking of <see cref="P:Sharp2D.Engine.Common.World.Camera.CameraTracker.Target" />'s position is enabled.
    ///     <para>
    ///         If true, every <see cref="M:Sharp2D.Engine.Common.World.Camera.CameraTracker.Update(Microsoft.Xna.Framework.GameTime)" />, it will alter <see cref="P:Sharp2D.Engine.Common.World.Camera.CameraTracker.CurrentPosition" /> to reflect the current
    ///         position of target.
    ///     </para>
    /// </summary>
    /// <value>
    ///     <c>true</c> if [enable position tracking]; otherwise, <c>false</c>.
    /// </value>
    public bool EnablePositionTracking { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether tracking of <see cref="P:Sharp2D.Engine.Common.World.Camera.CameraTracker.Target" />'s rotation is enabled.
    ///     <para>
    ///         If true, every <see cref="M:Sharp2D.Engine.Common.World.Camera.CameraTracker.Update(Microsoft.Xna.Framework.GameTime)" />, it will alter <see cref="P:Sharp2D.Engine.Common.World.Camera.CameraTracker.CurrentRotation" /> to reflect the current
    ///         rotation of target.
    ///     </para>
    /// </summary>
    /// <value>
    ///     <c>true</c> if [enable rotation tracking]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableRotationTracking { get; set; }

    /// <summary>
    ///     Gets or sets the target this <see cref="T:Sharp2D.Engine.Common.World.Camera.CameraTracker" /> is tracking.
    /// </summary>
    /// <value>The target.</value>
    public GameObject Target { get; set; }

    /// <summary>Does nothing. Can't draw a Camera Tracker</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
    }

    /// <summary>
    /// Updates the Camera Tracker. If <see cref="P:Sharp2D.Engine.Common.World.Camera.CameraTracker.EnablePositionTracking" /> is true, updates position. Likewise for
    ///     <see cref="P:Sharp2D.Engine.Common.World.Camera.CameraTracker.EnableRotationTracking" />.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
      Sharp2D.Engine.Common.World.Camera.Camera parent = (Sharp2D.Engine.Common.World.Camera.Camera) this.Parent;
      if (this.Target != null)
      {
        if (this.EnablePositionTracking)
          this.targetPosition = this.Target.GlobalPosition;
        if (this.EnableRotationTracking)
          this.targetRotation = SharpMathHelper.Loop(0.0f, 6.28318548f, -MathHelper.ToRadians(this.Target.GlobalRotation));
      }
      TimeSpan elapsedGameTime;
      if (this.EnablePositionTracking)
      {
        Vector2 vector2_1 = this.targetPosition - this.CurrentPosition;
        float num1 = vector2_1.Length();
        if ((double) num1 > 0.0)
          vector2_1 /= num1;
        float num2 = (double) num1 >= 100.0 ? 1f : (float) Math.Pow((double) num1 / 10.0, 1.3);
        Vector2 currentPosition = this.CurrentPosition;
        Vector2 vector2_2 = 100f * vector2_1 * num2;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double totalSeconds = elapsedGameTime.TotalSeconds;
        Vector2 vector2_3 = vector2_2 * (float) totalSeconds;
        this.CurrentPosition = currentPosition + vector2_3;
        Vector2 simUnits = ConvertUnits.ToSimUnits(parent.GlobalPosition - parent.LocalPosition);
        parent.LocalPosition = this.CurrentPosition + simUnits;
      }
      if (!this.EnableRotationTracking)
        return;
      float num3 = this.targetRotation - this.CurrentRotation;
      float num4 = (double) Math.Abs(num3) >= 5.0 ? 1f : (float) Math.Pow((double) num3 / 5.0, 2.0);
      if ((double) Math.Abs(num3) > 0.0)
        num3 /= Math.Abs(num3);
      double currentRotation = (double) this.CurrentRotation;
      double num5 = 80.0 * (double) num3 * (double) num4;
      elapsedGameTime = gameTime.ElapsedGameTime;
      double totalSeconds1 = elapsedGameTime.TotalSeconds;
      double num6 = num5 * totalSeconds1;
      this.CurrentRotation = (float) (currentRotation + num6);
      float num7 = parent.GlobalRotation - parent.LocalRotation;
      parent.LocalRotation = this.CurrentRotation + num7;
    }
  }
}
