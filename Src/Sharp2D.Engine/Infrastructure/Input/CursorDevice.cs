// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.CursorDevice
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sharp2D.Engine.Utility;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  /// <summary>
  ///     The Mouse cursor implementation used for Computer environments.
  /// </summary>
  public class CursorDevice : IPointerDevice
  {
    /// <summary>
    /// Gets the current delta scroll of the pointer device.
    /// <para>This will only have a different value for one Frame (Update call)</para>
    /// </summary>
    /// <value>The current delta scroll.</value>
    public int CurrentDeltaScroll { get; private set; }

    /// <summary>Gets the current position of the pointer device.</summary>
    /// <value>The current position.</value>
    public Vector2? CurrentPosition { get; private set; }

    /// <summary>Gets a value indicating whether is trigger key down.</summary>
    public bool IsTriggerKeyDown { get; private set; }

    /// <summary>
    /// Gets a value indicating whether is trigger key triggered.
    /// </summary>
    public bool IsTriggerKeyReleased { get; private set; }

    /// <summary>
    /// Gets a value indicating whether is trigger key pressed.
    /// </summary>
    public bool IsTriggerKeyPressed { get; private set; }

    /// <summary>
    ///     Gets the previous delta scroll of the pointer device.
    /// </summary>
    /// <value>The previous delta scroll.</value>
    public int PreviousDeltaScroll { get; private set; }

    /// <summary>Gets the previous position of the pointer device.</summary>
    /// <value>The previous position.</value>
    public Vector2? PreviousPosition { get; private set; }

    /// <summary>
    /// Updates the pointer device, allowing for changed states to take effect.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public void Update(GameTime gameTime)
    {
      this.PreviousPosition = this.CurrentPosition;
      this.PreviousDeltaScroll = this.CurrentDeltaScroll;
      MouseState mouse = InputManager.Mouse;
      double x = (double) mouse.X;
      mouse = InputManager.Mouse;
      double y = (double) mouse.Y;
      this.CurrentPosition = new Vector2?(new Vector2((float) x, (float) y));
      this.CurrentDeltaScroll = InputManager.DeltaScroll;
      this.IsTriggerKeyReleased = InputManager.IsLeftButtonClicked;
      this.IsTriggerKeyDown = InputManager.IsLeftButtonDown;
      this.IsTriggerKeyPressed = InputManager.IsLeftButtonPressed;
    }
  }
}
