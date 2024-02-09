// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Utility.PenWorkspace
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Sharp2D.Engine.Drawing.Utility
{
  internal class PenWorkspace
  {
    private float _pathLength;
    private float _pathLengthScale;
    public Buffer<Vector2> XYBuffer;
    public Buffer<Vector2> XYInsetBuffer;
    public Buffer<Vector2> XYOutsetBuffer;
    public Buffer<Vector2> UVBuffer;
    public Buffer<Vector2> UVInsetBuffer;
    public Buffer<Vector2> UVOutsetBuffer;
    public Buffer<short> IndexBuffer;
    public Buffer<short> OutlineIndexBuffer;
    public Vector2[] BoundingQuad;

    public float PathLength
    {
      get => this._pathLength;
      set
      {
        this._pathLength = value;
        this._pathLengthScale = (double) value == 0.0 ? 1f : 1f / this._pathLength;
      }
    }

    public float PathLengthScale => this._pathLengthScale;

    public PenWorkspace()
    {
      this.XYBuffer = new Buffer<Vector2>();
      this.XYInsetBuffer = new Buffer<Vector2>();
      this.XYOutsetBuffer = new Buffer<Vector2>();
      this.UVBuffer = new Buffer<Vector2>();
      this.UVInsetBuffer = new Buffer<Vector2>();
      this.UVOutsetBuffer = new Buffer<Vector2>();
      this.IndexBuffer = new Buffer<short>();
      this.OutlineIndexBuffer = new Buffer<short>();
      this.BoundingQuad = new Vector2[4];
    }

    public PenWorkspace(Pen pen)
    {
      this.XYBuffer = new Buffer<Vector2>(Math.Max(pen.StartPointVertexBound(), pen.EndPointVertexBound()));
      this.XYInsetBuffer = new Buffer<Vector2>(pen.LineJoinVertexBound());
      this.XYOutsetBuffer = new Buffer<Vector2>(this.XYInsetBuffer.Capacity);
      this.UVBuffer = new Buffer<Vector2>(this.XYBuffer.Capacity);
      this.UVInsetBuffer = new Buffer<Vector2>(this.XYInsetBuffer.Capacity);
      this.UVOutsetBuffer = new Buffer<Vector2>(this.XYOutsetBuffer.Capacity);
      this.IndexBuffer = new Buffer<short>(Math.Max(pen.StartCapInfo.IndexCount, pen.EndCapInfo.IndexCount));
      this.OutlineIndexBuffer = new Buffer<short>(this.XYBuffer.Capacity);
      this.BoundingQuad = new Vector2[4];
    }

    public void ResetWorkspace(Pen pen)
    {
      this.XYBuffer.EnsureCapacity(Math.Max(pen.StartPointVertexBound(), pen.EndPointVertexBound()));
      this.XYInsetBuffer.EnsureCapacity(pen.LineJoinVertexBound());
      this.XYOutsetBuffer.EnsureCapacity(this.XYInsetBuffer.Capacity);
      this.UVBuffer.EnsureCapacity(this.XYBuffer.Capacity);
      this.UVInsetBuffer.EnsureCapacity(this.XYInsetBuffer.Capacity);
      this.UVOutsetBuffer.EnsureCapacity(this.XYOutsetBuffer.Capacity);
      this.IndexBuffer.EnsureCapacity(Math.Max(pen.StartCapInfo.IndexCount, pen.EndCapInfo.IndexCount));
      this.OutlineIndexBuffer.EnsureCapacity(this.XYBuffer.Capacity);
      this.PathLength = 0.0f;
    }
  }
}
