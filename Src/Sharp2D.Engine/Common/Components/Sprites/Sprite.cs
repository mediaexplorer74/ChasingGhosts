// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Sprites.Sprite
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Sprites
{
  /// <summary>
  ///     In-game Sprite. Wraps a <see cref="T:Microsoft.Xna.Framework.Graphics.Texture2D" /> under the hood.
  /// </summary>
  public class Sprite : Component
  {
    private Vector2 transformOrigin;

    /// <summary>Gets the Frame.</summary>
    /// <value>The Frame.</value>
    protected SpriteFrame Frame
    {
      get => this.Children.OfType<SpriteFrame>().FirstOrDefault<SpriteFrame>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" /> class.
    /// </summary>
    /// <param name="assetName">Name of the asset.</param>
    /// <param name="region">The region.</param>
    /// <param name="scale">The scale.</param>
    public Sprite(string assetName, Rectangle? region = null, Vector2? scale = null)
      : this()
    {
      this.Scale = scale ?? Vector2.One;
      this.Children.Add(!region.HasValue ? (Component) new SpriteFrame(assetName) : (Component) new SpriteFrame(assetName, region.Value));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" /> class.
    /// </summary>
    public Sprite()
    {
      this.Tint = Color.White;
      this.SpriteEffect = SpriteEffects.None;
      this.Scale = Vector2.One;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" /> class.
    /// </summary>
    /// <param name="fillColor">Color of the fill.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public Sprite(Color fillColor, int width, int height)
      : this()
    {
      this.Children.Add((Component) new SpriteFrame(fillColor, width, height));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" /> class.
    /// </summary>
    /// <param name="rawTexture">A raw 2D texture.</param>
    public Sprite(Texture2D rawTexture)
      : this()
    {
      this.Children.Add((Component) new SpriteFrame(rawTexture));
    }

    /// <summary>Gets or sets the scale.</summary>
    /// <value>The scale.</value>
    public Vector2 Scale { get; set; }

    /// <summary>
    ///     Gets the height of this Sprite.
    /// <para>Returns 0 if Texture is null</para>
    /// </summary>
    /// <value>The height.</value>
    public virtual int Height
    {
      get
      {
        SpriteFrame frame = this.Frame;
        return frame == null ? 0 : frame.Region.Height;
      }
    }

    /// <summary>
    ///     Gets or sets the Sprite effect.
    ///     <para>Used to draw the Sprite in horizontally/vertically flipped</para>
    ///     <para>Default value is <see cref="F:Microsoft.Xna.Framework.Graphics.SpriteEffects.None" /></para>
    /// </summary>
    /// <value>The Sprite effect.</value>
    public SpriteEffects SpriteEffect { get; set; }

    /// <summary>Gets or sets the tint.</summary>
    /// <value>The tint.</value>
    public Color Tint { get; set; }

    /// <summary>
    ///     Gets the width of this Sprite.
    /// <para>Returns 0 if Texture is null</para>
    /// </summary>
    /// <value>The width.</value>
    public virtual int Width
    {
      get
      {
        SpriteFrame frame = this.Frame;
        return frame == null ? 0 : frame.Region.Width;
      }
    }

    /// <summary>Gets or sets the opacity.</summary>
    /// <value>The opacity.</value>
    public virtual float Opacity { get; set; } = 1f;

    /// <summary>Gets the transform origin.</summary>
    /// <value>The transform origin.</value>
    public virtual Vector2 TransformOrigin
    {
      get => this.transformOrigin;
      set
      {
        this.transformOrigin = value;
        this.UpdateFrame();
      }
    }

    /// <summary>
    /// Loads a new instance of <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" /> with the given <see cref="!:assetName" />.
    /// Same as new'ing one up yourself.
    /// </summary>
    /// <param name="assetName">Name of the asset.</param>
    /// <param name="region">The region.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.Components.Sprites.Sprite" />.
    /// </returns>
    public static Sprite Load(string assetName, Rectangle? region = null, Vector2? scale = null)
    {
      return new Sprite(assetName, region, scale);
    }

    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.UpdateFrame();
    }

    /// <summary>
    /// Centers the object, setting its TransformOrigin to 50% of its Sprite.
    /// </summary>
    public void CenterObject() => this.TransformOrigin = new Vector2(0.5f, 0.5f);

    /// <summary>Draws this object to the screen.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    /// <exception cref="T:System.NotSupportedException">Use the overloads.</exception>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (!this.IsVisible)
        return;
      this.Draw(batch, time, this.Parent.GlobalPosition, this.Tint, this.Parent.GlobalRotation, this.Parent.GlobalScale);
    }

    /// <summary>
    /// Draws this Sprite to the passed <see cref="!:batch" />.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    /// <param name="position">The position.</param>
    /// <param name="tint">The tint.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="origin">The origin.</param>
    /// <param name="scale">The scale.</param>
    /// <param name="effects">The effects.</param>
    /// <param name="depth">The depth.</param>
    public virtual void Draw(
      SharpDrawBatch batch,
      GameTime time,
      Vector2 position,
      Color tint,
      float rotation,
      Vector2 scale,
      SpriteEffects effects,
      float depth)
    {
      if (!this.IsVisible)
        return;
      this.Frame.Draw(batch, position, tint * this.Opacity, rotation, scale * this.Scale, effects, depth);
    }

    /// <summary>
    /// Draws this Sprite to the passed <see cref="!:batch" />.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    /// <param name="position">The position.</param>
    /// <param name="tint">The tint.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="scale">The scale.</param>
    public void Draw(
      SharpDrawBatch batch,
      GameTime time,
      Vector2 position,
      Color tint,
      float rotation,
      Vector2 scale)
    {
      if (!this.IsVisible)
        return;
      this.Draw(batch, time, position, tint, rotation, scale, this.SpriteEffect, 1f);
    }

    /// <summary>
    /// Draws this Sprite to the passed <see cref="!:batch" />.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    /// <param name="position">The position.</param>
    /// <param name="tint">The tint.</param>
    public virtual void Draw(SharpDrawBatch batch, GameTime time, Vector2 position, Color tint)
    {
      if (!this.IsVisible)
        return;
      this.Draw(batch, time, position, tint, 0.0f, Vector2.One, this.SpriteEffect, 1f);
    }

    /// <summary>
    /// Called every Frame. This is where you want to handle your logic.
    /// <para>Does nothing for this instance.</para>
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
    }

    protected virtual void UpdateFrame()
    {
      SpriteFrame frame = this.Frame;
      if (frame == null)
        return;
      frame.TransformOrigin = this.transformOrigin;
    }
  }
}
