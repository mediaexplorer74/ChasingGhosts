// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.UiContainerHelper
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>UIManager Helpers.</summary>
  public static class UiContainerHelper
  {
    /// <summary>
    ///     Finds the next focusable control. Does NOT focus it!
    ///     The idea is to mimic Windows' Tab with focusable controls.
    /// </summary>
    /// <returns>
    ///     The <see cref="T:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl" />.
    /// </returns>
    public static InteractableUiControl FindNextFocusableControl()
    {
      List<InteractableUiControl> source = Sharp2D.Engine.Common.Scene.Scene.CurrentScene != null ? Sharp2D.Engine.Common.Scene.Scene.CurrentScene.UiRoot.RecursiveWhere(new Func<GameObject, bool>(UiContainerHelper.FocusableControlsPredicate)).Cast<InteractableUiControl>().ToList<InteractableUiControl>() : throw new InvalidOperationException("There is no instance of Scene available. (Scene.Current was null)");
      if (!source.Any<InteractableUiControl>())
        return (InteractableUiControl) null;
      InteractableUiControl currentlyFocusedControl = InteractableUiControl.CurrentlyFocusedControl;
      int num = -1;
      if (currentlyFocusedControl != null)
        num = source.IndexOf(currentlyFocusedControl);
      int index = num + 1;
      if (source.Count<InteractableUiControl>() <= index)
        index = 0;
      return source[index];
    }

    /// <summary>
    ///     Finds the previous focusable control. Does NOT focus is.
    ///     The idea is to mimic Windows' Shift+Tab with focusable controls.
    /// </summary>
    /// <returns>
    ///     The <see cref="T:Sharp2D.Engine.Common.UI.Controls.InteractableUiControl" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///     There is no instance of Scene available. (Scene.Current was null)
    /// </exception>
    public static InteractableUiControl FindPreviousFocusableControl()
    {
      List<InteractableUiControl> source = Sharp2D.Engine.Common.Scene.Scene.CurrentScene != null ? Sharp2D.Engine.Common.Scene.Scene.CurrentScene.UiRoot.RecursiveWhere(new Func<GameObject, bool>(UiContainerHelper.FocusableControlsPredicate)).Cast<InteractableUiControl>().ToList<InteractableUiControl>() : throw new InvalidOperationException("There is no instance of Scene available. (Scene.Current was null)");
      if (!source.Any<InteractableUiControl>())
        return (InteractableUiControl) null;
      InteractableUiControl currentlyFocusedControl = InteractableUiControl.CurrentlyFocusedControl;
      int num = source.Count;
      if (currentlyFocusedControl != null)
        num = source.IndexOf(currentlyFocusedControl);
      int index = num - 1;
      if (index < 0)
        index = source.Count - 1;
      return source[index];
    }

    /// <summary>
    /// Predicate for determining if a GameObject is focusable.
    /// </summary>
    /// <param name="gameObject">The game object.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    private static bool FocusableControlsPredicate(GameObject gameObject)
    {
      if (!(gameObject is InteractableUiControl))
        return false;
      InteractableUiControl interactableUiControl = (InteractableUiControl) gameObject;
      return interactableUiControl.IsFocusable && interactableUiControl.Enabled && interactableUiControl.IsActive;
    }
  }
}
