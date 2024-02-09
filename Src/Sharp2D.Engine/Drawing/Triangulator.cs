// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Triangulator
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// Computes a set of interior triangles from a set of points that defines a containing path.
  /// </summary>
  public class Triangulator
  {
    private int[] _triPrev = new int[128];
    private int[] _triNext = new int[128];
    private int[] _indexComputeBuffer = new int[128];
    private int _indexCount = 0;

    /// <summary>
    /// The indexes of triangle list entries for the list of points used in the last <see cref="M:Sharp2D.Engine.Drawing.Triangulator.Triangulate(System.Collections.Generic.IList{Microsoft.Xna.Framework.Vector2},System.Int32,System.Int32)" /> call.
    /// </summary>
    public int[] ComputedIndexes => this._indexComputeBuffer;

    /// <summary>
    /// The number of indexes generated in the last computation.
    /// </summary>
    public int ComputedIndexCount => this._indexCount;

    /// <summary>
    /// Computes a triangle list that fully covers the area enclosed by the given set of points.
    /// </summary>
    /// <param name="points">A list of points that defines an enclosing path.</param>
    /// <param name="offset">The offset of the first point in the list.</param>
    /// <param name="count">The number of points in the path.</param>
    public void Triangulate(IList<Vector2> points, int offset, int count)
    {
      this.Initialize(count);
      int index1 = 0;
      int num1 = 0;
      while (count >= 3)
      {
        bool flag = true;
        Vector2 point1 = points[offset + this._triPrev[index1]];
        Vector2 point2 = points[offset + index1];
        Vector2 point3 = points[offset + this._triNext[index1]];
        if (this.TriangleIsCCW(point1, point2, point3))
        {
          int index2 = this._triNext[this._triNext[index1]];
          while (!this.PointInTriangleInclusive(points[offset + index2], point1, point2, point3))
          {
            index2 = this._triNext[index2];
            if (index2 == this._triPrev[index1])
              goto label_7;
          }
          flag = false;
        }
        else
          flag = false;
label_7:
        if (flag)
        {
          if (this._indexComputeBuffer.Length < num1 + 3)
            Array.Resize<int>(ref this._indexComputeBuffer, this._indexComputeBuffer.Length * 2);
          int[] indexComputeBuffer1 = this._indexComputeBuffer;
          int index3 = num1;
          int num2 = index3 + 1;
          int num3 = offset + this._triPrev[index1];
          indexComputeBuffer1[index3] = num3;
          int[] indexComputeBuffer2 = this._indexComputeBuffer;
          int index4 = num2;
          int num4 = index4 + 1;
          int num5 = offset + index1;
          indexComputeBuffer2[index4] = num5;
          int[] indexComputeBuffer3 = this._indexComputeBuffer;
          int index5 = num4;
          num1 = index5 + 1;
          int num6 = offset + this._triNext[index1];
          indexComputeBuffer3[index5] = num6;
          this._triNext[this._triPrev[index1]] = this._triNext[index1];
          this._triPrev[this._triNext[index1]] = this._triPrev[index1];
          --count;
          index1 = this._triPrev[index1];
        }
        else
          index1 = this._triNext[index1];
      }
      this._indexCount = num1;
    }

    private void Initialize(int count)
    {
      this._indexCount = 0;
      if (this._triNext.Length < count)
        Array.Resize<int>(ref this._triNext, Math.Max(this._triNext.Length * 2, count));
      if (this._triPrev.Length < count)
        Array.Resize<int>(ref this._triPrev, Math.Min(this._triPrev.Length * 2, count));
      for (int index = 0; index < count; ++index)
      {
        this._triPrev[index] = index - 1;
        this._triNext[index] = index + 1;
      }
      this._triPrev[0] = count - 1;
      this._triNext[count - 1] = 0;
    }

    private float Cross2D(Vector2 u, Vector2 v)
    {
      return (float) ((double) u.Y * (double) v.X - (double) u.X * (double) v.Y);
    }

    private bool PointInTriangleInclusive(Vector2 point, Vector2 a, Vector2 b, Vector2 c)
    {
      return (double) this.Cross2D(point - a, b - a) > 0.0 && (double) this.Cross2D(point - b, c - b) > 0.0 && (double) this.Cross2D(point - c, a - c) > 0.0;
    }

    private bool TriangleIsCCW(Vector2 a, Vector2 b, Vector2 c)
    {
      return (double) this.Cross2D(b - a, c - b) < 0.0;
    }
  }
}
