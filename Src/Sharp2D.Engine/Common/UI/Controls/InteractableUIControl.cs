// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.InteractableUiControl
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.StateProviders;
using Sharp2D.Engine.Infrastructure.Input;
using System;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>
  ///     A UI Control that has a hover tint. When the mouse
  ///     enters the region of this control, the HoverTint will be used
  ///     to draw the texture.
  /// </summary>
  public abstract class InteractableUiControl : UiControl
  {
        private Func<string, bool> b;
        private Func<string, bool> a;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl" /> class.
        /// </summary>
        /// <param name="position">
        /// </param>
        /// <param name="sprite">
        /// </param>
        /// <param name="children">
        /// </param>
        protected InteractableUiControl(Vector2 position, Sprite sprite, params GameObject[] children)
      : base(position, sprite, children)
    {
      this.Interactable = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="spriteSheet">The Sprite sheet.</param>
    /// <param name="children">The children.</param>
    protected InteractableUiControl(
      Vector2 position,
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet,
      params GameObject[] children)
      : base(position, (Sprite) spriteSheet, children)
    {
      this.Interactable = true;
      this.Components.Add((Component) spriteSheet);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl" /> class.
    /// </summary>
    protected InteractableUiControl() => this.Interactable = true;

    /// <summary>Gets or sets the currently focused control.</summary>
    /// <value>The currently focused control.</value>
    public static InteractableUiControl CurrentlyFocusedControl { get; set; }

    /// <summary>Occurs when [got focus].</summary>
    public event EventHandler GotFocus;

    /// <summary>Occurs when [lost focus].</summary>
    public event EventHandler LostFocus;

    /// <summary>
    ///     Gets the region/area of this UI control. Overriden to provide different values
    ///     when <see cref="!:SpriteType" /> is set to <see cref="!:Enums.SpriteType.Sheet" />
    /// </summary>
    /// <value>The region.</value>
    public override Rectanglef GlobalRegion => this.GetRegion();

    /// <summary>
    ///     Gets or sets a value indicating whether [has focus].
    /// </summary>
    /// <value>
    ///     <c>true</c> if [has focus]; otherwise, <c>false</c>.
    /// </value>
    public bool HasFocus => InteractableUiControl.CurrentlyFocusedControl == this;

    /// <summary>
    ///     Gets or sets the current user interaction with this control since the last update.
    /// </summary>
    /// <value>The state of the UI interaction.</value>
    public UiControlInteractionState InteractionState { get; set; }

    /// <summary>
    ///     Determines whether this control is focusable.
    ///     Each derived control is responsible for setting it's focus, with an exception to the
    ///     Focus Navigation that is built in to the UI system.
    /// </summary>
    /// <value></value>
    public virtual bool IsFocusable => this.Enabled;

    /// <summary>
    ///     Gets or sets the region used for drawing the next frame.
    /// </summary>
    /// <value>The region index.</value>
    public string SpriteRegionIndex { get; set; }

    /// <summary>Gets or sets the Sprite sheet. Only used if</summary>
    /// <remarks>
    ///     We chose to use <see cref="T:System.String" /> as the region index type, as it is the most commonly used in UI state.
    /// </remarks>
    /// <value>The Sprite sheet.</value>
    public Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> SpriteSheet
    {
      get => this.Components.OfType<Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string>>().FirstOrDefault<Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string>>();
    }

    /// <summary>Gets or sets the state provider for this control.</summary>
    /// <value>The state provider.</value>
    public virtual IInteractionStateProvider StateProvider { get; set; }

    /// <summary>Gets the width of the underlying texture.</summary>
    /// <value>The width.</value>
    public override float Width
    {
      get
      {
        Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet = this.SpriteSheet;
        return spriteSheet != null ? spriteSheet.SpriteSize.X * spriteSheet.Scale.X : base.Width;
      }
    }

    /// <summary>Gets the height of the underlying texture.</summary>
    /// <value>The height.</value>
    public override float Height
    {
      get
      {
        Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet = this.SpriteSheet;
        return spriteSheet != null ? spriteSheet.SpriteSize.Y * spriteSheet.Scale.Y : base.Height;
      }
    }

    public bool IsActive
    {
      get
      {
        GameObject gameObject = (GameObject) this;
        while (!gameObject.IsPaused && !gameObject.IsHidden)
        {
          gameObject = gameObject.Parent;
          if (gameObject == null)
            return true;
        }
        return false;
      }
    }

    /// <summary>
    ///     Sets this control as the currently focused control.
    /// </summary>
    public void SetFocus()
    {
      InteractableUiControl currentlyFocusedControl = InteractableUiControl.CurrentlyFocusedControl;
      InteractableUiControl.CurrentlyFocusedControl = this;
      if (currentlyFocusedControl != null)
      {
        EventHandler lostFocus = currentlyFocusedControl.LostFocus;
        if (lostFocus != null)
          lostFocus((object) this, EventArgs.Empty);
      }
      EventHandler gotFocus = this.GotFocus;
      if (gotFocus == null)
        return;
      gotFocus((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Updates this control, and runs the update method on all children.
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      if (this.IsPaused)
        return;
      int num = (int) this.UpdateInteractionState();
      switch (this.InteractionState)
      {
        case UiControlInteractionState.None:
          this.HandleNoneState();
          break;
        case UiControlInteractionState.Disabled:
          this.HandleDisabledState();
          break;
        case UiControlInteractionState.Focus:
          this.HandleFocusState();
          break;
        case UiControlInteractionState.Hover:
          this.HandleHoverState();
          break;
        case UiControlInteractionState.Down:
          this.HandleDownState();
          break;
        case UiControlInteractionState.Trigger:
          this.HandleTriggerState();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet = this.SpriteSheet;
      if (spriteSheet != null)
      {
                //RnD
                // ISSUE: method pointer
                //var a = __methodptr(\u003CUpdate\u003Eg__Predicate\u007C40_0);
        // ISSUE: method pointer
        //spriteSheet.RegionKey = spriteSheet.Regions.Keys.Any<string>
        //            (
        //    new Func<string, bool>(this, a)
        //    ) ? spriteSheet.Regions.Keys.First<string>(new Func<string, bool>(this, /*__methodptr(\u003CUpdate\u003Eg__Predicate\u007C40_0)*/b)) : spriteSheet.Regions.Keys.FirstOrDefault<string>();
      }
      base.Update(time);
    }

    internal UiControlInteractionState DoUpdateInteractionState() => this.UpdateInteractionState();

    protected UiControlInteractionState UpdateInteractionState()
    {
      this.InteractionState = this.IsVisible ? (this.Enabled ? (!this.Interactions.Any<Interaction>((Func<Interaction, bool>) (i => i.State.HasFlag((Enum) PressState.Down) && i.State.HasFlag((Enum) PressState.Primary))) ? (!this.Interactions.Any<Interaction>() ? UiControlInteractionState.None : (this.InteractionState == UiControlInteractionState.Down ? UiControlInteractionState.Trigger : UiControlInteractionState.Hover)) : UiControlInteractionState.Down) : UiControlInteractionState.Disabled) : UiControlInteractionState.None;
      return this.InteractionState;
    }

    /// <summary>The handle disabled state.</summary>
    protected virtual void HandleDisabledState() => this.StateProvider?.HandleDisabledState(this);

    /// <summary>
    ///     When this control's state is <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Down" />,
    ///     use the state provider.
    ///     <remarks>
    ///         When overridden in a derived class, if you do not call base, the state provider will not be called.
    ///     </remarks>
    /// </summary>
    protected virtual void HandleDownState() => this.StateProvider?.HandleDownState(this);

    /// <summary>
    ///     Does the same as <see cref="M:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.HandleDownState" />, but with focus state.
    /// </summary>
    protected virtual void HandleFocusState() => this.StateProvider?.HandleFocusState(this);

    /// <summary>
    ///     Does the same as <see cref="M:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.HandleDownState" />, but with hover state.
    /// </summary>
    protected virtual void HandleHoverState() => this.StateProvider?.HandleHoverState(this);

    /// <summary>
    ///     Does the same as <see cref="M:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.HandleDownState" />, but with none state.
    ///     This is where you do stuff when the user is not doing anything with the control.
    /// </summary>
    protected virtual void HandleNoneState() => this.StateProvider?.HandleNoneState(this);

    /// <summary>
    ///     Does the same as <see cref="M:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.HandleDownState" />, but with trigger state, except there
    ///     is probably no trigger visual state.
    /// </summary>
    /// <remarks>
    ///     This also handles setting the focus to this control.
    ///     If you want to implement this yourself, don't call base.
    /// </remarks>
    protected virtual void HandleTriggerState() => this.SetFocus();

    /// <summary>
    ///     Gets the region of this <see cref="T:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl" />.
    /// </summary>
    /// <remarks>
    ///     If <see cref="!:SpriteType" /> is set to <see cref="!:Enums.SpriteType.Sheet" />, the <see cref="P:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl.SpriteSheet" /> will
    ///     be used.
    ///     If set to <see cref="!:Enums.SpriteType.Single" />, <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" /> will be used.
    /// </remarks>
    /// <returns>
    ///     The <see cref="T:Microsoft.Xna.Framework.Rectangle" />.
    /// </returns>
    private Rectanglef GetRegion()
    {
      float x = this.GlobalPosition.X;
      float y = this.GlobalPosition.Y;
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<int> spriteSheet = this.Components.OfType<Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<int>>().FirstOrDefault<Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<int>>();
      if (spriteSheet != null)
        return new Rectanglef(x - (float) spriteSheet.Width * spriteSheet.TransformOrigin.X * this.GlobalScale.X * spriteSheet.Scale.X, y - (float) spriteSheet.Height * spriteSheet.TransformOrigin.Y * this.GlobalScale.Y * spriteSheet.Scale.Y, this.Width * this.GlobalScale.X, this.Height * this.GlobalScale.Y);
      return this.Sprite != null ? new Rectanglef(x - (float) this.Sprite.Width * this.Sprite.TransformOrigin.X * this.GlobalScale.X * this.Sprite.Scale.X, y - (float) this.Sprite.Height * this.Sprite.TransformOrigin.Y * this.GlobalScale.Y * this.Sprite.Scale.Y, this.Width * this.GlobalScale.X, this.Height * this.GlobalScale.Y) : new Rectanglef(x, y, this.Width * this.GlobalScale.X, this.Height * this.GlobalScale.Y);
    }
  }
}
