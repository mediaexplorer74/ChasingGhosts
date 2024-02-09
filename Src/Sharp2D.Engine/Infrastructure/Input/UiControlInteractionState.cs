// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.UiControlInteractionState
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  /// <summary>
  ///     Represents the state of user interaction with a UI control.
  /// </summary>
  public enum UiControlInteractionState
  {
    /// <summary>Nothing.</summary>
    None,
    /// <summary>The control is disabled.</summary>
    Disabled,
    /// <summary>The control is focussed.</summary>
    Focus,
    /// <summary>The control is being hovered over.</summary>
    Hover,
    /// <summary>The control is down.</summary>
    Down,
    /// <summary>The control is triggered.</summary>
    Trigger,
  }
}
