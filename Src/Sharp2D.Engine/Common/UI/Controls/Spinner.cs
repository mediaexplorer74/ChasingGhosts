// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.Spinner
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Common.UI.Events;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Infrastructure;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>Spinner control</summary>
  public sealed class Spinner : InteractableUiControl
  {
    /// <summary>The _index.</summary>
    private int index;
    /// <summary>The _label.</summary>
    private Label label;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Spinner" /> class.
    /// </summary>
    /// <param name="localPosition">The local position.</param>
    /// <param name="mainSprite">The main Sprite.</param>
    /// <param name="items">The items.</param>
    /// <param name="plusButton">The plus button.</param>
    /// <param name="minusButton">The minus button.</param>
    /// <param name="label">The label.</param>
    /// <param name="children">The children.</param>
    public Spinner(
      Vector2 localPosition,
      Sprite mainSprite,
      List<object> items,
      Button plusButton,
      Button minusButton,
      Label label,
      params GameObject[] children)
      : base(localPosition, mainSprite, children)
    {
      this.Items = items;
      this.PlusButton = plusButton;
      this.PlusButton.Sprite.CenterObject();
      this.MinusButton = minusButton;
      this.MinusButton.Sprite.CenterObject();
      this.Add((GameObject) this.MinusButton);
      this.Add((GameObject) this.PlusButton);
      this.Label = label;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Spinner" /> class.
    /// </summary>
    /// <param name="localPosition">The local position.</param>
    /// <param name="mainSprite">The main Sprite.</param>
    /// <param name="items">The items.</param>
    /// <param name="plusButton">The plus button.</param>
    /// <param name="minusButton">The minus button.</param>
    /// <param name="fontPath">The font path.</param>
    /// <param name="children">The children.</param>
    public Spinner(
      Vector2 localPosition,
      Sprite mainSprite,
      List<object> items,
      Button plusButton,
      Button minusButton,
      string fontPath,
      params GameObject[] children)
      : this(localPosition, mainSprite, items, plusButton, minusButton, new Label(fontPath), children)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Spinner" /> class.
    /// </summary>
    public Spinner() => this.Items = new List<object>();

    /// <summary>
    ///     This control is not focusable - the buttons are, though.
    /// </summary>
    public override bool IsFocusable => false;

    /// <summary>
    ///     Gets or sets the items. Their ToString method will be called.
    /// </summary>
    /// <value>The items.</value>
    public List<object> Items { get; private set; }

    /// <summary>
    ///     Gets or sets the label showing the selected item's text.
    /// </summary>
    /// <value>The label.</value>
    public Label Label
    {
      get => this.label;
      set => this.SetLabel(value);
    }

    /// <summary>
    ///     Gets or sets the minus button. Decrements the index when triggered.
    /// </summary>
    /// <value>The minus button.</value>
    public Button MinusButton { get; set; }

    /// <summary>
    ///     Gets or sets the plus button. Increments the index when triggered.
    /// </summary>
    /// <value>The plus button.</value>
    public Button PlusButton { get; set; }

    /// <summary>Gets or sets the index of the selected item.</summary>
    /// <value>The index of the selected.</value>
    public int SelectedIndex
    {
      get => this.index;
      set
      {
        this.index = value;
        this.UpdateControl();
      }
    }

    /// <summary>Draws this spinner control.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (this.IsHidden)
        return;
      base.Draw(batch, time);
      if (this.Label == null)
        return;
      this.Label.Draw(batch, time);
    }

    /// <summary>Initializes the spinner.</summary>
    /// <param name="resolver"></param>
    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.UpdateControl();
      this.PlusButton.Triggered += (ButtonTriggeredEventHandler) ((sender, args) =>
      {
        if (this.SelectedIndex + 1 < this.Items.Count)
          this.index = this.SelectedIndex + 1;
        this.UpdateControl();
      });
      this.MinusButton.Triggered += (ButtonTriggeredEventHandler) ((sender, args) =>
      {
        if (this.SelectedIndex - 1 >= 0)
          this.index = this.SelectedIndex - 1;
        this.UpdateControl();
      });
    }

    /// <summary>
    /// Updates this control, and runs the update method on all children.
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      if (this.IsPaused)
        return;
      if (this.Label != null)
        this.Label.Update(time);
      base.Update(time);
    }

    /// <summary>Updates the control.</summary>
    private void UpdateControl() => this.Label.Text = this.Items[this.SelectedIndex].ToString();

    /// <summary>
    ///     Repositions the label to appear centered inside the button's region.
    /// </summary>
    private void CenterLabel() => this.Label.Center();

    /// <summary>
    /// Setter for <see cref="P:Sharp2D.Engine.Common.UI.Controls.Spinner.Label" />, ensures that we center the label per default.
    /// </summary>
    /// <param name="value">The value.</param>
    private void SetLabel(Label value)
    {
      this.label = value;
      if (value == null)
        return;
      this.label.Parent = (GameObject) this;
      if (this.label.Alignment != TextAlignment.Left)
        return;
      this.CenterLabel();
    }
  }
}
