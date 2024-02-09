// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.WindowsKeyboardManagerFactory
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Windows.Keyboard;

#nullable disable
namespace Sharp2D.Windows
{
  public class WindowsKeyboardManagerFactory : IKeyboardManagerFactory
  {
    static WindowsKeyboardManagerFactory() => WindowsKeyboardManager.InitializeManager();

    public KeyboardManager CreateKeyboardManager(TextBox parent)
    {
      return (KeyboardManager) new WindowsKeyboardManager(parent);
    }
  }
}
