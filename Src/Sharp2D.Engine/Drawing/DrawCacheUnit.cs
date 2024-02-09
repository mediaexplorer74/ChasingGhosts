// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.DrawCacheUnit
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  internal class DrawCacheUnit
  {
    private Texture2D _texture;
    private VertexPositionColorTexture[] _vertexBuffer;
    private short[] _indexBuffer;

    public DrawCacheUnit(
      VertexPositionColorTexture[] vertexBuffer,
      short[] indexBuffer,
      Texture2D texture)
    {
      this._texture = texture;
      this._vertexBuffer = vertexBuffer;
      this._indexBuffer = indexBuffer;
    }

    public DrawCacheUnit(
      VertexPositionColorTexture[] vertexBuffer,
      int vertexOffset,
      int vertexCount,
      short[] indexBuffer,
      int indexOffset,
      int indexCount,
      Texture2D texture)
    {
      if (vertexCount > vertexBuffer.Length - vertexOffset)
        throw new ArgumentException("vertexBuffer is too small for the given vertexOffset and vertexCount.");
      if (indexCount > indexBuffer.Length - indexOffset)
        throw new ArgumentException("indexBuffer is too small for the given indexOffset and indexCount.");
      this._texture = texture;
      this._vertexBuffer = new VertexPositionColorTexture[vertexCount];
      this._indexBuffer = new short[indexCount];
      Array.Copy((Array) vertexBuffer, vertexOffset, (Array) this._vertexBuffer, 0, vertexCount);
      Array.Copy((Array) indexBuffer, indexOffset, (Array) this._indexBuffer, 0, indexCount);
    }

    public virtual bool IsValid => true;

    public void Render(GraphicsDevice device, Texture defaultTexture)
    {
      device.Textures[0] = (Texture) this._texture ?? defaultTexture;
      device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, this._vertexBuffer, 0, this._vertexBuffer.Length, this._indexBuffer, 0, this._indexBuffer.Length / 3);
    }
  }
}
