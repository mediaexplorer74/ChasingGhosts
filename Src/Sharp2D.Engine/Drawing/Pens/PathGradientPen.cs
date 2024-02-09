// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Pens.PathGradientPen
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Drawing.Pens
{
  /// <summary>
  /// A <see cref="T:Sharp2D.Engine.Drawing.Pen" /> that blends two colors across the length of the stroked path.
  /// </summary>
  public class PathGradientPen : Pen
  {
    private byte _r1;
    private byte _g1;
    private byte _b1;
    private byte _a1;
    private short _rdiff;
    private short _gdiff;
    private short _bdiff;
    private short _adiff;

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pens.GradientPen" /> with the given colors and width.
    /// </summary>
    /// <param name="startColor">The starting pen color.</param>
    /// <param name="endColor">The ending pen color.</param>
    /// <param name="width">The width of the paths drawn by the pen.</param>
    public PathGradientPen(Color startColor, Color endColor, float width)
      : base(Color.White, width)
    {
      this._r1 = startColor.R;
      this._g1 = startColor.G;
      this._b1 = startColor.B;
      this._a1 = startColor.A;
      this._rdiff = (short) ((int) endColor.R - (int) this._r1);
      this._gdiff = (short) ((int) endColor.G - (int) this._g1);
      this._bdiff = (short) ((int) endColor.B - (int) this._b1);
      this._adiff = (short) ((int) endColor.A - (int) this._a1);
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Pens.GradientPen" /> with the given colors and a width of 1.
    /// </summary>
    /// <param name="startColor">The starting pen color.</param>
    /// <param name="endColor">The ending pen color.</param>
    public PathGradientPen(Color startColor, Color endColor)
      : this(startColor, endColor, 1f)
    {
    }

    /// <InheritDoc />
    public override bool NeedsPathLength => true;

    /// <InheritDoc />
    protected internal override Color ColorAt(
      float widthPosition,
      float lengthPosition,
      float lengthScale)
    {
      return this.Lerp(lengthPosition * lengthScale);
    }

    private Color Lerp(float amount)
    {
      return Color.TransparentBlack with
      {
        R = (byte) ((double) this._r1 + (double) this._rdiff * (double) amount),
        G = (byte) ((double) this._g1 + (double) this._gdiff * (double) amount),
        B = (byte) ((double) this._b1 + (double) this._bdiff * (double) amount),
        A = (byte) ((double) this._a1 + (double) this._adiff * (double) amount)
      };
    }
  }
}
