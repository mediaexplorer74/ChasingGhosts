// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Utility.FontDefinition
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Infrastructure;

#nullable disable
namespace Sharp2D.Engine.Utility
{
  /// <summary>The font definition.</summary>
  public class FontDefinition : Component
  {
    /// <summary>The _font.</summary>
    private SpriteFont font;
    /// <summary>The _font path.</summary>
    private string fontPath;
    private ContentManager contentManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Utility.FontDefinition" /> class.
    ///     Initializes a new empty instance of the <see cref="T:Sharp2D.Engine.Utility.FontDefinition" /> class.
    /// </summary>
    public FontDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Utility.FontDefinition" /> class.
    /// </summary>
    /// <param name="fontPath">The font path.</param>
    /// <param name="originalSize">Size of the original.</param>
    public FontDefinition(string fontPath, float originalSize)
    {
      this.FontPath = fontPath;
      this.OriginalSize = originalSize;
    }

    /// <summary>Gets or sets the font path.</summary>
    /// <value>The font path.</value>
    public string FontPath
    {
      get => this.fontPath;
      set
      {
        this.fontPath = value;
        this.UpdateFont();
      }
    }

    private void UpdateFont()
    {
      if (this.contentManager == null || string.IsNullOrEmpty(this.fontPath) || this.font != null)
        return;
      this.font = this.contentManager.Load<SpriteFont>(this.fontPath);
    }

    /// <summary>
    ///     Gets or sets the original size of the font provided.
    /// </summary>
    /// <value>The original size of the font.</value>
    public float OriginalSize { get; set; }

    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.contentManager = this.Resolver.Resolve<ContentManager>();
      this.UpdateFont();
    }

    /// <summary>Gets the font this font definition uses.</summary>
    /// <returns>
    ///     The <see cref="T:Microsoft.Xna.Framework.Graphics.SpriteFont" />.
    /// </returns>
    public SpriteFont GetFont()
    {
      this.UpdateFont();
      return this.font;
    }

    /// <summary>
    /// Gets the scaling ratio of the Default Font size compared to the font size provided.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <returns>
    /// Returns a floating point value representing the difference between the two font sizes.
    /// </returns>
    public float GetScale(float fontSize) => fontSize / this.OriginalSize;
  }
}
