// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.IKeyboardManagerFactory
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.UI.Controls;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  public interface IKeyboardManagerFactory
  {
    KeyboardManager CreateKeyboardManager(TextBox parent);
  }
}
