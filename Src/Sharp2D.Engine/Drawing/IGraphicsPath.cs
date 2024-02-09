// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.IGraphicsPath
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>Represents computed path geometry.</summary>
  public interface IGraphicsPath
  {
    /// <summary>The number of vertices in the computed geometry.</summary>
    int VertexCount { get; }

    /// <summary>
    /// The number of vertex indexes in the computed geometry.
    /// </summary>
    int IndexCount { get; }

    /// <summary>The raw vertex data of the computed geometry.</summary>
    Vector2[] VertexPositionData { get; }

    /// <summary>The raw texture data of the computed geometry.</summary>
    Vector2[] VertexTextureData { get; }

    /// <summary>The raw color data of the computed geometry.</summary>
    Color[] VertexColorData { get; }

    /// <summary>The raw index data of the computed geometry.</summary>
    short[] IndexData { get; }

    /// <summary>
    /// The <see cref="P:Sharp2D.Engine.Drawing.IGraphicsPath.Pen" /> used to compute the geometry.
    /// </summary>
    Pen Pen { get; }
  }
}
