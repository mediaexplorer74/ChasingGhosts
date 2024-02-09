// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.PressState
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  [Flags]
  public enum PressState
  {
    None = 1,
    Down = 2,
    Primary = 4,
    Secondary = 8,
    Tertiary = 16, // 0x00000010
  }
}
