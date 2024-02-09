// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.ValueAnimator
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Animations.Predefined;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations
{
  /// <summary>
  ///     A basic value animator. Animates a <see cref="T:System.Single" /> value from 0 to 1 over the duration, and optionally loops,
  ///     starting from 0 again.
  ///     <para>
  ///         <see cref="M:Sharp2D.Engine.Common.Components.Animations.ValueAnimator.Start" /> to start it, <see cref="M:Sharp2D.Engine.Common.Components.Animations.ValueAnimator.Stop" /> to stop it. Implement <see cref="M:Sharp2D.Engine.Common.Components.Animations.ValueAnimator.UpdateValue(System.Single)" /> to use
  ///         the animated value.
  ///     </para>
  /// </summary>
  /// <seealso cref="T:Sharp2D.Engine.Common.Components.Component" />
  public abstract class ValueAnimator : Component
  {
    /// <summary>The current time</summary>
    private TimeSpan currentTime = TimeSpan.Zero;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" /> class.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <param name="loop">
    /// if set to <c>true</c> [loop].
    /// </param>
    protected ValueAnimator(TimeSpan time, bool loop = false)
      : this()
    {
      this.Duration = time;
      this.Loop = loop;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" /> class.
    /// </summary>
    protected ValueAnimator()
    {
      this.IsPaused = true;
      this.Easing = AnimationEase.Linear;
    }

    public TimeSpan Duration { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this <see cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" /> is loop.
    /// </summary>
    /// <value>
    ///     <c>true</c> if loop; otherwise, <c>false</c>.
    /// </value>
    public bool Loop { get; set; }

    /// <summary>
    ///     Gets the most recent percentage (Number from 0 to 1).
    /// </summary>
    /// <value>The percentage.</value>
    public float Percentage { get; private set; }

    /// <summary>Occurs when the animation finished.</summary>
    public event EventHandler Finished;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Sharp2D.Engine.Common.Components.Animations.ValueAnimator" /> is inverse.
    /// <para>Simply whether the value goes from 0 to 1, or 1 to 0. Really simple, and really neat :)</para>
    /// </summary>
    /// <value>
    ///   <c>true</c> if inverse; otherwise, <c>false</c>.
    /// </value>
    public bool Inverse { get; set; }

    /// <summary>
    ///     Starts this value animator. From now on, every <see cref="M:Sharp2D.Engine.Common.Components.Animations.ValueAnimator.Update(Microsoft.Xna.Framework.GameTime)" /> call will invoke <see cref="M:Sharp2D.Engine.Common.Components.Animations.ValueAnimator.UpdateValue(System.Single)" />.
    /// </summary>
    public virtual void Start()
    {
      if (!this.IsPaused)
        return;
      this.Restart();
    }

    /// <summary>Restarts this value animator.</summary>
    public virtual void Restart()
    {
      this.Percentage = this.Inverse ? 1f : 0.0f;
      this.IsPaused = false;
      this.currentTime = TimeSpan.Zero;
    }

    /// <summary>Stops this value animator and resets all values.</summary>
    public virtual void Stop()
    {
      this.IsPaused = true;
      this.currentTime = TimeSpan.Zero;
      EventHandler finished = this.Finished;
      if (finished == null)
        return;
      finished((object) this, EventArgs.Empty);
    }

    /// <summary>Gets or sets the easing.</summary>
    /// <value>The easing.</value>
    public AnimationEase Easing { get; set; }

    /// <summary>
    /// Called every Frame. This is where you want to handle your logic.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
      if (this.IsPaused)
        return;
      this.currentTime += gameTime.ElapsedGameTime;
      float val2 = (float) (this.currentTime.TotalMilliseconds / this.Duration.TotalMilliseconds);
      float num = this.ApplyEasing(Math.Max(0.0f, Math.Min(1f, val2)));
      this.Percentage = this.Inverse ? 1f - num : num;
      this.UpdateValue(this.Percentage);
      if ((double) val2 < 1.0)
        return;
      if (this.Loop)
        this.currentTime -= this.Duration;
      else
        this.Stop();
    }

    /// <summary>Applies the defined Easing effect to the value.</summary>
    /// <param name="percentage">The percentage.</param>
    /// <returns></returns>
    protected virtual float ApplyEasing(float percentage)
    {
      switch (this.Easing)
      {
        case AnimationEase.Linear:
          return percentage;
        case AnimationEase.SineEaseIn:
          return ScaleFuncs.SineEaseIn(percentage);
        case AnimationEase.SineEaseOut:
          return ScaleFuncs.SineEaseOut(percentage);
        case AnimationEase.SineEaseInOut:
          return ScaleFuncs.SineEaseInOut(percentage);
        case AnimationEase.QuadraticEaseIn:
          return ScaleFuncs.QuadraticEaseIn(percentage);
        case AnimationEase.QuadraticEaseOut:
          return ScaleFuncs.QuadraticEaseOut(percentage);
        case AnimationEase.QuadraticEaseInOut:
          return ScaleFuncs.QuadraticEaseInOut(percentage);
        case AnimationEase.CubicEaseIn:
          return ScaleFuncs.CubicEaseIn(percentage);
        case AnimationEase.CubicEaseOut:
          return ScaleFuncs.CubicEaseOut(percentage);
        case AnimationEase.CubicEaseInOut:
          return ScaleFuncs.CubicEaseInOut(percentage);
        case AnimationEase.QuarticEaseIn:
          return ScaleFuncs.QuarticEaseIn(percentage);
        case AnimationEase.QuarticEaseOut:
          return ScaleFuncs.QuarticEaseOut(percentage);
        case AnimationEase.QuarticEaseInOut:
          return ScaleFuncs.QuarticEaseInOut(percentage);
        case AnimationEase.QuinticEaseIn:
          return ScaleFuncs.QuinticEaseIn(percentage);
        case AnimationEase.QuinticEaseOut:
          return ScaleFuncs.QuinticEaseOut(percentage);
        case AnimationEase.QuinticEaseInOut:
          return ScaleFuncs.QuinticEaseInOut(percentage);
        case AnimationEase.SexticEaseIn:
          return ScaleFuncs.SexticEaseIn(percentage);
        case AnimationEase.SexticEaseOut:
          return ScaleFuncs.SexticEaseOut(percentage);
        case AnimationEase.SexticEaseInOut:
          return ScaleFuncs.SexticEaseInOut(percentage);
        case AnimationEase.SepticEaseIn:
          return ScaleFuncs.SepticEaseIn(percentage);
        case AnimationEase.SepticEaseOut:
          return ScaleFuncs.SepticEaseOut(percentage);
        case AnimationEase.SepticEaseInOut:
          return ScaleFuncs.SepticEaseInOut(percentage);
        default:
          return 0.0f;
      }
    }

    /// <summary>
    /// Draws this object to the screen.
    ///     <para>
    /// Does nothing for a value animator.
    ///     </para>
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
    }

    /// <summary>Updates the value.</summary>
    /// <param name="value">The value.</param>
    protected abstract void UpdateValue(float value);

    /// <summary>
    /// Creates and play a basic animation that invokes <see cref="!:valueChanged" />
    /// every time the animated value changes over the <see cref="!:duration" /> specified.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="valueChanged">The value changed.</param>
    /// <param name="duration">The duration.</param>
    public static ValueAnimator PlayAnimation(
      GameObject parent,
      Action<float> valueChanged,
      TimeSpan duration)
    {
      EventValueAnimator animation = new EventValueAnimator(duration);
      animation.ValueUpdated += (EventHandler<float>) ((sender, val) => valueChanged(val));
      animation.Finished += (EventHandler) ((sender, args) => parent.Components.Remove((Component) animation));
      parent.Components.Add((Component) animation);
      animation.Start();
      return (ValueAnimator) animation;
    }

    public async Task WaitAsync()
    {
      TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();
      EventHandler onFinished = (EventHandler) null;
      onFinished = (EventHandler) ((sender, args) =>
      {
        source.TrySetResult(true);
        this.Finished -= onFinished;
      });
      this.Finished += onFinished;
      if (this.IsPaused)
        this.Start();
      int num = await source.Task ? 1 : 0;
    }
  }
}
