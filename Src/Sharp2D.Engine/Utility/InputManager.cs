// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Utility.InputManager
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sharp2D.Engine.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Utility
{
  /// <summary>
  ///     Represents the current and previous state of input devices.
  /// </summary>
  public static class InputManager
  {
    /// <summary>
    ///     this many updates will occur between two consecutive true returns of IsKeyPulsed
    /// </summary>
    public static int KeyPulses = 5;
    /// <summary>The all keys.</summary>
    private static readonly Keys[] AllKeys = (Keys[]) Enum.GetValues(typeof (Keys));
    /// <summary>The key list.</summary>
    private static readonly List<Keys> KeyList = new List<Keys>();
    /// <summary>The key pulses.</summary>
    private static readonly List<int> keyPulses = new List<int>();
    /// <summary>The left double click time max.</summary>
    private static readonly TimeSpan LeftDoubleClickTimeMax = TimeSpan.FromSeconds(1.0);
    /// <summary>The middle double click time max.</summary>
    private static readonly TimeSpan MiddleDoubleClickTimeMax = TimeSpan.FromSeconds(1.0);
    /// <summary>The pulsing keys.</summary>
    private static readonly List<Keys> PulsingKeys = new List<Keys>();
    /// <summary>The right double click time max.</summary>
    private static readonly TimeSpan RightDoubleClickTimeMax = TimeSpan.FromSeconds(1.0);
    /// <summary>The delta x.</summary>
    private static int deltaX;
    /// <summary>The delta y.</summary>
    private static int deltaY;
    /// <summary>The left click count.</summary>
    private static bool leftClickCount;
    /// <summary>The left double click time.</summary>
    private static TimeSpan leftDoubleClickTime;
    /// <summary>The middle click count.</summary>
    private static bool middleClickCount;
    /// <summary>The middle double click time.</summary>
    private static TimeSpan middleDoubleClickTime;
    /// <summary>The right click count.</summary>
    private static bool rightClickCount;
    /// <summary>The right double click time.</summary>
    private static TimeSpan rightDoubleClickTime;

    /// <summary>
    ///     Initializes static members of the <see cref="T:Sharp2D.Engine.Utility.InputManager" /> class.
    /// </summary>
    static InputManager()
    {
      InputManager.MouseScale = Resolution.DetermineDrawScaling();
      InputManager.IsMouseVisible = true;
    }

    /// <summary>
    ///     Gets the change in the value of the mouse scroll wheel.
    /// </summary>
    public static int DeltaScroll
    {
      get
      {
        MouseState mouseState = InputManager.Mouse;
        int scrollWheelValue1 = mouseState.ScrollWheelValue;
        mouseState = InputManager.OldMouse;
        int scrollWheelValue2 = mouseState.ScrollWheelValue;
        return scrollWheelValue1 - scrollWheelValue2;
      }
    }

    /// <summary>
    ///     Gets the change in the horizontal position of the mouse cursor.
    /// </summary>
    public static int DeltaX => InputManager.deltaX;

    /// <summary>
    ///     Gets the change in the vertical position of the mouse cursor.
    /// </summary>
    public static int DeltaY => InputManager.deltaY;

    /// <summary>
    ///     Returns whether the left mouse button was clicked (previously pressed, but now released).
    /// </summary>
    public static bool IsLeftButtonClicked
    {
      get
      {
        return InputManager.Mouse.LeftButton == ButtonState.Released && InputManager.OldMouse.LeftButton == ButtonState.Pressed;
      }
    }

    /// <summary>
    ///     Returns whether the left mouse button was double clicked.
    /// </summary>
    public static bool IsLeftButtonDoubleClick
    {
      get => InputManager.IsLeftButtonPressed && InputManager.leftClickCount;
    }

    /// <summary>Returns whether the left mouse button is down.</summary>
    public static bool IsLeftButtonDown => InputManager.Mouse.LeftButton == ButtonState.Pressed;

    /// <summary>
    ///     Returns whether the left mouse button was pressed (previously up, but now down).
    /// </summary>
    public static bool IsLeftButtonPressed
    {
      get
      {
        return InputManager.OldMouse.LeftButton == ButtonState.Released && InputManager.Mouse.LeftButton == ButtonState.Pressed;
      }
    }

    /// <summary>
    ///     Returns whether the middle mouse button was clicked (previously pressed, but now released).
    /// </summary>
    public static bool IsMiddleButtonClicked
    {
      get
      {
        return InputManager.Mouse.MiddleButton == ButtonState.Released && InputManager.OldMouse.MiddleButton == ButtonState.Pressed;
      }
    }

    /// <summary>
    ///     Returns whether the middle mouse button was double clicked.
    /// </summary>
    public static bool IsMiddleButtonDoubleClick
    {
      get => InputManager.IsMiddleButtonPressed && InputManager.middleClickCount;
    }

    /// <summary>Returns whether the middle mouse button is held.</summary>
    public static bool IsMiddleButtonDown => InputManager.Mouse.MiddleButton == ButtonState.Pressed;

    /// <summary>
    ///     Returns whether the middle mouse button was pressed (previously up, but now down).
    /// </summary>
    public static bool IsMiddleButtonPressed
    {
      get
      {
        return InputManager.OldMouse.MiddleButton == ButtonState.Released && InputManager.Mouse.MiddleButton == ButtonState.Pressed;
      }
    }

    /// <summary>Gets or sets the visibility of the cursor.</summary>
    public static bool IsMouseVisible { get; set; }

    /// <summary>
    ///     Returns whether the right mouse button was clicked (previously pressed, but now released).
    /// </summary>
    public static bool IsRightButtonClicked
    {
      get
      {
        return InputManager.Mouse.RightButton == ButtonState.Released && InputManager.OldMouse.RightButton == ButtonState.Pressed;
      }
    }

    /// <summary>
    ///     Returns whether the right mouse button was double clicked.
    /// </summary>
    public static bool IsRightButtonDoubleClick
    {
      get => InputManager.IsRightButtonPressed && InputManager.rightClickCount;
    }

    /// <summary>Returns whether the right mouse button is held.</summary>
    public static bool IsRightButtonDown => InputManager.Mouse.RightButton == ButtonState.Pressed;

    /// <summary>
    ///     Returns whether the left mouse button was pressed (previously up, but now down).
    /// </summary>
    public static bool IsRightButtonPressed
    {
      get
      {
        return InputManager.OldMouse.RightButton == ButtonState.Released && InputManager.Mouse.RightButton == ButtonState.Pressed;
      }
    }

    /// <summary>
    ///     Returns whether XBUTTON1 was clicked (previously pressed, but now released).
    /// </summary>
    public static bool IsXButton1Clicked
    {
      get
      {
        return InputManager.Mouse.XButton1 == ButtonState.Released && InputManager.OldMouse.XButton1 == ButtonState.Pressed;
      }
    }

    /// <summary>
    ///     Returns whether XBUTTON2 was clicked (previously pressed, but now released).
    /// </summary>
    public static bool IsXButton2Clicked
    {
      get
      {
        return InputManager.Mouse.XButton2 == ButtonState.Released && InputManager.OldMouse.XButton2 == ButtonState.Pressed;
      }
    }

    /// <summary>Gets the current keyboard state.</summary>
    public static KeyboardState Keyboard { get; private set; }

    /// <summary>Gets the current mouse state.</summary>
    public static MouseState Mouse { get; private set; }

    /// <summary>Gets the mouse position in vector2 coordinates.</summary>
    /// <value>The mouse position.</value>
    public static Vector2 MousePosition
    {
      get
      {
        MouseState mouse = InputManager.Mouse;
        double x = (double) mouse.X / (double) InputManager.MouseScale.X;
        mouse = InputManager.Mouse;
        double y = (double) mouse.Y / (double) InputManager.MouseScale.Y;
        return new Vector2((float) x, (float) y);
      }
    }

    /// <summary>
    ///     Gets or sets the scale to apply to mouse cursor coordinates.
    /// </summary>
    public static Vector2 MouseScale { get; set; }

    /// <summary>Gets the previous keyboard state.</summary>
    public static KeyboardState OldKeyboard { get; private set; }

    /// <summary>Gets the previous mouse state.</summary>
    public static MouseState OldMouse { get; private set; }

    /// <summary>
    ///     Gets the position of the mouse cursor, with scaling applied.
    /// </summary>
    public static Point ScaledCursor => new Point(InputManager.ScaledX, InputManager.ScaledY);

    /// <summary>
    ///     Gets the position of the mouse cursor, with scaling applied.
    /// </summary>
    public static Vector2 ScaledCursorF
    {
      get => new Vector2((float) InputManager.ScaledX, (float) InputManager.ScaledY);
    }

    /// <summary>
    ///     Gets the horizontal position of the mouse cursor, with scaling applied.
    /// </summary>
    public static int ScaledX
    {
      get => (int) Math.Round((double) InputManager.Mouse.X * (double) InputManager.MouseScale.X);
    }

    /// <summary>
    ///     Gets the vertical position of the mouse cursor, with scaling applied.
    /// </summary>
    public static int ScaledY
    {
      get => (int) Math.Round((double) InputManager.Mouse.Y * (double) InputManager.MouseScale.Y);
    }

    /// <summary>
    ///     Gets an array of values that correspond to the keyboard keys that are currently being held down.
    /// </summary>
    /// <returns>Returns an array of values that correspond to the keyboard keys that are currently being held down.</returns>
    public static Keys[] GetHeldKeys()
    {
      lock (InputManager.KeyList)
      {
        foreach (Keys allKey in InputManager.AllKeys)
        {
          if (InputManager.IsKeyHeld(new Keys?(allKey)))
            InputManager.KeyList.Add(allKey);
        }
        return InputManager.KeyList.ToArray();
      }
    }

    /// <summary>
    /// Returns a modified string where all the pressed keys are applied to the referenced string value.
    /// </summary>
    /// <param name="value">The value.</param>
    public static void GetInput(ref string value)
    {
      InputManager.getInput(InputManager.GetPressedKeys(), ref value);
    }

    /// <summary>
    ///     Gets an array of values that correspond to the keyboard keys that have been pressed (were up, but are now down).
    /// </summary>
    /// <returns>
    ///     Returns an array of values that correspond to the keyboard keys that have been pressed (were up, but are now down).
    /// </returns>
    public static Keys[] GetPressedKeys()
    {
      lock (InputManager.KeyList)
      {
        InputManager.KeyList.Clear();
        foreach (Keys allKey in InputManager.AllKeys)
        {
          if (InputManager.IsKeyPressed(new Keys?(allKey)))
            InputManager.KeyList.Add(allKey);
        }
        return InputManager.KeyList.ToArray();
      }
    }

    /// <summary>The get pulsed keys.</summary>
    /// <returns>
    ///     The <see cref="!:Keys[]" />.
    /// </returns>
    public static Keys[] GetPulsedKeys()
    {
      lock (InputManager.KeyList)
      {
        foreach (Keys allKey in InputManager.AllKeys)
        {
          if (InputManager.IsKeyHeld(new Keys?(allKey)) && !InputManager.PulsingKeys.Contains(allKey))
            InputManager.KeyList.Add(allKey);
        }
        return InputManager.KeyList.ToArray();
      }
    }

    /// <summary>
    ///     Gets an array of values that correspond to the keyboard keys that have been released (were down, but are now up).
    /// </summary>
    /// <returns>
    ///     Returns an array of values that correspond to the keyboard keys that have been pressed (were down, but are now up).
    /// </returns>
    public static Keys[] GetReleasedKeys()
    {
      lock (InputManager.KeyList)
      {
        InputManager.KeyList.Clear();
        foreach (Keys allKey in InputManager.AllKeys)
        {
          if (InputManager.IsKeyReleased(new Keys?(allKey)))
            InputManager.KeyList.Add(allKey);
        }
        return InputManager.KeyList.ToArray();
      }
    }

    /// <summary>Returns whether a specific key is down.</summary>
    /// <param name="key">
    /// Enumerated value that specifies the key to query.
    /// </param>
    /// <returns>Returns true if the key is down; otherwise, false.</returns>
    public static bool IsKeyDown(Keys? key)
    {
      return key.HasValue && InputManager.Keyboard.IsKeyDown(key.Value);
    }

    /// <summary>Returns whether a specific key is held.</summary>
    /// <param name="key">
    /// Enumerated value that specifies the key to query.
    /// </param>
    /// <returns>
    /// Returns true if the key is held down; otherwise, false.
    /// </returns>
    public static bool IsKeyHeld(Keys? key)
    {
      return key.HasValue && InputManager.Keyboard.IsKeyDown(key.Value);
    }

    /// <summary>
    /// Returns whether a specific key was pressed (previously up, but now down).
    /// </summary>
    /// <param name="key">
    /// Enumerated value that specifies the key to query.
    /// </param>
    /// <returns>
    /// Returns true if the key was previously up, but is now down; otherwise, false.
    /// </returns>
    public static bool IsKeyPressed(Keys? key)
    {
      return key.HasValue && InputManager.Keyboard.IsKeyDown(key.Value) && InputManager.OldKeyboard.IsKeyUp(key.Value);
    }

    /// <summary>
    /// Returns whether a specific key was released (previously down, but now up).
    /// </summary>
    /// <param name="key">
    /// Enumerated value that specifies the key to query.
    /// </param>
    /// <returns>
    /// Returns true if the key was previously down, but is now up; otherwise, false.
    /// </returns>
    public static bool IsKeyReleased(Keys? key)
    {
      return key.HasValue && InputManager.Keyboard.IsKeyUp(key.Value) && InputManager.OldKeyboard.IsKeyDown(key.Value);
    }

    /// <summary>Returns whether a specific key is up.</summary>
    /// <param name="key">
    /// Enumerated value that specifies the key to query.
    /// </param>
    /// <returns>Returns true if the key is up; otherwise, false.</returns>
    public static object IsKeyUp(Keys? key)
    {
      return !key.HasValue ? (object) false : (object) InputManager.Keyboard.IsKeyUp(key.Value);
    }

    /// <summary>Updates the input state of all devices.</summary>
    /// <param name="gameTime">The game Time.</param>
    public static void Update(GameTime gameTime)
    {
      InputManager.OldKeyboard = InputManager.Keyboard;
      InputManager.OldMouse = InputManager.Mouse;
      InputManager.Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
      InputManager.Mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
      MouseState mouseState1 = InputManager.Mouse;
      int x1 = mouseState1.X;
      mouseState1 = InputManager.OldMouse;
      int x2 = mouseState1.X;
      InputManager.deltaX = x1 - x2;
      MouseState mouseState2 = InputManager.Mouse;
      int y1 = mouseState2.Y;
      mouseState2 = InputManager.OldMouse;
      int y2 = mouseState2.Y;
      InputManager.deltaY = y1 - y2;
      if (InputManager.IsLeftButtonClicked && !InputManager.leftClickCount)
      {
        InputManager.leftDoubleClickTime = gameTime.TotalGameTime;
        InputManager.leftClickCount = true;
      }
      if (InputManager.leftClickCount && gameTime.TotalGameTime - InputManager.leftDoubleClickTime > InputManager.LeftDoubleClickTimeMax)
        InputManager.leftClickCount = false;
      if (InputManager.IsRightButtonClicked && !InputManager.rightClickCount)
      {
        InputManager.rightDoubleClickTime = gameTime.TotalGameTime;
        InputManager.rightClickCount = true;
      }
      if (InputManager.rightClickCount && gameTime.TotalGameTime - InputManager.rightDoubleClickTime > InputManager.RightDoubleClickTimeMax)
        InputManager.rightClickCount = false;
      if (InputManager.IsMiddleButtonClicked && !InputManager.middleClickCount)
      {
        InputManager.middleDoubleClickTime = gameTime.TotalGameTime;
        InputManager.middleClickCount = true;
      }
      if (InputManager.middleClickCount && gameTime.TotalGameTime - InputManager.middleDoubleClickTime > InputManager.MiddleDoubleClickTimeMax)
        InputManager.middleClickCount = false;
      for (int index = 0; index < InputManager.keyPulses.Count; ++index)
        InputManager.keyPulses[index]--;
      foreach (Keys allKey in InputManager.AllKeys)
      {
        if (InputManager.Keyboard.IsKeyDown(allKey) && !InputManager.PulsingKeys.Contains(allKey))
        {
          InputManager.PulsingKeys.Add(allKey);
          InputManager.keyPulses.Add(InputManager.KeyPulses);
        }
      }
      for (int index = 0; index < InputManager.keyPulses.Count; ++index)
      {
        if (InputManager.keyPulses[index] <= 0)
        {
          InputManager.keyPulses.RemoveAt(index);
          InputManager.PulsingKeys.RemoveAt(index);
        }
      }
    }

    /// <summary>The get input.</summary>
    /// <param name="pressedKeys">The pressed keys.</param>
    /// <param name="value">The value.</param>
    internal static void getInput(Keys[] pressedKeys, ref string value)
    {
      bool flag = InputManager.IsKeyDown(new Keys?(Keys.LeftShift)) || InputManager.IsKeyDown(new Keys?(Keys.RightShift));
      foreach (Keys pressedKey in pressedKeys)
      {
        switch (pressedKey)
        {
          case Keys.Back:
            if (!string.IsNullOrEmpty(value))
            {
              value = value.Remove(value.Length - 1, 1);
              break;
            }
            break;
          case Keys.Space:
            value += " ";
            break;
          case Keys.A:
            value += flag ? "A" : "a";
            break;
          case Keys.B:
            value += flag ? "B" : "b";
            break;
          case Keys.C:
            value += flag ? "C" : "c";
            break;
          case Keys.D:
            value += flag ? "D" : "d";
            break;
          case Keys.E:
            value += flag ? "E" : "e";
            break;
          case Keys.F:
            value += flag ? "F" : "f";
            break;
          case Keys.G:
            value += flag ? "G" : "g";
            break;
          case Keys.H:
            value += flag ? "H" : "h";
            break;
          case Keys.I:
            value += flag ? "I" : "i";
            break;
          case Keys.J:
            value += flag ? "J" : "j";
            break;
          case Keys.K:
            value += flag ? "K" : "k";
            break;
          case Keys.L:
            value += flag ? "L" : "l";
            break;
          case Keys.M:
            value += flag ? "M" : "m";
            break;
          case Keys.N:
            value += flag ? "N" : "n";
            break;
          case Keys.O:
            value += flag ? "O" : "o";
            break;
          case Keys.P:
            value += flag ? "P" : "p";
            break;
          case Keys.Q:
            value += flag ? "Q" : "q";
            break;
          case Keys.R:
            value += flag ? "R" : "r";
            break;
          case Keys.S:
            value += flag ? "S" : "s";
            break;
          case Keys.T:
            value += flag ? "T" : "t";
            break;
          case Keys.U:
            value += flag ? "U" : "u";
            break;
          case Keys.V:
            value += flag ? "V" : "v";
            break;
          case Keys.W:
            value += flag ? "W" : "w";
            break;
          case Keys.X:
            value += flag ? "X" : "x";
            break;
          case Keys.Y:
            value += flag ? "Y" : "y";
            break;
          case Keys.Z:
            value += flag ? "Z" : "z";
            break;
        }
      }
    }
  }
}
