// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.IGameContract
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Drawing;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  /// <summary>
  ///     A common interface for XNA classes that nearly all classes inherit.
  /// </summary>
  public interface IGameContract
  {
    /// <summary>
    /// Gets or sets a value indicating whether this object is generated via the editor.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is generated; otherwise, <c>false</c>.
    /// </value>
    bool IsGenerated { get; set; }

    /// <summary>
    ///     Does the exact opposite of <see cref="P:Sharp2D.Engine.Infrastructure.IGameContract.IsVisible" />. Provided as a shortcut for readability.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [hidden]; otherwise, <c>false</c>.
    /// </value>
    bool IsHidden { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this object should receive updates.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [paused]; otherwise, <c>false</c>.
    /// </value>
    bool IsPaused { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this object is visible in the game.
    /// </summary>
    /// <value>
    ///     <c>true</c> if visible; otherwise, <c>false</c>.
    /// </value>
    bool IsVisible { get; set; }

    /// <summary>Draws this object to the screen.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    void Draw(SharpDrawBatch batch, GameTime time);

    /// <summary>
    ///     Helper method for hiding this object. Same as setting <see cref="P:Sharp2D.Engine.Infrastructure.GameContract.IsVisible" /> to <c>false</c>.
    /// </summary>
    void Hide();

    /// <inheritdoc />
    void Initialize(IResolver resolver);

    /// <summary>
    ///     Helper method for showing this object. Same as setting <see cref="P:Sharp2D.Engine.Infrastructure.GameContract.IsVisible" /> to <c>true</c>.
    /// </summary>
    void Show();

    /// <summary>
    /// Called every Frame. This is where you want to handle your logic.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    void Update(GameTime gameTime);

    /// <summary>
    /// Runs the specified operation on the next Update cycle.
    /// </summary>
    /// <param name="operation">The operation.</param>
    void ScheduleRun(Action operation);

    /// <summary>
    /// Runs the specified asynchronous operation on the next Update cycle.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operation">The operation.</param>
    /// <returns></returns>
    Task<T> ScheduleRunAsync<T>(Func<Task<T>> operation);

    /// <summary>Waits for the next update cycle.</summary>
    /// <returns></returns>
    Task WaitForUpdate();
  }
}
