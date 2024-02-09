// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.Predefined.EventValueAnimator
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations.Predefined
{
  /// <summary>
  /// A basic value animator that invokes <see cref="E:Sharp2D.Engine.Common.Components.Animations.Predefined.EventValueAnimator.ValueUpdated" /> every time the value updates.
  /// Use this if you want to make a very simple animation that doesn't need an inheriting class.
  /// </summary>
  /// <seealso cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" />
  public sealed class EventValueAnimator : ValueAnimator
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.Predefined.EventValueAnimator" /> class.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <param name="loop">if set to <c>true</c> [loop].</param>
    public EventValueAnimator(TimeSpan time, bool loop = false)
      : base(time, loop)
    {
    }

    /// <summary>Updates the value.</summary>
    /// <param name="value">The value.</param>
    protected override void UpdateValue(float value)
    {
      EventHandler<float> valueUpdated = this.ValueUpdated;
      if (valueUpdated == null)
        return;
      valueUpdated((object) this, value);
    }

    /// <summary>Occurs when the animating value is changed.</summary>
    public event EventHandler<float> ValueUpdated;
  }
}
