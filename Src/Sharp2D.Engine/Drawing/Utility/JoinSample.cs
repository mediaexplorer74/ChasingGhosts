// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Utility.JoinSample
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Drawing.Utility
{
  internal struct JoinSample
  {
    public Vector2 PointA;
    public Vector2 PointB;
    public Vector2 PointC;
    public float LengthA;
    public float LengthB;
    public float LengthC;

    public JoinSample(Vector2 pointA, Vector2 pointB, Vector2 pointC)
    {
      this.PointA = pointA;
      this.PointB = pointB;
      this.PointC = pointC;
      this.LengthA = 0.0f;
      this.LengthB = 0.0f;
      this.LengthC = 0.0f;
    }

    public JoinSample(
      Vector2 pointA,
      Vector2 pointB,
      Vector2 pointC,
      float lengthA,
      float lengthB,
      float lengthC)
    {
      this.PointA = pointA;
      this.PointB = pointB;
      this.PointC = pointC;
      this.LengthA = lengthA;
      this.LengthB = lengthB;
      this.LengthC = lengthC;
    }

    public void Advance(Vector2 nextPoint)
    {
      this.PointA = this.PointB;
      this.PointB = this.PointC;
      this.PointC = nextPoint;
    }

    public void Advance(Vector2 nextPoint, float nextLength)
    {
      this.PointA = this.PointB;
      this.PointB = this.PointC;
      this.PointC = nextPoint;
      this.LengthA = this.LengthB;
      this.LengthB = this.LengthC;
      this.LengthC = nextLength;
    }
  }
}
