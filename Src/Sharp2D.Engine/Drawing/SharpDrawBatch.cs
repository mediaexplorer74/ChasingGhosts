// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.SharpDrawBatch
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing.Utility;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  public class SharpDrawBatch : IDisposable
  {
    private static Matrix identityMatrix = Matrix.Identity;
    private bool inDraw;
    private bool isDisposed;
    private SharpDrawBatch.DrawingInfo[] infoBuffer;
    private short[] indexBuffer;
    private VertexPositionColorTexture[] vertexBuffer;
    private Vector2[] geometryBuffer;
    private PathBuilder pathBuilder;
    private int infoBufferIndex;
    private int indexBufferIndex;
    private int vertexBufferIndex;
    private Triangulator triangulator;
    private DrawSortMode sortMode;
    private BlendState blendState;
    private SamplerState samplerState;
    private DepthStencilState depthStencilState;
    private RasterizerState rasterizerState;
    private Effect effect;
    private Matrix transform;
    private BasicEffect standardEffect;
    private Texture2D defaultTexture;
    private PenWorkspace ws;
    private Dictionary<int, List<Vector2>> circleCache = new Dictionary<int, List<Vector2>>();

    public SharpDrawBatch(GraphicsDevice device)
    {
      this.Batch = new SpriteBatch(device);
      this.Initialize();
    }

    public SharpDrawBatch(SpriteBatch batch)
    {
      this.Batch = batch;
      this.Initialize();
    }

    private void Initialize()
    {
      this.Batch.GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(this.GraphicsDeviceReset);
      this.infoBuffer = new SharpDrawBatch.DrawingInfo[2048];
      this.indexBuffer = new short[32768];
      this.vertexBuffer = new VertexPositionColorTexture[8192];
      this.geometryBuffer = new Vector2[256];
      this.pathBuilder = new PathBuilder();
      this.standardEffect = new BasicEffect(this.Batch.GraphicsDevice);
      this.standardEffect.TextureEnabled = true;
      this.standardEffect.VertexColorEnabled = true;
      this.defaultTexture = new Texture2D(this.Batch.GraphicsDevice, 1, 1);
      this.defaultTexture.SetData<Color>(new Color[1]
      {
        Color.White
      });
      this.ws = new PenWorkspace();
    }

    /// <summary>
    /// Releases all resources used by the <see cref="!:DrawBatch" /> object.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!(!this.isDisposed & disposing))
        return;
      this.Batch.GraphicsDevice.DeviceReset -= new EventHandler<EventArgs>(this.GraphicsDeviceReset);
      this.standardEffect.Dispose();
      this.defaultTexture.Dispose();
      this.isDisposed = true;
      this.Batch.Dispose();
    }

    private void GraphicsDeviceReset(object sender, EventArgs e)
    {
      this.standardEffect.Dispose();
      this.standardEffect = new BasicEffect(this.Batch.GraphicsDevice);
      this.standardEffect.TextureEnabled = true;
      this.standardEffect.VertexColorEnabled = true;
      this.defaultTexture.Dispose();
      this.defaultTexture = new Texture2D(this.Batch.GraphicsDevice, 1, 1);
      this.defaultTexture.SetData<Color>(new Color[1]
      {
        Color.White
      });
    }

    /// <summary>
    /// Gets whether the <see cref="!:DrawBatch" /> has been disposed or not.
    /// </summary>
    public bool IsDisposed => this.isDisposed;

    public SpriteBatch Batch { get; }

    public void Begin(
      SpriteSortMode sortMode = SpriteSortMode.Deferred,
      BlendState blendState = null,
      SamplerState samplerState = null,
      DepthStencilState depthStencilState = null,
      RasterizerState rasterizerState = null,
      Effect effect = null,
      Matrix? transformMatrix = null)
    {
      this.Batch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
      if (this.inDraw)
        throw new InvalidOperationException("DrawBatch already inside Begin/End pair");
      this.sortMode = (DrawSortMode) sortMode;
      this.blendState = blendState;
      this.samplerState = samplerState;
      this.depthStencilState = depthStencilState;
      this.rasterizerState = rasterizerState;
      this.effect = effect;
      this.transform = transformMatrix ?? Matrix.Identity;
      this.infoBufferIndex = 0;
      this.indexBufferIndex = 0;
      this.vertexBufferIndex = 0;
      if (sortMode == SpriteSortMode.Immediate)
        this.SetRenderState();
      this.inDraw = true;
    }

    public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
    {
      this.Batch.Draw(texture, destinationRectangle, color);
    }

    public void Draw(Texture2D texture, Vector2 position, Color color)
    {
      this.Batch.Draw(texture, position, color);
    }

    public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
    {
      this.Batch.Draw(texture, position, sourceRectangle, color);
    }

    public void Draw(
      Texture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
    }

    public void Draw(
      Texture2D texture,
      Rectangle destinationRectangle,
      Rectangle? sourceRectangle,
      Color color)
    {
      this.Batch.Draw(texture, destinationRectangle, sourceRectangle, color);
    }

    public void Draw(
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      Vector2 scale,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    [Obsolete("In future versions this method can be removed.")]
    public void Draw(
      Texture2D texture,
      Rectangle position = default,
      Vector2 destinationRectangle = default,
      Rectangle? sourceRectangle = default,
      Vector2 origin = default,
      float rotation = default,
      Vector2 scale = default,
      Color color = default,
      SpriteEffects effects = SpriteEffects.None,
      float layerDepth = 0.0f)
    {
       //RnD
       this.Batch.Draw(texture, destinationRectangle, sourceRectangle, color, 
         rotation, origin, scale, effects, layerDepth);
         //sourceRectangle, origin, rotation, scale, color, effects, layerDepth);
    }

    public void Draw(
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    public void DrawString(
      SpriteFont spriteFont,
      StringBuilder text,
      Vector2 position,
      Color color,
      float rotation,
      Vector2 origin,
      Vector2 scale,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.DrawString(spriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
    }

    public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color)
    {
      this.Batch.DrawString(spriteFont, text, position, color);
    }

    public void DrawString(
      SpriteFont spriteFont,
      string text,
      Vector2 position,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.DrawString(spriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
    }

    public void DrawString(
      SpriteFont spriteFont,
      string text,
      Vector2 position,
      Color color,
      float rotation,
      Vector2 origin,
      Vector2 scale,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.DrawString(spriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
    }

    public void DrawString(
      SpriteFont spriteFont,
      StringBuilder text,
      Vector2 position,
      Color color)
    {
      this.Batch.DrawString(spriteFont, text, position, color);
    }

    public void DrawString(
      SpriteFont spriteFont,
      StringBuilder text,
      Vector2 position,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects,
      float layerDepth)
    {
      this.Batch.DrawString(spriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
    }

    public void End()
    {
      this.Batch.End();
      this.inDraw = this.inDraw ? false : throw new InvalidOperationException();
      if (this.sortMode != DrawSortMode.Immediate)
        this.SetRenderState();
      this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds a rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="rect">The rectangle to be rendered.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawRectangle(Pen pen, Rectangle rect) => this.DrawRectangle(pen, rect, 0.0f);

    /// <summary>
    /// Computes and adds a rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="rect">The rectangle to be rendered.</param>
    /// <param name="angle">The angle to rotate the rectangle by around its center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawRectangle(Pen pen, Rectangle rect, float angle)
    {
      this.geometryBuffer[0] = new Vector2((float) rect.Left, (float) rect.Top);
      this.geometryBuffer[1] = new Vector2((float) rect.Right, (float) rect.Top);
      this.geometryBuffer[2] = new Vector2((float) rect.Right, (float) rect.Bottom);
      this.geometryBuffer[3] = new Vector2((float) rect.Left, (float) rect.Bottom);
      this.DrawQuad(pen, this.geometryBuffer, 0, angle);
    }

    /// <summary>
    /// Computes and adds a rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="location">The top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawRectangle(Pen pen, Vector2 location, float width, float height)
    {
      this.DrawRectangle(pen, location, width, height, 0.0f);
    }

    /// <summary>
    /// Computes and adds a rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="location">The top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="angle">The angle to rotate the rectangle by around its center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawRectangle(Pen pen, Vector2 location, float width, float height, float angle)
    {
      this.geometryBuffer[0] = location;
      this.geometryBuffer[1] = new Vector2(location.X + width, location.Y);
      this.geometryBuffer[2] = new Vector2(location.X + width, location.Y + height);
      this.geometryBuffer[3] = new Vector2(location.X, location.Y + height);
      this.DrawQuad(pen, this.geometryBuffer, 0, angle);
    }

    /// <summary>
    /// Computes and adds a quadrilateral path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="points">An array containing the coordinates of the quad.</param>
    /// <param name="offset">The offset into the points array.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawQuad</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawQuad(Pen pen, Vector2[] points, int offset)
    {
      this.DrawQuad(pen, points, offset, 0.0f);
    }

    /// <summary>
    /// Computes and adds a quadrilateral path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="points">An array containing the coordinates of the quad.</param>
    /// <param name="offset">The offset into the points array.</param>
    /// <param name="angle">The angle to rotate the quad by around its weighted center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawQuad</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawQuad(Pen pen, Vector2[] points, int offset, float angle)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      if (points == null)
        throw new ArgumentNullException(nameof (points));
      if (points.Length < offset + 4)
        throw new ArgumentException("Points array is too small for the given offset.");
      this.RequestBufferSpace(8, 24);
      this.ws.ResetWorkspace(pen);
      this.AddInfo(PrimitiveType.TriangleList, 8, 24, pen.Brush);
      if (points != this.geometryBuffer)
        Array.Copy((Array) points, (Array) this.geometryBuffer, 4);
      if ((double) angle != 0.0)
      {
        Vector2 vector2 = new Vector2((float) (((double) this.geometryBuffer[0].X + (double) this.geometryBuffer[1].X + (double) this.geometryBuffer[2].X + (double) this.geometryBuffer[3].X) / 4.0), (float) (((double) this.geometryBuffer[0].Y + (double) this.geometryBuffer[1].Y + (double) this.geometryBuffer[2].Y + (double) this.geometryBuffer[3].Y) / 4.0));
        Matrix rotationZ = Matrix.CreateRotationZ(angle) with
        {
          Translation = new Vector3(vector2, 0.0f)
        };
        for (int index = 0; index < 4; ++index)
          this.geometryBuffer[index] = Vector2.Transform(this.geometryBuffer[index] - vector2, rotationZ);
      }
      int vertexBufferIndex = this.vertexBufferIndex;
      JoinSample js = new JoinSample(this.geometryBuffer[0], this.geometryBuffer[1], this.geometryBuffer[2]);
      this.AddMiteredJoint(ref js, pen, this.ws);
      js.Advance(this.geometryBuffer[3]);
      this.AddMiteredJoint(ref js, pen, this.ws);
      js.Advance(this.geometryBuffer[0]);
      this.AddMiteredJoint(ref js, pen, this.ws);
      js.Advance(this.geometryBuffer[1]);
      this.AddMiteredJoint(ref js, pen, this.ws);
      this.AddSegment(vertexBufferIndex, vertexBufferIndex + 2);
      this.AddSegment(vertexBufferIndex + 2, vertexBufferIndex + 4);
      this.AddSegment(vertexBufferIndex + 4, vertexBufferIndex + 6);
      this.AddSegment(vertexBufferIndex + 6, vertexBufferIndex);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="rect">The rectangle to be rendered.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveRectangle(Pen pen, Rectangle rect)
    {
      this.DrawPrimitiveRectangle(pen, rect, 0.0f);
    }

    /// <summary>
    /// Adds a primitive rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="rect">The rectangle to be rendered.</param>
    /// <param name="angle">The angle to rotate the rectangle by around its center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveRectangle(Pen pen, Rectangle rect, float angle)
    {
      this.geometryBuffer[0] = new Vector2((float) rect.Left, (float) rect.Top);
      this.geometryBuffer[1] = new Vector2((float) rect.Right, (float) rect.Top);
      this.geometryBuffer[2] = new Vector2((float) rect.Right, (float) rect.Bottom);
      this.geometryBuffer[3] = new Vector2((float) rect.Left, (float) rect.Bottom);
      this.DrawPrimitiveQuad(pen, this.geometryBuffer, 0, angle);
    }

    /// <summary>
    /// Adds a primitive rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="location">The top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveRectangle(Pen pen, Vector2 location, float width, float height)
    {
      this.DrawPrimitiveRectangle(pen, location, width, height, 0.0f);
    }

    /// <summary>
    /// Adds a primitive rectangle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="location">The top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="angle">The angle to rotate the rectangle by around its center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveRectangle(
      Pen pen,
      Vector2 location,
      float width,
      float height,
      float angle)
    {
      this.geometryBuffer[0] = location;
      this.geometryBuffer[1] = new Vector2(location.X + width, location.Y);
      this.geometryBuffer[2] = new Vector2(location.X + width, location.Y + height);
      this.geometryBuffer[3] = new Vector2(location.X, location.Y + height);
      this.DrawPrimitiveQuad(pen, this.geometryBuffer, 0, angle);
    }

    /// <summary>
    /// Adds a primitive quadrilateral to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="points">An array containing the coordinates of the quad.</param>
    /// <param name="offset">The offset into the points array.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveQuad</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveQuad(Pen pen, Vector2[] points, int offset)
    {
      this.DrawPrimitiveQuad(pen, points, offset, 0.0f);
    }

    /// <summary>
    /// Adds a primitive quadrilateral to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="points">An array containing the coordinates of the quad.</param>
    /// <param name="offset">The offset into the points array.</param>
    /// <param name="angle">The angle to rotate the quad by around its weighted center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveQuad</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveQuad(Pen pen, Vector2[] points, int offset, float angle)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      if (points == null)
        throw new ArgumentNullException(nameof (points));
      if (points.Length < offset + 4)
        throw new ArgumentException("Points array is too small for the given offset.");
      this.RequestBufferSpace(4, 8);
      this.AddInfo(PrimitiveType.LineList, 4, 8, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(points[offset], 0.0f), pen.Color, points[offset]);
      this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(points[offset + 1], 0.0f), pen.Color, points[offset + 1]);
      this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(points[offset + 2], 0.0f), pen.Color, points[offset + 2]);
      this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(points[offset + 3], 0.0f), pen.Color, points[offset + 3]);
      if ((double) angle != 0.0)
      {
        Vector3 vector3 = new Vector3((float) (((double) points[offset].X + (double) points[offset + 1].X + (double) points[offset + 2].X + (double) points[offset + 3].X) / 4.0), (float) (((double) points[offset].Y + (double) points[offset + 1].Y + (double) points[offset + 2].Y + (double) points[offset + 3].Y) / 4.0), 0.0f);
        Matrix rotationZ = Matrix.CreateRotationZ(angle) with
        {
          Translation = vector3
        };
        for (int index = this.vertexBufferIndex - 4; index < this.vertexBufferIndex; ++index)
          this.vertexBuffer[index].Position = Vector3.Transform(this.vertexBuffer[index].Position - vector3, rotationZ);
      }
      this.indexBuffer[this.indexBufferIndex++] = (short) vertexBufferIndex;
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 1);
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 1);
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 2);
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 2);
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 3);
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 3);
      this.indexBuffer[this.indexBufferIndex++] = (short) vertexBufferIndex;
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds a point path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="point">The point to be rendered.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPoint</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPoint(Pen pen, Vector2 point)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.RequestBufferSpace(4, 6);
      this.AddInfo(PrimitiveType.TriangleList, 4, 6, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      float num = pen.Width / 2f;
      this.AddVertex(new Vector2(point.X - num, point.Y - num), pen);
      this.AddVertex(new Vector2(point.X + num, point.Y - num), pen);
      this.AddVertex(new Vector2(point.X - num, point.Y + num), pen);
      this.AddVertex(new Vector2(point.X + num, point.Y + num), pen);
      this.AddSegment(vertexBufferIndex, vertexBufferIndex + 2);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds a line segment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="p0">The first point of the line segment.</param>
    /// <param name="p1">The second point of the line segment.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawLine</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawLine(Pen pen, Vector2 p0, Vector2 p1)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.RequestBufferSpace(4, 6);
      this.ws.ResetWorkspace(pen);
      if (pen.NeedsPathLength)
        this.ws.PathLength = Vector2.Distance(p0, p1);
      this.AddInfo(PrimitiveType.TriangleList, 4, 6, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      this.AddStartPoint(p0, p1, pen, this.ws);
      this.AddEndPoint(p0, p1, pen, this.ws);
      this.AddSegment(vertexBufferIndex, vertexBufferIndex + 2);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive line segment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="p0">The first point of the line segment.</param>
    /// <param name="p1">The second point of the line segment.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveLine</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveLine(Pen pen, Vector2 p0, Vector2 p1)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.RequestBufferSpace(2, 2);
      this.AddInfo(PrimitiveType.LineList, 2, 2, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(p0.X, p0.Y, 0.0f), pen.Color, p0);
      this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(p1.X, p1.Y, 0.0f), pen.Color, p1);
      this.indexBuffer[this.indexBufferIndex++] = (short) vertexBufferIndex;
      this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + 1);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive multisegment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="points">The list of points that make up the path to be rendered.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitivePath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitivePath(Pen pen, IList<Vector2> points)
    {
      this.DrawPrimitivePath(pen, points, 0, points.Count, PathType.Open);
    }

    /// <summary>
    /// Adds a primitive multisegment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="points">The list of points that make up the path to be rendered.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitivePath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitivePath(Pen pen, IList<Vector2> points, PathType pathType)
    {
      this.DrawPrimitivePath(pen, points, 0, points.Count, pathType);
    }

    /// <summary>
    /// Adds a primitive multisegment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="points">The list of points that make up the path to be rendered.</param>
    /// <param name="offset">The offset into the <paramref name="points" /> list to begin rendering.</param>
    /// <param name="count">The number of points that should be rendered, starting from <paramref name="offset" />.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitivePath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitivePath(Pen pen, IList<Vector2> points, int offset, int count)
    {
      this.DrawPrimitivePath(pen, points, offset, count, PathType.Open);
    }

    /// <summary>
    /// Adds a primitive multisegment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying a color to render the path with.</param>
    /// <param name="points">The list of points that make up the path to be rendered.</param>
    /// <param name="offset">The offset into the <paramref name="points" /> list to begin rendering.</param>
    /// <param name="count">The number of points that should be rendered, starting from <paramref name="offset" />.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitivePath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitivePath(
      Pen pen,
      IList<Vector2> points,
      int offset,
      int count,
      PathType pathType)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      if (offset + count > points.Count)
        throw new ArgumentOutOfRangeException(nameof (points), "The offset and count exceed the bounds of the list");
      this.RequestBufferSpace(count, pathType == PathType.Open ? count * 2 - 2 : count * 2);
      this.AddInfo(PrimitiveType.LineList, count, pathType == PathType.Open ? count * 2 - 2 : count * 2, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      for (int index = 0; index < count; ++index)
        this.vertexBuffer[this.vertexBufferIndex++] = new VertexPositionColorTexture(new Vector3(new Vector2(points[offset + index].X, points[offset + index].Y), 0.0f), pen.Color, Vector2.Zero);
      for (int index = 1; index < count; ++index)
      {
        this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + index - 1);
        this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + index);
      }
      if (pathType == PathType.Closed)
      {
        this.indexBuffer[this.indexBufferIndex++] = (short) (vertexBufferIndex + count - 1);
        this.indexBuffer[this.indexBufferIndex++] = (short) vertexBufferIndex;
      }
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a precomputed path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="path">A path that has already been stroked with a <see cref="T:Sharp2D.Engine.Drawing.Pen" />.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPath(GraphicsPath path)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      this.DrawPathInner(path, ref SharpDrawBatch.identityMatrix, false);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    private void DrawPathInner(
      GraphicsPath path,
      ref Matrix vertexTransform,
      bool applyVertexTransform)
    {
      this.RequestBufferSpace(path.VertexCount, path.IndexCount);
      this.AddInfo(PrimitiveType.TriangleList, path.VertexCount, path.IndexCount, path.Pen.Brush);
      if (path.VertexTextureData != null)
      {
        for (int index = 0; index < path.VertexCount; ++index)
          this.vertexBuffer[this.vertexBufferIndex + index] = new VertexPositionColorTexture(new Vector3(path.VertexPositionData[index], 0.0f), path.VertexColorData[index], path.VertexTextureData[index]);
      }
      else
      {
        for (int index = 0; index < path.VertexCount; ++index)
          this.vertexBuffer[this.vertexBufferIndex + index] = new VertexPositionColorTexture(new Vector3(path.VertexPositionData[index], 0.0f), path.VertexColorData[index], Vector2.Zero);
      }
      for (int index = 0; index < path.IndexCount; ++index)
        this.indexBuffer[this.indexBufferIndex + index] = (short) ((int) path.IndexData[index] + this.vertexBufferIndex);
      this.vertexBufferIndex += path.VertexCount;
      this.indexBufferIndex += path.IndexCount;
      foreach (GraphicsPath outlinePath in path.OutlinePaths)
        this.DrawPathInner(outlinePath, ref vertexTransform, applyVertexTransform);
    }

    private void TransformData(Vector3[] data, int start, int length, ref Matrix tranform)
    {
    }

    /// <summary>
    /// Computes and adds a circle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawCircle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the circle is computed as the radius / 1.5.</remarks>
    public void DrawCircle(Pen pen, Vector2 center, float radius)
    {
      this.DrawCircle(pen, center, radius, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Computes and adds a circle path to the batch of figures to be rendered using a given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the circle with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawCircle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawCircle(Pen pen, Vector2 center, float radius, int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.BuildCircleGeometryBuffer(center, radius, subdivisions, false);
      this.AddClosedPath(this.geometryBuffer, 0, subdivisions, pen, this.ws);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds an ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void DrawEllipse(Pen pen, Rectangle bound)
    {
      this.DrawEllipse(pen, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, 0.0f);
    }

    /// <summary>
    /// Computes and adds an ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void DrawEllipse(Pen pen, Rectangle bound, float angle)
    {
      this.DrawEllipse(pen, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, angle);
    }

    /// <summary>
    /// Computes and adds an ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the ellipse with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawEllipse(Pen pen, Rectangle bound, float angle, int subdivisions)
    {
      this.DrawEllipse(pen, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, angle, subdivisions);
    }

    /// <summary>
    /// Computes and adds an ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the ellipse along the y-acis.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void DrawEllipse(Pen pen, Vector2 center, float xRadius, float yRadius)
    {
      this.DrawEllipse(pen, center, xRadius, yRadius, 0.0f);
    }

    /// <summary>
    /// Computes and adds an ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the ellipse along the y-acis.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void DrawEllipse(Pen pen, Vector2 center, float xRadius, float yRadius, float angle)
    {
      this.DrawEllipse(pen, center, xRadius, yRadius, angle, SharpDrawBatch.DefaultSubdivisions(xRadius, yRadius));
    }

    /// <summary>
    /// Computes and adds an ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the ellipse along the y-acis.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the ellipse with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawEllipse(
      Pen pen,
      Vector2 center,
      float xRadius,
      float yRadius,
      float angle,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.BuildEllipseGeometryBuffer(center, xRadius, yRadius, angle, subdivisions);
      this.AddClosedPath(this.geometryBuffer, 0, subdivisions, pen, this.ws);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive circle path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveCircle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the circle is computed as the radius / 1.5.</remarks>
    public void DrawPrimitiveCircle(Pen pen, Vector2 center, float radius)
    {
      this.DrawPrimitiveCircle(pen, center, radius, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Adds a primitive circle path to the batch of figures to be rendered using a given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the circle with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveCircle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveCircle(Pen pen, Vector2 center, float radius, int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.BuildCircleGeometryBuffer(center, radius, subdivisions, false);
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.geometryBuffer, 0, subdivisions, PathType.Closed);
    }

    private void BuildCircleGeometryBuffer(
      Vector2 center,
      float radius,
      int subdivisions,
      bool connect)
    {
      List<Vector2> circleSubdivisions = this.CalculateCircleSubdivisions(subdivisions);
      if (this.geometryBuffer.Length < subdivisions + 1)
        Array.Resize<Vector2>(ref this.geometryBuffer, (subdivisions + 1) * 2);
      for (int index = 0; index < subdivisions; ++index)
        this.geometryBuffer[index] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y + radius * circleSubdivisions[index].Y);
      if (!connect)
        return;
      this.geometryBuffer[subdivisions] = new Vector2(center.X + radius * circleSubdivisions[0].X, center.Y + radius * circleSubdivisions[0].Y);
    }

    /// <summary>
    /// Adds a primitive ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(width, height) / 3.0.</remarks>
    public void DrawPrimitiveEllipse(Pen pen, Rectangle bound)
    {
      this.DrawPrimitiveEllipse(pen, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, 0.0f);
    }

    /// <summary>
    /// Adds a primitive ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(width, height) / 3.0.</remarks>
    public void DrawPrimitiveEllipse(Pen pen, Rectangle bound, float angle)
    {
      this.DrawPrimitiveEllipse(pen, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, angle);
    }

    /// <summary>
    /// Adds a primitive ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the ellipse with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveEllipse(Pen pen, Rectangle bound, float angle, int subdivisions)
    {
      this.DrawPrimitiveEllipse(pen, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, angle, subdivisions);
    }

    /// <summary>
    /// Adds a primitive ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the ellipse along the y-acis.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void DrawPrimitiveEllipse(Pen pen, Vector2 center, float xRadius, float yRadius)
    {
      this.DrawPrimitiveEllipse(pen, center, xRadius, yRadius, 0.0f);
    }

    /// <summary>
    /// Adds a primitive ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the ellipse along the y-acis.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void DrawPrimitiveEllipse(
      Pen pen,
      Vector2 center,
      float xRadius,
      float yRadius,
      float angle)
    {
      this.DrawPrimitiveEllipse(pen, center, xRadius, yRadius, angle, SharpDrawBatch.DefaultSubdivisions(xRadius, yRadius));
    }

    /// <summary>
    /// Adds a primitive ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the ellipse along the y-acis.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the ellipse with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveEllipse(
      Pen pen,
      Vector2 center,
      float xRadius,
      float yRadius,
      float angle,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.BuildEllipseGeometryBuffer(center, xRadius, yRadius, angle, subdivisions);
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.geometryBuffer, 0, subdivisions, PathType.Closed);
    }

    private void BuildEllipseGeometryBuffer(
      Vector2 center,
      float xRadius,
      float yRadius,
      float angle,
      int subdivisions)
    {
      float radius = Math.Min(xRadius, yRadius);
      this.BuildCircleGeometryBuffer(Vector2.Zero, radius, subdivisions, false);
      Matrix matrix = (Matrix.CreateScale(xRadius / radius, yRadius / radius, 1f) * Matrix.CreateRotationZ(angle)) with
      {
        Translation = new Vector3(center, 0.0f)
      };
      for (int index = 0; index < subdivisions; ++index)
        this.geometryBuffer[index] = Vector2.Transform(this.geometryBuffer[index], matrix);
    }

    /// <summary>
    /// Computes and adds an arc path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawArc(Pen pen, Vector2 center, float radius, float startAngle, float arcAngle)
    {
      this.DrawArc(pen, center, radius, startAngle, arcAngle, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Computes and adds an arc path to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same radius.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>subdivisions * (arcAngle / 2PI)</c>.</remarks>
    public void DrawArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddArc(center, radius, startAngle, arcAngle, subdivisions);
      if (this.pathBuilder.Count <= 1)
        return;
      this.AddPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count, pen, this.ws);
      if (this.sortMode == DrawSortMode.Immediate)
        this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds an arc path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="p0">The starting point of the arc.</param>
    /// <param name="p1">The ending point of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting <paramref name="p0" /> and <paramref name="p1" />.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawArc(Pen pen, Vector2 p0, Vector2 p1, float height)
    {
      float num = (p1 - p0).Length();
      float radius = (float) ((double) height / 2.0 + (double) num * (double) num / ((double) height * 8.0));
      this.DrawArc(pen, p0, p1, height, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Computes and adds an arc path to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="p0">The starting point of the arc.</param>
    /// <param name="p1">The ending point of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting <paramref name="p0" /> and <paramref name="p1" />.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same arc radius.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(subdivisions) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawArc(Pen pen, Vector2 p0, Vector2 p1, float height, int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddArc(p0, p1, height, subdivisions);
      if (this.pathBuilder.Count <= 1)
        return;
      this.AddPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count, pen, this.ws);
      if (this.sortMode == DrawSortMode.Immediate)
        this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive arc path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawPrimitiveArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle)
    {
      this.DrawPrimitiveArc(pen, center, radius, startAngle, arcAngle, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Adds a primitive arc path to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same radius.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawPrimitiveArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddArc(center, radius, startAngle, arcAngle, subdivisions);
      if (this.pathBuilder.Count <= 1)
        return;
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count, PathType.Open);
    }

    /// <summary>
    /// Adds a primitive arc path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="p0">The starting point of the arc.</param>
    /// <param name="p1">The ending point of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting <paramref name="p0" /> and <paramref name="p1" />.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawPrimitiveArc(Pen pen, Vector2 p0, Vector2 p1, float height)
    {
      float num = (p1 - p0).Length();
      float radius = (float) ((double) height / 2.0 + (double) num * (double) num / ((double) height * 8.0));
      this.DrawPrimitiveArc(pen, p0, p1, height, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Adds a primitive arc path to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="p0">The starting point of the arc.</param>
    /// <param name="p1">The ending point of the arc.</param>
    /// <param name="height">The furthest point on the arc from the line connecting <paramref name="p0" /> and <paramref name="p1" />.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same arc radius.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(subdivisions) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawPrimitiveArc(Pen pen, Vector2 p0, Vector2 p1, float height, int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddArc(p0, p1, height, subdivisions);
      if (this.pathBuilder.Count <= 1)
        return;
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count, PathType.Open);
    }

    /// <summary>
    /// Adds a closed primitive arc path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="arcType">Whether the arc is drawn as a segment or a sector.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveClosedArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawPrimitiveClosedArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      ArcType arcType)
    {
      this.DrawPrimitiveClosedArc(pen, center, radius, startAngle, arcAngle, arcType, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Adds a closed primitive arc path to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="arcType">Whether the arc is drawn as a segment or a sector.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same radius.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveClosedArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(subdivisions * (arcAngle / 2PI)</c>.</remarks>
    public void DrawPrimitiveClosedArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      ArcType arcType,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      if (arcType == ArcType.Sector)
        this.pathBuilder.AddPoint(center);
      this.pathBuilder.AddArc(center, radius, startAngle, arcAngle, subdivisions);
      if (this.pathBuilder.Count <= 1)
        return;
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count, PathType.Closed);
    }

    /// <summary>
    /// Computes and adds a closed arc path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="arcType">Whether the arc is drawn as a segment or a sector.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawClosedArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void DrawClosedArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      ArcType arcType)
    {
      this.DrawClosedArc(pen, center, radius, startAngle, arcAngle, arcType, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Computes and adds a closed arc path to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="center">The center coordinate of the the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="arcType">Whether the arc is drawn as a segment or a sector.</param>
    /// <param name="subdivisions">The number of subdivisions in a circle of the same radius.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawClosedArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>subdivisions * (arcAngle / 2PI)</c>.</remarks>
    public void DrawClosedArc(
      Pen pen,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      ArcType arcType,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      if (arcType == ArcType.Sector)
        this.pathBuilder.AddPoint(center);
      this.pathBuilder.AddArc(center, radius, startAngle, arcAngle, subdivisions);
      if (this.pathBuilder.Count <= 1)
        return;
      this.AddClosedPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count, pen, this.ws);
      if (this.sortMode == DrawSortMode.Immediate)
        this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds a quadratic Bezier path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="p0">The start point of the curve.</param>
    /// <param name="p1">The first control point.</param>
    /// <param name="p2">The end point of the curve.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawBezier</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawBezier(Pen pen, Vector2 p0, Vector2 p1, Vector2 p2)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddBezier(p0, p1, p2);
      this.AddPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count, pen, this.ws);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds a cubic Bezier path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="p0">The start point of the curve.</param>
    /// <param name="p1">The first control point.</param>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The end point of the curve.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawBezier</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawBezier(Pen pen, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.ws.ResetWorkspace(pen);
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddBezier(p0, p1, p2, p3);
      this.AddPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count, pen, this.ws);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Computes and adds a series of Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawBeziers(Pen pen, IList<Vector2> points, BezierType bezierType)
    {
      this.DrawBeziers(pen, points, 0, points.Count, bezierType, PathType.Open);
    }

    /// <summary>
    /// Computes and adds a series of Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawBeziers(
      Pen pen,
      IList<Vector2> points,
      BezierType bezierType,
      PathType pathType)
    {
      this.DrawBeziers(pen, points, 0, points.Count, bezierType, pathType);
    }

    /// <summary>
    /// Computes and adds a series of Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="offset">The index of the first start point in the list.</param>
    /// <param name="count">The number of points to use.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawBeziers(
      Pen pen,
      IList<Vector2> points,
      int offset,
      int count,
      BezierType bezierType)
    {
      this.DrawBeziers(pen, points, offset, count, bezierType, PathType.Open);
    }

    /// <summary>
    /// Computes and adds a series of Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="offset">The index of the first start point in the list.</param>
    /// <param name="count">The number of points to use.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawBeziers(
      Pen pen,
      IList<Vector2> points,
      int offset,
      int count,
      BezierType bezierType,
      PathType pathType)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      if (points.Count < offset + count)
        throw new ArgumentOutOfRangeException("The offset and count are out of range for the given points argument.");
      this.ws.ResetWorkspace(pen);
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      switch (bezierType)
      {
        case BezierType.Quadratic:
          for (int index = offset + 2; index < offset + count; index += 2)
            this.pathBuilder.AddBezier(points[index - 2], points[index - 1], points[index]);
          if (pathType == PathType.Closed)
          {
            this.pathBuilder.AddBezier(points[offset + count - 2], points[offset + count - 1], points[offset]);
            break;
          }
          break;
        case BezierType.Cubic:
          for (int index = offset + 3; index < offset + count; index += 3)
            this.pathBuilder.AddBezier(points[index - 3], points[index - 2], points[index - 1], points[index]);
          if (pathType == PathType.Closed)
          {
            this.pathBuilder.AddBezier(points[offset + count - 3], points[offset + count - 2], points[offset + count - 1], points[offset]);
            break;
          }
          break;
      }
      switch (pathType)
      {
        case PathType.Open:
          this.AddPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count, pen, this.ws);
          break;
        case PathType.Closed:
          this.AddClosedPath(this.pathBuilder.Buffer, 0, this.pathBuilder.Count - 1, pen, this.ws);
          break;
      }
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive quadratic Bezier path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="p0">The start point of the curve.</param>
    /// <param name="p1">The first control point.</param>
    /// <param name="p2">The end point of the curve.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveBezier</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveBezier(Pen pen, Vector2 p0, Vector2 p1, Vector2 p2)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddBezier(p0, p1, p2);
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a primitive cubic Bezier path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="p0">The start point of the curve.</param>
    /// <param name="p1">The first control point.</param>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The end point of the curve.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveBezier</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveBezier(Pen pen, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      this.pathBuilder.AddBezier(p0, p1, p2, p3);
      this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a series of primitive Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveBeziers(Pen pen, IList<Vector2> points, BezierType bezierType)
    {
      this.DrawPrimitiveBeziers(pen, points, 0, points.Count, bezierType, PathType.Open);
    }

    /// <summary>
    /// Adds a series of primitive Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveBeziers(
      Pen pen,
      IList<Vector2> points,
      BezierType bezierType,
      PathType pathType)
    {
      this.DrawPrimitiveBeziers(pen, points, 0, points.Count, bezierType, pathType);
    }

    /// <summary>
    /// Adds a series of primitive Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="offset">The index of the first start point in the list.</param>
    /// <param name="count">The number of points to use.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveBeziers(
      Pen pen,
      IList<Vector2> points,
      int offset,
      int count,
      BezierType bezierType)
    {
      this.DrawPrimitiveBeziers(pen, points, offset, count, bezierType, PathType.Open);
    }

    /// <summary>
    /// Adds a series of primitive Bezier paths to the batch of figures to be rendered.
    /// </summary>
    /// <param name="pen">The pen supplying the color to render the path with.</param>
    /// <param name="points">A list of Bezier points.</param>
    /// <param name="offset">The index of the first start point in the list.</param>
    /// <param name="count">The number of points to use.</param>
    /// <param name="bezierType">The type of Bezier curves to draw.</param>
    /// <param name="pathType">Whether the path is open or closed.</param>
    /// <remarks><para>For quadratic Bezier curves, the number of points defined by the parameters should be a multiple of 2 plus 1
    /// for open curves or 2 for closed curves.  For cubic Bezier curves, the number of points defined by the parameters should be
    /// a multiple of 3 plus 1 for open curves or 3 for closed curves.  For each curve drawn after the first, the ending point of
    /// the previous curve is used as the starting point.  For closed curves, the end point of the last curve is the start point
    /// of the first curve.</para></remarks>
    /// <exception cref="T:System.InvalidOperationException"><c>DrawPrimitiveBeziers</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void DrawPrimitiveBeziers(
      Pen pen,
      IList<Vector2> points,
      int offset,
      int count,
      BezierType bezierType,
      PathType pathType)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (pen == null)
        throw new ArgumentNullException(nameof (pen));
      if (points.Count < offset + count)
        throw new ArgumentOutOfRangeException("The offset and count are out of range for the given points argument.");
      this.pathBuilder.Reset();
      this.pathBuilder.CalculateLengths = pen.NeedsPathLength;
      switch (bezierType)
      {
        case BezierType.Quadratic:
          for (int index = offset + 2; index < offset + count; index += 2)
            this.pathBuilder.AddBezier(points[index - 2], points[index - 1], points[index]);
          if (pathType == PathType.Closed)
          {
            this.pathBuilder.AddBezier(points[offset + count - 2], points[offset + count - 1], points[offset]);
            break;
          }
          break;
        case BezierType.Cubic:
          for (int index = offset + 3; index < offset + count; index += 3)
            this.pathBuilder.AddBezier(points[index - 3], points[index - 2], points[index - 1], points[index]);
          if (pathType == PathType.Closed)
          {
            this.pathBuilder.AddBezier(points[offset + count - 3], points[offset + count - 2], points[offset + count - 1], points[offset]);
            break;
          }
          break;
      }
      switch (pathType)
      {
        case PathType.Open:
          this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count, PathType.Open);
          break;
        case PathType.Closed:
          this.DrawPrimitivePath(pen, (IList<Vector2>) this.pathBuilder.Buffer, 0, this.pathBuilder.Count - 1, PathType.Closed);
          break;
      }
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    private float ClampAngle(float angle)
    {
      if ((double) angle < 0.0)
        angle += (float) (Math.Ceiling((double) angle / (-2.0 * Math.PI)) * Math.PI * 2.0);
      else if ((double) angle >= 2.0 * Math.PI)
        angle -= (float) (Math.Floor((double) angle / (2.0 * Math.PI)) * Math.PI * 2.0);
      return angle;
    }

    private float PointToAngle(Vector2 center, Vector2 point)
    {
      double angle = Math.Atan2((double) point.Y - (double) center.Y, (double) point.X - (double) center.X);
      if (angle < 0.0)
        angle += 2.0 * Math.PI;
      return (float) angle;
    }

    private int BuildArcGeometryBuffer(
      Vector2 center,
      float radius,
      int subdivisions,
      float startAngle,
      float arcAngle)
    {
      float angle = startAngle + arcAngle;
      startAngle = this.ClampAngle(startAngle);
      float num1 = this.ClampAngle(angle);
      List<Vector2> circleSubdivisions = this.CalculateCircleSubdivisions(subdivisions);
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
      int num6 = 0;
      if (this.geometryBuffer.Length < num5 + 2)
        Array.Resize<Vector2>(ref this.geometryBuffer, (num5 + 2) * 2);
      int num7;
      if ((double) arcAngle >= 0.0)
      {
        if ((double) num3 * (double) num2 - (double) startAngle > 0.004999999888241291)
        {
          this.geometryBuffer[num6++] = new Vector2(center.X + radius * (float) Math.Cos(-(double) startAngle), center.Y - radius * (float) Math.Sin(-(double) startAngle));
          ++num5;
        }
        if (num3 <= num4)
        {
          for (int index = num3; index <= num4; ++index)
            this.geometryBuffer[num6++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        else
        {
          for (int index = num3; index < circleSubdivisions.Count; ++index)
            this.geometryBuffer[num6++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
          for (int index = 0; index <= num4; ++index)
            this.geometryBuffer[num6++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        if ((double) num1 - (double) num4 * (double) num2 > 0.004999999888241291)
        {
          Vector2[] geometryBuffer = this.geometryBuffer;
          int index = num6;
          num7 = index + 1;
          Vector2 vector2_3 = new Vector2(center.X + radius * (float) Math.Cos(-(double) num1), center.Y - radius * (float) Math.Sin(-(double) num1));
          geometryBuffer[index] = vector2_3;
          ++num5;
        }
      }
      else
      {
        if ((double) startAngle - (double) num3 * (double) num2 > 0.004999999888241291)
        {
          this.geometryBuffer[num6++] = new Vector2(center.X + radius * (float) Math.Cos(-(double) startAngle), center.Y - radius * (float) Math.Sin(-(double) startAngle));
          ++num5;
        }
        if (num4 <= num3)
        {
          for (int index = num3; index >= num4; --index)
            this.geometryBuffer[num6++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        else
        {
          for (int index = num3; index >= 0; --index)
            this.geometryBuffer[num6++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
          for (int index = circleSubdivisions.Count - 1; index >= num4; --index)
            this.geometryBuffer[num6++] = new Vector2(center.X + radius * circleSubdivisions[index].X, center.Y - radius * circleSubdivisions[index].Y);
        }
        if ((double) num4 * (double) num2 - (double) num1 > 0.004999999888241291)
        {
          Vector2[] geometryBuffer = this.geometryBuffer;
          int index = num6;
          num7 = index + 1;
          Vector2 vector2_4 = new Vector2(center.X + radius * (float) Math.Cos(-(double) num1), center.Y - radius * (float) Math.Sin(-(double) num1));
          geometryBuffer[index] = vector2_4;
          ++num5;
        }
      }
      return num5;
    }

    /// <summary>
    /// Adds a filled circle to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillCircle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the circle is computed as <c>(radius / 1.5)</c>.</remarks>
    public void FillCircle(Brush brush, Vector2 center, float radius)
    {
      this.FillCircle(brush, center, radius, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Adds a filled circle to the batch of figures to be rendered using the given number of subdivisions.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="subdivisions">The number of subdivisions to render the circle with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillCircle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillCircle(Brush brush, Vector2 center, float radius, int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.RequestBufferSpace(subdivisions + 1, subdivisions * 3);
      this.AddInfo(PrimitiveType.TriangleList, subdivisions + 1, subdivisions * 3, brush);
      this.BuildCircleGeometryBuffer(center, radius, subdivisions, true);
      int vertexBufferIndex = this.vertexBufferIndex;
      for (int index = 0; index < subdivisions; ++index)
        this.AddVertex(this.geometryBuffer[index], brush);
      this.AddVertex(new Vector2(center.X, center.Y), brush);
      for (int index = 0; index < subdivisions - 1; ++index)
        this.AddTriangle(vertexBufferIndex + subdivisions, vertexBufferIndex + index + 1, vertexBufferIndex + index);
      this.AddTriangle(vertexBufferIndex + subdivisions, vertexBufferIndex, vertexBufferIndex + subdivisions - 1);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a filled ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(width, height) / 3.0.</remarks>
    public void FillEllipse(Brush brush, Rectangle bound)
    {
      this.FillEllipse(brush, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, 0.0f);
    }

    /// <summary>
    /// Adds a filled ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(width, height) / 3.0.</remarks>
    public void FillEllipse(Brush brush, Rectangle bound, float angle)
    {
      this.FillEllipse(brush, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, angle);
    }

    /// <summary>
    /// Adds a filled ellipse path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="bound">The bounding rectangle of the ellipse.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions (sides) to render the ellipse with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillEllipse(Brush brush, Rectangle bound, float angle, int subdivisions)
    {
      this.FillEllipse(brush, new Vector2((float) bound.Center.X, (float) bound.Center.Y), (float) bound.Width / 2f, (float) bound.Height / 2f, angle, subdivisions);
    }

    /// <summary>
    /// Adds a filled ellipse to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the llipse along the y-axis.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void FillEllipse(Brush brush, Vector2 center, float xRadius, float yRadius)
    {
      this.FillEllipse(brush, center, xRadius, yRadius, 0.0f);
    }

    /// <summary>
    /// Adds a filled ellipse to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the llipse along the y-axis.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the ellipse is computed as max(xRadius, yRadius) / 1.5.</remarks>
    public void FillEllipse(
      Brush brush,
      Vector2 center,
      float xRadius,
      float yRadius,
      float angle)
    {
      this.FillEllipse(brush, center, xRadius, yRadius, angle, SharpDrawBatch.DefaultSubdivisions(xRadius, yRadius));
    }

    /// <summary>
    /// Adds a filled ellipse to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the ellipse.</param>
    /// <param name="xRadius">The radius of the ellipse along the x-axis.</param>
    /// <param name="yRadius">The radius of the llipse along the y-axis.</param>
    /// <param name="angle">The angle to rotate the ellipse by in radians.  Positive values rotate clockwise.</param>
    /// <param name="subdivisions">The number of subdivisions to render the circle with.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillEllipse</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillEllipse(
      Brush brush,
      Vector2 center,
      float xRadius,
      float yRadius,
      float angle,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      this.RequestBufferSpace(subdivisions + 1, subdivisions * 3);
      this.AddInfo(PrimitiveType.TriangleList, subdivisions + 1, subdivisions * 3, brush);
      this.BuildEllipseGeometryBuffer(center, xRadius, yRadius, angle, subdivisions);
      int vertexBufferIndex = this.vertexBufferIndex;
      for (int index = 0; index < subdivisions; ++index)
        this.AddVertex(this.geometryBuffer[index], brush);
      this.AddVertex(new Vector2(center.X, center.Y), brush);
      for (int index = 0; index < subdivisions - 1; ++index)
        this.AddTriangle(vertexBufferIndex + subdivisions, vertexBufferIndex + index + 1, vertexBufferIndex + index);
      this.AddTriangle(vertexBufferIndex + subdivisions, vertexBufferIndex, vertexBufferIndex + subdivisions - 1);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a filled arc to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="arcType">Whether the arc is drawn as a segment or a sector.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>(radius / 1.5) * (arcAngle / 2PI)</c>.</remarks>
    public void FillArc(
      Brush brush,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      ArcType arcType)
    {
      this.FillArc(brush, center, radius, startAngle, arcAngle, arcType, SharpDrawBatch.DefaultSubdivisions(radius));
    }

    /// <summary>
    /// Adds a filled arc to the batch of figures to be rendered using up to the given number of subdivisions.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="center">The center coordinate of the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The starting angle of the arc in radians, where 0 is 3 O'Clock.</param>
    /// <param name="arcAngle">The sweep of the arc in radians.  Positive values draw clockwise.</param>
    /// <param name="arcType">Whether the arc is drawn as a segment or a sector.</param>
    /// <param name="subdivisions">The number of subdivisions in the circle with the same radius as the arc.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillArc</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>The number of subdivisions in the arc is computed as <c>subdivisions * (arcAngle / 2PI)</c>.</remarks>
    public void FillArc(
      Brush brush,
      Vector2 center,
      float radius,
      float startAngle,
      float arcAngle,
      ArcType arcType,
      int subdivisions)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      int num = this.BuildArcGeometryBuffer(center, radius, subdivisions, startAngle, arcAngle);
      this.RequestBufferSpace(num + 1, (num - 1) * 3);
      this.AddInfo(PrimitiveType.TriangleList, num + 1, (num - 1) * 3, brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      for (int index = 0; index < num; ++index)
        this.AddVertex(this.geometryBuffer[index], brush);
      switch (arcType)
      {
        case ArcType.Segment:
          this.AddVertex(new Vector2((float) (((double) this.geometryBuffer[0].X + (double) this.geometryBuffer[num - 1].X) / 2.0), (float) (((double) this.geometryBuffer[0].Y + (double) this.geometryBuffer[num - 1].Y) / 2.0)), brush);
          break;
        case ArcType.Sector:
          this.AddVertex(new Vector2(center.X, center.Y), brush);
          break;
      }
      if ((double) arcAngle < 0.0)
      {
        for (int index = 0; index < num - 1; ++index)
          this.AddTriangle(vertexBufferIndex + num, vertexBufferIndex + index + 1, vertexBufferIndex + index);
      }
      else
      {
        for (int index = num - 1; index > 0; --index)
          this.AddTriangle(vertexBufferIndex + num, vertexBufferIndex + index - 1, vertexBufferIndex + index);
      }
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a filled rectangle to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="rect">The rectangle to be rendered.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillRectangle(Brush brush, Rectangle rect) => this.FillRectangle(brush, rect, 0.0f);

    /// <summary>
    /// Adds a filled rectangle to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="rect">The rectangle to be rendered.</param>
    /// <param name="angle">The angle to rotate the rectangle by around its center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillRectangle(Brush brush, Rectangle rect, float angle)
    {
      this.geometryBuffer[0] = new Vector2((float) rect.Left, (float) rect.Top);
      this.geometryBuffer[1] = new Vector2((float) rect.Right, (float) rect.Top);
      this.geometryBuffer[2] = new Vector2((float) rect.Right, (float) rect.Bottom);
      this.geometryBuffer[3] = new Vector2((float) rect.Left, (float) rect.Bottom);
      this.FillQuad(brush, this.geometryBuffer, 0, angle);
    }

    /// <summary>
    /// Adds a filled rectangle to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="location">The top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillRectangle(Brush brush, Vector2 location, float width, float height)
    {
      this.FillRectangle(brush, location, width, height, 0.0f);
    }

    /// <summary>
    /// Adds a filled rectangle to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="location">The top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="angle">The angle to rotate the rectangle by around its center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillRectangle</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillRectangle(
      Brush brush,
      Vector2 location,
      float width,
      float height,
      float angle)
    {
      this.geometryBuffer[0] = location;
      this.geometryBuffer[1] = new Vector2(location.X + width, location.Y);
      this.geometryBuffer[2] = new Vector2(location.X + width, location.Y + height);
      this.geometryBuffer[3] = new Vector2(location.X, location.Y + height);
      this.FillQuad(brush, this.geometryBuffer, 0, angle);
    }

    /// <summary>
    /// Adds a filled quadrilateral to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="points">An array containing the coordinates of the quad.</param>
    /// <param name="offset">The offset into the points array.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillQuad</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillQuad(Brush brush, Vector2[] points, int offset)
    {
      this.FillQuad(brush, points, offset, 0.0f);
    }

    /// <summary>
    /// Adds a filled quadrilateral to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="points">An array containing the coordinates of the quad.</param>
    /// <param name="offset">The offset into the points array.</param>
    /// <param name="angle">The angle to rotate the quad around its weighted center in radians.  Positive values rotate clockwise.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillQuad</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    public void FillQuad(Brush brush, Vector2[] points, int offset, float angle)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      if (points == null)
        throw new ArgumentNullException(nameof (points));
      if (points.Length < offset + 4)
        throw new ArgumentException("Points array is too small for the given offset.");
      this.RequestBufferSpace(4, 6);
      this.AddInfo(PrimitiveType.TriangleList, 4, 6, brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      if (points != this.geometryBuffer)
        Array.Copy((Array) points, (Array) this.geometryBuffer, 4);
      if ((double) angle != 0.0)
      {
        Vector2 vector2 = new Vector2((float) (((double) this.geometryBuffer[0].X + (double) this.geometryBuffer[1].X + (double) this.geometryBuffer[2].X + (double) this.geometryBuffer[3].X) / 4.0), (float) (((double) this.geometryBuffer[0].Y + (double) this.geometryBuffer[1].Y + (double) this.geometryBuffer[2].Y + (double) this.geometryBuffer[3].Y) / 4.0));
        Matrix rotationZ = Matrix.CreateRotationZ(angle) with
        {
          Translation = new Vector3(vector2, 0.0f)
        };
        for (int index = 0; index < 4; ++index)
          this.geometryBuffer[index] = Vector2.Transform(this.geometryBuffer[index] - vector2, rotationZ);
      }
      for (int index = 0; index < 4; ++index)
        this.AddVertex(this.geometryBuffer[index], brush);
      this.AddTriangle(vertexBufferIndex, vertexBufferIndex + 1, vertexBufferIndex + 3);
      this.AddTriangle(vertexBufferIndex + 1, vertexBufferIndex + 2, vertexBufferIndex + 3);
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Adds a filled region enclosed by the given multisegment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="points">The list of points that make up the multisegment path enclosing the region.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillPath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>Paths should be created with a clockwise winding order, or the resulting geometry will be backface-culled.</remarks>
    public void FillPath(Brush brush, IList<Vector2> points)
    {
      this.FillPath(brush, points, 0, points.Count);
    }

    /// <summary>
    /// Adds a filled region enclosed by the given multisegment path to the batch of figures to be rendered.
    /// </summary>
    /// <param name="brush">The brush to render the shape with.</param>
    /// <param name="points">The list of points that make up the multisegment path enclosing the region.</param>
    /// <param name="offset">The offset into the <paramref name="points" /> list to begin rendering.</param>
    /// <param name="count">The number of points that should be rendered, starting from <paramref name="offset" />.</param>
    /// <exception cref="T:System.InvalidOperationException"><c>FillPath</c> was called, but <see cref="!:Begin()" /> has not yet been called.</exception>
    /// <remarks>Paths should be created with a clockwise winding order, or the resulting geometry will be backface-culled.</remarks>
    public void FillPath(Brush brush, IList<Vector2> points, int offset, int count)
    {
      if (!this.inDraw)
        throw new InvalidOperationException();
      if (brush == null)
        throw new ArgumentNullException(nameof (brush));
      if (this.triangulator == null)
        this.triangulator = new Triangulator();
      this.triangulator.Triangulate(points, offset, count);
      this.RequestBufferSpace(count, this.triangulator.ComputedIndexCount);
      this.AddInfo(PrimitiveType.TriangleList, count, this.triangulator.ComputedIndexCount, brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      for (int index = 0; index < count; ++index)
        this.AddVertex(points[offset + index], brush);
      for (int index = 0; index < this.triangulator.ComputedIndexCount; ++index)
        this.indexBuffer[this.indexBufferIndex + index] = (short) (this.triangulator.ComputedIndexes[index] + vertexBufferIndex);
      this.indexBufferIndex += this.triangulator.ComputedIndexCount;
      if (this.sortMode != DrawSortMode.Immediate)
        return;
      this.FlushBuffer();
    }

    /// <summary>
    /// Immediatley renders a <see cref="M:Sharp2D.Engine.Drawing.SharpDrawBatch.DrawCache(Sharp2D.Engine.Drawing.DrawCache)" /> object.
    /// </summary>
    /// <param name="cache">A <see cref="M:Sharp2D.Engine.Drawing.SharpDrawBatch.DrawCache(Sharp2D.Engine.Drawing.DrawCache)" /> object.</param>
    /// <remarks>Any previous unflushed geometry will be rendered first.</remarks>
    public void DrawCache(Sharp2D.Engine.Drawing.DrawCache cache)
    {
      if (this.sortMode != DrawSortMode.Immediate)
        this.SetRenderState();
      this.FlushBuffer();
      cache.Render(this.Batch.GraphicsDevice, this.defaultTexture);
    }

    private void SetRenderState()
    {
      this.Batch.GraphicsDevice.BlendState = this.blendState != null ? this.blendState : BlendState.AlphaBlend;
      this.Batch.GraphicsDevice.DepthStencilState = this.depthStencilState != null ? this.depthStencilState : DepthStencilState.None;
      this.Batch.GraphicsDevice.RasterizerState = this.rasterizerState != null ? this.rasterizerState : RasterizerState.CullCounterClockwise;
      this.Batch.GraphicsDevice.SamplerStates[0] = this.samplerState != null ? this.samplerState : SamplerState.PointWrap;
      this.standardEffect.Projection = Matrix.CreateOrthographicOffCenter(0.0f, (float) this.Batch.GraphicsDevice.Viewport.Width, (float) this.Batch.GraphicsDevice.Viewport.Height, 0.0f, -1f, 1f);
      this.standardEffect.World = this.transform;
      this.standardEffect.CurrentTechnique.Passes[0].Apply();
      if (this.effect == null)
        return;
      this.effect.CurrentTechnique.Passes[0].Apply();
    }

    private void AddMiteredJoint(ref JoinSample js, Pen pen, PenWorkspace ws)
    {
      pen.ComputeMiter(ref js, ws);
      this.AddVertex(ws.XYInsetBuffer[0], pen.ColorAt(ws.UVInsetBuffer[0], ws.PathLengthScale), pen);
      this.AddVertex(ws.XYOutsetBuffer[0], pen.ColorAt(ws.UVOutsetBuffer[0], ws.PathLengthScale), pen);
    }

    private void AddStartPoint(Vector2 a, Vector2 b, Pen pen, PenWorkspace ws)
    {
      pen.ComputeStartPoint(a, b, ws);
      this.AddVertex(ws.XYBuffer[1], pen.ColorAt(ws.UVBuffer[1], ws.PathLengthScale), pen);
      this.AddVertex(ws.XYBuffer[0], pen.ColorAt(ws.UVBuffer[0], ws.PathLengthScale), pen);
    }

    private void AddEndPoint(Vector2 a, Vector2 b, Pen pen, PenWorkspace ws)
    {
      pen.ComputeEndPoint(a, b, ws);
      this.AddVertex(ws.XYBuffer[0], pen.ColorAt(ws.UVBuffer[0], ws.PathLengthScale), pen);
      this.AddVertex(ws.XYBuffer[1], pen.ColorAt(ws.UVBuffer[1], ws.PathLengthScale), pen);
    }

    private void AddInfo(
      PrimitiveType primitiveType,
      int vertexCount,
      int indexCount,
      Brush brush)
    {
      this.AddInfo(primitiveType, vertexCount, indexCount, brush != null ? brush.Texture : this.defaultTexture);
    }

    private void AddInfo(
      PrimitiveType primitiveType,
      int vertexCount,
      int indexCount,
      Texture2D texture)
    {
      this.infoBuffer[this.infoBufferIndex].Primitive = primitiveType;
      this.infoBuffer[this.infoBufferIndex].Texture = texture ?? this.defaultTexture;
      this.infoBuffer[this.infoBufferIndex].IndexCount = indexCount;
      this.infoBuffer[this.infoBufferIndex].VertexCount = vertexCount;
      ++this.infoBufferIndex;
    }

    private void AddClosedPath(Vector2[] points, int offset, int count, Pen pen, PenWorkspace ws)
    {
      this.RequestBufferSpace(count * 2, count * 6);
      this.AddInfo(PrimitiveType.TriangleList, count * 2, count * 6, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      JoinSample js = new JoinSample(Vector2.Zero, points[offset], points[offset + 1]);
      for (int index = 0; index < count - 2; ++index)
      {
        js.Advance(points[offset + index + 2]);
        this.AddMiteredJoint(ref js, pen, ws);
      }
      js.Advance(points[offset]);
      this.AddMiteredJoint(ref js, pen, ws);
      js.Advance(points[offset + 1]);
      this.AddMiteredJoint(ref js, pen, ws);
      for (int index = 0; index < count - 1; ++index)
        this.AddSegment(vertexBufferIndex + index * 2, vertexBufferIndex + (index + 1) * 2);
      this.AddSegment(vertexBufferIndex + (count - 1) * 2, vertexBufferIndex);
    }

    private void AddPath(Vector2[] points, int offset, int count, Pen pen, PenWorkspace ws)
    {
      this.RequestBufferSpace(count * 2, (count - 1) * 6);
      this.ws.ResetWorkspace(pen);
      this.AddInfo(PrimitiveType.TriangleList, count * 2, (count - 1) * 6, pen.Brush);
      int vertexBufferIndex = this.vertexBufferIndex;
      this.AddStartPoint(points[offset], points[offset + 1], pen, this.ws);
      JoinSample js = new JoinSample(Vector2.Zero, points[offset], points[offset + 1]);
      for (int index = 0; index < count - 2; ++index)
      {
        js.Advance(points[offset + index + 2]);
        this.AddMiteredJoint(ref js, pen, ws);
      }
      this.AddEndPoint(points[offset + count - 2], points[offset + count - 1], pen, this.ws);
      for (int index = 0; index < count - 1; ++index)
        this.AddSegment(vertexBufferIndex + index * 2, vertexBufferIndex + (index + 1) * 2);
    }

    private void AddVertex(Vector2 position, Pen pen) => this.AddVertex(position, pen.Color, pen);

    private void AddVertex(Vector2 position, Color color, Pen pen)
    {
      VertexPositionColorTexture positionColorTexture = new VertexPositionColorTexture();
      positionColorTexture.Position = new Vector3(position, 0.0f);
      positionColorTexture.Color = color;
      if (pen.Brush != null)
      {
        positionColorTexture.TextureCoordinate = Vector2.Transform(position, pen.Brush.Transform);
        positionColorTexture.Color *= pen.Brush.Alpha;
      }
      else
        positionColorTexture.TextureCoordinate = new Vector2(position.X, position.Y);
      this.vertexBuffer[this.vertexBufferIndex++] = positionColorTexture;
    }

    private void AddVertex(Vector2 position, Brush brush)
    {
      VertexPositionColorTexture positionColorTexture = new VertexPositionColorTexture();
      positionColorTexture.Position = new Vector3(position, 0.0f);
      positionColorTexture.Color = brush.Color;
      if (brush != null)
      {
        positionColorTexture.TextureCoordinate = Vector2.Transform(position, brush.Transform);
        positionColorTexture.Color *= brush.Alpha;
      }
      else
        positionColorTexture.TextureCoordinate = new Vector2(position.X, position.Y);
      this.vertexBuffer[this.vertexBufferIndex++] = positionColorTexture;
    }

    private void AddSegment(int startVertexIndex, int endVertexIndex)
    {
      this.indexBuffer[this.indexBufferIndex++] = (short) startVertexIndex;
      this.indexBuffer[this.indexBufferIndex++] = (short) (startVertexIndex + 1);
      this.indexBuffer[this.indexBufferIndex++] = (short) (endVertexIndex + 1);
      this.indexBuffer[this.indexBufferIndex++] = (short) startVertexIndex;
      this.indexBuffer[this.indexBufferIndex++] = (short) (endVertexIndex + 1);
      this.indexBuffer[this.indexBufferIndex++] = (short) endVertexIndex;
    }

    private void AddPrimitiveLineSegment(int startVertexIndex, int endVertexIndex)
    {
      this.indexBuffer[this.indexBufferIndex++] = (short) startVertexIndex;
      this.indexBuffer[this.indexBufferIndex++] = (short) endVertexIndex;
    }

    private void AddTriangle(int a, int b, int c)
    {
      this.indexBuffer[this.indexBufferIndex++] = (short) a;
      this.indexBuffer[this.indexBufferIndex++] = (short) b;
      this.indexBuffer[this.indexBufferIndex++] = (short) c;
    }

    private void FlushBuffer()
    {
      int vertexOffset = 0;
      int indexOffset = 0;
      int vertexCount = 0;
      int indexCount = 0;
      Texture2D texture = (Texture2D) null;
      PrimitiveType primitiveType = PrimitiveType.TriangleList;
      for (int index1 = 0; index1 < this.infoBufferIndex; ++index1)
      {
        if (texture != this.infoBuffer[index1].Texture || primitiveType != this.infoBuffer[index1].Primitive)
        {
          if (indexCount > 0)
          {
            for (int index2 = 0; index2 < indexCount; ++index2)
              this.indexBuffer[indexOffset + index2] -= (short) vertexOffset;
            this.RenderBatch(primitiveType, indexOffset, indexCount, vertexOffset, vertexCount, texture);
          }
          vertexOffset += vertexCount;
          indexOffset += indexCount;
          vertexCount = 0;
          indexCount = 0;
          texture = this.infoBuffer[index1].Texture;
          primitiveType = this.infoBuffer[index1].Primitive;
        }
        vertexCount += this.infoBuffer[index1].VertexCount;
        indexCount += this.infoBuffer[index1].IndexCount;
      }
      if (indexCount > 0)
      {
        for (int index = 0; index < indexCount; ++index)
          this.indexBuffer[indexOffset + index] -= (short) vertexOffset;
        this.RenderBatch(primitiveType, indexOffset, indexCount, vertexOffset, vertexCount, texture);
      }
      this.ClearInfoBuffer();
      this.infoBufferIndex = 0;
      this.indexBufferIndex = 0;
      this.vertexBufferIndex = 0;
    }

    private void RenderBatch(
      PrimitiveType primitiveType,
      int indexOffset,
      int indexCount,
      int vertexOffset,
      int vertexCount,
      Texture2D texture)
    {
      this.Batch.GraphicsDevice.Textures[0] = (Texture) texture;
      switch (primitiveType)
      {
        case PrimitiveType.TriangleList:
          this.Batch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(primitiveType, this.vertexBuffer, vertexOffset, vertexCount, this.indexBuffer, indexOffset, indexCount / 3);
          break;
        case PrimitiveType.LineList:
          this.Batch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(primitiveType, this.vertexBuffer, vertexOffset, vertexCount, this.indexBuffer, indexOffset, indexCount / 2);
          break;
      }
    }

    private void ClearInfoBuffer()
    {
      for (int index = 0; index < this.infoBufferIndex; ++index)
        this.infoBuffer[index].Texture = (Texture2D) null;
    }

    private void RequestBufferSpace(int newVertexCount, int newIndexCount)
    {
      if (this.indexBufferIndex + newIndexCount > (int) short.MaxValue)
      {
        if (this.sortMode != DrawSortMode.Immediate)
          this.SetRenderState();
        this.FlushBuffer();
      }
      if (this.infoBufferIndex + 1 > this.infoBuffer.Length)
        Array.Resize<SharpDrawBatch.DrawingInfo>(ref this.infoBuffer, this.infoBuffer.Length * 2);
      if (this.indexBufferIndex + newIndexCount >= this.indexBuffer.Length)
        Array.Resize<short>(ref this.indexBuffer, this.indexBuffer.Length * 2);
      if (this.vertexBufferIndex + newVertexCount < this.vertexBuffer.Length)
        return;
      Array.Resize<VertexPositionColorTexture>(ref this.vertexBuffer, this.vertexBuffer.Length * 2);
    }

    private List<Vector2> CalculateCircleSubdivisions(int divisions)
    {
      if (this.circleCache.ContainsKey(divisions))
        return this.circleCache[divisions];
      if (divisions < 0)
        throw new ArgumentOutOfRangeException(nameof (divisions));
      double num1 = 2.0 * Math.PI / (double) divisions;
      List<Vector2> circleSubdivisions = new List<Vector2>();
      for (int index = 0; index < divisions; ++index)
      {
        double num2 = -num1 * (double) index;
        circleSubdivisions.Add(new Vector2((float) Math.Cos(num2), (float) Math.Sin(num2)));
      }
      this.circleCache.Add(divisions, circleSubdivisions);
      return circleSubdivisions;
    }

    private static int DefaultSubdivisions(float radius)
    {
      return Math.Max(8, (int) Math.Ceiling((double) radius / 1.5));
    }

    private static int DefaultSubdivisions(float xRadius, float yRadius)
    {
      return Math.Max(8, (int) Math.Ceiling((double) Math.Max(xRadius, yRadius) / 1.5));
    }

    private struct DrawingInfo
    {
      public Texture2D Texture;
      public PrimitiveType Primitive;
      public int IndexCount;
      public int VertexCount;
    }
  }
}
