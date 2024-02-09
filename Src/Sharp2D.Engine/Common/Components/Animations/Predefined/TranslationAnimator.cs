// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.Predefined.TranslationAnimator
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations.Predefined
{
  /// <summary>
  /// Animates movement between the Start and Destination Vector2 positions.
  /// </summary>
  /// <seealso cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" />
  public class TranslationAnimator : ValueAnimator
  {
    /// <summary>Gets or sets the source.</summary>
    /// <value>The source.</value>
    public Vector2 Source { get; set; }

    /// <summary>Gets or sets the destination.</summary>
    /// <value>The destination.</value>
    public Vector2 Destination { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.Predefined.TranslationAnimator" /> class.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public TranslationAnimator(TimeSpan time, Vector2 source, Vector2 destination)
      : base(time)
    {
      this.Source = source;
      this.Destination = destination;
    }

    public TranslationAnimator()
    {
    }

    /// <summary>
    /// Creates an instance of <see cref="T:Sharp2D.Engine.Common.Components.Animations.Predefined.TranslationAnimator" /> that gets attached to
    /// <see cref="!:parent" />, animates for the duration specified, and removes itself again.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="start">The start.</param>
    /// <param name="dest">The dest.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="easing">The easing.</param>
    /// <returns></returns>
    public static async Task Animate(
      GameObject parent,
      Vector2 start,
      Vector2 dest,
      TimeSpan duration,
      AnimationEase? easing = null)
    {
      AnimationEase e = (AnimationEase) (easing ?? (AnimationEase)9);
      parent.LocalPosition = start;
      TranslationAnimator translationAnimator = new TranslationAnimator(duration, start, dest);
      translationAnimator.Easing = e;
      TranslationAnimator anim = translationAnimator;
      parent.Components.Add((Component) anim);
      anim.Start();
      await anim.WaitAsync();
      parent.Components.Remove((Component) anim);
    }

    /// <summary>Updates the value.</summary>
    /// <param name="value">The value.</param>
    protected override void UpdateValue(float value)
    {
      Vector2 vector2 = this.Destination - this.Source;
      this.Parent.LocalPosition = new Vector2(this.Source.X + vector2.X * value, this.Source.Y + vector2.Y * value);
    }
  }
}
