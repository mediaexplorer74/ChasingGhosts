// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.IGameHost
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing;

#nullable disable
namespace Sharp2D.Engine.Common
{
  public interface IGameHost
  {
    ContentManager Content { get; }

    GraphicsDevice GraphicsDevice { get; }

    bool IsActive { get; }

    GameServiceContainer Services { get; }

    GameWindow Window { get; }

    IGraphicsDeviceService Graphics { get; }

    SharpDrawBatch DrawBatch { get; }

    float ScreenWidth { get; set; }

    float ScreenHeight { get; set; }

    void Run();

    void ApplyChanges();

        /*
        ContentManager Content();
        GraphicsDevice GraphicsDevice();
        bool IsActive();
        GameWindow Window();
        GameServiceContainer Services();
        */
    }
}
