// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.Keyboard.KeyEventArgs
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Microsoft.Xna.Framework.Input;
using System;

#nullable disable
namespace Sharp2D.Windows.Keyboard
{
  public class KeyEventArgs : EventArgs
  {
    private Keys keyCode;

    public KeyEventArgs(Keys keyCode) => this.keyCode = keyCode;

    public Keys KeyCode => this.keyCode;
  }
}
