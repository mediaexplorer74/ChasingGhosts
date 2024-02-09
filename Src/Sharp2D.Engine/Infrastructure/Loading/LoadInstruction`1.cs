// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Loading.LoadInstruction`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Loading
{
  public abstract class LoadInstruction<T>
  {
    public abstract T Load(IResolver resolver);
  }
}
