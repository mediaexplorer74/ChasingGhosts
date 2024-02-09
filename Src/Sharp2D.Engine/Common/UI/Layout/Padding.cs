// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Layout.Padding
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.ObjectSystem;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Layout
{
  /// <summary>Padding struct.</summary>
  public struct Padding
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Layout.Padding" /> struct.
    /// </summary>
    /// <param name="topBottom">The top bottom.</param>
    /// <param name="leftRight">The left right.</param>
    public Padding(float topBottom, float leftRight)
      : this()
    {
      this.Top = this.Bottom = topBottom;
      this.Left = this.Right = leftRight;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Layout.Padding" /> struct.
    /// </summary>
    /// <param name="top">The top.</param>
    /// <param name="right">The right.</param>
    /// <param name="bottom">The bottom.</param>
    /// <param name="left">The left.</param>
    public Padding(float top, float right, float bottom, float left)
      : this()
    {
      this.Top = top;
      this.Right = right;
      this.Bottom = bottom;
      this.Left = left;
    }

    /// <summary>Gets or sets the bottom.</summary>
    /// <value>The bottom.</value>
    public float Bottom { get; set; }

    /// <summary>Gets or sets the left.</summary>
    /// <value>The left.</value>
    public float Left { get; set; }

    /// <summary>Gets or sets the right.</summary>
    /// <value>The right.</value>
    public float Right { get; set; }

    /// <summary>Gets or sets the top.</summary>
    /// <value>The top.</value>
    public float Top { get; set; }

    /// <summary>The +.</summary>
    /// <param name="source">The source.</param>
    /// <param name="padding">The padding.</param>
    /// <returns>
    /// </returns>
    public static Rectanglef operator +(Rectanglef source, Padding padding)
    {
      return new Rectanglef(source.X + padding.Left, source.Y + padding.Top, source.Width - (padding.Right + padding.Left), source.Height - (padding.Bottom + padding.Top));
    }

    /// <summary>The +.</summary>
    /// <param name="padding">The padding.</param>
    /// <param name="source">The source.</param>
    /// <returns>
    /// </returns>
    public static Rectanglef operator +(Padding padding, Rectanglef source) => source + padding;

    /// <summary>The -.</summary>
    /// <param name="source">The source.</param>
    /// <param name="padding">The padding.</param>
    /// <returns>
    /// </returns>
    public static Rectanglef operator -(Rectanglef source, Padding padding)
    {
      return new Rectanglef(source.X - padding.Left, source.Y - padding.Top, source.Width + (padding.Right + padding.Left), source.Height + (padding.Bottom + padding.Top));
    }
  }
}
