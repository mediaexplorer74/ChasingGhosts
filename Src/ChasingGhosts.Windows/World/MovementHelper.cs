// ChasingGhosts.Windows.World.MovementHelper

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Helper;
using System;

#nullable disable
namespace ChasingGhosts.Windows.World
{
  public static class MovementHelper
  {
    public static Movement GetMovement(Vector2 direction)
    {
      return direction == Vector2.Zero ? Movement.None : MovementHelper.GetMovement(MathHelper.ToDegrees((float) Math.Atan2((double) direction.Y, (double) direction.X)));
    }

    public static Movement GetMovement(float rotation)
    {
      rotation = SharpMathHelper.Loop(0.0f, 360f, rotation);
      if (MovementHelper.TestDirection(rotation, -45f, 45f) || (double) rotation > 315.0)
        return Movement.Right;
      if (MovementHelper.TestDirection(rotation, 45f, 135f))
        return Movement.Down;
      if (MovementHelper.TestDirection(rotation, 135f, 225f))
        return Movement.Left;
      return MovementHelper.TestDirection(rotation, 225f, 315f) ? Movement.Top : Movement.None;
    }

    private static bool TestDirection(float rotation, float start, float end)
    {
      if (TestDirection())
        return true;
      if ((double) start < 0.0)
        start = SharpMathHelper.Loop(0.0f, 360f, start);
      if ((double) end < 0.0)
        end = SharpMathHelper.Loop(0.0f, 360f, start);
      return TestDirection();

      bool TestDirection()
      {
        return (double) start < (double) rotation && (double) rotation <= (double) end;
      }
    }
  }
}
