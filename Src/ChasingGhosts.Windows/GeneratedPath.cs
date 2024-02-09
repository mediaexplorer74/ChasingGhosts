// ChasingGhosts.Windows.GamePath

using ChasingGhosts.Windows.World;
using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ChasingGhosts.Windows
{
  public class GamePath : GameObject
  {
    private readonly Vector2[] dots;

    public GamePath(Vector2[] dots)
    {
      Vector2 root = ((IEnumerable<Vector2>) dots).First<Vector2>();
      if (root != Vector2.Zero)
        dots = ((IEnumerable<Vector2>) dots).Select<Vector2, Vector2>((Func<Vector2, Vector2>) (v => v - root)).ToArray<Vector2>();
      this.dots = dots;
    }

    public override void Initialize(IResolver resolver)
    {
      ShoeFoot foot = ShoeFoot.Right;
      for (int index = 0; index < this.dots.Length - 1; ++index)
      {
        Vector2 dot1 = this.dots[index];
        Vector2 dot2 = this.dots[index + 1];
        float degrees = MathHelper.ToDegrees((float) Math.Atan2((double) dot2.Y - (double) dot1.Y, (double) dot2.X - (double) dot1.X));
        while ((double) Vector2.Distance(dot1, dot2) > 40.0)
        {
          Vector2 vector2 = dot2 - dot1;
          vector2.Normalize();
          vector2 *= 40f;
          dot1 += vector2;
          this.Add((GameObject) new ShoePrint(dot1, degrees, foot));
          foot = foot == ShoeFoot.Left ? ShoeFoot.Right : ShoeFoot.Left;
        }
      }
      ShoePrint[] array = this.Children.OfType<ShoePrint>().ToArray<ShoePrint>();
      float num = (float) array.Length / 5f;
      for (int index = 0; index < array.Length; ++index)
      {
        if ((double) index >= (double) num * 4.0)
          array[index].Level = 4;
        else if ((double) index >= (double) num * 3.0)
          array[index].Level = 3;
        else if ((double) index >= (double) num * 2.0)
          array[index].Level = 2;
        else if ((double) index >= (double) num * 1.0)
          array[index].Level = 1;
      }
      base.Initialize(resolver);
    }
  }
}
