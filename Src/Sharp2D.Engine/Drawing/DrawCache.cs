// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.DrawCache
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// An opaque object that represents pre-compiled low-level geometry.
  /// </summary>
  /// <remarks><see cref="T:Sharp2D.Engine.Drawing.DrawCache" /> objects can be rendered by a <see cref="!:DrawBatch" />.</remarks>
  public class DrawCache
  {
    private List<DrawCacheUnit> _units = new List<DrawCacheUnit>();

    /// <summary>
    /// Gets whether the <see cref="T:Sharp2D.Engine.Drawing.DrawCache" /> is still valid.
    /// </summary>
    public bool IsValid
    {
      get
      {
        foreach (DrawCacheUnit unit in this._units)
        {
          if (!unit.IsValid)
            return false;
        }
        return true;
      }
    }

    internal void AddUnit(DrawCacheUnit unit) => this._units.Add(unit);

    internal void Render(GraphicsDevice device, Texture2D defaultTexture)
    {
      foreach (DrawCacheUnit unit in this._units)
        unit.Render(device, (Texture) defaultTexture);
    }
  }
}
