// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Pen
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Drawing.Utility;
using System;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>Objects used to draw paths.</summary>
  public class Pen : IDisposable
  {
    private float _joinLimit;
    private float _joinLimitCos2;
    private float _width;
    private LineCap _startCap;
    private LineCap _endCap;
    private bool _disposed;

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Black { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Blue { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Brown { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Cyan { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkBlue { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkCyan { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkGoldenrod { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkGray { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkGreen { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkMagenta { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkOrange { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen DarkRed { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Goldenrod { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Gray { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Green { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen LightBlue { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen LightCyan { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen LightGray { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen LightGreen { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen LightPink { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen LightYellow { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Lime { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Magenta { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Orange { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Pink { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Purple { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Red { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Teal { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen White { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.</summary>
    public static Pen Yellow { get; private set; }

    static Pen()
    {
      Pen.Black = new Pen(Brush.Black);
      Pen.Blue = new Pen(Brush.Blue);
      Pen.Brown = new Pen(Brush.Brown);
      Pen.Cyan = new Pen(Brush.Cyan);
      Pen.DarkBlue = new Pen(Brush.DarkBlue);
      Pen.DarkCyan = new Pen(Brush.DarkCyan);
      Pen.DarkGoldenrod = new Pen(Brush.DarkGoldenrod);
      Pen.DarkGray = new Pen(Brush.DarkGray);
      Pen.DarkGreen = new Pen(Brush.DarkGreen);
      Pen.DarkMagenta = new Pen(Brush.DarkMagenta);
      Pen.DarkOrange = new Pen(Brush.DarkOrange);
      Pen.DarkRed = new Pen(Brush.DarkRed);
      Pen.Goldenrod = new Pen(Brush.Goldenrod);
      Pen.Gray = new Pen(Brush.Gray);
      Pen.Green = new Pen(Brush.Green);
      Pen.LightBlue = new Pen(Brush.LightBlue);
      Pen.LightCyan = new Pen(Brush.LightCyan);
      Pen.LightGray = new Pen(Brush.LightGray);
      Pen.LightGreen = new Pen(Brush.LightGreen);
      Pen.LightPink = new Pen(Brush.LightPink);
      Pen.LightYellow = new Pen(Brush.LightYellow);
      Pen.Lime = new Pen(Brush.Lime);
      Pen.Magenta = new Pen(Brush.Magenta);
      Pen.Orange = new Pen(Brush.Orange);
      Pen.Pink = new Pen(Brush.Pink);
      Pen.Purple = new Pen(Brush.Purple);
      Pen.Red = new Pen(Brush.Red);
      Pen.Teal = new Pen(Brush.Teal);
      Pen.White = new Pen(Brush.White);
      Pen.Yellow = new Pen(Brush.Yellow);
    }

    /// <summary>Gets the solid color or blending color of the pen.</summary>
    public Color Color => this.Brush.Color;

    /// <summary>
    /// Gets the <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" /> used to fill stroked paths.
    /// </summary>
    public Brush Brush { get; private set; }

    /// <summary>
    /// Gets or sets the width of the stroked path in graphical units (usually pixels).
    /// </summary>
    public float Width
    {
      get => this._width;
      set
      {
        this._width = value;
        this.StartCapInfo = this.CreateLineCapInfo(this.StartCap, value);
        this.EndCapInfo = this.CreateLineCapInfo(this.EndCap, value);
      }
    }

    /// <summary>
    /// Gets or sets the alignment of the stroked path relative to the ideal path being stroked.
    /// </summary>
    public PenAlignment Alignment { get; set; }

    /// <summary>
    /// Gets or sets how the start of a stroked path is terminated.
    /// </summary>
    public LineCap StartCap
    {
      get => this._startCap;
      set
      {
        this._startCap = value;
        this.StartCapInfo = this.CreateLineCapInfo(value, this.Width);
      }
    }

    /// <summary>
    /// Gets or sets how the end of a stroked path is terminated.
    /// </summary>
    public LineCap EndCap
    {
      get => this._endCap;
      set
      {
        this._endCap = value;
        this.EndCapInfo = this.CreateLineCapInfo(value, this.Width);
      }
    }

    /// <summary>
    /// Gets or sets how the segments in the path are joined together.
    /// </summary>
    public LineJoin LineJoin { get; set; }

    /// <summary>
    /// Gets or sets the limit of the thickness of the join on a mitered corner.
    /// </summary>
    /// <remarks><para>The miter length is the distance from the intersection of the line walls on the inside of the join to the intersection of the line walls outside of the join. The miter length can be large when the angle between two lines is small. The miter limit is the maximum allowed ratio of miter length to stroke width. The default value is 10.0f.</para>
    /// <para>If the miter length of the join of the intersection exceeds the limit of the join, then the join will be beveled to keep it within the limit of the join of the intersection.</para></remarks>
    public float MiterLimit { get; set; }

    /// <summary>
    /// Gets or sets the angle difference threshold in radians under which joins will be mitered instead of beveled or rounded.
    /// Defaults to PI / 8 (11.25 degrees).
    /// </summary>
    public float JoinLimit
    {
      get => this._joinLimit;
      set
      {
        this._joinLimit = value;
        this._joinLimitCos2 = (float) Math.Cos((double) this._joinLimit);
        this._joinLimitCos2 *= this._joinLimitCos2;
      }
    }

    /// <summary>
    /// Gets or sets whether this pen "owns" the brush used to construct it, and should therefor dispose the brush
    /// along with itself.
    /// </summary>
    public bool OwnsBrush { get; set; }

    /// <summary>
    /// Gets whether this pen needs path length values to properly calculate values at each sample point on the path.
    /// </summary>
    public virtual bool NeedsPathLength => false;

    internal LineCapInfo StartCapInfo { get; private set; }

    internal LineCapInfo EndCapInfo { get; private set; }

    private Pen()
    {
      this.Alignment = PenAlignment.Center;
      this.MiterLimit = 10f;
      this.JoinLimit = 0.3926991f;
      this._startCap = LineCap.Flat;
      this._endCap = LineCap.Flat;
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pen" /> with the given brush and width.
    /// </summary>
    /// <param name="brush">The <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" /> used to stroke the pen.</param>
    /// <param name="width">The width of the paths drawn by the pen.</param>
    /// <param name="ownsBrush"><c>true</c> if the pen should be responsible for disposing the <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" />, <c>false</c> otherwise.</param>
    public Pen(Brush brush, float width, bool ownsBrush)
      : this()
    {
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this._width = width;
      this.Brush = brush;
      this.OwnsBrush = ownsBrush;
      this.StartCapInfo = this.CreateLineCapInfo(this.StartCap, this.Width);
      this.EndCapInfo = this.CreateLineCapInfo(this.EndCap, this.Width);
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pen" /> with the given brush and width.
    /// </summary>
    /// <param name="brush">The <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" /> used to stroke the pen.</param>
    /// <param name="width">The width of the paths drawn by the pen.</param>
    /// <remarks>By default, the pen will not take resposibility for disposing the <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" />.</remarks>
    public Pen(Brush brush, float width)
      : this(brush, width, false)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pen" /> with the given color and width.
    /// </summary>
    /// <param name="color">The color used to stroke the pen.</param>
    /// <param name="width">The width of the paths drawn by the pen.</param>
    public Pen(Color color, float width)
      : this((Brush) new SolidColorBrush(color), width, true)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pen" /> with the given brush and a width of 1.
    /// </summary>
    /// <param name="brush">The <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" /> used to stroke the pen.</param>
    /// <remarks>By default, the pen will not take resposibility for disposing the <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" />.</remarks>
    public Pen(Brush brush)
      : this(brush, 1f, false)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pen" /> with the given brush and a width of 1.
    /// </summary>
    /// <param name="brush">The <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" /> used to stroke the pen.</param>
    /// <param name="ownsBrush"><c>true</c> if the pen should be responsible for disposing the <see cref="P:Sharp2D.Engine.Drawing.Pen.Brush" />, <c>false</c> otherwise.</param>
    public Pen(Brush brush, bool ownsBrush)
      : this(brush, 1f, ownsBrush)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pen" /> with the given color and a width of 1.
    /// </summary>
    /// <param name="color">The color used to stroke the pen.</param>
    public Pen(Color color)
      : this(color, 1f)
    {
    }

    /// <summary>
    /// Releases all resources used by the <see cref="T:Sharp2D.Engine.Drawing.Pen" /> object.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (this._disposed)
        return;
      if (disposing)
      {
        if (this.OwnsBrush && this.Brush != null)
          this.Brush.Dispose();
        this.DisposeManaged();
      }
      this.DisposeUnmanaged();
      this._disposed = true;
    }

    /// <summary>Attempts to dispose unmanaged resources.</summary>
    ~Pen() => this.Dispose(false);

    /// <summary>
    /// Releases the managed resources used by the <see cref="T:Sharp2D.Engine.Drawing.Pen" />.
    /// </summary>
    protected virtual void DisposeManaged()
    {
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:Sharp2D.Engine.Drawing.Pen" />.
    /// </summary>
    protected virtual void DisposeUnmanaged()
    {
    }

    /// <summary>
    /// Queries the <see cref="T:Sharp2D.Engine.Drawing.Pen" /> for its color at a coordinate relative to the stroke width of the pen and length of the path.
    /// </summary>
    /// <param name="widthPosition">A value between 0 and 1 interpolated across the stroke width.</param>
    /// <param name="lengthPosition">A value between 0 and the full length of the path.</param>
    /// <param name="length">A scaling factor such that lengthPosition can be normalized to a value between 0 and 1.</param>
    /// <returns>A color value.</returns>
    protected internal virtual Color ColorAt(
      float widthPosition,
      float lengthPosition,
      float length)
    {
      return this.Brush.Color;
    }

    private LineCapInfo CreateLineCapInfo(LineCap lineCapType, float width)
    {
      switch (lineCapType)
      {
        case LineCap.Flat:
          return (LineCapInfo) new LineCapFlat(width);
        case LineCap.Square:
          return (LineCapInfo) new LineCapSquare(width);
        case LineCap.Triangle:
          return (LineCapInfo) new LineCapTriangle(width);
        case LineCap.InvTriangle:
          return (LineCapInfo) new LineCapInvTriangle(width);
        case LineCap.Arrow:
          return (LineCapInfo) new LineCapArrow(width);
        default:
          return (LineCapInfo) new LineCapFlat(width);
      }
    }

    internal Color ColorAt(Vector2 uv, float lengthScale) => this.ColorAt(uv.X, uv.Y, lengthScale);

    internal int StartPointVertexBound() => this.StartCapInfo.VertexCount;

    internal int EndPointVertexBound() => this.EndCapInfo.VertexCount;

    internal int LineJoinVertexBound()
    {
      switch (this.LineJoin)
      {
        case LineJoin.Miter:
          return 3;
        case LineJoin.Bevel:
          return 3;
        default:
          return 0;
      }
    }

    internal int MaximumVertexCount(int pointCount)
    {
      int num1 = this.StartCapInfo.VertexCount + this.EndCapInfo.VertexCount;
      int num2 = Math.Max(0, pointCount - 1);
      switch (this.LineJoin)
      {
        case LineJoin.Miter:
          num1 += num2 * 3;
          break;
        case LineJoin.Bevel:
          num1 += num2 * 3;
          break;
      }
      return num1;
    }

    internal int MaximumIndexCount(int pointCount)
    {
      int num1 = this.StartCapInfo.IndexCount + this.EndCapInfo.IndexCount;
      int num2 = Math.Max(0, pointCount - 1);
      switch (this.LineJoin)
      {
        case LineJoin.Miter:
          num1 += num2;
          break;
        case LineJoin.Bevel:
          num1 += num2;
          break;
      }
      return num1 * 3 + (pointCount - 1) * 6;
    }

    internal InsetOutsetCount ComputeMiter(ref JoinSample js, PenWorkspace ws)
    {
      Vector2 pointA = js.PointA;
      Vector2 pointB = js.PointB;
      Vector2 pointC = js.PointC;
      Vector2 vector2_1 = new Vector2(pointB.X - pointA.X, pointB.Y - pointA.Y);
      vector2_1.Normalize();
      Vector2 vector2_2 = new Vector2(-vector2_1.Y, vector2_1.X);
      Vector2 vector2_3 = new Vector2(pointC.X - pointB.X, pointC.Y - pointB.Y);
      vector2_3.Normalize();
      Vector2 vector2_4 = new Vector2(-vector2_3.Y, vector2_3.X);
      Vector2 vector2_5;
      Vector2 vector2_6;
      Vector2 vector2_7;
      Vector2 vector2_8;
      switch (this.Alignment)
      {
        case PenAlignment.Center:
          float num1 = this.Width / 2f;
          vector2_5 = new Vector2(pointA.X + num1 * vector2_2.X, pointA.Y + num1 * vector2_2.Y);
          vector2_6 = new Vector2(pointA.X - num1 * vector2_2.X, pointA.Y - num1 * vector2_2.Y);
          vector2_7 = new Vector2(pointC.X + num1 * vector2_4.X, pointC.Y + num1 * vector2_4.Y);
          vector2_8 = new Vector2(pointC.X - num1 * vector2_4.X, pointC.Y - num1 * vector2_4.Y);
          break;
        case PenAlignment.Inset:
          vector2_5 = new Vector2(pointA.X + this.Width * vector2_2.X, pointA.Y + this.Width * vector2_2.Y);
          vector2_6 = pointA;
          vector2_7 = new Vector2(pointC.X + this.Width * vector2_4.X, pointC.Y + this.Width * vector2_4.Y);
          vector2_8 = pointC;
          break;
        case PenAlignment.Outset:
          vector2_5 = pointA;
          vector2_6 = new Vector2(pointA.X - this.Width * vector2_2.X, pointA.Y - this.Width * vector2_2.Y);
          vector2_7 = pointC;
          vector2_8 = new Vector2(pointC.X - this.Width * vector2_4.X, pointC.Y - this.Width * vector2_4.Y);
          break;
        default:
          vector2_5 = Vector2.Zero;
          vector2_6 = Vector2.Zero;
          vector2_7 = Vector2.Zero;
          vector2_8 = Vector2.Zero;
          break;
      }
      float num2 = Vector2.Dot(vector2_4, vector2_1);
      Vector2 vector2_9;
      Vector2 vector2_10;
      if ((double) Math.Abs(num2) < 0.00050000002374872565)
      {
        vector2_9 = new Vector2((float) (((double) vector2_5.X + (double) vector2_7.X) / 2.0), (float) (((double) vector2_5.Y + (double) vector2_7.Y) / 2.0));
        vector2_10 = new Vector2((float) (((double) vector2_6.X + (double) vector2_8.X) / 2.0), (float) (((double) vector2_6.Y + (double) vector2_8.Y) / 2.0));
      }
      else
      {
        float num3 = (Vector2.Dot(vector2_4, vector2_7) - Vector2.Dot(vector2_4, vector2_5)) / num2;
        float num4 = (Vector2.Dot(vector2_4, vector2_8) - Vector2.Dot(vector2_4, vector2_6)) / num2;
        vector2_9 = new Vector2(vector2_5.X + num3 * vector2_1.X, vector2_5.Y + num3 * vector2_1.Y);
        vector2_10 = new Vector2(vector2_6.X + num4 * vector2_1.X, vector2_6.Y + num4 * vector2_1.Y);
      }
      double num5 = (double) this.MiterLimit * (double) this.Width;
      if ((double) (vector2_9 - vector2_10).LengthSquared() > num5 * num5)
        return this.ComputeBevel(ref js, ws);
      ws.XYInsetBuffer[0] = vector2_9;
      ws.XYOutsetBuffer[0] = vector2_10;
      ws.UVInsetBuffer[0] = new Vector2(0.0f, js.LengthB);
      ws.UVOutsetBuffer[0] = new Vector2(1f, js.LengthB);
      return new InsetOutsetCount((short) 1, (short) 1);
    }

    internal InsetOutsetCount ComputeBevel(ref JoinSample js, PenWorkspace ws)
    {
      Vector2 pointA = js.PointA;
      Vector2 pointB = js.PointB;
      Vector2 pointC = js.PointC;
      Vector2 vector2_1 = new Vector2(pointA.X - pointB.X, pointA.Y - pointB.Y);
      Vector2 v = new Vector2(pointC.X - pointB.X, pointC.Y - pointB.Y);
      double num1 = (double) Vector2.Dot(vector2_1, v);
      if (num1 < 0.0)
      {
        double num2 = (double) vector2_1.LengthSquared() * (double) v.LengthSquared();
        if (num1 * num1 / num2 > (double) this._joinLimitCos2)
          return this.ComputeMiter(ref js, ws);
      }
      Vector2 u = new Vector2(pointB.X - pointA.X, pointB.Y - pointA.Y);
      u.Normalize();
      Vector2 vector2_2 = new Vector2(-u.Y, u.X);
      v.Normalize();
      Vector2 vector2_3 = new Vector2(-v.Y, v.X);
      Vector2 vector2_4 = pointA;
      Vector2 vector2_5 = pointC;
      short num3 = 0;
      if ((double) this.Cross2D(u, v) > 0.0)
      {
        switch (this.Alignment)
        {
          case PenAlignment.Center:
            float num4 = this.Width / 2f;
            vector2_4 = new Vector2(pointA.X - num4 * vector2_2.X, pointA.Y - num4 * vector2_2.Y);
            vector2_5 = new Vector2(pointC.X - num4 * vector2_3.X, pointC.Y - num4 * vector2_3.Y);
            ws.XYInsetBuffer[0] = new Vector2(pointB.X + num4 * vector2_2.X, pointB.Y + num4 * vector2_2.Y);
            ws.XYInsetBuffer[1] = new Vector2(pointB.X + num4 * vector2_3.X, pointB.Y + num4 * vector2_3.Y);
            num3 = (short) 2;
            break;
          case PenAlignment.Inset:
            ws.XYInsetBuffer[0] = new Vector2(pointB.X + this.Width * vector2_2.X, pointB.Y + this.Width * vector2_2.Y);
            ws.XYInsetBuffer[1] = new Vector2(pointB.X + this.Width * vector2_3.X, pointB.Y + this.Width * vector2_3.Y);
            num3 = (short) 2;
            break;
          case PenAlignment.Outset:
            vector2_4 = new Vector2(pointA.X - this.Width * vector2_2.X, pointA.Y - this.Width * vector2_2.Y);
            vector2_5 = new Vector2(pointC.X - this.Width * vector2_3.X, pointC.Y - this.Width * vector2_3.Y);
            ws.XYInsetBuffer[0] = pointB;
            num3 = (short) 1;
            break;
        }
        float num5 = Vector2.Dot(vector2_3, u);
        Vector2 vector2_6;
        if ((double) Math.Abs(num5) < 0.00050000002374872565)
        {
          vector2_6 = new Vector2((float) (((double) vector2_4.X + (double) vector2_5.X) / 2.0), (float) (((double) vector2_4.Y + (double) vector2_5.Y) / 2.0));
        }
        else
        {
          float num6 = (Vector2.Dot(vector2_3, vector2_5) - Vector2.Dot(vector2_3, vector2_4)) / num5;
          vector2_6 = new Vector2(vector2_4.X + num6 * u.X, vector2_4.Y + num6 * u.Y);
        }
        ws.XYOutsetBuffer[0] = vector2_6;
        ws.UVOutsetBuffer[0] = new Vector2(1f, js.LengthB);
        for (int index = 0; index < (int) num3; ++index)
          ws.UVInsetBuffer[index] = new Vector2(0.0f, js.LengthB);
        return new InsetOutsetCount(num3, (short) 1, false);
      }
      switch (this.Alignment)
      {
        case PenAlignment.Center:
          float num7 = this.Width / 2f;
          vector2_4 = new Vector2(pointA.X + num7 * vector2_2.X, pointA.Y + num7 * vector2_2.Y);
          vector2_5 = new Vector2(pointC.X + num7 * vector2_3.X, pointC.Y + num7 * vector2_3.Y);
          ws.XYOutsetBuffer[0] = new Vector2(pointB.X - num7 * vector2_2.X, pointB.Y - num7 * vector2_2.Y);
          ws.XYOutsetBuffer[1] = new Vector2(pointB.X - num7 * vector2_3.X, pointB.Y - num7 * vector2_3.Y);
          num3 = (short) 2;
          break;
        case PenAlignment.Inset:
          vector2_4 = new Vector2(pointA.X + this.Width * vector2_2.X, pointA.Y + this.Width * vector2_2.Y);
          vector2_5 = new Vector2(pointC.X + this.Width * vector2_3.X, pointC.Y + this.Width * vector2_3.Y);
          ws.XYOutsetBuffer[0] = pointB;
          num3 = (short) 1;
          break;
        case PenAlignment.Outset:
          ws.XYOutsetBuffer[0] = new Vector2(pointB.X - this.Width * vector2_2.X, pointB.Y - this.Width * vector2_2.Y);
          ws.XYOutsetBuffer[1] = new Vector2(pointB.X - this.Width * vector2_3.X, pointB.Y - this.Width * vector2_3.Y);
          num3 = (short) 2;
          break;
      }
      float num8 = Vector2.Dot(vector2_3, u);
      Vector2 vector2_7;
      if ((double) Math.Abs(num8) < 0.00050000002374872565)
      {
        vector2_7 = new Vector2((float) (((double) vector2_4.X + (double) vector2_5.X) / 2.0), (float) (((double) vector2_4.Y + (double) vector2_5.Y) / 2.0));
      }
      else
      {
        float num9 = (Vector2.Dot(vector2_3, vector2_5) - Vector2.Dot(vector2_3, vector2_4)) / num8;
        vector2_7 = new Vector2(vector2_4.X + num9 * u.X, vector2_4.Y + num9 * u.Y);
      }
      ws.XYInsetBuffer[0] = vector2_7;
      ws.UVInsetBuffer[0] = new Vector2(0.0f, js.LengthB);
      for (int index = 0; index < (int) num3; ++index)
        ws.UVOutsetBuffer[index] = new Vector2(1f, js.LengthB);
      return new InsetOutsetCount((short) 1, num3, true);
    }

    private float Cross2D(Vector2 u, Vector2 v)
    {
      return (float) ((double) u.Y * (double) v.X - (double) u.X * (double) v.Y);
    }

    private bool TriangleIsCCW(Vector2 a, Vector2 b, Vector2 c)
    {
      return (double) this.Cross2D(b - a, c - b) < 0.0;
    }

    internal void ComputeStartPoint(Vector2 a, Vector2 b, PenWorkspace ws)
    {
      this.StartCapInfo.Calculate(a, b - a, ws, this.Alignment, true);
    }

    internal void ComputeEndPoint(Vector2 a, Vector2 b, PenWorkspace ws)
    {
      this.EndCapInfo.Calculate(b, a - b, ws, this.Alignment, false);
      for (int index = 0; index < ws.UVBuffer.Index; ++index)
        ws.UVBuffer[index] = new Vector2(1f - ws.UVBuffer[index].X, ws.PathLength);
    }
  }
}
