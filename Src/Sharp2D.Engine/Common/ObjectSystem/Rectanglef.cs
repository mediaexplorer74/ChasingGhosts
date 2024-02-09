// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.ObjectSystem.Rectanglef
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.ObjectSystem
{
  public struct Rectanglef : IEquatable<Rectanglef>
  {
    public float X { get; set; }

    public float Y { get; set; }

    public float Width { get; set; }

    public float Height { get; set; }

    public Rectanglef(float x, float y, float width, float height)
    {
      this.X = x;
      this.Y = y;
      this.Width = width;
      this.Height = height;
    }

    public Rectanglef(Rectangle rect)
    {
      this.X = (float) rect.X;
      this.Y = (float) rect.Y;
      this.Width = (float) rect.Width;
      this.Height = (float) rect.Height;
    }

    public float Top => this.Y;

    public float Bottom => this.Y + this.Height;

    public float Left => this.X;

    public float Right => this.X + this.Width;

    public Vector2 Position => new Vector2(this.X, this.Y);

    public Vector2 Center => new Vector2(this.X + this.Width / 2f, this.Y + this.Height / 2f);

    public Vector2 BottomLeft => new Vector2(this.X, this.Bottom);

    public Vector2 BottomRight => new Vector2(this.Right, this.Bottom);

    public Vector2 TopLeft => new Vector2(this.X, this.Y);

    public Vector2 TopRight => new Vector2(this.Right, this.Y);

    public Vector2 Size => new Vector2(this.Width, this.Height);

    public bool Contains(int x, int y)
    {
      return (double) this.X < (double) x && (double) this.Y < (double) y && (double) this.Right > (double) x && (double) this.Bottom > (double) y;
    }

    public Rectangle ToRect()
    {
      return new Rectangle((int) this.X, (int) this.Y, (int) this.Width, (int) this.Height);
    }

    public bool Intersects(Rectanglef other)
    {
      return (double) this.Top < (double) other.Bottom && (double) this.Bottom > (double) other.Top && (double) this.Left < (double) other.Right && (double) this.Right > (double) other.Left;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (obj == (ValueType) this)
        return true;
      return obj is Rectanglef other ? this.Equals(other) : base.Equals(obj);
    }

    public bool Equals(Rectanglef other)
    {
      int num1;
      if (this.X.Equals(other.X))
      {
        float num2 = this.Y;
        if (num2.Equals(other.Y))
        {
          num2 = this.Width;
          if (num2.Equals(other.Width))
          {
            num2 = this.Height;
            num1 = num2.Equals(other.Height) ? 1 : 0;
            goto label_5;
          }
        }
      }
      num1 = 0;
label_5:
      return num1 != 0;
    }

    public override int GetHashCode()
    {
      return ((this.X.GetHashCode() * 397 ^ this.Y.GetHashCode()) * 397 ^ this.Width.GetHashCode()) * 397 ^ this.Height.GetHashCode();
    }

    public static bool operator ==(Rectanglef left, Rectanglef right) => left.Equals(right);

    public static bool operator !=(Rectanglef left, Rectanglef right) => !left.Equals(right);

    public override string ToString()
    {
      return string.Format("X: {0}, Y: {1}, W: {2}, H: {3}", (object) this.X, (object) this.Y, (object) this.Width, (object) this.Height);
    }

    public static Rectanglef operator -(Rectanglef self, Vector2 other)
    {
      return new Rectanglef(self.X - other.X, self.Y - other.Y, self.Width, self.Height);
    }
  }
}
