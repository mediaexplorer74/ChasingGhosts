// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.BezierType
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// The type of Bezier curve specified in draw operations.
  /// </summary>
  public enum BezierType
  {
    /// <summary>Specifies a quadratic Bezier curve (1 control point).</summary>
    Quadratic,
    /// <summary>Specifies a cubic Bezier curve (2 control points).</summary>
    Cubic,
  }
}
