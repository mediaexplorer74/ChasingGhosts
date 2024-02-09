// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Utility.FrameCounter
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Utility
{
  /// <summary>Frame Counter, for getting FPS in the game.</summary>
  /// <author>
  ///     Original source code from CraftWorkGames: http://stackoverflow.com/users/1596973/craftworkgames
  ///     Modified by Jeff Hansen.
  /// </author>
  public class FrameCounter
  {
    /// <summary>The maximum amount of samples to average on.</summary>
    public const int MaximumSamples = 100;
    /// <summary>A singleton instance.</summary>
    public static FrameCounter Instance = new FrameCounter();

    /// <summary>The _sample buffer.</summary>
    private readonly Queue<float> sampleBuffer = new Queue<float>();
    /// <summary>The _real Frame counter.</summary>
    private int realFrameCounter;
    /// <summary>The elapsed.</summary>
    private TimeSpan elapsed = TimeSpan.Zero;

    /// <summary>Gets the average frames per second.</summary>
    /// <value>The average frames per second.</value>
    public float AverageFramesPerSecond { get; private set; }

    /// <summary>Gets the current frames per second.</summary>
    /// <value>The current frames per second.</value>
    public float CurrentFramesPerSecond { get; private set; }

    /// <summary>Gets the real frames per second.</summary>
    /// <value>The real frames per second.</value>
    public float RealFramesPerSecond { get; private set; }

    /// <summary>Gets the total frames.</summary>
    /// <value>The total frames.</value>
    public long TotalFrames { get; private set; }

    /// <summary>Gets the total seconds.</summary>
    /// <value>The total seconds.</value>
    public float TotalSeconds { get; private set; }

        /// <summary>Gets the average frames per second.</summary>
        /// <value>The average frames per second.</value>
        /// <returns>
        ///     The <see cref="T:System.Single" />.
        /// </returns>
        public static float GetAverageFramesPerSecond()
        {
            return FrameCounter.Instance.AverageFramesPerSecond;
        }

        /// <summary>
        ///     Gets the real frames per second.
        ///     That is, how many calls to draw were called in the last second.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Single" />.
        /// </returns>
        public static float GetRealFramesPerSecond()
        {
            return FrameCounter.Instance.RealFramesPerSecond;
        }

        /// <summary>
        /// Updates the singleton instance. Call this from within a Draw call.
        /// </summary>
        /// <param name="time">The time.</param>
        public static void Update(GameTime time)
    {
      FrameCounter.Instance.UpdateAverage((float) time.ElapsedGameTime.TotalSeconds);
      FrameCounter.Instance.UpdateReal(time);
    }

    /// <summary>
    /// Runs the magic that calculates the current average FPS.
    /// </summary>
    /// <param name="deltaTime">The delta time.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    public bool UpdateAverage(float deltaTime)
    {
      this.CurrentFramesPerSecond = 1f / deltaTime;
      this.sampleBuffer.Enqueue(this.CurrentFramesPerSecond);
      if (this.sampleBuffer.Count > 100)
      {
        double num = (double) this.sampleBuffer.Dequeue();
        this.AverageFramesPerSecond = this.sampleBuffer.Average<float>((Func<float, float>) (i => i));
      }
      else
        this.AverageFramesPerSecond = this.CurrentFramesPerSecond;
      ++this.TotalFrames;
      this.TotalSeconds += deltaTime;
      return true;
    }

    /// <summary>Updates the real.</summary>
    /// <param name="time">The time.</param>
    private void UpdateReal(GameTime time)
    {
      this.elapsed += time.ElapsedGameTime;
      TimeSpan timeSpan = TimeSpan.FromSeconds(1.0);
      if (this.elapsed > timeSpan)
      {
        this.elapsed -= timeSpan;
        this.RealFramesPerSecond = (float) this.realFrameCounter;
        this.realFrameCounter = 0;
      }
      ++this.realFrameCounter;
    }
  }
}
