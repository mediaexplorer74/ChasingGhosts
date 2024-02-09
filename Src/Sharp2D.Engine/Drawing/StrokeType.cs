// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.StrokeType
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// Specifies what components of a path should be stroked.
  /// </summary>
  public enum StrokeType
  {
    /// <summary>
    /// Only stroke the path itself.  Should not be confused with filling a shape enclosed by a path.
    /// </summary>
    Fill,
    /// <summary>Only stroke the outline of the path.</summary>
    Outline,
    /// <summary>Stroke both the path and its outline.</summary>
    Both,
  }
}
