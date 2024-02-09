// ChasingGhosts.Windows.MainApp

using ChasingGhosts.Windows.Interfaces;
using ChasingGhosts.Windows.Scenes;
using ChasingGhosts.Windows.Services;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Windows;
using System;

#nullable disable
namespace ChasingGhosts.Windows
{
  public class MainApp : Sharp2DWindowsApp
  {
    public MainApp(IResolver resolver)
      : base(resolver)
    {
    }

    protected override void RegisterServices()
    {
      base.RegisterServices();
      this.Resolver.Register<IMusicManager, MusicManager>();
    }

    public override IGameHost CreateGame(SharpGameManager gameManager)
    {
      IGameHost game = base.CreateGame(gameManager);
      SharpGameManager.ContentLoaded += (EventHandler) ((sender, args) 
                => gameManager.StartScene = (Sharp2D.Engine.Common.Scene.Scene) 
                new PrologueScene(this.Resolver));
      return game;
    }

    public override void SetupGraphics()
    {
      Sharp2DApplication.GameManager.SetVirtualResolution(/*1280*/800, /*800*/480);
      Sharp2DApplication.GameManager.SetWindowResolution(/*1280*/800, /*800*/480);
      Sharp2DApplication.GameManager.ApplyResolutionChanges();
    }
  }
}
