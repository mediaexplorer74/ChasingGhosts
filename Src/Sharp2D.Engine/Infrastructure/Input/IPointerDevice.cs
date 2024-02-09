// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.IPointerDevice
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  /// <summary>
  ///     A pointer device used for abstracting the Mouse class out of the scope, while still using its features in code.
  ///     Used for example for pointing somewhere on the game window.
  /// </summary>
  public interface IPointerDevice
  {
    /// <summary>
    ///     Gets the current delta scroll of the pointer device.
    /// <para>This is likely only applicable on Computer devices.</para>
    /// </summary>
    /// <value>The current delta scroll.</value>
    int CurrentDeltaScroll { get; }

    /// <summary>Gets the current position of the pointer device.</summary>
    /// <value>The current position.</value>
    Vector2? CurrentPosition { get; }

    /// <summary>
    ///     Gets a value indicating whether is trigger key down.
    /// </summary>
    bool IsTriggerKeyDown { get; }

    /// <summary>
    ///     Gets a value indicating whether is trigger key released.
    /// </summary>
    bool IsTriggerKeyReleased { get; }

    /// <summary>
    ///     Gets a value indicating whether is trigger key pressed.
    /// </summary>
    bool IsTriggerKeyPressed { get; }

    /// <summary>
    ///     Gets the previous delta scroll of the pointer device.
    /// </summary>
    /// <value>The previous delta scroll.</value>
    int PreviousDeltaScroll { get; }

    /// <summary>Gets the previous position of the pointer device.</summary>
    /// <value>The previous position.</value>
    Vector2? PreviousPosition { get; }

    /// <summary>
    /// Updates the pointer device, allowing for changed states to take effect.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    void Update(GameTime gameTime);
  }
}
