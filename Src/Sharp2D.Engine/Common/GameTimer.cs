// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.GameTimer
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Exceptions;
using Sharp2D.Engine.Drawing;
using System;

#nullable disable
namespace Sharp2D.Engine.Common
{
  /// <summary>
  ///     A basic timer that executes after a set delay. Useful for a lot of stuff.
  /// </summary>
  public class GameTimer : Component
  {
    /// <summary>The _suppress redundant updates.</summary>
    private readonly bool suppressRedundantUpdates;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.GameTimer" /> class.
    /// </summary>
    /// <param name="duration">The duration.</param>
    /// <param name="suppressRedundantUpdates">
    /// The suppress redundant updates.
    /// </param>
    public GameTimer(TimeSpan duration, bool suppressRedundantUpdates = false)
    {
      this.Duration = duration;
      this.suppressRedundantUpdates = suppressRedundantUpdates;
      this.Restart();
    }

    /// <summary>The expired.</summary>
    public event EventHandler Expired;

    /// <summary>Gets the duration.</summary>
    public TimeSpan Duration { get; }

    /// <summary>Gets a value indicating whether finished.</summary>
    public bool Finished { get; private set; }

    /// <summary>Gets the remaining.</summary>
    public TimeSpan Remaining { get; private set; }

    public bool Looped { get; set; }

    /// <summary>The draw.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
    }

    /// <summary>The restart.</summary>
    public void Restart()
    {
      this.Remaining = this.Duration;
      this.Finished = false;
    }

    /// <summary>The update.</summary>
    /// <param name="gameTime">The game time.</param>
    /// <exception cref="T:Sharp2D.Engine.Common.Exceptions.DevelopmentMishapException">
    /// </exception>
    public override void Update(GameTime gameTime)
    {
      if (this.Finished)
      {
        if (!this.suppressRedundantUpdates)
          throw new DevelopmentMishapException("Dispose of a game timer when the timer has expired.");
      }
      else
      {
        this.Remaining -= gameTime.ElapsedGameTime;
        if (!(this.Remaining <= TimeSpan.Zero))
          return;
        this.Finished = true;
        this.OnExpired();
        if (this.Looped)
          this.Restart();
      }
    }

    /// <summary>The on expired.</summary>
    protected virtual void OnExpired()
    {
      EventHandler expired = this.Expired;
      if (expired == null)
        return;
      expired((object) this, EventArgs.Empty);
    }
  }
}
