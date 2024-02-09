// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Scene.Scene
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Menus;
using Sharp2D.Engine.Common.World;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Sharp2D.Engine.Common.Scene
{
  /// <summary>Scene is the root of everything. Including evil.</summary>
  public class Scene : GameContract
  {
    private InteractionManager interactionManager;
    private IResolver resolver;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Scene.Scene" /> class.
    /// </summary>
    public Scene(IResolver resolver)
    {
      this.resolver = resolver;
      this.Name = this.GetType().Name;
      this.SceneData = new SceneData();
    }

    /// <summary>
    ///     Occurs when any game object in the scene has been moved.
    /// </summary>
    public static event GameObjectMoved ObjectMoved;

    /// <summary>
    ///     Occurs when any component in the scene has been moved.
    /// </summary>
    public static event Sharp2D.Engine.Common.ObjectSystem.ComponentMoved ComponentMoved;

    /// <summary>Occurs when a scene is initialized.</summary>
    public static event EventHandler SceneInitialized;

    /// <summary>
    ///     Gets or sets the current scene being rendered and updated.
    ///     If the scene is not ready (<see cref="P:Sharp2D.Engine.Common.Scene.Scene.IsReady" />), draw/update it's <see cref="P:Sharp2D.Engine.Common.Scene.Scene.LoadingScene" /> instead.
    /// </summary>
    /// <value>The current.</value>
    public static Sharp2D.Engine.Common.Scene.Scene CurrentScene { get; set; }

    /// <summary>Gets or sets the name of this scene.</summary>
    /// <value>The name of this scene.</value>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether fixed scale.
    /// </summary>
    public bool FixedScale { get; set; }

    /// <summary>
    /// Gets a value indicating whether this scene has a loading scene.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [has loading scene]; otherwise, <c>false</c>.
    /// </value>
    public bool HasLoadingScene => this.LoadingScene != null;

    /// <summary>
    ///     Gets or sets a value indicating whether this scene has loaded.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [is ready]; otherwise, <c>false</c>.
    /// </value>
    public bool IsReady { get; set; }

    /// <summary>
    ///     Gets the loading scene. If null, it means there is no loading scene.
    /// </summary>
    /// <value>The loading scene.</value>
    public Sharp2D.Engine.Common.Scene.Scene LoadingScene { get; set; }

    /// <summary>Gets or sets the camera that controls the world.</summary>
    /// <value>The camera.</value>
    public Sharp2D.Engine.Common.World.Camera.Camera MainCamera { get; set; }

    /// <summary>Gets or sets the scene data.</summary>
    /// <value>The scene data.</value>
    public SceneData SceneData { get; set; }

    /// <summary>Gets the UI root.</summary>
    /// <value>The UI root.</value>
    public UiManager UiRoot => this.SceneData?.UiRoot;

    /// <summary>Gets the world root.</summary>
    /// <value>The world root.</value>
    public WorldManager WorldRoot => this.SceneData?.WorldRoot;

    /// <summary>Cleans up the static event handlers.</summary>
    public static void CleanupEvents() => Sharp2D.Engine.Common.Scene.Scene.ObjectMoved = (GameObjectMoved) null;

    /// <summary>Loads the scene from the specified scene file path.</summary>
    /// <param name="scene">The scene.</param>
    /// <param name="loadingScene">The loading scene.</param>
    public static async Task Load(Sharp2D.Engine.Common.Scene.Scene scene, Sharp2D.Engine.Common.Scene.Scene loadingScene = null)
    {
      if (scene == Sharp2D.Engine.Common.Scene.Scene.CurrentScene)
        return;
      Sharp2D.Engine.Common.Scene.Scene.UnloadCurrent();
      Sharp2D.Engine.Common.Scene.Scene.CurrentScene = scene;
      Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene = loadingScene ?? Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene;
      if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene.HasLoadingScene)
      {
        if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene != null)
        {
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene.SceneData.UiRoot.DoInitialize(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene.resolver);
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene.SceneData.WorldRoot.DoInitialize(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.LoadingScene.resolver);
        }
        Task task = await Task.Factory.StartNew<Task>((Func<Task>) (async () =>
        {
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.Initialize(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.resolver);
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.SceneData.UiRoot.DoInitialize(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.resolver);
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.SceneData.WorldRoot.DoInitialize(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.resolver);
          await Sharp2D.Engine.Common.Scene.Scene.CurrentScene.InitializeAsync();
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.IsReady = true;
        }), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
      }
      else
      {
        Sharp2D.Engine.Common.Scene.Scene.CurrentScene.Initialize(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.resolver);
        await Sharp2D.Engine.Common.Scene.Scene.CurrentScene.InitializeAsync();
      }
    }

    protected virtual Task InitializeAsync() => (Task) Task.FromResult<bool>(true);

    /// <summary>
    ///     Unloads the current scene. !VERY IMPORTANT ON PHONES!.
    /// </summary>
    public static void UnloadCurrent() => Sharp2D.Engine.Common.Scene.Scene.CurrentScene?.Unload();

    /// <summary>Initializes this object.</summary>
    /// <param name="resolver"></param>
    /// <inheritdoc />
    public override void Initialize(IResolver resolver)
    {
      this.resolver = resolver;
      this.interactionManager = new InteractionManager(resolver.Resolve<IInteractionProvider>());
      this.WorldRoot.DoInitialize(resolver);
      this.UiRoot.DoInitialize(resolver);
      EventHandler sceneInitialized = Sharp2D.Engine.Common.Scene.Scene.SceneInitialized;
      if (sceneInitialized == null)
        return;
      sceneInitialized((object) this, EventArgs.Empty);
    }

    /// <summary>The unload.</summary>
    public virtual void Unload()
    {
      if (this.WorldRoot != null)
      {
        for (int index = this.WorldRoot.Count - 1; index >= 0; --index)
          this.WorldRoot.RemoveAt(index);
        this.WorldRoot.Clear();
      }
      if (this.UiRoot != null)
      {
        for (int index = this.UiRoot.Count - 1; index >= 0; --index)
          this.UiRoot.RemoveAt(index);
        this.UiRoot.Clear();
      }
      Sharp2D.Engine.Common.Scene.Scene.CurrentScene = (Sharp2D.Engine.Common.Scene.Scene) null;
      this.MainCamera = (Sharp2D.Engine.Common.World.Camera.Camera) null;
      GC.Collect();
    }

    /// <summary>
    /// Called every Frame. Updates the loading scene, if this scene is currently being loaded.
    ///     If not, updates this scene.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
      this.InvokeRunables();
      if (!this.IsReady && this.LoadingScene != null)
      {
        this.LoadingScene.Update(gameTime);
      }
      else
      {
        this.interactionManager?.Update(gameTime);
        this.WorldRoot.DoUpdate(gameTime);
        this.UiRoot.DoUpdate(gameTime);
      }
    }

    /// <summary>
    /// Called every Frame. Draws the loading scene, if this scene is currently being loaded.
    ///     If not, draws this scene.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (!this.IsReady && this.LoadingScene != null)
      {
        this.LoadingScene.Draw(batch, time);
      }
      else
      {
        if (this.MainCamera != null)
        {
          this.MainCamera.Update(time);
          Matrix view = this.MainCamera.View;
          if (this.FixedScale)
            view *= Resolution.Scale;
          batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: new Matrix?(view));
          this.SceneData.WorldRoot.DoDraw(batch, time);
          batch.End();
        }
        batch.Begin(transformMatrix: new Matrix?(Resolution.Scale));
        this.SceneData.UiRoot.DoDraw(batch, time);
        batch.End();
      }
    }

    /// <summary>
    /// Called when an object has been moved around the scene hierachy.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    internal static void OnObjectMoved(GameObject sender, ChildObjectMovedArgs args)
    {
      GameObjectMoved objectMoved = Sharp2D.Engine.Common.Scene.Scene.ObjectMoved;
      if (objectMoved == null)
        return;
      objectMoved(sender, args);
    }

    /// <summary>
    /// Called when an object has been moved around the scene hierachy.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    internal static void OnComponentMoved(GameObject sender, ChildComponentMovedArgs args)
    {
      Sharp2D.Engine.Common.ObjectSystem.ComponentMoved componentMoved = Sharp2D.Engine.Common.Scene.Scene.ComponentMoved;
      if (componentMoved == null)
        return;
      componentMoved(sender, args);
    }
  }
}
