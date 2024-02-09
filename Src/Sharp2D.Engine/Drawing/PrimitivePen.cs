// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.PrimitivePen
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// A <see cref="T:Sharp2D.Engine.Drawing.Pen" /> that can only have a solid color and width of 1.
  /// </summary>
  public class PrimitivePen : Pen
  {
    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.PrimitivePen" /> with the given color.
    /// </summary>
    /// <param name="color">The pen color.</param>
    public PrimitivePen(Color color)
      : base(color, 1f)
    {
    }
  }
}
