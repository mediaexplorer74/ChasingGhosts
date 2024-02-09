// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.TextBox
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Common.UI.Events;
using Sharp2D.Engine.Common.UI.Layout;
using Sharp2D.Engine.Common.UI.StateProviders;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Engine.Utility;
using System;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>TextBox control.</summary>
  public class TextBox : InteractableUiControl
  {
    /// <summary>The keyboard manager.</summary>
    private KeyboardManager keyboardManager;
    /// <summary>The rasterizer.</summary>
    private readonly RasterizerState rasterizer = new RasterizerState()
    {
      ScissorTestEnable = true
    };
    /// <summary>The multi line.</summary>
    private bool multiLine;
    /// <summary>The text field.</summary>
    private Label textField;
    /// <summary>The alignment.</summary>
    private TextAlignment alignment;
    private string oldText;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.TextBox" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="font">The font.</param>
    /// <param name="spriteSheet">The Sprite sheet.</param>
    /// <param name="stateProvider">The state provider.</param>
    /// <param name="children">The children.</param>
    public TextBox(
      Vector2 position,
      FontDefinition font,
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet = null,
      IInteractionStateProvider stateProvider = null,
      params GameObject[] children)
      : base(position, spriteSheet, children)
    {
      this.TextField = new Label(font)
      {
        Alignment = TextAlignment.Left
      };
      this.TextField.MoveLabel(new Rectanglef(0.0f, 0.0f, this.Width, this.Height) + this.Padding, this.TextField.Alignment);
      this.alignment = this.TextField.Alignment;
      this.StateProvider = stateProvider ?? (IInteractionStateProvider) new SpriteSheetInteractionStateProvider();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.TextBox" /> class.
    /// </summary>
    public TextBox()
    {
    }

    /// <summary>Occurs when the text is changed.</summary>
    public event TextChangedEventHandler TextChanged;

    /// <summary>
    ///     Occurs when the textbox has been triggered. Usually by a left-button mouse click.
    /// </summary>
    public event TextboxTriggeredEventHandlerArgs Triggered;

    /// <summary>
    ///     Gets or sets a value indicating whether the textbox is single, or multi line.
    /// </summary>
    /// <value>
    ///     <c>true</c> if multi lined; otherwise, <c>false</c>.
    /// </value>
    public bool MultiLine
    {
      get => this.multiLine;
      set
      {
        this.multiLine = value;
        this.CalculateNewPosition();
      }
    }

    /// <summary>
    ///     Gets or sets the padding for the underlying label.
    /// </summary>
    /// <value>The padding.</value>
    public Padding Padding { get; set; }

    /// <summary>Gets or sets the placeholder text.</summary>
    /// <value>The placeholder text.</value>
    public string Placeholder { get; set; }

    /// <summary>Gets or sets the text field.</summary>
    /// <value>The text field.</value>
    public Label TextField
    {
      get => this.textField;
      set => this.SetLabel(value);
    }

    public bool IsPassword { get; set; }

    public char PasswordCharacter { get; set; } = '*';

    /// <summary>Initializes this specific game object.</summary>
    /// <param name="resolver"></param>
    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.keyboardManager = this.Resolver.Resolve<IKeyboardManagerFactory>().CreateKeyboardManager(this);
      this.Components.Add((Component) this.keyboardManager);
      this.CalculateNewPosition();
    }

    /// <summary>
    /// Draws a <see cref="!:SpriteSheet" /> if <see cref="!:SpriteType" /> is set to <see cref="!:Enums.SpriteType.Sheet" />.
    ///     Calls <see cref="M:Sharp2D.Engine.Common.ObjectSystem.GameObject.Draw(Sharp2D.Engine.Drawing.SharpDrawBatch,Microsoft.Xna.Framework.GameTime)" />.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (this.IsHidden)
        return;
      this.TextField.Hide();
      base.Draw(batch, time);
      this.TextField.Show();
      this.DrawContainerContent(batch, time);
    }

    /// <summary>
    /// Moves the label, aligning it according to this textbox's region.
    /// </summary>
    /// <param name="alignment">The alignment.</param>
    public void MoveLabel(TextAlignment alignment)
    {
      this.alignment = alignment;
      this.TextField.MoveLabel(this.GlobalRegion, alignment);
    }

    /// <summary>
    /// Updates this control, and runs the update method on all children.
    ///     If focused, allows the user to input text in the field.
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      if (this.IsPaused)
        return;
      string text = this.textField.Text;
      if (this.oldText != text)
      {
        TextChangedEventArgs e = new TextChangedEventArgs(this.oldText, text);
        this.OnTextChanged(e);
        if (e.UpdatePositioning)
          this.CalculateNewPosition();
      }
      this.textField.DoUpdate(time);
      base.Update(time);
      this.oldText = this.textField.Text;
    }

    /// <summary>
    ///     Does the same as <see cref="!:HandleDownState" />, but with trigger state, except there
    ///     is probably no trigger visual state.
    /// </summary>
    /// <remarks>
    ///     This also handles setting the focus to this control.
    ///     If you want to implement this yourself, don't call base.
    /// </remarks>
    protected override void HandleTriggerState()
    {
      base.HandleTriggerState();
      this.OnTriggered(new TextboxTriggeredEventArgs());
    }

    /// <summary>
    /// Raises the <see cref="E:TextChanged" /> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="T:Sharp2D.Engine.Common.UI.Events.TextChangedEventArgs" /> instance containing the event data.
    /// </param>
    protected virtual void OnTextChanged(TextChangedEventArgs e)
    {
      TextChangedEventHandler textChanged = this.TextChanged;
      if (textChanged == null)
        return;
      textChanged((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:Sharp2D.Engine.Common.UI.Controls.TextBox.Triggered" /> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="T:Sharp2D.Engine.Common.UI.Events.ButtonTriggeredEventArgs" /> instance containing the event data.
    /// </param>
    protected virtual void OnTriggered(TextboxTriggeredEventArgs e)
    {
      TextboxTriggeredEventHandlerArgs triggered = this.Triggered;
      if (triggered == null)
        return;
      triggered((object) this, e);
    }

    /// <summary>Draws the content of the container.</summary>
    /// <param name="spriteBatch">The batch.</param>
    /// <param name="time">The time.</param>
    private void DrawContainerContent(SharpDrawBatch batch, GameTime time)
    {
      SpriteBatch batch1 = batch.Batch;
      RasterizerState rasterizerState = batch1.GraphicsDevice.RasterizerState;
      Rectanglef currentRect = new Rectanglef(batch1.GraphicsDevice.ScissorRectangle);
      Vector2 drawScaling = Resolution.DetermineDrawScaling();
      Rectanglef scissorRectangle = this.GlobalRegion + this.Padding;
      scissorRectangle = new Rectanglef(scissorRectangle.X * drawScaling.X, scissorRectangle.Y * drawScaling.Y, scissorRectangle.Width * drawScaling.X, scissorRectangle.Height * drawScaling.Y);
      if (!ClippingHelper.GetRectangle(currentRect, ref scissorRectangle))
        return;
      batch1.End();
      batch1.Begin(SpriteSortMode.Immediate, rasterizerState: this.rasterizer, transformMatrix: new Matrix?(Resolution.Scale));
      batch1.GraphicsDevice.ScissorRectangle = scissorRectangle.ToRect();
      foreach (GameContract gameContract in this.Children.Where<GameObject>((Func<GameObject, bool>) (k => k != this.TextField)))
        gameContract.Draw(batch, time);
      Vector2 localPosition = this.TextField.LocalPosition;
      Label textField = this.TextField;
      textField.LocalPosition = textField.LocalPosition + new Vector2(this.Padding.Left, this.Padding.Top);
      string str = this.TextField.Text ?? string.Empty;
      if (!this.HasFocus && string.IsNullOrEmpty(this.TextField.Text))
      {
        this.TextField.Text = this.Placeholder ?? string.Empty;
      }
      else
      {
        int num;
        if (this.IsPassword)
        {
          string text = this.TextField.Text;
          num = text != null ? (text.Length > 0 ? 1 : 0) : 0;
        }
        else
          num = 0;
        if (num != 0)
          this.TextField.Text = new string(Enumerable.Range(0, this.TextField.Text.Length).Select<int, char>((Func<int, char>) (i => this.PasswordCharacter)).ToArray<char>());
      }
      this.TextField.Draw(batch, time);
      this.TextField.Text = str;
      this.TextField.LocalPosition = localPosition;
      batch1.GraphicsDevice.ScissorRectangle = currentRect.ToRect();
      batch1.End();
      batch1.Begin(SpriteSortMode.Immediate, rasterizerState: rasterizerState, transformMatrix: new Matrix?(Resolution.Scale));
    }

    /// <summary>Calculates the new position.</summary>
    private void CalculateNewPosition()
    {
      if (this.TextField == null)
        return;
      float scale = this.TextField.FontDefinition.GetScale(this.TextField.FontSize);
      SpriteFont font = this.TextField.FontDefinition.GetFont();
      if (font == null)
        return;
      Vector2 vector2_1 = font.MeasureString(this.TextField.Text) * scale;
      Vector2 vector2_2 = vector2_1;
      int length = this.TextField.Text.IndexOf("\n", StringComparison.Ordinal);
      if (0 < length)
      {
        string text = this.TextField.Text.Substring(0, length);
        vector2_2 = font.MeasureString(text) * scale;
      }
      if (this.MultiLine)
      {
        this.TextField.LocalPosition = new Vector2(this.Padding.Left, vector2_2.Y / 2f + this.Padding.Top);
        double x = (double) vector2_1.X;
        double width = (double) this.Width;
        Padding padding = this.Padding;
        double left = (double) padding.Left;
        padding = this.Padding;
        double right = (double) padding.Right;
        double num1 = left + right;
        double num2 = width - num1;
        if (x <= num2)
          return;
        this.TextField.Text = string.Format("{0}\n{1}", new object[2]
        {
          (object) this.TextField.Text.Substring(0, this.TextField.Text.Length - 1),
          (object) this.TextField.Text.Substring(this.TextField.Text.Length - 1)
        });
      }
      else
        this.TextField.MoveLabel((new Rectanglef(0.0f, 0.0f, this.Width, this.Height) + this.Padding) with
        {
          Height = this.Height
        }, (double) vector2_1.X > (double) this.Width - ((double) this.Padding.Left + (double) this.Padding.Right) ? TextAlignment.Right : this.alignment);
    }

    /// <summary>
    /// Sets the label, ensuring that the previous textfield is removed (if it existed).
    ///     This is done by the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" /> base class.
    /// </summary>
    /// <param name="value">The new label.</param>
    private void SetLabel(Label value)
    {
      if (value == null)
      {
        this.textField = (Label) null;
      }
      else
      {
        this.textField = value;
        this.textField.ActualParent = (GameObject) this;
        this.Children.Add((GameObject) this.textField);
      }
    }
  }
}
