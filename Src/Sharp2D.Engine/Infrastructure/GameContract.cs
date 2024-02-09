// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.GameContract
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  /// <summary>
  ///     Common GameContract. Everything game-related should implement this.
  /// </summary>
  public abstract class GameContract : IGameContract
  {
    /// <summary>Gets the runables.</summary>
    /// <value>The runables.</value>
    private readonly List<Action> runables = new List<Action>();

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Infrastructure.GameContract" /> class.
    ///     Sets up default values.
    /// </summary>
    protected GameContract()
    {
      this.IsVisible = true;
      this.IsPaused = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this object is generated via the editor.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is generated; otherwise, <c>false</c>.
    /// </value>
    public bool IsGenerated { get; set; }

    /// <summary>
    ///     Does the exact opposite of <see cref="P:Sharp2D.Engine.Infrastructure.GameContract.IsVisible" />. Provided as a shortcut for readability.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [hidden]; otherwise, <c>false</c>.
    /// </value>
    public bool IsHidden
    {
      get => !this.IsVisible;
      set => this.IsVisible = !value;
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this object should receive updates.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [paused]; otherwise, <c>false</c>.
    /// </value>
    public bool IsPaused { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this object is visible in the game.
    /// </summary>
    /// <value>
    ///     <c>true</c> if visible; otherwise, <c>false</c>.
    /// </value>
    public bool IsVisible { get; set; }

    /// <summary>Draws this object to the screen.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public abstract void Draw(SharpDrawBatch batch, GameTime time);

    /// <summary>
    ///     Helper method for hiding this object. Same as setting <see cref="P:Sharp2D.Engine.Infrastructure.GameContract.IsVisible" /> to <c>false</c>.
    /// </summary>
    public virtual void Hide() => this.IsVisible = false;

    /// <inheritdoc />
    public abstract void Initialize(IResolver resolver);

    /// <summary>
    ///     Helper method for showing this object. Same as setting <see cref="P:Sharp2D.Engine.Infrastructure.GameContract.IsVisible" /> to <c>true</c>.
    /// </summary>
    public virtual void Show() => this.IsVisible = true;

    /// <summary>
    /// Called every Frame. This is where you want to handle your logic.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public abstract void Update(GameTime gameTime);

    /// <summary>
    /// Runs the specified operation on the next Update cycle.
    /// </summary>
    /// <param name="operation">The operation.</param>
    public void ScheduleRun(Action operation) => this.runables.Add(operation);

    /// <summary>
    /// Runs the specified asynchronous operation on the next Update cycle.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operation">The operation.</param>
    /// <returns></returns>
    public async Task<T> ScheduleRunAsync<T>(Func<Task<T>> operation)
    {
      TaskCompletionSource<T> source = new TaskCompletionSource<T>();
      T obj = await operation();
      T result = obj;
      obj = default (T);
      this.runables.Add((Action) (() => source.SetResult(result)));
      T task = await source.Task;
      return task;
    }

    public async Task WaitForUpdate()
    {
      TaskCompletionSource<object> source = new TaskCompletionSource<object>();
      this.runables.Add((Action) (() => source.SetResult((object) null)));
      object task = await source.Task;
    }

    /// <summary>Invokes the runables.</summary>
    protected virtual void InvokeRunables()
    {
      if (this.runables.Count == 0)
        return;
      Action[] r = this.runables.ToArray();
      foreach (Action action in r)
        action();
      this.runables.RemoveAll((Predicate<Action>) (a => ((IEnumerable<Action>) r).Contains<Action>(a)));
    }
  }
}
