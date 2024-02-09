// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.StateProviders.TintInteractionStateProvider
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.UI.Controls;

#nullable disable
namespace Sharp2D.Engine.Common.UI.StateProviders
{
  /// <summary>Handles states using tints.</summary>
  public class TintInteractionStateProvider : IInteractionStateProvider
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.StateProviders.TintInteractionStateProvider" /> class.
    /// </summary>
    /// <param name="standardTint">The standard tint.</param>
    /// <param name="disabledTint">The disabled tint.</param>
    /// <param name="focusTint">The focus tint.</param>
    /// <param name="hoverTint">The hover tint.</param>
    /// <param name="downTint">Down tint.</param>
    public TintInteractionStateProvider(
      Color? standardTint = null,
      Color? disabledTint = null,
      Color? focusTint = null,
      Color? hoverTint = null,
      Color? downTint = null)
    {
      this.StandardTint = standardTint.HasValue ? standardTint.Value : Color.White;
      this.DisabledTint = disabledTint.HasValue ? disabledTint.Value : Color.DarkGray;
      this.FocusTint = focusTint.HasValue ? focusTint.Value : Color.Yellow;
      this.HoverTint = hoverTint.HasValue ? hoverTint.Value : Color.Green;
      this.DownTint = downTint.HasValue ? downTint.Value : Color.GreenYellow;
    }

    /// <summary>Gets or sets the disabled tint.</summary>
    /// <value>The disabled tint.</value>
    public Color DisabledTint { get; set; }

    /// <summary>Gets or sets down tint.</summary>
    /// <value>Down tint.</value>
    public Color DownTint { get; set; }

    /// <summary>Gets or sets the focus tint.</summary>
    /// <value>The focus tint.</value>
    public Color FocusTint { get; set; }

    /// <summary>Gets or sets the hover tint.</summary>
    /// <value>The hover tint.</value>
    public Color HoverTint { get; set; }

    /// <summary>Gets or sets the standard tint.</summary>
    /// <value>The standard tint.</value>
    public Color StandardTint { get; set; }

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Disabled" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public virtual void HandleDisabledState(InteractableUiControl control)
    {
      if (control.Sprite == null)
        return;
      control.Sprite.Tint = this.DisabledTint;
    }

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Down" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public virtual void HandleDownState(InteractableUiControl control)
    {
      if (control.Sprite == null)
        return;
      control.Sprite.Tint = this.DownTint;
    }

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Focus" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public virtual void HandleFocusState(InteractableUiControl control)
    {
      if (control.Sprite == null)
        return;
      control.Sprite.Tint = this.FocusTint;
    }

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Hover" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public virtual void HandleHoverState(InteractableUiControl control)
    {
      if (control.Sprite == null)
        return;
      control.Sprite.Tint = this.HoverTint;
    }

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.None" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    public virtual void HandleNoneState(InteractableUiControl control)
    {
      if (control.Sprite == null)
        return;
      control.Sprite.Tint = this.StandardTint;
    }
  }
}
