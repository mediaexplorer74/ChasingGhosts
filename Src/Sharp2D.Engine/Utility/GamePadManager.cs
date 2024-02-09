// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Utility.GamePadManager
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace Sharp2D.Engine.Utility
{
  public static class GamePadManager
  {
    public static void Update(GameTime time)
    {
      GamePadManager.OldGamepadState = GamePadManager.GamepadState;
      GamePadManager.GamepadState = GamePad.GetState(PlayerIndex.One);
    }

    public static Vector2 LeftThumbstickMovement()
    {
      return !GamePadManager.GamepadState.IsConnected ? Vector2.Zero : GamePadManager.GamepadState.ThumbSticks.Left;
    }

    public static Vector2 RightThumbstickMovement()
    {
      return !GamePadManager.GamepadState.IsConnected ? Vector2.Zero : GamePadManager.GamepadState.ThumbSticks.Right;
    }

    public static bool IsButtonDown(Buttons button)
    {
      return GamePadManager.GamepadState.IsConnected && GamePadManager.GamepadState.IsButtonDown(button);
    }

    public static bool IsButtonUp(Buttons button)
    {
      return GamePadManager.GamepadState.IsConnected && GamePadManager.GamepadState.IsButtonDown(button);
    }

    public static bool IsButtonPressed(Buttons button)
    {
      return GamePadManager.OldGamepadState.IsConnected && !GamePadManager.OldGamepadState.IsButtonDown(button) && GamePadManager.GamepadState.IsConnected && GamePadManager.GamepadState.IsButtonDown(button);
    }

    public static bool IsButtonReleased(Buttons button)
    {
      return GamePadManager.OldGamepadState.IsConnected && GamePadManager.OldGamepadState.IsButtonDown(button) && GamePadManager.GamepadState.IsConnected && !GamePadManager.GamepadState.IsButtonDown(button);
    }

    public static GamePadState OldGamepadState { get; private set; }

    public static GamePadState GamepadState { get; private set; }
  }
}
