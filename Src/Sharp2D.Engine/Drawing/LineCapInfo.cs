// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.LineCapInfo
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Drawing.Utility;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  internal abstract class LineCapInfo
  {
    protected readonly float _width;
    protected readonly Vector2[] _xyBuffer;
    protected readonly Vector2[] _uvBuffer;
    protected readonly short[] _indexBuffer;
    protected readonly short[] _outlineBuffer;

    protected LineCapInfo(float width, int vertexCount, int polyCount)
    {
      this._width = width;
      this._xyBuffer = new Vector2[vertexCount];
      this._indexBuffer = new short[polyCount * 3];
      this._outlineBuffer = new short[vertexCount];
    }

    protected LineCapInfo(
      float width,
      Vector2[] xyBuffer,
      Vector2[] uvBuffer,
      short[] indexBuffer,
      short[] outlineBuffer)
    {
      this._width = width;
      this._xyBuffer = xyBuffer;
      this._uvBuffer = uvBuffer;
      this._indexBuffer = indexBuffer;
      this._outlineBuffer = outlineBuffer;
    }

    public int VertexCount => this._xyBuffer.Length;

    public int IndexCount => this._indexBuffer.Length;

    public void Calculate(
      Vector2 p,
      Vector2 edgeAB,
      PenWorkspace ws,
      PenAlignment alignment,
      bool start)
    {
      edgeAB.Normalize();
      float num1 = edgeAB.X * this._width;
      float num2 = edgeAB.Y * this._width;
      float num3 = p.X;
      float num4 = p.Y;
      switch (alignment)
      {
        case PenAlignment.Inset:
          if (start)
          {
            num3 = p.X + -0.5f * num2;
            num4 = p.Y - -0.5f * num1;
            break;
          }
          num3 = p.X - -0.5f * num2;
          num4 = p.Y + -0.5f * num1;
          break;
        case PenAlignment.Outset:
          if (start)
          {
            num3 = p.X + 0.5f * num2;
            num4 = p.Y - 0.5f * num1;
            break;
          }
          num3 = p.X - 0.5f * num2;
          num4 = p.Y + 0.5f * num1;
          break;
      }
      for (int index = 0; index < this._xyBuffer.Length; ++index)
        ws.XYBuffer[index] = new Vector2((float) ((double) this._xyBuffer[index].X * (double) num1 - (double) this._xyBuffer[index].Y * (double) num2) + num3, (float) ((double) this._xyBuffer[index].X * (double) num2 + (double) this._xyBuffer[index].Y * (double) num1) + num4);
      for (int index = 0; index < this._uvBuffer.Length; ++index)
        ws.UVBuffer[index] = this._uvBuffer[index];
      for (int index = 0; index < this._indexBuffer.Length; ++index)
        ws.IndexBuffer[index] = this._indexBuffer[index];
      for (int index = 0; index < this._outlineBuffer.Length; ++index)
        ws.OutlineIndexBuffer[index] = this._outlineBuffer[index];
      ws.XYBuffer.Index = this._xyBuffer.Length;
      ws.UVBuffer.Index = this._uvBuffer.Length;
      ws.IndexBuffer.Index = this._indexBuffer.Length;
      ws.OutlineIndexBuffer.Index = this._outlineBuffer.Length;
    }
  }
}
