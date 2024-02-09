// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.LabelHelper
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Common.UI.Enums;
using System;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>Helper methods for labels.</summary>
  public static class LabelHelper
  {
    /// <summary>Centers the label.</summary>
    /// <param name="label">The label.</param>
    public static void Center(this Label label)
    {
      if (label == null)
        throw new InvalidOperationException("Label is null - cannot center null. Idiot. :)");
      label.LocalPosition = new Vector2(0.0f, 0.0f);
      label.Alignment = TextAlignment.Center;
    }
  }
}
