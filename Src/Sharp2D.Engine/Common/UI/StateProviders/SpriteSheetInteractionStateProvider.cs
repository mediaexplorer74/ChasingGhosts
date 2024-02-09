// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.StateProviders.SpriteSheetInteractionStateProvider
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.UI.Controls;

#nullable disable
namespace Sharp2D.Engine.Common.UI.StateProviders
{
  /// <summary>Sprite Sheet interaction state provider.</summary>
  public class SpriteSheetInteractionStateProvider : TintInteractionStateProvider
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.StateProviders.SpriteSheetInteractionStateProvider" /> class.
    /// </summary>
    /// <param name="standard">The standard.</param>
    /// <param name="disabled">The disabled.</param>
    /// <param name="focus">The focus.</param>
    /// <param name="hover">The hover.</param>
    /// <param name="down">Down.</param>
    public SpriteSheetInteractionStateProvider(
      string standard = "standard",
      string disabled = "disabled",
      string focus = "focus",
      string hover = "hover",
      string down = "down")
      : base()
    {
      this.StandardTint = Color.White;
      this.FocusTint = Color.White;
      this.HoverTint = Color.White;
      this.DownTint = Color.White;
      this.StandardRegion = standard;
      this.DisabledRegion = disabled;
      this.FocusRegion = focus;
      this.HoverRegion = hover;
      this.DownRegion = down;
    }

    /// <summary>Gets or sets the disabled region.</summary>
    /// <value>Down region.</value>
    public string DisabledRegion { get; set; }

    /// <summary>Gets or sets the down region.</summary>
    /// <value>Down region.</value>
    public string DownRegion { get; set; }

    /// <summary>Gets or sets the focus region.</summary>
    /// <value>The focus region.</value>
    public string FocusRegion { get; set; }

    /// <summary>Gets or sets the hover region.</summary>
    /// <value>The hover region.</value>
    public string HoverRegion { get; set; }

    /// <summary>Gets or sets the standard region.</summary>
    /// <value>The standard region.</value>
    public string StandardRegion { get; set; }

    /// <summary>
    /// Handles the
    ///     <see cref="!:UiControlInteractionState.Disabled" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public override void HandleDisabledState(InteractableUiControl control)
    {
      control.SpriteRegionIndex = this.DisabledRegion;
      base.HandleDisabledState(control);
    }

    /// <summary>
    /// Handles the
    ///     <see cref="!:UiControlInteractionState.Down" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public override void HandleDownState(InteractableUiControl control)
    {
      control.SpriteRegionIndex = this.DownRegion;
      base.HandleDownState(control);
    }

    /// <summary>
    /// Handles the
    ///     <see cref="!:UiControlInteractionState.Focus" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public override void HandleFocusState(InteractableUiControl control)
    {
      control.SpriteRegionIndex = this.FocusRegion;
      base.HandleFocusState(control);
    }

    /// <summary>
    /// Handles the
    ///     <see cref="!:UiControlInteractionState.Hover" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public override void HandleHoverState(InteractableUiControl control)
    {
      control.SpriteRegionIndex = this.HoverRegion;
      base.HandleHoverState(control);
    }

    /// <summary>
    /// Handles the
    ///     <see cref="!:UiControlInteractionState.None" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public override void HandleNoneState(InteractableUiControl control)
    {
      control.SpriteRegionIndex = this.StandardRegion;
      base.HandleNoneState(control);
    }
  }
}
