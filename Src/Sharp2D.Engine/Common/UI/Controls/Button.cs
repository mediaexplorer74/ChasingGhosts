// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.Button
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Common.UI.Events;
using Sharp2D.Engine.Common.UI.StateProviders;
using Sharp2D.Engine.Helper;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>Button UI Control.</summary>
  public class Button : InteractableUiControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Button" /> class and set's the state manager to a
    ///     <see cref="T:Sharp2D.Engine.Common.UI.StateProviders.SpriteSheetInteractionStateProvider" />
    /// </summary>
    /// <param name="position">
    /// </param>
    /// <param name="sprite">
    /// </param>
    /// <param name="children">
    /// </param>
    public Button(Vector2 position, Sprite sprite = null, params GameObject[] children)
      : base(position, sprite, children)
    {
      this.StateProvider = (IInteractionStateProvider) new TintInteractionStateProvider();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Button" /> class and set's the state manager to a
    ///     <see cref="T:Sharp2D.Engine.Common.UI.StateProviders.SpriteSheetInteractionStateProvider" />
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="spriteSheet">The Sprite sheet.</param>
    /// <param name="children">The children.</param>
    public Button(Vector2 position, Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet, params GameObject[] children)
      : base(position, spriteSheet, children)
    {
      this.StateProvider = (IInteractionStateProvider) new SpriteSheetInteractionStateProvider();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Button" /> class and sets the <see cref="P:Sharp2D.Engine.Common.UI.Controls.Button.Label" />
    /// </summary>
    /// <param name="texture">The texture.</param>
    /// <param name="label">The label.</param>
    public Button(Sprite texture, Label label = null)
      : this(Vector2.Zero, texture)
    {
      this.Label = label;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Button" /> class.
    /// </summary>
    public Button()
      : base(Vector2.Zero, (Sprite) null)
    {
    }

    /// <summary>
    ///     Occurs when the button has been triggered. Usually by a left-button mouse click.
    /// </summary>
    public event ButtonTriggeredEventHandler Triggered;

    /// <summary>
    ///     Gets or sets the label. When setting, the label's parent is set to 'this'.
    /// </summary>
    /// <value>The label.</value>
    public Label Label
    {
      get => this.Children.OfType<Label>().FirstOrDefault<Label>();
      set => this.SetLabel(value);
    }

    /// <summary>The trigger.</summary>
    public void Trigger() => this.OnTriggered(new ButtonTriggeredEventArgs());

    /// <summary>
    ///     Does the same as <see cref="M:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.HandleDownState" />, but with trigger state, except there
    ///     is probably no trigger visual state.
    /// </summary>
    protected override void HandleTriggerState()
    {
      base.HandleTriggerState();
      this.OnTriggered(new ButtonTriggeredEventArgs());
    }

    /// <summary>
    /// Raises the <see cref="E:Sharp2D.Engine.Common.UI.Controls.Button.Triggered" /> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="T:Sharp2D.Engine.Common.UI.Events.ButtonTriggeredEventArgs" /> instance containing the event data.
    /// </param>
    protected virtual void OnTriggered(ButtonTriggeredEventArgs e)
    {
      ButtonTriggeredEventHandler triggered = this.Triggered;
      if (triggered == null)
        return;
      triggered((object) this, e);
    }

    /// <summary>
    ///     Repositions the label to appear centered inside the button's region.
    /// </summary>
    private void CenterLabel()
    {
      Label label = this.Label;
      if (label == null)
        return;
      label.Center();
    }

    /// <summary>
    /// Setter for <see cref="P:Sharp2D.Engine.Common.UI.Controls.Button.Label" />, ensures that we center the label per default.
    /// </summary>
    /// <param name="value">The value.</param>
    private void SetLabel(Label value)
    {
      if (value == null)
        Debug.WriteLine("Deprecated! You can't set the label to null by setting this property. Instead remove it from Children!");
      else if (this.Children.Contains((GameObject) value))
      {
        Debug.WriteLine("Already contains the label you're trying to set! Nothing changed.");
      }
      else
      {
        this.Children.Add((GameObject) value);
        if (value.Alignment != TextAlignment.Left)
          return;
        this.CenterLabel();
      }
    }
  }
}
