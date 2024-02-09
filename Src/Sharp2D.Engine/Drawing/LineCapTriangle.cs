// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.LineCapTriangle
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  internal class LineCapTriangle : LineCapInfo
  {
    private static readonly Vector2[] XYBuffer = new Vector2[3]
    {
      new Vector2(0.0f, -0.5f),
      new Vector2(-0.5f, 0.0f),
      new Vector2(0.0f, 0.5f)
    };
    private static readonly Vector2[] UVBuffer = new Vector2[3]
    {
      new Vector2(1f, 0.0f),
      new Vector2(0.5f, 0.0f),
      new Vector2(0.0f, 0.0f)
    };
    private static readonly short[] IndexBuffer = new short[3]
    {
      (short) 2,
      (short) 1,
      (short) 0
    };
    private static readonly short[] OutlineBuffer = new short[3]
    {
      (short) 0,
      (short) 1,
      (short) 2
    };

    public LineCapTriangle(float width)
      : base(width, LineCapTriangle.XYBuffer, LineCapTriangle.UVBuffer, LineCapTriangle.IndexBuffer, LineCapTriangle.OutlineBuffer)
    {
    }
  }
}
