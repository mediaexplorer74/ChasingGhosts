// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Loading.RawTextureInstruction
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Loading
{
  public class RawTextureInstruction : LoadInstruction<Texture2D>
  {
    private readonly Texture2D texture;

    public RawTextureInstruction(Texture2D texture) => this.texture = texture;

    public override Texture2D Load(IResolver resolver) => this.texture;
  }
}
