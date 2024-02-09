// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.SolidColorBrush
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// A <see cref="T:Sharp2D.Engine.Drawing.Brush" /> that represents a solid color.
  /// </summary>
  public class SolidColorBrush : Brush
  {
    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.SolidColorBrush" /> from the given <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" /> and <see cref="P:Sharp2D.Engine.Drawing.SolidColorBrush.Color" />.
    /// </summary>
    /// <param name="color">A color.</param>
    /// <remarks>The <see cref="P:Sharp2D.Engine.Drawing.Brush.Alpha" /> property of the brush is initialized
    /// to the alpha value of the color.</remarks>
    public SolidColorBrush(Color color)
      : base((float) color.A / (float) byte.MaxValue)
    {
      this.Color = color;
    }

    /// <summary>The color of the brush.</summary>
    public new Color Color
    {
      get => base.Color;
      private set => base.Color = value;
    }
  }
}
