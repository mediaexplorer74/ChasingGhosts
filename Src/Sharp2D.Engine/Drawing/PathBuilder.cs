// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.PathBuilder
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
  /// <summary>An object for building up an ideal path from pieces.</summary>
  public class PathBuilder
  {
    private static Dictionary<int, List<Vector2>> _circleCache = new Dictionary<int, List<Vector2>>();
    private static readonly double[] _factorials = new double[33]
    {
      1.0,
      1.0,
      2.0,
      6.0,
      24.0,
      120.0,
      720.0,
      5040.0,
      40320.0,
      362880.0,
      3628800.0,
      39916800.0,
      479001600.0,
      6227020800.0,
      87178291200.0,
      1307674368000.0,
      20922789888000.0,
      355687428096000.0,
      6.402373705728E+15,
      1.21645100408832E+17,
      2.43290200817664E+18,
      5.109094217170944E+19,
      1.1240007277776077E+21,
      2.5852016738884978E+22,
      6.2044840173323941E+23,
      1.5511210043330986E+25,
      4.0329146112660565E+26,
      1.0888869450418352E+28,
      3.0488834461171387E+29,
      8.8417619937397019E+30,
      2.6525285981219107E+32,
      8.2228386541779224E+33,
      2.6313083693369352E+35
    };
    private Vector2[] _geometryBuffer;
    private float[] _lengthBuffer;
    private int _geometryIndex;
    private bool _calculateLengths;

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.PathBuilder" /> object.
    /// </summary>
    public PathBuilder()
      : this(256)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.PathBuilder" /> object with a given initial buffer size.
    /// </summary>
    /// <param name="initialBufferSize">The initial size of the internal vertex buffer.</param>
    public PathBuilder(int initialBufferSize)
    {
      this._geometryBuffer = new Vector2[initialBufferSize];
      this._lengthBuffer = new float[initialBufferSize];
      this._geometryIndex = 0;
    }

    /// <summary>
    /// Gets the raw vertex buffer from the <see cref="T:Sharp2D.Engine.Drawing.PathBuilder" />.
    /// </summary>
    /// <seealso cref="P:Sharp2D.Engine.Drawing.PathBuilder.LengthBuffer" />
    public Vector2[] Buffer => this._geometryBuffer;

    /// <summary>
    /// Gets the buffer containing the length of each vertex in the vertex buffer from the start of the path.
    /// </summary>
    /// <remarks><para>The length for the first vertex is always 0.</para></remarks>
    /// <seealso cref="P:Sharp2D.Engine.Drawing.PathBuilder.Buffer" />
    /// <seealso cref="P:Sharp2D.Engine.Drawing.PathBuilder.CalculateLengths" />
    public float[] LengthBuffer => this._lengthBuffer;

    /// <summary>
    /// Gets the number of vertices currently in the path and buffer.
    /// </summary>
    public int Count => this._geometryIndex;

    /// <summary>
    /// Gets or sets whether lengths are calculated for line segments.
    /// </summary>
    /// <remarks><para>This property will be checked at the time a path is stroked to determine whether it can use length information or not.</para>
    /// <para>If this property is set to false during some or all of the path building operations, lengths for those sections may appear as any value.</para></remarks>
    /// <seealso cref="P:Sharp2D.Engine.Drawing.PathBuilder.LengthBuffer" />
    public bool CalculateLengths
    {
      get => this._calculateLengths;
      set => this._calculateLengths = value;
    }

    /// <summary>Appends a point to the end of the path.</summary>
    /// <param name="point">A point.</param>
    public void AddPoint(Vector2 point)
    {
      this.CheckBufferFreeSpace(1);
      if (this.LastPointEqual(point))
        return;
      if (this._calculateLengths)
        this._lengthBuffer[this._geometryIndex] = this._geometryIndex == 0 ? 0.0f : this._lengthBuffer[this._geometryIndex - 1] + Vector2.Distance(this._geometryBuffer[this._geometryIndex - 1], point);
      this._geometryBuffer[this._geometryIndex++] = point;
    }

    /// <summary>Appends a list of points to the end of the path.</summary>
    /// <param name="points">A list of points.</param>
    public void AddPath(IList<Vector2> points)
    {
      if (points.Count == 0)
        return;
      Vector2 vector2 = this._geometryIndex > 0 ? this._geometryBuffer[this._geometryIndex - 1] : new Vector2(float.NaN, float.NaN);
      this.CheckBufferFreeSpace(points.Count);
      if (points[0] != vector2)
      {
        if (this._calculateLengths)
          this._lengthBuffer[this._geometryIndex] = this._geometryIndex == 0 ? 0.0f : this._lengthBuffer[this._geometryIndex - 1] + Vector2.Distance(this._geometryBuffer[this._geometryIndex - 1], points[0]);
        this._geometryBuffer[this._geometryIndex++] = points[0];
      }
      int geometryIndex = this._geometryIndex;
      int index = 1;
      for (int count = points.Count; index < count; ++index)
      {
        Vector2 point = points[index];
        if (point != vector2)
          this._geometryBuffer[this._geometryIndex++] = point;
        vector2 = point;
      }
      if (!this._calculateLengths)
        return;
      this.CalculateLengthsInRange(geometryIndex, this._geometryIndex - geometryIndex);
    }

    /// <summary>
    /// Appends all of the points within another <see cref="T:Sharp2D.Engine.Drawing.PathBuilder" /> object to the end of the path.
    /// </summary>
    /// <param name="path">An existing path.</param>
    public void AddPath(PathBuilder path)
    {
      if (path._geometryIndex == 0)
        return;
      if (path._geometryIndex == 1)
      {
        this.AddPoint(path._geometryBuffer[0]);
      }
      else
      {
        this.CheckBufferFreeSpace(path._geometryIndex);
        int num = this.LastPointEqual(path._geometryBuffer[0]) ? 1 : 0;
        int geometryIndex = this._geometryIndex;
        for (int index = num; index < path._geometryIndex; ++index)
          this._geometryBuffer[this._geometryIndex++] = path._geometryBuffer[index];
        if (!this._calculateLengths)
          return;
        if (path._lengthBuffer != null)
        {
          for (int index = num; index < path._geometryIndex; ++index)
            this._lengthBuffer[geometryIndex++] = path._lengthBuffer[index];
        }
        else
          this.CalculateLengthsInRange(geometryIndex, path._geometryIndex - geometryIndex);
      }
    }

    /// <summary>
    /// Appends a point to the end of the path offset from the path's current endpoint by the given length and angle.
    /// </summary>
    /// <param name="length">The length of the line being added.</param>
    /// <param name="angle">The angle of the line in radians.  Positive values are clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException">The path has no existing points.</exception>
    public void AddLine(float length, float angle)
    {
      if (this._geometryIndex == 0)
        throw new InvalidOperationException("Cannot add a line from partial information to an empty path.");
      if ((double) length == 0.0)
        return;
      Vector2 vector2_1 = this._geometryBuffer[this._geometryIndex - 1];
      Vector2 vector2_2 = new Vector2(vector2_1.X + length * (float) Math.Cos((double) angle), vector2_1.Y + length * (float) Math.Sin((double) angle));
      if (this._calculateLengths)
        this._lengthBuffer[this._geometryIndex] = this._lengthBuffer[this._geometryIndex - 1] + length;
      this._geometryBuffer[this._geometryIndex++] = vector2_2;
    }

    /// <summary>
    /// Appends an arc between the current endpoint and given point to the end of the path.
    /// </summary>
    /// <param name="point">The endpoint of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting the path's current endpoint and <paramref name="point" />.</param>
    /// <exception cref="T:System.InvalidOperationException">The path has no existing points.</exception>
    public void AddArcByPoint(Vector2 point, float height)
    {
      if (this._geometryIndex == 0)
        throw new InvalidOperationException("Cannot add an arc from partial information to an empty path.");
      float num = (point - this._geometryBuffer[this._geometryIndex - 1]).Length();
      float radius = (float) ((double) height / 2.0 + (double) num * (double) num / ((double) height * 8.0));
      this.AddArcByPoint(point, height, PathBuilder.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Appends an arc between the current endpoint and given point to the end of the path.
    /// </summary>
    /// <param name="point">The endpoint of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting the path's current endpoint and <paramref name="point" />.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same arc radius.</param>
    /// <exception cref="T:System.InvalidOperationException">The path has no existing points.</exception>
    public void AddArcByPoint(Vector2 point, float height, int subdivisions)
    {
      if (this._geometryIndex == 0)
        throw new InvalidOperationException("Cannot add an arc from partial information to an empty path.");
      if (this._geometryBuffer[this._geometryIndex - 1] == point)
        return;
      --this._geometryIndex;
      this.BuildArcGeometryBuffer(this._geometryBuffer[this._geometryIndex], point, height, subdivisions);
    }

    /// <summary>
    /// Appends an arc between the current endpoint and a point defined by a center and arc angle.
    /// </summary>
    /// <param name="center">The center of a circle containing the path's current endpoint and destination point.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException">The path has no existing points.</exception>
    public void AddArcByAngle(Vector2 center, float arcAngle)
    {
      if (this._geometryIndex == 0)
        throw new InvalidOperationException("Cannot add an arc from partial information to an empty path.");
      float radius = Math.Abs((this._geometryBuffer[this._geometryIndex - 1] - center).Length());
      this.AddArcByAngle(center, arcAngle, PathBuilder.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Appends an arc between the current endpoint and a point defined by a center and arc angle.
    /// </summary>
    /// <param name="center">The center of a circle containing the path's current endpoint and destination point.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same arc radius.</param>
    /// <exception cref="T:System.InvalidOperationException">The path has no existing points.</exception>
    public void AddArcByAngle(Vector2 center, float arcAngle, int subdivisions)
    {
      if (this._geometryIndex == 0)
        throw new InvalidOperationException("Cannot add an arc from partial information to an empty path.");
      --this._geometryIndex;
      float radius = Math.Abs((this._geometryBuffer[this._geometryIndex] - center).Length());
      float angle = PathBuilder.PointToAngle(center, this._geometryBuffer[this._geometryIndex]);
      this.BuildArcGeometryBuffer(center, radius, subdivisions, angle, arcAngle);
    }

    /// <summary>
    /// Appends a fully-defined arc to the end of the path, connected by an additional line segment if the arc does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    public void AddArc(Vector2 center, float radius, float startAngle, float arcAngle)
    {
      this.AddArc(center, radius, startAngle, arcAngle, PathBuilder.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Appends a fully-defined arc to the end of the path, connected by an additional line segment if the arc does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same arc radius.</param>
    public void AddArc(
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      int subdivisions)
    {
      if (this.LastPointEqual(new Vector2(center.X + radius * (float) Math.Cos((double) startAngle), center.Y + radius * (float) Math.Sin((double) startAngle))))
        --this._geometryIndex;
      this.BuildArcGeometryBuffer(center, radius, subdivisions, startAngle, arcAngle);
    }

    /// <summary>
    /// Appends a fully-defined arc to the end of the path, connected by an additional line segment if the arc does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="p0">The starting point of the arc.</param>
    /// <param name="p1">The ending point of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting <paramref name="p0" /> and <paramref name="p1" />.</param>
    public void AddArc(Vector2 p0, Vector2 p1, float height)
    {
      float num = (p1 - p0).Length();
      float radius = (float) ((double) height / 2.0 + (double) num * (double) num / ((double) height * 8.0));
      this.AddArc(p0, p1, height, PathBuilder.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Appends a fully-defined arc to the end of the path, connected by an additional line segment if the arc does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="p0">The starting point of the arc.</param>
    /// <param name="p1">The ending point of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting <paramref name="p0" /> and <paramref name="p1" />.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same arc radius.</param>
    public void AddArc(Vector2 p0, Vector2 p1, float height, int subdivisions)
    {
      if (p0 == p1)
        return;
      if (this.LastPointEqual(p0))
        --this._geometryIndex;
      this.BuildArcGeometryBuffer(p0, p1, height, subdivisions);
    }

    /// <summary>
    /// Appends a quadratic bezier curve to the end of the path, connected by an additional line segment if the curve does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="p0">The starting point of the curve.</param>
    /// <param name="p1">The first control point of the curve.</param>
    /// <param name="p2">The ending point of the curve.</param>
    public void AddBezier(Vector2 p0, Vector2 p1, Vector2 p2)
    {
      this.AddBezier(p0, p1, p2, PathBuilder.DefaultBezierSubdivisions(p0, p1, p2));
    }

    /// <summary>
    /// Appends a quadratic Bezier curve to the end of the path, connected by an additional line segment if the curve does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="p0">The starting point of the curve.</param>
    /// <param name="p1">The first control point of the curve.</param>
    /// <param name="p2">The ending point of the curve.</param>
    /// <param name="subdivisions">The number of subdivisions in the curve.</param>
    public void AddBezier(Vector2 p0, Vector2 p1, Vector2 p2, int subdivisions)
    {
      if (this.LastPointClose(p0))
        --this._geometryIndex;
      this.BuildQuadraticBezierGeometryBuffer(p0, p1, p2, subdivisions);
    }

    /// <summary>
    /// Appends a cubic Bezier curve to the end of the path, connected by an additional line segment if the curve does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="p0">The starting point of the curve.</param>
    /// <param name="p1">The first control point.</param>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The ending point of the curve.</param>
    public void AddBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
      this.AddBezier(p0, p1, p2, p3, PathBuilder.DefaultBezierSubdivisions(p0, p1, p2, p3));
    }

    /// <summary>
    /// Appends a cubic Bezier curve to the end of the path, connected by an additional line segment if the curve does not
    /// begin at the path's current endpoint.
    /// </summary>
    /// <param name="p0">The starting point of the curve.</param>
    /// <param name="p1">The first control point.</param>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The ending point of the curve.</param>
    /// <param name="subdivisions">The number of subdivisions in the curve.</param>
    public void AddBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, int subdivisions)
    {
      if (this.LastPointClose(p0))
        --this._geometryIndex;
      this.BuildCubicBezierGeometryBuffer(p0, p1, p2, p3, subdivisions);
    }

    /// <summary>
    /// Appends a series of Bezier curves to the end of the path, connected by an additional line segment if the first curve
    /// does not begin at the path's current endpoint.
    /// </summary>
    /// <param name="points">A list of points.</param>
    /// <param name="bezierType">The type of Bezier</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1.
    /// For cubic Bezier curves, the number of points defined by the parameters should be a multiple of 3 plus 1.  For each curve
    /// drawn after the first, the ending point of the previous curve is used as the starting point.</para></remarks>
    public void AddBeziers(IList<Vector2> points, BezierType bezierType)
    {
      this.AddBeziers(points, 0, points.Count, bezierType);
    }

    /// <summary>
    /// Appends a series of Bezier curves to the end of the path, connected by an additional line segment if the first curve
    /// does not begin at the path's current endpoint.
    /// </summary>
    /// <param name="points">A list of points.</param>
    /// <param name="offset">The index of the first point to use from the list.</param>
    /// <param name="length">The number of points to use from the list.</param>
    /// <param name="bezierType">The type of Bezier</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1.
    /// For cubic Bezier curves, the number of points defined by the parameters should be a multiple of 3 plus 1.  For each curve
    /// drawn after the first, the ending point of the previous curve is used as the starting point.</para></remarks>
    public void AddBeziers(IList<Vector2> points, int offset, int length, BezierType bezierType)
    {
      if (offset < 0 || points.Count < offset + length)
        throw new ArgumentOutOfRangeException("The offset and length are out of range for the given points argument.");
      switch (bezierType)
      {
        case BezierType.Quadratic:
          if (length < 3)
            throw new ArgumentOutOfRangeException("A quadratic bezier needs at least 3 points");
          for (int index = offset + 2; index < offset + length; index += 2)
            this.AddBezier(points[index - 2], points[index - 1], points[index]);
          break;
        case BezierType.Cubic:
          if (length < 4)
            throw new ArgumentOutOfRangeException("A cubic bezier needs at least 4 points");
          for (int index = offset + 3; index < offset + length; index += 3)
            this.AddBezier(points[index - 3], points[index - 2], points[index - 1], points[index]);
          break;
      }
    }

    /// <summary>
    /// Creates an open <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" /> from the path with a given <see cref="T:Sharp2D.Engine.Drawing.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <returns>A computed <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" />.</returns>
    public GraphicsPath Stroke(Pen pen) => this.Stroke(pen, PathType.Open);

    /// <summary>
    /// Creates an open or closed <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" /> from the path with a given <see cref="T:Sharp2D.Engine.Drawing.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <returns>A computed <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" />.</returns>
    public GraphicsPath Stroke(Pen pen, PathType pathType)
    {
      return new GraphicsPath(pen, (IList<Vector2>) this._geometryBuffer, (IList<float>) this._lengthBuffer, pathType, 0, this._geometryIndex);
    }

    /// <summary>
    /// Creates an open <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" /> from a transformed copy of the path with a given <see cref="T:Sharp2D.Engine.Drawing.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="transform">The transform matrix to apply to all of the points in the path.</param>
    /// <returns>A computed <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" />.</returns>
    public GraphicsPath Stroke(Pen pen, Matrix transform)
    {
      return this.Stroke(pen, transform, PathType.Open);
    }

    /// <summary>
    /// Creates an open or closed <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" /> from a transformed copy of the path with a given <see cref="T:Sharp2D.Engine.Drawing.Pen" />.
    /// </summary>
    /// <param name="pen">The pen to stroke the path with.</param>
    /// <param name="transform">The transform matrix to apply to all of the points in the path.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <returns>A computed <see cref="T:Sharp2D.Engine.Drawing.GraphicsPath" />.</returns>
    public GraphicsPath Stroke(Pen pen, Matrix transform, PathType pathType)
    {
      Vector2[] points = new Vector2[this._geometryIndex];
      for (int index = 0; index < this._geometryIndex; ++index)
        points[index] = Vector2.Transform(this._geometryBuffer[index], transform);
      return new GraphicsPath(pen, (IList<Vector2>) points, (IList<float>) this._lengthBuffer, pathType, 0, this._geometryIndex);
    }

    /// <summary>
    /// Resets the <see cref="T:Sharp2D.Engine.Drawing.PathBuilder" /> to empty.
    /// </summary>
    public void Reset() => this._geometryIndex = 0;

    private void CheckBufferFreeSpace(int vertexCount)
    {
      if (this._geometryBuffer.Length >= this._geometryIndex + vertexCount)
        return;
      Array.Resize<Vector2>(ref this._geometryBuffer, (this._geometryIndex + vertexCount) * 2);
      if (this._lengthBuffer != null)
        Array.Resize<float>(ref this._lengthBuffer, (this._geometryIndex + vertexCount) * 2);
    }

    private void CalculateLengthsInRange(int startIndex, int count)
    {
      int num = startIndex + count;
      if (startIndex == 0)
      {
        this._lengthBuffer[0] = 0.0f;
        ++startIndex;
      }
      for (int index = startIndex; index < num; ++index)
        this._lengthBuffer[index] = this._lengthBuffer[index - 1] + Vector2.Distance(this._geometryBuffer[index - 1], this._geometryBuffer[index]);
    }

    private int BuildArcGeometryBuffer(Vector2 p0, Vector2 p1, float height, int subdivisions)
    {
      Vector2 vector2_1 = p1 - p0;
      Vector2 vector2_2 = Vector2.Lerp(p0, p1, 0.5f);
      float num1 = vector2_1.Length();
      float num2 = (float) ((double) height / 2.0 + (double) num1 * (double) num1 / ((double) height * 8.0));
      vector2_1.Normalize();
      Vector2 vector2_3 = new Vector2(-vector2_1.Y, vector2_1.X);
      Vector2 center = vector2_2 + vector2_3 * (num2 - height);
      float angle1 = PathBuilder.PointToAngle(center, p0);
      float angle2 = PathBuilder.PointToAngle(center, p1);
      float arcAngle = (double) height < 0.0 ? (-(double) height >= (double) num1 / 2.0 ? ((double) angle2 - (double) angle1 > Math.PI ? angle1 - angle2 : angle2 - 6.28318548f - angle1) : ((double) Math.Abs(angle2 - angle1) < Math.PI ? angle2 - angle1 : angle2 - 6.28318548f - angle1)) : ((double) height >= (double) num1 / 2.0 ? ((double) angle2 - (double) angle1 > Math.PI ? angle2 - angle1 : angle2 + 6.28318548f - angle1) : ((double) Math.Abs(angle2 - angle1) < Math.PI ? angle2 - angle1 : angle2 + 6.28318548f - angle1));
      return this.BuildArcGeometryBuffer(center, Math.Abs(num2), subdivisions, angle1, arcAngle);
    }

    private int BuildArcGeometryBuffer(
      Vector2 center,
      float radius,
      int subdivisions,
      float startAngle,
      float arcAngle)
    {
      float angle = startAngle + arcAngle;
      startAngle = PathBuilder.ClampAngle(startAngle);
      float num1 = PathBuilder.ClampAngle(angle);
      List<Vector2> circleSubdivisions = PathBuilder.CalculateCircleSubdivisions(subdivisions);
      float num2 = 6.28318548f / (float) subdivisions;
      Vector2 vector2_1 = new Vector2((float) Math.Cos(-(double) startAngle), (float) Math.Sin(-(double) startAngle));
      Vector2 vector2_2 = new Vector2((float) Math.Cos(-(double) num1), (float) Math.Sin(-(double) num1));
      int num3;
      int num4;
      int num5;
      if ((double) arcAngle >= 0.0)
      {
        num3 = (int) Math.Ceiling((double) startAngle / (double) num2);
        num4 = (int) Math.Floor((double) num1 / (double) num2);
        num5 = num4 >= num3 ? num4 - num3 + 1 : circleSubdivisions.Count - num3 + num4 + 1;
      }
      else
      {
        num3 = (int) Math.Floor((double) startAngle / (double) num2);
        num4 = (int) Math.Ceiling((double) num1 / (double) num2);
        num5 = num3 >= num4 ? num3 - num4 + 1 : circleSubdivisions.Count - num4 + num3 + 1;
      }
      this.CheckBufferFreeSpace(num5 + 2);
      int geometryIndex = this._geometryIndex;
      if ((double) arcAngle >= 0.0)
      {
        if ((double) num3 * (double) num2 - (double) startAngle > 0.004999999888241291)
        {
          this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * (float) Math.Cos(-(double) startAngle), center.Y - radius * (float) Math.Sin(-(double) startAngle));
          ++num5;
        }
        if (num3 <= num4)
        {
          for (int index = num3; index <= num4; ++index)
            this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        else
        {
          for (int index = num3; index < circleSubdivisions.Count; ++index)
            this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
          for (int index = 0; index <= num4; ++index)
            this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        if ((double) num1 - (double) num4 * (double) num2 > 0.004999999888241291)
        {
          this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * (float) Math.Cos(-(double) num1), center.Y - radius * (float) Math.Sin(-(double) num1));
          ++num5;
        }
      }
      else
      {
        if ((double) startAngle - (double) num3 * (double) num2 > 0.004999999888241291)
        {
          this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * (float) Math.Cos(-(double) startAngle), center.Y - radius * (float) Math.Sin(-(double) startAngle));
          ++num5;
        }
        if (num4 <= num3)
        {
          for (int index = num3; index >= num4; --index)
            this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        else
        {
          for (int index = num3; index >= 0; --index)
            this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
          for (int index = circleSubdivisions.Count - 1; index >= num4; --index)
            this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        if ((double) num4 * (double) num2 - (double) num1 > 0.004999999888241291)
        {
          this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * (float) Math.Cos(-(double) num1), center.Y - radius * (float) Math.Sin(-(double) num1));
          ++num5;
        }
      }
      if (this._calculateLengths)
      {
        float num6 = 2f * arcAngle * radius / num2;
        for (int index = geometryIndex; index < this._geometryIndex; ++index)
          this._lengthBuffer[geometryIndex + index] = this._lengthBuffer[geometryIndex + index - 1] + num6;
      }
      return num5;
    }

    private void BuildCircleGeometryBuffer(
      Vector2 center,
      float radius,
      int subdivisions,
      bool connect)
    {
      List<Vector2> circleSubdivisions = PathBuilder.CalculateCircleSubdivisions(subdivisions);
      this.CheckBufferFreeSpace(subdivisions + 1);
      int geometryIndex = this._geometryIndex;
      for (int index = 0; index < subdivisions; ++index)
        this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y + radius * circleSubdivisions[index].Y);
      if (connect)
        this._geometryBuffer[this._geometryIndex++] = new Vector2(center.X + radius * circleSubdivisions[0].X, center.Y + radius * circleSubdivisions[0].Y);
      if (!this._calculateLengths)
        return;
      float num = 6.28318548f * radius / (float) subdivisions;
      for (int index = geometryIndex; index < this._geometryIndex; ++index)
        this._lengthBuffer[geometryIndex + index] = this._lengthBuffer[geometryIndex + index - 1] + num;
    }

    private static List<Vector2> CalculateCircleSubdivisions(int divisions)
    {
      lock (PathBuilder._circleCache)
      {
        if (PathBuilder._circleCache.ContainsKey(divisions))
          return PathBuilder._circleCache[divisions];
      }
      if (divisions < 0)
        throw new ArgumentOutOfRangeException(nameof (divisions));
      double num1 = 2.0 * Math.PI / (double) divisions;
      List<Vector2> circleSubdivisions = new List<Vector2>();
      for (int index = 0; index < divisions; ++index)
      {
        double num2 = -num1 * (double) index;
        circleSubdivisions.Add(new Vector2((float) Math.Cos(num2), (float) Math.Sin(num2)));
      }
      lock (PathBuilder._circleCache)
      {
        PathBuilder._circleCache.Add(divisions, circleSubdivisions);
        return circleSubdivisions;
      }
    }

    private void BuildQuadraticBezierGeometryBuffer(
      Vector2 v0,
      Vector2 v1,
      Vector2 v2,
      int subdivisions)
    {
      this.CheckBufferFreeSpace(subdivisions + 1);
      int geometryIndex = this._geometryIndex;
      float num1 = 1f / (float) (subdivisions - 1);
      float t = 0.0f;
      int num2 = 0;
      while (num2 < subdivisions)
      {
        if (1.0 - (double) t < 5E-06)
          t = 1f;
        float num3 = PathBuilder.Bernstein(2, 0, t);
        float num4 = PathBuilder.Bernstein(2, 1, t);
        float num5 = PathBuilder.Bernstein(2, 2, t);
        this._geometryBuffer[this._geometryIndex++] = new Vector2((float) ((double) num3 * (double) v0.X + (double) num4 * (double) v1.X + (double) num5 * (double) v2.X), (float) ((double) num3 * (double) v0.Y + (double) num4 * (double) v1.Y + (double) num5 * (double) v2.Y));
        ++num2;
        t += num1;
      }
      if (!this._calculateLengths)
        return;
      this.CalculateLengthsInRange(geometryIndex, this._geometryIndex - geometryIndex);
    }

    private void BuildCubicBezierGeometryBuffer(
      Vector2 v0,
      Vector2 v1,
      Vector2 v2,
      Vector2 v3,
      int subdivisions)
    {
      this.CheckBufferFreeSpace(subdivisions + 1);
      int geometryIndex = this._geometryIndex;
      float num1 = 1f / (float) (subdivisions - 1);
      float t = 0.0f;
      int num2 = 0;
      while (num2 < subdivisions)
      {
        if (1.0 - (double) t < 5E-06)
          t = 1f;
        float num3 = PathBuilder.Bernstein(3, 0, t);
        float num4 = PathBuilder.Bernstein(3, 1, t);
        float num5 = PathBuilder.Bernstein(3, 2, t);
        float num6 = PathBuilder.Bernstein(3, 3, t);
        this._geometryBuffer[this._geometryIndex++] = new Vector2((float) ((double) num3 * (double) v0.X + (double) num4 * (double) v1.X + (double) num5 * (double) v2.X + (double) num6 * (double) v3.X), (float) ((double) num3 * (double) v0.Y + (double) num4 * (double) v1.Y + (double) num5 * (double) v2.Y + (double) num6 * (double) v3.Y));
        ++num2;
        t += num1;
      }
      if (!this._calculateLengths)
        return;
      this.CalculateLengthsInRange(geometryIndex, this._geometryIndex - geometryIndex);
    }

    private static double Factorial(int n)
    {
      return n >= 0 && n <= 32 ? PathBuilder._factorials[n] : throw new ArgumentOutOfRangeException(nameof (n), "n must be between 0 and 32.");
    }

    private static float Ni(int n, int i)
    {
      return (float) (PathBuilder.Factorial(n) / (PathBuilder.Factorial(i) * PathBuilder.Factorial(n - i)));
    }

    private static float Bernstein(int n, int i, float t)
    {
      float num1 = (double) t != 0.0 || i != 0 ? (float) Math.Pow((double) t, (double) i) : 1f;
      float num2 = n != i || (double) t != 1.0 ? (float) Math.Pow(1.0 - (double) t, (double) (n - i)) : 1f;
      return PathBuilder.Ni(n, i) * num1 * num2;
    }

    private static int DefaultBezierSubdivisions(Vector2 p0, Vector2 p1, Vector2 p2)
    {
      Vector2 vector2_1 = Vector2.Lerp(p0, p1, 0.5f);
      Vector2 vector2_2 = Vector2.Lerp(p1, p2, 0.5f);
      return (int) ((double) (PathBuilder.ApproxDistance(p0, vector2_1) + PathBuilder.ApproxDistance(vector2_1, vector2_2) + PathBuilder.ApproxDistance(vector2_2, p2)) * 0.10000000149011612);
    }

    private static int DefaultBezierSubdivisions(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
      Vector2 vector2_1 = Vector2.Lerp(p0, p1, 0.5f);
      Vector2 vector2_2 = Vector2.Lerp(p1, p2, 0.5f);
      Vector2 vector2_3 = Vector2.Lerp(p2, p3, 0.5f);
      return (int) ((double) (PathBuilder.ApproxDistance(p0, vector2_1) + PathBuilder.ApproxDistance(vector2_1, vector2_2) + PathBuilder.ApproxDistance(vector2_2, vector2_3) + PathBuilder.ApproxDistance(vector2_3, p3)) * 0.10000000149011612);
    }

    private static float ApproxDistance(Vector2 p0, Vector2 p1)
    {
      return PathBuilder.ApproxDistance(Math.Abs(p1.X - p0.X), Math.Abs(p1.Y - p0.Y));
    }

    private static float ApproxDistance(float dx, float dy)
    {
      if ((double) dy < (double) dx)
      {
        float num = dy * 0.25f;
        return (float) ((double) dx + (double) num + (double) num * 0.5);
      }
      float num1 = dx * 0.25f;
      return (float) ((double) dy + (double) num1 + (double) num1 * 0.5);
    }

    private static float PointToAngle(Vector2 center, Vector2 point)
    {
      double angle = Math.Atan2((double) point.Y - (double) center.Y, (double) point.X - (double) center.X);
      if (angle < 0.0)
        angle += 2.0 * Math.PI;
      return (float) angle;
    }

    private static float ClampAngle(float angle)
    {
      if ((double) angle < 0.0)
        angle += (float) (Math.Ceiling((double) angle / (-2.0 * Math.PI)) * Math.PI * 2.0);
      else if ((double) angle >= 2.0 * Math.PI)
        angle -= (float) (Math.Floor((double) angle / (2.0 * Math.PI)) * Math.PI * 2.0);
      return angle;
    }

    private static int DefaultSubdivisions(float radius)
    {
      return (int) Math.Ceiling((double) radius / 1.5);
    }

    private bool LastPointEqual(Vector2 point)
    {
      return this._geometryIndex > 0 && this._geometryBuffer[this._geometryIndex - 1] == point;
    }

    private bool LastPointClose(Vector2 point)
    {
      return this._geometryIndex > 0 && PathBuilder.PointsClose(this._geometryBuffer[this._geometryIndex - 1], point);
    }

    private static bool PointsClose(Vector2 a, Vector2 b)
    {
      return (double) Math.Abs(a.X - b.X) < 0.005 && (double) Math.Abs(a.Y - b.Y) < 0.005;
    }
  }
}
