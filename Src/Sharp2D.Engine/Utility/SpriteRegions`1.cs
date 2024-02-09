// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Utility.SpriteRegions`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.Components.Sprites;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Utility
{
  /// <summary>Sprite Region dictionary.</summary>
  /// <typeparam name="TSpriteKey">The type of the Sprite key.</typeparam>
  public class SpriteRegions<TSpriteKey> : Dictionary<TSpriteKey, SpriteFrame>
  {
  }
}
