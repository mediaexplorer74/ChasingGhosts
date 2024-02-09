// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.IUiInteractionProvider
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.UI.Controls;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  /// <summary>The UIInteractionProvider interface.</summary>
  public interface IUiInteractionProvider
  {
    /// <summary>Gets the state of the UI control interaction.</summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState" />.
    /// </returns>
    UiControlInteractionState GetUiControlInteractionState(InteractableUiControl control);

    /// <summary>Should we focus the next control now?</summary>
    /// <returns>
    ///     The <see cref="T:System.Boolean" />.
    /// </returns>
    bool ShouldFocusNext();

    /// <summary>Should we focus the previous control now?</summary>
    /// <returns>
    ///     The <see cref="T:System.Boolean" />.
    /// </returns>
    bool ShouldFocusPrevious();
  }
}
