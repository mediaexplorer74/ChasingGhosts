// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.InsetOutsetCount
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  internal struct InsetOutsetCount
  {
    public readonly short InsetCount;
    public readonly short OutsetCount;
    public readonly bool CCW;

    public InsetOutsetCount(short insetCount, short outsetCount)
    {
      this.InsetCount = insetCount;
      this.OutsetCount = outsetCount;
      this.CCW = true;
    }

    public InsetOutsetCount(short insetCount, short outsetCount, bool ccw)
    {
      this.InsetCount = insetCount;
      this.OutsetCount = outsetCount;
      this.CCW = ccw;
    }
  }
}
