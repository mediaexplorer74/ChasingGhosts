// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.SharpMathHelper
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>
  ///     A tool for assistance in mathematical situations all around the engine
  /// </summary>
  public static class SharpMathHelper
  {
    /// <summary>
    /// Gets the percentage value based on the <see cref="!:current" /> difference between <see cref="!:min" /> and
    /// <see cref="!:max" />
    /// </summary>
    /// <example>Current is 0, min is -5, max is 5. The return value would be 50</example>
    /// <param name="min">The minimum value.</param>
    /// <param name="current">The current value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>Returns the percentage value.</returns>
    public static float GetPercentage(float min, float current, float max)
    {
      if ((double) min < 0.0)
      {
        current -= min;
        max -= min;
        min -= min;
      }
      float num = max - min;
      float percentage = (current - min) / num;
      if ((double) percentage <= 1.0)
        ;
      return percentage;
    }

    /// <summary>Maps a value to be within the Min and Max value.</summary>
    /// <example>Min is 50, max is 150, value is 200. Return value will be 100, because 'value - (max - min) == 100'</example>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static float Loop(float min, float max, float value)
    {
      float num = value;
      do
      {
        if ((double) num < (double) min)
          num += max - min;
        else if ((double) num > (double) max)
          num -= max - min;
      }
      while ((double) num < (double) min || (double) num > (double) max);
      return num;
    }

    /// <summary>
    /// Rotates the specified point around the origin, returning the new position.
    /// <para>This should technically always be the same distance away from origin as before, however the return value will be different.</para>
    /// </summary>
    /// <param name="pointToRotate">The point.</param>
    /// <param name="centerPoint">The origin.</param>
    /// <param name="angleDegree">The angle degree.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public static Vector2 Rotate(Vector2 pointToRotate, Vector2 centerPoint, float angleDegree)
    {
      if ((double) angleDegree == 0.0 || pointToRotate == centerPoint)
        return pointToRotate;
      double radians = (double) MathHelper.ToRadians(angleDegree);
      double num1 = Math.Cos(radians);
      double num2 = Math.Sin(radians);
      float num3 = pointToRotate.X - centerPoint.X;
      float num4 = pointToRotate.Y - centerPoint.Y;
      return new Vector2()
      {
        X = (float) (num1 * (double) num3 - num2 * (double) num4) + centerPoint.X,
        Y = (float) (num2 * (double) num3 + num1 * (double) num4) + centerPoint.Y
      };
    }

    /// <summary>
    /// Sets the absolute value based on the <see cref="!:percentage" /> difference between <see cref="!:min" /> and
    /// <see cref="!:max" />
    /// </summary>
    /// <param name="percentage">The percentage value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="value">Outputs the value.</param>
    public static void SetPercentage(float percentage, float min, float max, out float value)
    {
      value = min + percentage * (max - min);
    }
  }
}
