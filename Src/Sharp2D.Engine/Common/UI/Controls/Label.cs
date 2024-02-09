// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.Label
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>Label control.</summary>
  public class Label : UiControl
  {
    /// <summary>The _alignment.</summary>
    private TextAlignment alignment;
    /// <summary>The _local position.</summary>
    private Vector2 localPosition;
    /// <summary>The _old global pos.</summary>
    private Vector2 oldGlobalPos;
    /// <summary>The _text.</summary>
    private string text;
    /// <summary>The _text lines.</summary>
    private List<string> textLines;
    /// <summary>The _text positions.</summary>
    private List<Vector2> textPositions;
    /// <summary>The _update position.</summary>
    private bool updatePosition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Label" /> class.
    ///     <remarks>
    /// This will generate a Font Definition with a default OriginalFontSize of 100, upon <see cref="!:LoadContent" />
    ///         execution.
    ///     </remarks>
    /// </summary>
    /// <param name="fontPath">The font path.</param>
    /// <param name="position">The position.</param>
    public Label(string fontPath, Vector2 position = default (Vector2))
      : base(position, (Sprite) null)
    {
      this.InitLabel();
      this.Components.Add((Component) new FontDefinition(fontPath, 10f));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Label" /> class.
    /// </summary>
    /// <param name="fontDefinition">The font definition.</param>
    /// <param name="position">The position.</param>
    public Label(FontDefinition fontDefinition, Vector2 position = default (Vector2))
      : base(position, (Sprite) null)
    {
      this.InitLabel();
      this.Components.Add((Component) fontDefinition);
      this.FontSize = fontDefinition.OriginalSize;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Label" /> class.
    /// </summary>
    public Label() => this.InitLabel();

    /// <summary>
    /// Gets or sets a value indicating whether this instance has border.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance has border; otherwise, <c>false</c>.
    /// </value>
    public bool HasBorder { get; set; }

    /// <summary>Gets or sets the color of the border.</summary>
    /// <value>The color of the border.</value>
    public Color BorderTint { get; set; }

    /// <summary>
    ///     Gets or sets the alignment of the text.
    ///     <para>
    ///         If <see cref="P:Sharp2D.Engine.Common.UI.Controls.Label.Alignment" /> is set to <see cref="F:Sharp2D.Engine.Common.UI.Enums.TextAlignment.Center" />,
    ///         this will be the center of which to draw the text.
    ///         If <see cref="P:Sharp2D.Engine.Common.UI.Controls.Label.Alignment" /> is set to <see cref="F:Sharp2D.Engine.Common.UI.Enums.TextAlignment.Right" />,
    ///         the last letter of the text will be drawn at this position.
    ///     </para>
    /// </summary>
    /// <value>The alignment.</value>
    public TextAlignment Alignment
    {
      get => this.alignment;
      set
      {
        if (this.alignment != value)
          this.updatePosition = true;
        this.alignment = value;
      }
    }

    /// <summary>
    ///     Gets or sets the tint to use when this control receives focus.
    /// </summary>
    /// <value>The focus tint.</value>
    public Color FocusTint { get; set; }

    /// <summary>Gets or sets the font definition.</summary>
    /// <value>The font definition.</value>
    public FontDefinition FontDefinition
    {
      get => this.Components.OfType<FontDefinition>().FirstOrDefault<FontDefinition>();
    }

    /// <summary>Gets or sets the size of the font.</summary>
    /// <value>The size of the font.</value>
    public float FontSize { get; set; }

    /// <summary>
    ///     Gets a value indicating whether this control is currently being hovered.
    ///     <para>
    ///         Uses <see cref="M:Sharp2D.Engine.Common.UI.Controls.Label.GetHoverState" />, which is virtual. Override at will.
    ///     </para>
    /// </summary>
    /// <value>
    ///     <c>true</c> if [is hovering]; otherwise, <c>false</c>.
    /// </value>
    public bool IsHovering => this.GetHoverState();

    /// <summary>Gets or sets the line spacing.</summary>
    /// <value>The line spacing.</value>
    public float LineSpacing { get; set; }

    /// <summary>
    ///     Gets or sets the local position relative to it's parent object.
    ///     If <see cref="P:Sharp2D.Engine.Common.UI.Controls.Label.Alignment" /> is set to <see cref="F:Sharp2D.Engine.Common.UI.Enums.TextAlignment.Center" />,
    ///     this will be the center of which to draw the text.
    ///     If <see cref="P:Sharp2D.Engine.Common.UI.Controls.Label.Alignment" /> is set to <see cref="F:Sharp2D.Engine.Common.UI.Enums.TextAlignment.Right" />,
    ///     the last letter of the text will be drawn at this position.
    /// </summary>
    /// <value>The local position.</value>
    public override Vector2 LocalPosition
    {
      get => this.localPosition;
      set
      {
        if (this.localPosition != value)
          this.updatePosition = true;
        this.localPosition = value;
      }
    }

    /// <summary>Gets or sets the text of the label.</summary>
    /// <value>The text.</value>
    public string Text
    {
      get => this.text;
      set
      {
        if (this.text != value)
          this.updatePosition = true;
        this.text = value ?? string.Empty;
        this.textLines = ((IEnumerable<string>) this.text.Split(new char[1]
        {
          '\n'
        }, StringSplitOptions.None)).ToList<string>();
      }
    }

    /// <summary>Gets or sets the tint.</summary>
    public Color Tint { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [drop shadow].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [drop shadow]; otherwise, <c>false</c>.
    /// </value>
    public bool HasDropShadow { get; set; }

    /// <summary>Gets or sets the drop shadow offset.</summary>
    /// <value>The drop shadow offset.</value>
    public Vector2 DropShadowOffset { get; set; }

    /// <summary>Gets or sets the drop shadow opacity.</summary>
    /// <value>The drop shadow opacity.</value>
    public float DropShadowOpacity { get; set; }

    /// <summary>Draws the string and calls base.Draw.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (this.IsHidden)
        return;
      base.Draw(batch, time);
      if (string.IsNullOrEmpty(this.Text))
        this.Text = string.Empty;
      Vector2 localScale = this.LocalScale;
      this.LocalScale = Vector2.One;
      Vector2 globalPosition = this.GlobalPosition;
      this.LocalScale = localScale;
      if (this.oldGlobalPos != globalPosition)
        this.updatePosition = true;
      this.oldGlobalPos = globalPosition;
      if (this.updatePosition)
      {
        this.CalculatePosition();
        this.updatePosition = false;
      }
      SpriteFont spriteFont = this.FontDefinition.GetFont();
      Vector2 scale = new Vector2(this.FontDefinition.GetScale(this.FontSize));
      scale *= this.GlobalScale;
      for (int index1 = 0; index1 < this.textLines.Count; ++index1)
      {
        string text = this.textLines[index1];
        Vector2 textPosition = this.textPositions[index1];
        Vector2 textSize = spriteFont.MeasureString(text);
        Vector2 vector2_1 = globalPosition + textPosition;
        if (this.HasDropShadow)
          batch.DrawString(spriteFont, text, vector2_1 + this.DropShadowOffset, this.DropShadowTint * this.DropShadowOpacity, MathHelper.ToRadians(this.GlobalRotation), textSize / 2f, scale, SpriteEffects.None, 1f);
        Action<Vector2, Color> action = (Action<Vector2, Color>) ((position, tint) => batch.DrawString(spriteFont, text, position, tint, MathHelper.ToRadians(this.GlobalRotation), textSize / 2f, scale, SpriteEffects.None, 1f));
        if (this.HasBorder)
        {
          for (int index2 = -1; index2 <= 1; ++index2)
          {
            for (int index3 = -1; index3 <= 1; ++index3)
            {
              if (index2 != 0 || index3 != 0)
              {
                Vector2 vector2_2 = new Vector2((float) (index2 * this.BorderWidth), (float) (index3 * this.BorderWidth));
                action(vector2_1 + vector2_2, this.BorderTint);
              }
            }
          }
        }
        action(vector2_1, this.Tint);
      }
    }

    public Color DropShadowTint { get; set; }

    /// <summary>Gets or sets the width of the border.</summary>
    /// <value>The width of the border.</value>
    public int BorderWidth { get; set; }

    /// <summary>
    ///     Determines if this control is currently being hovered. Override for custom behavior.
    /// </summary>
    /// <returns>
    ///     The <see cref="T:System.Boolean" />.
    /// </returns>
    public virtual bool GetHoverState()
    {
      return this.GlobalRegion.IsPointerInRegion(this.Resolver.Resolve<IPointerDevice>());
    }

    /// <summary>
    /// Moves the label, aligning it according to the provided region.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <param name="alignment">The alignment.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// alignment
    /// </exception>
    public void MoveLabel(Rectanglef region, TextAlignment alignment)
    {
      switch (alignment)
      {
        case TextAlignment.Left:
          this.LocalPosition = new Vector2(0.0f, region.Height / 2f);
          break;
        case TextAlignment.Center:
          this.LocalPosition = new Vector2(region.Width / 2f, region.Height / 2f);
          break;
        case TextAlignment.Right:
          this.LocalPosition = new Vector2(region.Width, region.Height / 2f);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (alignment));
      }
      this.Alignment = alignment;
    }

    /// <summary>
    /// Updates this control, and runs the update method on all children.
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      if (this.IsPaused)
        return;
      base.Update(time);
    }

    /// <summary>
    ///     Initializes the label. Shared method for constructors.
    /// </summary>
    private void InitLabel()
    {
      this.BorderWidth = 1;
      this.BorderTint = Color.Black;
      this.DropShadowOffset = new Vector2(2f);
      this.DropShadowOpacity = 0.5f;
      this.updatePosition = true;
      this.Tint = Color.Black;
      this.DropShadowTint = Color.Black;
      this.Alignment = TextAlignment.Left;
      this.Text = "UNDEFINED";
      this.FontSize = 24f;
      this.LineSpacing = 1f;
      this.textPositions = new List<Vector2>();
    }

    /// <summary>
    ///     Calculates the position of the text. Sets the value of <see cref="F:Sharp2D.Engine.Common.UI.Controls.Label.textPositions" />.
    /// </summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    private void CalculatePosition()
    {
      while (this.textLines.Count > this.textPositions.Count)
        this.textPositions.Add(Vector2.Zero);
      while (this.textLines.Count < this.textPositions.Count)
        this.textPositions.RemoveAt(this.textPositions.Count - 1);
      float scale = this.FontDefinition.GetScale(this.FontSize);
      SpriteFont font = this.FontDefinition.GetFont();
      for (int index = 0; index < this.textLines.Count; ++index)
      {
        Vector2 vector2_1 = font.MeasureString(this.textLines[index]) * scale;
        Vector2 vector2_2 = GameObject.V2(vector2_1.X / 2f, 0.0f);
        Vector2 vector2_3 = GameObject.V2((float) -((double) vector2_1.X / 2.0), 0.0f);
        this.textPositions[index] = new Vector2(0.0f, 0.0f);
        Vector2 pointToRotate = Vector2.Zero;
        switch (this.Alignment)
        {
          case TextAlignment.Left:
            pointToRotate = vector2_2;
            break;
          case TextAlignment.Right:
            pointToRotate = vector2_3;
            break;
        }
        this.textPositions[index] += SharpMathHelper.Rotate(pointToRotate, Vector2.Zero, this.GlobalRotation);
        float y = (float) index * vector2_1.Y * this.LineSpacing;
        this.textPositions[index] += new Vector2(0.0f, y);
      }
    }
  }
}
