// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.ArcType
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>The type of arc in closed drawing or fill operations.</summary>
  public enum ArcType
  {
    /// <summary>
    /// Causes the endpoints of the arc to be connected directly.
    /// </summary>
    Segment,
    /// <summary>
    /// Causes the endpoints of the arc to be connected to the arc center, as in a pie wedge.
    /// </summary>
    Sector,
  }
}
