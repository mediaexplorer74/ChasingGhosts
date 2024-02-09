// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Sprites.SpriteFrame
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Loading;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Sprites
{
  public class SpriteFrame : Component
  {
    private LoadInstruction<Texture2D> instruction;

    public SpriteFrame()
    {
    }

    public SpriteFrame(Texture2D rawTexture, Rectangle? region = null)
    {
      this.Instruction = (LoadInstruction<Texture2D>) new RawTextureInstruction(rawTexture);
      this.Region = region ?? Rectangle.Empty;
    }

    public SpriteFrame(Color fillColor, int width, int height)
    {
      this.Instruction = (LoadInstruction<Texture2D>) new ColorInstruction()
      {
        Color = fillColor
      };
      this.Region = new Rectangle(0, 0, width, height);
    }

    public SpriteFrame(string assetPath, Rectangle region)
      : this(assetPath)
    {
      this.Region = region;
    }

    public SpriteFrame(string assetPath)
      : this()
    {
      this.Instruction = (LoadInstruction<Texture2D>) new TextureAssetInstruction()
      {
        Asset = assetPath
      };
    }

    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.UpdateTexture();
    }

    public LoadInstruction<Texture2D> Instruction
    {
      get => this.instruction;
      set
      {
        this.instruction = value;
        this.UpdateTexture();
      }
    }

    public Rectangle Region { get; set; }

    public Texture2D Texture { get; private set; }

    public Vector2 TransformOrigin { get; set; }

    public virtual void Draw(SharpDrawBatch batch, Color tint)
    {
      this.UpdateRegion();
      batch.Draw(this.Texture, this.Region, new Rectangle?(), tint);
    }

    public virtual void Draw(
      SharpDrawBatch batch,
      Vector2 position,
      Color tint,
      float rotation,
      Vector2 scale,
      SpriteEffects effects,
      float depth)
    {
      this.UpdateRegion();
      batch.Draw(this.Texture, position, new Rectangle?(this.Region), tint, MathHelper.ToRadians(rotation), new Vector2((float) this.Region.Width * this.TransformOrigin.X, (float) this.Region.Height * this.TransformOrigin.Y), scale, effects, depth);
    }

    private void UpdateTexture()
    {
      if (this.Resolver == null)
        return;
      this.Texture = this.Instruction?.Load(this.Resolver);
    }

    private void UpdateRegion()
    {
      if (!(this.Region == Rectangle.Empty) || this.Texture == null)
        return;
      this.Region = this.Texture.Bounds;
    }
  }
}
