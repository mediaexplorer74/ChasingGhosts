// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.StateProviders.IInteractionStateProvider
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.UI.Controls;

#nullable disable
namespace Sharp2D.Engine.Common.UI.StateProviders
{
  /// <summary>Handles visual states for user interactions.</summary>
  public interface IInteractionStateProvider
  {
    /// <summary>Handles the state of the disabled.</summary>
    /// <param name="control">The control.</param>
    void HandleDisabledState(InteractableUiControl control);

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Down" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    void HandleDownState(InteractableUiControl control);

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Focus" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    void HandleFocusState(InteractableUiControl control);

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.Hover" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    void HandleHoverState(InteractableUiControl control);

    /// <summary>
    /// Handles the
    ///     <see cref="F:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState.None" />
    ///     state of the specified <see cref="!:control" />.
    /// </summary>
    /// <param name="control">The control to handle.</param>
    void HandleNoneState(InteractableUiControl control);
  }
}
