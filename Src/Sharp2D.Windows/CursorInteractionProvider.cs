// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.CursorInteractionProvider
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Engine.Utility;

#nullable disable
namespace Sharp2D.Windows
{
  public class CursorInteractionProvider : IInteractionProvider
  {
    public Interaction[] GetInteractions()
    {
      PressState pressState = PressState.None;
      if (InputManager.IsLeftButtonDown)
        pressState = pressState | PressState.Down | PressState.Primary;
      if (InputManager.IsRightButtonDown)
        pressState = pressState | PressState.Down | PressState.Secondary;
      return new Interaction[1]
      {
        new Interaction()
        {
          Position = InputManager.MousePosition,
          State = pressState
        }
      };
    }
  }
}
