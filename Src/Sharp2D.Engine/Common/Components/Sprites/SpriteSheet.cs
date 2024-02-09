// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Sprites.SpriteSheet`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Utility;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Sprites
{
  /// <summary>
  /// Sprite sheet container. Load in a Sprite sheet / Sprite atlas / tile map, and pass
  /// in the region to use when drawing. Simple as fuck.
  /// </summary>
  /// <typeparam name="TSpriteKey">The type of the Sprite key.</typeparam>
  public class SpriteSheet<TSpriteKey> : Sprite
  {
    private SpriteRegions<TSpriteKey> regions = new SpriteRegions<TSpriteKey>();

    /// <summary>Gets or sets the current region key.</summary>
    /// <value>The region key.</value>
    public virtual TSpriteKey RegionKey { get; set; }

    /// <summary>
    /// Gets the regions used for drawing a specific part of the underlying Sprite Sheet.
    /// </summary>
    /// <value>The regions.</value>
    public SpriteRegions<TSpriteKey> Regions
    {
      get => this.regions;
      set
      {
        if (this.regions != null)
        {
          foreach (KeyValuePair<TSpriteKey, SpriteFrame> region in (Dictionary<TSpriteKey, SpriteFrame>) this.regions)
            this.Children.Remove((Component) region.Value);
        }
        this.regions = value;
        foreach (KeyValuePair<TSpriteKey, SpriteFrame> region in (Dictionary<TSpriteKey, SpriteFrame>) this.regions)
          this.Children.Add((Component) region.Value);
        if (!object.Equals((object) this.RegionKey, (object) default (TSpriteKey)))
          return;
        this.RegionKey = this.Regions.FirstOrDefault<KeyValuePair<TSpriteKey, SpriteFrame>>().Key;
      }
    }

    /// <summary>
    /// Gets the width of a single Sprite. Same as in <see cref="P:Sharp2D.Engine.Common.Components.Sprites.SpriteSheet`1.SpriteSize" />.
    /// </summary>
    /// <value>The width.</value>
    public override int Width
    {
      get
      {
        return (object) this.RegionKey == null || !this.Regions.ContainsKey(this.RegionKey) ? 0 : this.Regions[this.RegionKey].Region.Width;
      }
    }

    /// <summary>
    /// Gets the height of a single Sprite. Same as in <see cref="P:Sharp2D.Engine.Common.Components.Sprites.SpriteSheet`1.SpriteSize" />.
    /// </summary>
    /// <value>The height.</value>
    public override int Height
    {
      get
      {
        return (object) this.RegionKey == null || !this.Regions.ContainsKey(this.RegionKey) ? 0 : this.Regions[this.RegionKey].Region.Height;
      }
    }

    /// <summary>Gets the size of the sprite.</summary>
    /// <value>The size of the sprite.</value>
    public Vector2 SpriteSize
    {
      get
      {
        if ((object) this.RegionKey == null || !this.Regions.ContainsKey(this.RegionKey))
          return Vector2.Zero;
        SpriteFrame region = this.Regions[this.RegionKey];
        return new Vector2((float) region.Region.Width, (float) region.Region.Height);
      }
    }

    /// <summary>
    /// Draws this Sprite to the underlying <see cref="!:batch" />
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    /// <param name="position">The position.</param>
    /// <param name="tint">The tint.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="scale">The scale.</param>
    /// <param name="effects">The effects.</param>
    /// <param name="depth">The depth.</param>
    public override void Draw(
      SharpDrawBatch batch,
      GameTime time,
      Vector2 position,
      Color tint,
      float rotation,
      Vector2 scale,
      SpriteEffects effects,
      float depth)
    {
      this.Regions[this.RegionKey].Draw(batch, position, tint, rotation, scale * this.Scale, effects, depth);
    }

    protected override void UpdateFrame()
    {
      foreach (KeyValuePair<TSpriteKey, SpriteFrame> region in (Dictionary<TSpriteKey, SpriteFrame>) this.Regions)
        region.Value.TransformOrigin = this.TransformOrigin;
    }
  }
}
