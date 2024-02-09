// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.SharpGameManager
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Engine.Utility;
using System;

#nullable disable
namespace Sharp2D.Engine.Common
{
  public sealed class SharpGameManager
  {
    private IGameHost gameHost;
    private bool lastActive = true;
    private IResolver resolver;

    public SharpGameManager(IResolver resolver)
    {
      this.resolver = resolver;
      resolver.Register<SharpGameManager>(this);
    }

    /// <summary>Occurs when content has loaded.</summary>
    public static event EventHandler ContentLoaded;

    /// <summary>Occurs when game has been initialized.</summary>
    public event EventHandler GameInitialized;

    /// <summary>Gets or sets the color of the background.</summary>
    /// <value>The color of the background.</value>
    public Color BackgroundColor { get; set; } = Color.CornflowerBlue;

    /// <summary>Gets the graphics device manager.</summary>
    /// <value>The graphics.</value>
    public IGraphicsDeviceService Graphics => this.gameHost?.Graphics;

    /// <summary>
    ///     Gets or sets a value indicating whether the game is paused.
    /// </summary>
    /// <value>
    ///     <c>true</c> if [is paused]; otherwise, <c>false</c>.
    /// </value>
    public bool IsPaused { get; set; }

    /// <summary>
    /// Gets a value indicating whether the game window is active (in foreground).
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive => this.gameHost.IsActive;

    /// <summary>Gets the Sprite batch.</summary>
    /// <value>The Sprite batch.</value>
    public SharpDrawBatch SpriteBatch => this.gameHost?.DrawBatch;

    /// <summary>Gets the virtual resolution.</summary>
    /// <value>The virtual resolution.</value>
    public Vector2 VirtualResolution
    {
      get => Resolution.VirtualScreen;
      set => this.SetVirtualResolution((int) value.X, (int) value.Y);
    }

    public IServiceProvider Service => (IServiceProvider) this.gameHost.Services;

    /// <summary>Gets the window resolution.</summary>
    /// <value>The window resolution.</value>
    public Vector2 WindowResolution
    {
      get => new Vector2(this.gameHost.ScreenWidth, this.gameHost.ScreenHeight);
      set => this.SetWindowResolution((int) value.X, (int) value.Y);
    }

    public Sharp2D.Engine.Common.Scene.Scene StartScene { get; set; }

    public GameWindow Window => this.gameHost.Window; //?

    public ContentManager Content
    {
      get => this.gameHost.Content; //?
      set => throw new NotImplementedException();
    }

    public event EventHandler<bool> FocusChanged;

    /// <summary>Applies any new resolution changes.</summary>
    public void ApplyResolutionChanges()
    {
      Resolution.Update((int) this.gameHost.ScreenWidth, (int) this.gameHost.ScreenHeight);
      this.gameHost.ApplyChanges();
      InputManager.MouseScale = Resolution.DetermineDrawScaling();
      if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene == null || Sharp2D.Engine.Common.Scene.Scene.CurrentScene.MainCamera == null)
        return;
      Sharp2D.Engine.Common.Scene.Scene.CurrentScene.MainCamera.SetCameraSettings();
    }

    /// <summary>
    /// Sets the virtual resolution.
    ///     Call <see cref="M:Sharp2D.Engine.Common.SharpGameManager.ApplyResolutionChanges" /> once you are done configuring.
    ///     <para>
    /// The Virtual Resolution is the resolution which you base all coordinates after.
    ///         If you're planning on using large graphics, you may want to set this to Full HD,
    ///         so that you do not loose a single pixel in the scaling act.
    ///         However if you're making an 8-bit game, and you don't care about scaling because of the
    ///         low quality of the graphics, you may want to set this to low, so sprites doesn't have
    ///         to be drawn large, to fit.
    ///     </para>
    /// <remarks>
    /// Define this at an early stage of development, seeing as all UI controls will be based on this resolution!
    ///     </remarks>
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void SetVirtualResolution(int width, int height)
    {
      Resolution.VirtualScreen = new Vector2((float) width, (float) height);
    }

    /// <summary>
    /// Sets the resolution of the game window. This scales everything.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void SetWindowResolution(int width, int height)
    {
      this.gameHost.ScreenWidth = (float) width;
      this.gameHost.ScreenHeight = (float) height;
    }

    /// <summary>
    /// Draws the game objects to the screen. Calls Root.Draw.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public void Draw(GameTime gameTime)
    {
      this.Graphics.GraphicsDevice.Clear(this.BackgroundColor);
      GameTime time = this.IsPaused ? new GameTime(TimeSpan.Zero, TimeSpan.Zero) : gameTime;
      if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene == null)
        return;
      Sharp2D.Engine.Common.Scene.Scene.CurrentScene.Draw(this.SpriteBatch, time);
    }

    /// <summary>Initializes the game.</summary>
    public void Initialize() => this.OnGameInitialized();

    /// <summary>Loads the content.</summary>
    public async void LoadContent()
    {
      this.OnLoadContent();
      if (this.StartScene == null)
        throw new ArgumentNullException("StartScene");
      await Sharp2D.Engine.Common.Scene.Scene.Load(this.StartScene);
    }

    /// <summary>Updates the game.</summary>
    /// <param name="gameTime">The game time.</param>
    public void Update(GameTime gameTime)
    {
      if (this.lastActive != this.IsActive)
      {
        EventHandler<bool> focusChanged = this.FocusChanged;
        if (focusChanged != null)
          focusChanged((object) this, this.IsActive);
        this.lastActive = this.IsActive;
      }
      if (this.IsPaused)
        return;
      if (this.IsActive)
      {
        InputManager.Update(gameTime);
        GamePadManager.Update(gameTime);
        this.resolver.Resolve<IPointerDevice>().Update(gameTime);
      }
      if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene != null)
        Sharp2D.Engine.Common.Scene.Scene.CurrentScene.Update(gameTime);
    }

    /// <summary>Called when [game initialized].</summary>
    private void OnGameInitialized()
    {
      EventHandler gameInitialized = this.GameInitialized;
      if (gameInitialized == null)
        return;
      gameInitialized((object) this, EventArgs.Empty);
    }

    /// <summary>Called when content has been loaded.</summary>
    private void OnLoadContent()
    {
      EventHandler contentLoaded = SharpGameManager.ContentLoaded;
      if (contentLoaded == null)
        return;
      contentLoaded((object) this, EventArgs.Empty);
    }

    public void Constructed(IGameHost gameHost) => this.gameHost = gameHost;
  }
}
