// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.LineCap
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// The style of termination used at the endpoints of stroked paths.
  /// </summary>
  public enum LineCap
  {
    /// <summary>
    /// The stroked path is cut off at the immediate edge of the path's endpoint.
    /// </summary>
    Flat,
    /// <summary>
    /// The stroked path runs half the pen's width past the edge of the path's endpoint.
    /// </summary>
    Square,
    /// <summary>
    /// The stroked path forms a triangular point half the pen's width past the edge of the path's endpoint.
    /// </summary>
    Triangle,
    /// <summary>
    /// The stroked path forms an inverse triangle half the pen's width past the edge of the path's endpoint.
    /// </summary>
    InvTriangle,
    /// <summary>
    /// The stroked path forms an arrow at the path's endpoint.
    /// </summary>
    Arrow,
  }
}
