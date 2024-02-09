// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.PenAlignment
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// The alignment of a path stroked by a <see cref="T:Sharp2D.Engine.Drawing.Pen" /> relative to the ideal path.
  /// </summary>
  public enum PenAlignment
  {
    /// <summary>
    /// The stroked path should be centered directly over the ideal path.
    /// </summary>
    Center,
    /// <summary>
    /// The stroked path should run along the inside edge of the ideal path.
    /// </summary>
    Inset,
    /// <summary>
    /// The stroked path should run along the outside edge of the ideal path.
    /// </summary>
    Outset,
  }
}
