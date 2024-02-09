// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.MultipleSpriteTypesException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>The multiple Sprite types exception.</summary>
  public class MultipleSpriteTypesException : InvalidOperationException
  {
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.MultipleSpriteTypesException" /> class.
    /// </summary>
    public MultipleSpriteTypesException()
      : base("Cannot use a Sprite and a SpriteSheet at the same time in a UI control. Please use one or the other.")
    {
    }
  }
}
