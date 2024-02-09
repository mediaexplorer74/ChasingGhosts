// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.SpriteHelper
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>Sprite Helper methods.</summary>
  public static class SpriteHelper
  {
    /// <summary>
    /// Creates the region keys by Sprite count in a Sprite sheet.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>
    /// The <see cref="!:int[]" />.
    /// </returns>
    public static int[] CreateRegionKeysBySpriteCount(int count)
    {
      int[] keysBySpriteCount = new int[count];
      for (int index = 0; index < count; ++index)
        keysBySpriteCount[index] = index;
      return keysBySpriteCount;
    }

    /// <summary>
    /// Creates the Sprite regions for a Sprite sheet. Intelligent as fuck.
    /// </summary>
    /// <typeparam name="TSpriteKey">The type of the Sprite key.</typeparam>
    /// <param name="assetPath">The asset path.</param>
    /// <param name="spriteSize">Size of each individual Sprite.</param>
    /// <param name="width">The width of the spritesheet.</param>
    /// <param name="height">The height of the spritesheet.</param>
    /// <param name="regionKeys">The region keys.</param>
    /// <param name="margin">The margin.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Utility.SpriteRegions" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// You cannot create regions with a zero X or Y spritesize!
    /// or
    /// There appears to be more region keys than sprites. Do the math.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">You cannot create regions with a zero X or Y Sprite size!
    /// or
    /// There appears to be more region keys than sprites. Do the math.</exception>
    public static SpriteRegions<TSpriteKey> CreateRegions<TSpriteKey>(
      string assetPath,
      Vector2 spriteSize,
      int width,
      int height,
      IEnumerable<TSpriteKey> regionKeys,
      int margin = 0)
    {
      if ((double) spriteSize.X == 0.0 || (double) spriteSize.Y == 0.0)
        throw new InvalidOperationException("You cannot create regions with a zero X or Y spritesize!");
      SpriteRegions<TSpriteKey> regions = new SpriteRegions<TSpriteKey>();
      int num1 = (int) ((double) width / (double) spriteSize.X);
      int num2 = (int) ((double) height / (double) spriteSize.Y);
      int num3 = 0;
      TSpriteKey[] array = regionKeys.ToArray<TSpriteKey>();
      if (array.Length > (num2 + num2 * margin) * (num1 + num1 * margin))
        throw new InvalidOperationException("There appears to be more region keys than sprites. Do the math.");
      for (int index1 = 0; index1 < num2; ++index1)
      {
        for (int index2 = 0; index2 < num1; ++index2)
        {
          Rectanglef rectanglef = new Rectanglef((float) ((int) ((double) index2 * (double) spriteSize.X) + margin * index2), (float) ((int) ((double) index1 * (double) spriteSize.Y) + margin * index1), spriteSize.X, spriteSize.Y);
          if (array.Length > num3)
          {
            TSpriteKey key = array[num3++];
            SpriteFrame spriteFrame = new SpriteFrame(assetPath, rectanglef.ToRect());
            regions.Add(key, spriteFrame);
          }
          else
            break;
        }
      }
      return regions;
    }
  }
}
