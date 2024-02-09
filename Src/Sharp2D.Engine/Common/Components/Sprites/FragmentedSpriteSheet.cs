// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Sprites.FragmentedSpriteSheet
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Sprites
{
  public class FragmentedSpriteSheet : SpriteSheet<int>
  {
    private readonly SpriteSheetFragment fragment;
    private readonly string assetName;

    public FragmentedSpriteSheet(string assetName, SpriteSheetFragment fragment)
    {
      this.fragment = fragment;
      this.Groups = new Dictionary<string, List<int>>();
      this.assetName = assetName;
    }

    public Dictionary<string, List<int>> Groups { get; }

    public override void Initialize(IResolver resolver)
    {
      this.GenerateFrames();
      base.Initialize(resolver);
      foreach (KeyValuePair<int, SpriteFrame> region in (Dictionary<int, SpriteFrame>) this.Regions)
        region.Value.Initialize(resolver);
    }

    private void GenerateFrames()
    {
      int key = 0;
      SpriteRegions<int> spriteRegions = new SpriteRegions<int>();
      foreach (SpriteSheetFragmentGroup group1 in this.fragment.Groups)
      {
        SpriteSheetFragmentGroup group = group1;
        IEnumerable<SpriteFrame> spriteFrames = group.Frames.Select<Rectangle, SpriteFrame>((Func<Rectangle, SpriteFrame>) (rectangle => new SpriteFrame(this.assetName, rectangle)
        {
          TransformOrigin = group.TransformOrigin ?? new Vector2(0.5f, 0.5f)
        }));
        List<int> intList = new List<int>();
        foreach (SpriteFrame spriteFrame in spriteFrames)
        {
          intList.Add(key);
          spriteRegions.Add(key, spriteFrame);
          ++key;
        }
        this.Groups.Add(group.GroupName, intList);
      }
      this.Regions = spriteRegions;
    }

    protected override void UpdateFrame()
    {
    }
  }
}
