// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.TextureBrush
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// A <see cref="T:Sharp2D.Engine.Drawing.Brush" /> that represents a texture.
  /// </summary>
  public class TextureBrush : Brush
  {
    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.TextureBrush" /> with the given texture.
    /// </summary>
    /// <param name="texture">A texture.</param>
    public TextureBrush(Texture2D texture)
      : this(texture, 1f)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.TextureBrush" /> with the texture and opacity.
    /// </summary>
    /// <param name="texture">A texture.</param>
    /// <param name="opacity">The opacity to render the texture with.</param>
    /// <remarks>The <see cref="P:Sharp2D.Engine.Drawing.Brush.Alpha" /> property of the brush is intialized to the opacity value.
    /// When the brush is rendered, any opacity already present in the texture is blended with
    /// the opacity value.</remarks>
    public TextureBrush(Texture2D texture, float opacity)
      : base(opacity)
    {
      this.Texture = texture;
      this.Transform = Matrix.CreateScale(1f / (float) texture.Width, 1f / (float) this.Texture.Height, 1f);
    }

    /// <summary>Gets or sets the texture resource of the brush.</summary>
    public new Texture2D Texture
    {
      get => base.Texture;
      protected set => base.Texture = value;
    }

    /// <summary>Gets or sets the color to blend with the texture.</summary>
    public new Color Color
    {
      get => base.Color;
      set => base.Color = value;
    }

    /// <summary>Gets or sets the transformation to apply to brush.</summary>
    /// <remarks>The default transform for <see cref="T:Sharp2D.Engine.Drawing.TextureBrush" /> scales the resultant UV coordinates by
    /// the dimensions of the texture.</remarks>
    public new Matrix Transform
    {
      get => base.Transform;
      set => base.Transform = value;
    }

    /// <summary>
    /// Gets or sets whether this brush "owns" the texture used to construct it, and should therefor dispose the texture
    /// along with itself.
    /// </summary>
    public bool OwnsTexture { get; set; }

    /// <inherit />
    protected override void DisposeManaged()
    {
      if (!this.OwnsTexture)
        return;
      this.Texture.Dispose();
    }
  }
}
