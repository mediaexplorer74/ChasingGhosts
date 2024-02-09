// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Loading.ColorInstruction
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Loading
{
  public class ColorInstruction : LoadInstruction<Texture2D>
  {
    public override Texture2D Load(IResolver resolver)
    {
      Texture2D texture2D = new Texture2D(resolver.Resolve<GraphicsDevice>(), 1, 1);
      texture2D.SetData<Color>(new Color[1]{ this.Color });
      return texture2D;
    }

    public Color Color { get; set; }
  }
}
