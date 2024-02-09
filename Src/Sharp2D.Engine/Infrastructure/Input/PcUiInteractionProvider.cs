// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.PcUiInteractionProvider
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Input;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Utility;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  /// <summary>A Standard Input Provider.</summary>
  public class PcUiInteractionProvider : IUiInteractionProvider
  {
    private readonly IResolver resolver;

    public PcUiInteractionProvider(IResolver resolver) => this.resolver = resolver;

    /// <summary>Gets the state of the UI control interaction.</summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState" />.
    /// </returns>
    public UiControlInteractionState GetUiControlInteractionState(InteractableUiControl control)
    {
      if (!control.IsVisible)
        return UiControlInteractionState.None;
      if (!control.Enabled)
        return UiControlInteractionState.Disabled;
      if (this.IsControlBeingTriggered(control))
        return UiControlInteractionState.Trigger;
      if (this.IsControlDown(control))
        return UiControlInteractionState.Down;
      if (control.GlobalRegion.IsPointerInRegion(this.resolver.Resolve<IPointerDevice>(), control.GlobalRotation))
        return UiControlInteractionState.Hover;
      return control.HasFocus ? UiControlInteractionState.Focus : UiControlInteractionState.None;
    }

    /// <summary>Determines whether the control is being clicked.</summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    public virtual bool IsControlBeingTriggered(InteractableUiControl control)
    {
      return InputManager.IsLeftButtonClicked && control.GlobalRegion.IsPointerInRegion(this.resolver.Resolve<IPointerDevice>(), control.GlobalRotation) || control.HasFocus && InputManager.IsKeyReleased(new Keys?(Keys.Enter));
    }

    /// <summary>
    /// Determines if the specified bvutton's state is "down".
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// <c>true</c> if the control is being pushed down; <c>false</c> otherwise.
    /// </returns>
    public virtual bool IsControlDown(InteractableUiControl control)
    {
      return InputManager.IsLeftButtonDown && control.GlobalRegion.IsPointerInRegion(this.resolver.Resolve<IPointerDevice>(), control.GlobalRotation) || control.HasFocus && InputManager.IsKeyDown(new Keys?(Keys.Enter));
    }

    /// <summary>Should we focus the next control now?</summary>
    /// <returns>
    ///     The <see cref="T:System.Boolean" />.
    /// </returns>
    public bool ShouldFocusNext()
    {
      return !InputManager.IsKeyDown(new Keys?(Keys.LeftShift)) && !InputManager.IsKeyDown(new Keys?(Keys.RightShift)) && InputManager.IsKeyPressed(new Keys?(Keys.Tab));
    }

    /// <summary>Should we focus the previous control now?</summary>
    /// <returns>
    ///     The <see cref="T:System.Boolean" />.
    /// </returns>
    public bool ShouldFocusPrevious()
    {
      return (InputManager.IsKeyDown(new Keys?(Keys.LeftShift)) || InputManager.IsKeyDown(new Keys?(Keys.RightShift))) && InputManager.IsKeyPressed(new Keys?(Keys.Tab));
    }
  }
}
