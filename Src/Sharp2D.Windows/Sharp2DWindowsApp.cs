// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.Sharp2DWindowsApp
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Windows.Services;
using System;
using System.Windows.Forms;

#nullable disable
namespace Sharp2D.Windows
{
  /// <summary>Windows implementation of Sharp2D application.</summary>
  public class Sharp2DWindowsApp : Sharp2DApplication
  {
    /// <summary>Gets or sets the window handle.</summary>
    /// <value>The window handle.</value>
    public IntPtr? WindowHandle { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Windows.Sharp2DWindowsApp" /> class.
    /// </summary>
    /// <param name="resolver"></param>
    public Sharp2DWindowsApp(IResolver resolver)
      : base(resolver)
    {
    }

    /// <summary>Creates the game.</summary>
    /// <returns></returns>
    public override IGameHost CreateGame(SharpGameManager gameManager)
    {
      Sharp2DGame sharp2DGame = new Sharp2DGame();
      sharp2DGame.SetGameManagerAndResolver(gameManager, this.Resolver);
      sharp2DGame.ActualGraphics.PreparingDeviceSettings += (EventHandler<PreparingDeviceSettingsEventArgs>) ((sender, args) =>
      {
        Form form = (Form) Control.FromHandle(sharp2DGame.Window.Handle);
        if (this.WindowHandle.HasValue)
        {
          form.Visible = false;
          form.WindowState = FormWindowState.Minimized;
          args.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = this.WindowHandle.Value;
        }
        else
        {
          float width = (float) args.GraphicsDeviceInformation.Adapter.CurrentDisplayMode.Width;
          float height = (float) args.GraphicsDeviceInformation.Adapter.CurrentDisplayMode.Height;
          float screenWidth = sharp2DGame.ScreenWidth;
          float screenHeight = sharp2DGame.ScreenHeight;
          form.SetDesktopLocation((int) ((double) width / 2.0 - (double) screenWidth / 2.0), (int) ((double) height / 2.0 - (double) screenHeight / 2.0));
        }
      });
      return (IGameHost) sharp2DGame;
    }

    /// <summary>Runs the game.</summary>
    /// <param name="game">The game.</param>
    public override void RunGame(IGameHost game) => game.Run();

    /// <summary>Registers Windows-specific services.</summary>
    protected override void RegisterServices()
    {
      this.Resolver.Register<IInteractionProvider, CursorInteractionProvider>();
      this.Resolver.Register<IUiInteractionProvider, PcUiInteractionProvider>();
      this.Resolver.Register<IPointerDevice, CursorDevice>();
      this.Resolver.Register<IFileService, NetCoreFileService>();
      this.Resolver.Register<IKeyboardManagerFactory, WindowsKeyboardManagerFactory>();
    }
  }
}
