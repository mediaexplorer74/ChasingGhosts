// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.GraphicsPath
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Drawing.Utility;
using System;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>Represents a stroked path.</summary>
  public class GraphicsPath : IGraphicsPath
  {
    private static GraphicsPath[] _emptyOutlinePaths = new GraphicsPath[0];
    private static ZeroList _zeroList = new ZeroList();
    private Pen _pen;
    private StrokeType _strokeType;
    private int _pointCount;
    private int _vertexBufferIndex;
    private int _indexBufferIndex;
    private Vector2[] _positionData;
    private Vector2[] _textureData;
    private Color[] _colorData;
    private short[] _indexData;
    private bool[] _jointCCW;
    private GraphicsPath[] _outlinePaths;

    /// <summary>
    /// Create an empty path with a given <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen"></param>
    public GraphicsPath(Pen pen) => this._pen = pen;

    /// <summary>
    /// Compute a stroked open path given a set of points and a <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    public GraphicsPath(Pen pen, IList<Vector2> points)
      : this(pen, points, PathType.Open, 0, points.Count)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    public GraphicsPath(Pen pen, IList<Vector2> points, PathType pathType)
      : this(pen, points, pathType, 0, points.Count)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <param name="offset">The offset into the list of points that starts the path.</param>
    /// <param name="count">The number of points in the path.</param>
    public GraphicsPath(Pen pen, IList<Vector2> points, PathType pathType, int offset, int count)
      : this(pen)
    {
      this._pointCount = count;
      this._strokeType = StrokeType.Fill;
      switch (pathType)
      {
        case PathType.Open:
          this.CompileOpenPath(points, (IList<float>) null, offset, count, (Pen) null);
          break;
        case PathType.Closed:
          this.CompileClosedPath(points, (IList<float>) null, offset, count, (Pen) null);
          break;
      }
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="lengths">The lengths of each point from the path start.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <param name="offset">The offset into the list of points that starts the path.</param>
    /// <param name="count">The number of points in the path.</param>
    public GraphicsPath(
      Pen pen,
      IList<Vector2> points,
      IList<float> lengths,
      PathType pathType,
      int offset,
      int count)
      : this(pen)
    {
      this._pointCount = count;
      this._strokeType = StrokeType.Fill;
      switch (pathType)
      {
        case PathType.Open:
          this.CompileOpenPath(points, lengths, offset, count, (Pen) null);
          break;
        case PathType.Closed:
          this.CompileClosedPath(points, lengths, offset, count, (Pen) null);
          break;
      }
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a path and outline <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="outlinePen">The pen to stroke the outline of the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    public GraphicsPath(Pen pen, Pen outlinePen, IList<Vector2> points)
      : this(pen, outlinePen, points, PathType.Open, 0, points.Count, StrokeType.Both)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a path and outline <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="outlinePen">The pen to stroke the outline of the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="strokeType">Whether to stroke just the path, the outline, or both.</param>
    public GraphicsPath(Pen pen, Pen outlinePen, IList<Vector2> points, StrokeType strokeType)
      : this(pen, outlinePen, points, PathType.Open, 0, points.Count, strokeType)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a path and outline <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="outlinePen">The pen to stroke the outline of the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    public GraphicsPath(Pen pen, Pen outlinePen, IList<Vector2> points, PathType pathType)
      : this(pen, outlinePen, points, pathType, 0, points.Count, StrokeType.Both)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a path and outline <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="outlinePen">The pen to stroke the outline of the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <param name="strokeType">Whether to stroke just the path, the outline, or both.</param>
    public GraphicsPath(
      Pen pen,
      Pen outlinePen,
      IList<Vector2> points,
      PathType pathType,
      StrokeType strokeType)
      : this(pen, outlinePen, points, pathType, 0, points.Count, strokeType)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a path and outline <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="outlinePen">The pen to stroke the outline of the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <param name="offset">The offset into the list of points that starts the path.</param>
    /// <param name="count">The number of points in the path.</param>
    public GraphicsPath(
      Pen pen,
      Pen outlinePen,
      IList<Vector2> points,
      PathType pathType,
      int offset,
      int count)
      : this(pen, outlinePen, points, pathType, offset, count, StrokeType.Both)
    {
    }

    /// <summary>
    /// Compute a stroked open or closed path given a set of points and a path and outline <see cref="P:Sharp2D.Engine.Drawing.GraphicsPath.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="outlinePen">The pen to stroke the outline of the path with.</param>
    /// <param name="points">The points making up the ideal path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <param name="offset">The offset into the list of points that starts the path.</param>
    /// <param name="count">The number of points in the path.</param>
    /// <param name="strokeType">Whether to stroke just the path, the outline, or both.</param>
    public GraphicsPath(
      Pen pen,
      Pen outlinePen,
      IList<Vector2> points,
      PathType pathType,
      int offset,
      int count,
      StrokeType strokeType)
      : this(pen)
    {
      this._pointCount = count;
      this._strokeType = strokeType;
      switch (pathType)
      {
        case PathType.Open:
          this.CompileOpenPath(points, (IList<float>) null, offset, count, outlinePen);
          break;
        case PathType.Closed:
          this.CompileClosedPath(points, (IList<float>) null, offset, count, outlinePen);
          break;
      }
    }

    /// <summary>
    /// Gets the outline paths that have been generated for this path.
    /// </summary>
    public GraphicsPath[] OutlinePaths => this._outlinePaths ?? GraphicsPath._emptyOutlinePaths;

    /// <inherit />
    public int IndexCount => this._indexBufferIndex;

    /// <inherit />
    public int VertexCount => this._vertexBufferIndex;

    /// <inherit />
    public Vector2[] VertexPositionData => this._positionData;

    /// <inherit />
    public Vector2[] VertexTextureData => this._textureData;

    /// <inherit />
    public Color[] VertexColorData => this._colorData;

    /// <inherit />
    public short[] IndexData => this._indexData;

    /// <inherit />
    public Pen Pen => this._pen;

    private void InitializeBuffers(int pointCount)
    {
      this._jointCCW = new bool[pointCount];
      int length = this.Pen.MaximumVertexCount(pointCount);
      this._indexData = new short[this.Pen.MaximumIndexCount(pointCount)];
      this._positionData = new Vector2[length];
      if (this._pen.Brush == null)
        return;
      Color color = this._pen.Brush.Color;
      if (true)
        this._colorData = new Color[length];
      if (this._pen.Brush.Texture != null)
        this._textureData = new Vector2[length];
    }

    private void CompileOpenPath(
      IList<Vector2> points,
      IList<float> accumLengths,
      int offset,
      int count,
      Pen outlinePen)
    {
      if (this._strokeType != StrokeType.Outline)
        this.InitializeBuffers(count);
      IList<float> floatList = accumLengths ?? (IList<float>) GraphicsPath._zeroList;
      Buffer<Vector2> buffer1 = (Buffer<Vector2>) null;
      Buffer<Vector2> outsetBuffer = (Buffer<Vector2>) null;
      if (outlinePen != null && this._strokeType != 0)
      {
        buffer1 = Pools<Buffer<Vector2>>.Obtain();
        outsetBuffer = Pools<Buffer<Vector2>>.Obtain();
        int capacity = this._positionData != null ? this._positionData.Length : this._pen.MaximumVertexCount(count);
        buffer1.EnsureCapacity(capacity);
        outsetBuffer.EnsureCapacity(capacity);
      }
      PenWorkspace ws = Pools<PenWorkspace>.Obtain();
      ws.ResetWorkspace(this._pen);
      ws.PathLength = floatList[offset + count - 1];
      int vEndCount1 = this.AddStartPoint(0, points[offset], points[offset + 1], ws, buffer1);
      if (buffer1 != null)
        Array.Reverse((Array) buffer1.Data, 0, buffer1.Index);
      JoinSample joinSample = new JoinSample(Vector2.Zero, points[offset], points[offset + 1], 0.0f, floatList[offset], floatList[offset + 1]);
      for (int index = 0; index < count - 2; ++index)
      {
        joinSample.Advance(points[offset + index + 2], floatList[offset + index + 2]);
        int vStartCount = vEndCount1;
        vEndCount1 = this.AddJoint(index + 1, ref joinSample, ws, buffer1, outsetBuffer);
        if (this._strokeType != StrokeType.Outline)
          this.AddSegment(this._vertexBufferIndex - vEndCount1 - vStartCount, vStartCount, this._jointCCW[index], this._vertexBufferIndex - vEndCount1, vEndCount1, this._jointCCW[index + 1]);
      }
      int vStartCount1 = vEndCount1;
      int vEndCount2 = this.AddEndPoint(count - 1, points[offset + count - 2], points[offset + count - 1], ws, buffer1);
      if (this._strokeType != StrokeType.Outline)
        this.AddSegment(this._vertexBufferIndex - vEndCount2 - vStartCount1, vStartCount1, this._jointCCW[count - 2], this._vertexBufferIndex - vEndCount2, vEndCount2, this._jointCCW[count - 1]);
      if (buffer1 != null)
        Array.Reverse((Array) buffer1.Data, 0, buffer1.Index);
      Pools<PenWorkspace>.Release(ws);
      if (outlinePen == null || this._strokeType == 0)
        return;
      Buffer<Vector2> buffer2 = Pools<Buffer<Vector2>>.Obtain();
      buffer2.EnsureCapacity(buffer1.Index + outsetBuffer.Index);
      Array.Copy((Array) buffer1.Data, 0, (Array) buffer2.Data, 0, buffer1.Index);
      Array.Copy((Array) outsetBuffer.Data, 0, (Array) buffer2.Data, buffer1.Index, outsetBuffer.Index);
      this._outlinePaths = new GraphicsPath[1]
      {
        new GraphicsPath(outlinePen, (IList<Vector2>) buffer2.Data, PathType.Closed, 0, buffer1.Index + outsetBuffer.Index)
      };
      Pools<Buffer<Vector2>>.Release(buffer2);
      Pools<Buffer<Vector2>>.Release(buffer1);
      Pools<Buffer<Vector2>>.Release(outsetBuffer);
    }

    private void CompileClosedPath(
      IList<Vector2> points,
      IList<float> accumLengths,
      int offset,
      int count,
      Pen outlinePen)
    {
      if (this._strokeType != StrokeType.Outline)
        this.InitializeBuffers(count + 1);
      if (this.IsClose(points[offset], points[offset + count - 1]))
        --count;
      IList<float> floatList = accumLengths ?? (IList<float>) GraphicsPath._zeroList;
      Buffer<Vector2> insetBuffer = (Buffer<Vector2>) null;
      Buffer<Vector2> outsetBuffer = (Buffer<Vector2>) null;
      if (outlinePen != null && this._strokeType != 0)
      {
        insetBuffer = Pools<Buffer<Vector2>>.Obtain();
        outsetBuffer = Pools<Buffer<Vector2>>.Obtain();
        int capacity = this._positionData != null ? this._positionData.Length : this._pen.MaximumVertexCount(count);
        insetBuffer.EnsureCapacity(capacity);
        outsetBuffer.EnsureCapacity(capacity);
      }
      PenWorkspace ws = Pools<PenWorkspace>.Obtain();
      ws.ResetWorkspace(this._pen);
      JoinSample joinSample = new JoinSample(points[offset + count - 1], points[offset], points[offset + 1], 0.0f, floatList[offset], floatList[offset + 1]);
      int vertexBufferIndex = this._vertexBufferIndex;
      int vEndCount1 = this.AddJoint(0, ref joinSample, ws, insetBuffer, outsetBuffer);
      int vEndCount2 = vEndCount1;
      for (int index = 0; index < count - 2; ++index)
      {
        joinSample.Advance(points[offset + index + 2], floatList[offset + index + 2]);
        int vStartCount = vEndCount2;
        vEndCount2 = this.AddJoint(index + 1, ref joinSample, ws, insetBuffer, outsetBuffer);
        if (this._strokeType != StrokeType.Outline)
          this.AddSegment(this._vertexBufferIndex - vEndCount2 - vStartCount, vStartCount, this._jointCCW[index], this._vertexBufferIndex - vEndCount2, vEndCount2, this._jointCCW[index + 1]);
      }
      joinSample.Advance(points[offset], floatList[offset]);
      int vStartCount1 = vEndCount2;
      int num = this.AddJoint(count - 1, ref joinSample, ws, insetBuffer, outsetBuffer);
      if (this._strokeType != StrokeType.Outline)
      {
        this.AddSegment(this._vertexBufferIndex - num - vStartCount1, vStartCount1, this._jointCCW[count - 2], this._vertexBufferIndex - num, num, this._jointCCW[count - 1]);
        this.AddSegment(this._vertexBufferIndex - num, num, this._jointCCW[count - 1], vertexBufferIndex, vEndCount1, this._jointCCW[0]);
      }
      Pools<PenWorkspace>.Release(ws);
      if (outlinePen == null || this._strokeType == 0)
        return;
      Array.Reverse((Array) insetBuffer.Data, 0, insetBuffer.Index);
      this._outlinePaths = new GraphicsPath[2]
      {
        new GraphicsPath(outlinePen, (IList<Vector2>) insetBuffer.Data, PathType.Closed, 0, insetBuffer.Index),
        new GraphicsPath(outlinePen, (IList<Vector2>) outsetBuffer.Data, PathType.Closed, 0, outsetBuffer.Index)
      };
      Pools<Buffer<Vector2>>.Release(insetBuffer);
      Pools<Buffer<Vector2>>.Release(outsetBuffer);
    }

    private bool IsClose(Vector2 a, Vector2 b)
    {
      return (double) Math.Abs(a.X - b.X) < 0.001 && (double) Math.Abs(a.Y - b.Y) < 0.001;
    }

    private int AddStartPoint(
      int pointIndex,
      Vector2 a,
      Vector2 b,
      PenWorkspace ws,
      Buffer<Vector2> positionBuffer)
    {
      this._pen.ComputeStartPoint(a, b, ws);
      int num = this.AddStartOrEndPoint(pointIndex, ws, positionBuffer, false);
      if (positionBuffer != null)
        Array.Reverse((Array) positionBuffer.Data, positionBuffer.Index - ws.OutlineIndexBuffer.Index, ws.OutlineIndexBuffer.Index);
      return num;
    }

    private int AddEndPoint(
      int pointIndex,
      Vector2 a,
      Vector2 b,
      PenWorkspace ws,
      Buffer<Vector2> positionBuffer)
    {
      this._pen.ComputeEndPoint(a, b, ws);
      return this.AddStartOrEndPoint(pointIndex, ws, positionBuffer, true);
    }

    private int AddStartOrEndPoint(
      int pointIndex,
      PenWorkspace ws,
      Buffer<Vector2> positionBuffer,
      bool ccw)
    {
      int index1 = ws.XYBuffer.Index;
      if (positionBuffer != null)
      {
        for (int index2 = 0; index2 < ws.OutlineIndexBuffer.Index; ++index2)
          positionBuffer.SetNext(ws.XYBuffer[(int) ws.OutlineIndexBuffer[index2]]);
      }
      if (this._strokeType == StrokeType.Outline)
        return 0;
      int vertexBufferIndex = this._vertexBufferIndex;
      this._vertexBufferIndex += index1;
      for (int index3 = 0; index3 < index1; ++index3)
        this._positionData[vertexBufferIndex + index3] = ws.XYBuffer[index3];
      for (int index4 = 0; index4 < ws.IndexBuffer.Index; ++index4)
        this._indexData[this._indexBufferIndex++] = (short) (vertexBufferIndex + (int) ws.IndexBuffer[index4]);
      if (this._colorData != null)
      {
        for (int index5 = 0; index5 < index1; ++index5)
          this._colorData[vertexBufferIndex + index5] = this._pen.ColorAt(ws.UVBuffer[index5], ws.PathLengthScale);
      }
      if (this._textureData != null)
      {
        for (int index6 = vertexBufferIndex; index6 < this._vertexBufferIndex; ++index6)
        {
          Vector2 position = this._positionData[index6];
          this._textureData[index6] = Vector2.Transform(position, this._pen.Brush.Transform);
        }
      }
      this._jointCCW[pointIndex] = ccw;
      return index1;
    }

    private int AddJoint(
      int pointIndex,
      ref JoinSample joinSample,
      PenWorkspace ws,
      Buffer<Vector2> insetBuffer,
      Buffer<Vector2> outsetBuffer)
    {
      InsetOutsetCount vioCount = new InsetOutsetCount();
      switch (this._pen.LineJoin)
      {
        case LineJoin.Miter:
          vioCount = this._pen.ComputeMiter(ref joinSample, ws);
          break;
        case LineJoin.Bevel:
          vioCount = this._pen.ComputeBevel(ref joinSample, ws);
          break;
      }
      if (insetBuffer != null)
      {
        for (int index = 0; index < (int) vioCount.InsetCount; ++index)
          insetBuffer.SetNext(ws.XYInsetBuffer[index]);
      }
      if (outsetBuffer != null)
      {
        for (int index = 0; index < (int) vioCount.OutsetCount; ++index)
          outsetBuffer.SetNext(ws.XYOutsetBuffer[index]);
      }
      return this._strokeType != StrokeType.Outline ? this.AddJoint(pointIndex, vioCount, ws) : 0;
    }

    private int AddJoint(int pointIndex, InsetOutsetCount vioCount, PenWorkspace ws)
    {
      int vertexBufferIndex = this._vertexBufferIndex;
      this._vertexBufferIndex += (int) vioCount.InsetCount + (int) vioCount.OutsetCount;
      if (!vioCount.CCW)
      {
        this._jointCCW[pointIndex] = false;
        this._positionData[vertexBufferIndex] = ws.XYOutsetBuffer[0];
        for (int index = 0; index < (int) vioCount.InsetCount; ++index)
          this._positionData[vertexBufferIndex + 1 + index] = ws.XYInsetBuffer[index];
        for (int index = 0; index < (int) vioCount.InsetCount - 1; ++index)
        {
          this._indexData[this._indexBufferIndex++] = (short) vertexBufferIndex;
          this._indexData[this._indexBufferIndex++] = (short) (vertexBufferIndex + index + 2);
          this._indexData[this._indexBufferIndex++] = (short) (vertexBufferIndex + index + 1);
        }
      }
      else
      {
        this._jointCCW[pointIndex] = true;
        this._positionData[vertexBufferIndex] = ws.XYInsetBuffer[0];
        for (int index = 0; index < (int) vioCount.OutsetCount; ++index)
          this._positionData[vertexBufferIndex + 1 + index] = ws.XYOutsetBuffer[index];
        for (int index = 0; index < (int) vioCount.OutsetCount - 1; ++index)
        {
          this._indexData[this._indexBufferIndex++] = (short) vertexBufferIndex;
          this._indexData[this._indexBufferIndex++] = (short) (vertexBufferIndex + index + 1);
          this._indexData[this._indexBufferIndex++] = (short) (vertexBufferIndex + index + 2);
        }
      }
      if (this._colorData != null)
      {
        if (!vioCount.CCW)
        {
          this._colorData[vertexBufferIndex] = this._pen.ColorAt(ws.UVOutsetBuffer[0], ws.PathLengthScale);
          for (int index = 0; index < (int) vioCount.InsetCount; ++index)
            this._colorData[vertexBufferIndex + 1 + index] = this._pen.ColorAt(ws.UVInsetBuffer[index], ws.PathLengthScale);
        }
        else
        {
          this._colorData[vertexBufferIndex] = this._pen.ColorAt(ws.UVInsetBuffer[0], ws.PathLengthScale);
          for (int index = 0; index < (int) vioCount.OutsetCount; ++index)
            this._colorData[vertexBufferIndex + 1 + index] = this._pen.ColorAt(ws.UVOutsetBuffer[index], ws.PathLengthScale);
        }
      }
      if (this._textureData != null)
      {
        int width = this._pen.Brush.Texture.Width;
        int height = this._pen.Brush.Texture.Height;
        for (int index = vertexBufferIndex; index < this._vertexBufferIndex; ++index)
        {
          Vector2 vector2 = this._positionData[index];
          this._textureData[index] = new Vector2(vector2.X / (float) width, vector2.Y / (float) height);
        }
      }
      return this._vertexBufferIndex - vertexBufferIndex;
    }

    private void AddSegment(
      int vIndexStart,
      int vStartCount,
      bool vStartCCW,
      int vIndexEnd,
      int vEndCount,
      bool vEndCCW)
    {
      if (vStartCCW)
      {
        if (vEndCCW)
        {
          this._indexData[this._indexBufferIndex++] = (short) vIndexStart;
          this._indexData[this._indexBufferIndex++] = (short) (vIndexStart + vStartCount - 1);
          this._indexData[this._indexBufferIndex++] = (short) (vIndexEnd + vEndCount - 1);
          this._indexData[this._indexBufferIndex++] = (short) (vIndexEnd + vEndCount - 1);
          this._indexData[this._indexBufferIndex++] = (short) vIndexEnd;
          this._indexData[this._indexBufferIndex++] = (short) vIndexStart;
        }
        else
        {
          this._indexData[this._indexBufferIndex++] = (short) vIndexStart;
          this._indexData[this._indexBufferIndex++] = (short) (vIndexStart + vStartCount - 1);
          this._indexData[this._indexBufferIndex++] = (short) vIndexEnd;
          this._indexData[this._indexBufferIndex++] = (short) vIndexEnd;
          this._indexData[this._indexBufferIndex++] = (short) (vIndexEnd + vEndCount - 1);
          this._indexData[this._indexBufferIndex++] = (short) vIndexStart;
        }
      }
      else if (vEndCCW)
      {
        this._indexData[this._indexBufferIndex++] = (short) (vIndexStart + vStartCount - 1);
        this._indexData[this._indexBufferIndex++] = (short) vIndexStart;
        this._indexData[this._indexBufferIndex++] = (short) (vIndexEnd + vEndCount - 1);
        this._indexData[this._indexBufferIndex++] = (short) (vIndexEnd + vEndCount - 1);
        this._indexData[this._indexBufferIndex++] = (short) vIndexEnd;
        this._indexData[this._indexBufferIndex++] = (short) (vIndexStart + vStartCount - 1);
      }
      else
      {
        this._indexData[this._indexBufferIndex++] = (short) (vIndexStart + vStartCount - 1);
        this._indexData[this._indexBufferIndex++] = (short) vIndexStart;
        this._indexData[this._indexBufferIndex++] = (short) vIndexEnd;
        this._indexData[this._indexBufferIndex++] = (short) vIndexEnd;
        this._indexData[this._indexBufferIndex++] = (short) (vIndexEnd + vEndCount - 1);
        this._indexData[this._indexBufferIndex++] = (short) (vIndexStart + vStartCount - 1);
      }
    }
  }
}
