// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.CheckBox
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Common.UI.StateProviders;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Utility;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>
  ///     CheckBox control.
  ///     TODO: Discuss assumed Sprite layout (2 spritesheets or 1?)
  ///     TODO: Bjarke's answer: Let's use 2 spritesheets. This will allow us to reuse the code we already have,
  ///     TODO: and all we'll have to do is add another spritesheet property to this class.
  /// </summary>
  public class CheckBox : InteractableUiControl
  {
    /// <summary>The _label.</summary>
    private Label label;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.CheckBox" /> class.
    ///     Also sets up a default <see cref="P:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.StateProvider" />
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="spriteSheet">The Sprite sheet.</param>
    /// <param name="checkMarkSprite">The check mark Sprite.</param>
    /// <param name="labelFont">The label font.</param>
    /// <param name="text">The text.</param>
    public CheckBox(
      Vector2 position,
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet,
      Sprite checkMarkSprite,
      FontDefinition labelFont,
      string text = "Check box")
      : base(position, spriteSheet, (GameObject[]) null)
    {
      this.StateProvider = (IInteractionStateProvider) new SpriteSheetInteractionStateProvider();
      this.CheckMarkSprite = checkMarkSprite;
      this.Label = new Label(labelFont) { Text = text };
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.CheckBox" /> class.
    /// </summary>
    public CheckBox()
    {
    }

    /// <summary>Gets or sets the check mark Sprite.</summary>
    /// <value>The check mark Sprite.</value>
    public Sprite CheckMarkSprite { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this checkbox is in the checked state.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [checked]; otherwise, <c>false</c>.
    /// </value>
    public bool IsChecked { get; set; }

    /// <summary>Gets or sets the label.</summary>
    /// <value>The label.</value>
    public Label Label
    {
      get => this.label;
      set => this.SetLabel(value);
    }

    /// <summary>Occurs when [checked].</summary>
    public event EventHandler<bool> CheckedChanged;

    /// <summary>
    /// Draws a <see cref="!:SpriteSheet" /> if <see cref="!:SpriteType" /> is set to <see cref="!:Enums.SpriteType.Sheet" />.
    ///     If <see cref="P:Sharp2D.Engine.Common.UI.Controls.CheckBox.IsChecked" />, also draws the <see cref="P:Sharp2D.Engine.Common.UI.Controls.CheckBox.CheckMarkSprite" />.
    ///     Calls <see cref="!:InteractableUiControl.Draw" />.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// Check mark Sprite cannot be null.
    /// </exception>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      base.Draw(batch, time);
      if (this.IsChecked)
      {
        if (this.CheckMarkSprite == null)
          throw new InvalidOperationException("Check mark Sprite cannot be null.");
        this.CheckMarkSprite.Draw(batch, time, this.GlobalPosition + this.CheckMarkSprite.TransformOrigin, Color.White);
      }
      this.Label?.Draw(batch, time);
    }

    /// <summary>
    ///     Does the same as <see cref="!:HandleDownState" />, but with trigger state, except there
    ///     is probably no trigger visual state.
    ///     Toggles the <see cref="P:Sharp2D.Engine.Common.UI.Controls.CheckBox.IsChecked" /> state on or off.
    /// </summary>
    /// <remarks>
    ///     This also handles setting the focus to this control.
    ///     If you want to implement this yourself, don't call base.
    /// </remarks>
    protected override void HandleTriggerState()
    {
      this.IsChecked = !this.IsChecked;
      EventHandler<bool> checkedChanged = this.CheckedChanged;
      if (checkedChanged != null)
        checkedChanged((object) this, this.IsChecked);
      base.HandleTriggerState();
    }

    /// <summary>Sets the label.</summary>
    /// <param name="value">The value.</param>
    private void SetLabel(Label value)
    {
      this.label = value;
      if (this.label == null)
        return;
      this.label.MoveLabel(this.GlobalRegion, TextAlignment.Left);
      Label label = this.label;
      label.LocalPosition = label.LocalPosition + GameObject.V2(this.Width + 16f, 0.0f);
      this.Add((GameObject) this.label);
    }
  }
}
