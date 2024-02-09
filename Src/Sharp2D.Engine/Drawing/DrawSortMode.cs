// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.DrawSortMode
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>Defines figure sort-rendering options.</summary>
  public enum DrawSortMode
  {
    /// <summary>
    /// Figures are not drawn until <see cref="!:DrawBatch.End" /> is called.  <see cref="!:DrawBatch.End" /> will apply graphics
    /// device settings and draw all figures in one batch in the same order drawing calls were received.  <see cref="!:DrawBatch" />
    /// defaults to <c>Deferred</c> mode.
    /// </summary>
    Deferred,
    /// <summary>
    /// <see cref="!:DrawBatch.Begin()" /> will apply new graphics device settings, and figures will be drawn within each drawing call.
    /// </summary>
    Immediate,
  }
}
