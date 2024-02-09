// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Sharp2DGame
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Sharp2D.Engine.Common
{
    /// <summary>Sharp2D Game.</summary>
    public sealed class Sharp2DGame : Game, IGameHost
    {
        /// <summary>The game manager</summary>
        private SharpGameManager gameManager;
        /// <summary>The resolver</summary>
        private IResolver resolver;

        /// <summary>Gets the actual graphics.</summary>
        /// <value>The actual graphics.</value>
        public GraphicsDeviceManager ActualGraphics { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Sharp2DGame" /> class.
        /// <para>If this is directly invoked, remember to call </para>
        /// </summary>
        public Sharp2DGame()
        {
            this.ActualGraphics = new GraphicsDeviceManager((Game)this)
            {
                SupportedOrientations = DisplayOrientation.Portrait
            };
            this.Content.RootDirectory = Sharp2DApplication.ContentRoot;
            try
            {
                this.IsMouseVisible = true;
            }
            catch (NotImplementedException ex)
            {
            }
        }

        /// <summary>Sets the game manager.</summary>
        /// <param name="gameManager">The game manager.</param>
        /// <param name="resolver"></param>
        public void SetGameManagerAndResolver(SharpGameManager gameManager, IResolver resolver)
        {
            this.gameManager = gameManager;
            this.gameManager.Constructed((IGameHost)this);
            this.resolver = resolver;
        }

        /// <summary>Gets the graphics device manager.</summary>
        /// <value>The graphics.</value>
        public IGraphicsDeviceService Graphics => (IGraphicsDeviceService)this.ActualGraphics;

        /// <summary>Gets the Sprite batch.</summary>
        /// <value>The Sprite batch.</value>
        public SharpDrawBatch DrawBatch { get; private set; }

        public float ScreenWidth
        {
            get => (float)this.ActualGraphics.PreferredBackBufferWidth;
            set
            {
                this.ActualGraphics.PreferredBackBufferWidth = (double)value > 0.0 
                    ? (int)value : throw new InvalidOperationException("Width cannot be 0 or below!");
            }
        }

        public float ScreenHeight
        {
            get => (float)this.ActualGraphics.PreferredBackBufferHeight;
            set
            {
                this.ActualGraphics.PreferredBackBufferHeight = (double)value > 0.0 
                    ? (int)value : throw new InvalidOperationException("Height cannot be 0 or below!");
            }
        }

        public void ApplyChanges() => this.ActualGraphics.ApplyChanges();

        /// <summary>Initializes the game.</summary>
        protected override void Initialize()
        {
            this.resolver.Unregister<GraphicsDevice>();
            this.resolver.Register<GraphicsDevice>(this.GraphicsDevice);
            base.Initialize();
            this.gameManager.Initialize();
        }

        /// <summary>Loads the content.</summary>
        protected override void LoadContent()
        {
            this.DrawBatch = new SharpDrawBatch(this.ActualGraphics.GraphicsDevice);
            this.gameManager.LoadContent();
            base.LoadContent();
        }

        /// <summary>Updates the game.</summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            this.gameManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game objects to the screen. Calls Root.Draw.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.gameManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        /*
        [SpecialName]
        ContentManager IGameHost.Content() => this.Content;
        

        [SpecialName]
        GraphicsDevice IGameHost.GraphicsDevice() => this.GraphicsDevice;
        

        [SpecialName]
        bool IGameHost.IsActive() => this.IsActive;

        [SpecialName]
        GameServiceContainer IGameHost.Services() => this.Services;

        [SpecialName]
        GameWindow IGameHost.Window()
        {
            return this.Window;
        }
        */

        void IGameHost.Run()
    {
        this.Run();
    }
    }
}
