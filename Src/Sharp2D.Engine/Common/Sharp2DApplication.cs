// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Sharp2DApplication
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Infrastructure;

#nullable disable
namespace Sharp2D.Engine.Common
{
  /// <summary>Sharp2D Application.</summary>
  public abstract class Sharp2DApplication
  {
    /// <summary>The float tolerance used for float comparisons.</summary>
    public const double FloatTolerance = 1E-08;
    /// <summary>
    ///     Gets or sets the root directory for the content manager.
    /// </summary>
    /// <value>The root directory.</value>
    public static string ContentRoot = "Content";
    private IGameHost gameHost;

    protected IResolver Resolver { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Sharp2DApplication" /> class.
    /// </summary>
    protected Sharp2DApplication(IResolver resolver) => this.Resolver = resolver;

    /// <summary>Occurs when game has been created.</summary>
    public static event Sharp2D.Engine.Common.GameCreated GameCreated;

    /// <summary>
    ///     Creates the game. It is up to each platform to decide how to do this.
    /// </summary>
    /// <returns>
    ///     The <see cref="T:Sharp2D.Engine.Common.SharpGameManager" />.
    /// </returns>
    public abstract IGameHost CreateGame(SharpGameManager gameManager);

    /// <summary>
    /// Runs the game. It is up to each platform to decide how to do this, or if it should be done.
    /// </summary>
    /// <param name="game">The game.</param>
    public abstract void RunGame(IGameHost game);

    /// <summary>Sets up the graphics.</summary>
    public virtual void SetupGraphics()
    {
      Sharp2DApplication.GameManager.SetVirtualResolution(1280, 800);
      Sharp2DApplication.GameManager.SetWindowResolution(1280, 800);
      Sharp2DApplication.GameManager.ApplyResolutionChanges();
    }

    /// <summary>Starts the game.</summary>
    public virtual void Start()
    {
      Sharp2DApplication.GameManager = new SharpGameManager(this.Resolver);
      this.InternalRegisterServices();
      this.gameHost = this.CreateGame(Sharp2DApplication.GameManager);
      this.Resolver.Register<IGameHost>(this.gameHost);
      this.Resolver.Register<ContentManager>(this.gameHost.Content);
      this.Resolver.Unregister<GraphicsDevice>();
      this.Resolver.Register<GraphicsDevice>(this.gameHost.GraphicsDevice);
      this.OnGameCreated();
      this.SetupGraphics();
      this.RunGame(this.gameHost);
    }

    public static SharpGameManager GameManager { get; set; }

    /// <summary>Registers the services.</summary>
    protected abstract void RegisterServices();

    /// <summary>Called when game has been created.</summary>
    /// <param name="args">The arguments.</param>
    private void OnGameCreated()
    {
      Sharp2D.Engine.Common.GameCreated gameCreated = Sharp2DApplication.GameCreated;
      if (gameCreated == null)
        return;
      gameCreated((object) null, new GameCreatedArgs(Sharp2DApplication.GameManager));
    }

    /// <summary>Internal version of the Register Services.</summary>
    private void InternalRegisterServices()
    {
      this.Resolver.Register<IContentManagerFactory, ContentManagerFactory>();
      this.RegisterServices();
    }
  }
}
