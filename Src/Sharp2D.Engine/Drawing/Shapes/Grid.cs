// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Shapes.Grid
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Sharp2D.Engine.Drawing.Shapes
{
  /// <summary>
  /// An object that compiles a low-level geometry cache for a grid.
  /// </summary>
  /// <remarks>Geometry compiled by a <see cref="T:Sharp2D.Engine.Drawing.Shapes.Grid" /> object does not overlap at intersections, unlike a grid rendered
  /// manually as a series of crossing lines.</remarks>
  public class Grid
  {
    private VertexPositionColorTexture[] _vertexBuffer;
    private short[] _indexBuffer;
    private int _columns;
    private int _rows;
    private int[] _serial = new int[1];

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Shapes.Grid" /> object from the given columns and rows.
    /// </summary>
    /// <param name="columns">The number of columns in the grid.</param>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <remarks><see cref="T:Sharp2D.Engine.Drawing.Shapes.Grid" /> objects initialize their memory buffers from the given column and row values.  A grid can
    /// be compiled any number of times without needing to allocate new buffers.</remarks>
    public Grid(int columns, int rows)
    {
      this._columns = columns;
      this._rows = rows;
      int num1 = (columns + 1) * (rows + 1);
      int num2 = (columns + 1) * rows;
      int num3 = columns * (rows + 1);
      this._vertexBuffer = new VertexPositionColorTexture[num1 * 4];
      this._indexBuffer = new short[(num1 * 2 + num2 * 2 + num3 * 2) * 3];
    }

    /// <summary>
    /// Compiles the grid's geometry from the given <see cref="T:Sharp2D.Engine.Drawing.Pen" /> and overall dimensions into a <see cref="T:Sharp2D.Engine.Drawing.DrawCache" /> object.
    /// </summary>
    /// <param name="pen">The <see cref="T:Sharp2D.Engine.Drawing.Pen" /> to render the grid's lines with.</param>
    /// <param name="left">The ledge edge of the grid.</param>
    /// <param name="top">The top edge of the grid.</param>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    /// <returns>A <see cref="T:Sharp2D.Engine.Drawing.DrawCache" /> with the compiled geometry.</returns>
    /// <remarks>Only the most recently compiled <see cref="T:Sharp2D.Engine.Drawing.DrawCache" /> is valid for any given <see cref="T:Sharp2D.Engine.Drawing.Shapes.Grid" />.</remarks>
    public DrawCache Compile(Pen pen, float left, float top, float width, float height)
    {
      short num1 = 0;
      short num2 = 0;
      float num3 = pen.Width / 2f;
      for (int index1 = 0; index1 <= this._rows; ++index1)
      {
        for (int index2 = 0; index2 <= this._columns; ++index2)
        {
          float num4 = left + width / (float) this._columns * (float) index2;
          float num5 = top + height / (float) this._rows * (float) index1;
          this.AddVertex(this._vertexBuffer, (int) num1, num4 - num3, num5 - num3, pen.ColorAt(0.0f, 0.0f, 0.0f));
          this.AddVertex(this._vertexBuffer, (int) num1 + 1, num4 + num3, num5 - num3, pen.ColorAt(0.0f, 0.0f, 0.0f));
          this.AddVertex(this._vertexBuffer, (int) num1 + 2, num4 - num3, num5 + num3, pen.ColorAt(0.0f, 0.0f, 0.0f));
          this.AddVertex(this._vertexBuffer, (int) num1 + 3, num4 + num3, num5 + num3, pen.ColorAt(0.0f, 0.0f, 0.0f));
          this.AddQuad(this._indexBuffer, num2, num1);
          num1 += (short) 4;
          num2 += (short) 6;
        }
      }
      for (int index3 = 0; index3 < this._rows; ++index3)
      {
        for (int index4 = 0; index4 <= this._columns; ++index4)
        {
          int num6 = (index3 * (this._columns + 1) + index4) * 4;
          int bl = ((index3 + 1) * (this._columns + 1) + index4) * 4;
          this.AddQuad(this._indexBuffer, num2, (short) (num6 + 2), (short) (num6 + 3), (short) bl, (short) (bl + 1));
          num2 += (short) 6;
        }
      }
      for (int index5 = 0; index5 <= this._rows; ++index5)
      {
        for (int index6 = 0; index6 < this._columns; ++index6)
        {
          int num7 = (index5 * (this._columns + 1) + index6) * 4;
          int tr = num7 + 4;
          this.AddQuad(this._indexBuffer, num2, (short) (num7 + 1), (short) tr, (short) (num7 + 3), (short) (tr + 2));
          num2 += (short) 6;
        }
      }
      ++this._serial[0];
      DrawCache drawCache = new DrawCache();
      drawCache.AddUnit((DrawCacheUnit) new Grid.GridCacheUnit(this._vertexBuffer, this._indexBuffer, pen.Brush.Texture, this._serial));
      return drawCache;
    }

    private void AddVertex(
      VertexPositionColorTexture[] buffer,
      int index,
      float x,
      float y,
      Color c)
    {
      buffer[index] = new VertexPositionColorTexture(new Vector3(x, y, 0.0f), c, new Vector2(0.0f, 0.0f));
    }

    private void AddQuad(short[] buffer, short iIndex, short vIndex)
    {
      buffer[(int) iIndex] = (short) ((int) vIndex + 2);
      buffer[(int) iIndex + 1] = vIndex;
      buffer[(int) iIndex + 2] = (short) ((int) vIndex + 1);
      buffer[(int) iIndex + 3] = (short) ((int) vIndex + 2);
      buffer[(int) iIndex + 4] = (short) ((int) vIndex + 1);
      buffer[(int) iIndex + 5] = (short) ((int) vIndex + 3);
    }

    private void AddQuad(short[] buffer, short index, short tl, short tr, short bl, short br)
    {
      buffer[(int) index] = bl;
      buffer[(int) index + 1] = tl;
      buffer[(int) index + 2] = tr;
      buffer[(int) index + 3] = bl;
      buffer[(int) index + 4] = tr;
      buffer[(int) index + 5] = br;
    }

    private class GridCacheUnit : DrawCacheUnit
    {
      private int[] _sourceSerial;
      private int _serial;

      public GridCacheUnit(
        VertexPositionColorTexture[] vertexBuffer,
        short[] indexBuffer,
        Texture2D texture,
        int[] serial)
        : base(vertexBuffer, indexBuffer, texture)
      {
        this._sourceSerial = serial;
        this._serial = serial[0];
      }

      public override bool IsValid => this._serial == this._sourceSerial[0];
    }
  }
}
