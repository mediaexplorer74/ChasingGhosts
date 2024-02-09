// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.RectangleExtensions
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure.Input;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>Rectangle extension methods.</summary>
  public static class RectangleExtensions
  {
    /// <summary>
    /// Determines whether the mouse is in the specified region. Uses <see cref="T:Sharp2D.Engine.Utility.InputManager" /><para>This also takes into account the screen resolution.</para>
    /// </summary>
    /// <param name="region">The region.</param>
    /// <param name="device">The device.</param>
    /// <param name="rotation">The rotation.</param>
    /// <returns>
    ///   <c>True</c> if the mouse position is in the specified region.
    /// </returns>
    public static bool IsPointerInRegion(
      this Rectanglef region,
      IPointerDevice device,
      float rotation = 0.0f)
    {
      Vector2? currentPosition = device.CurrentPosition;
      if (!currentPosition.HasValue)
        return false;
      Vector2 pointToRotate = GlobalConfig.IsEditor ? currentPosition.Value : Resolution.TransformPoint(currentPosition.Value);
      Vector2 vector2 = (double) rotation == 0.0 ? pointToRotate : SharpMathHelper.Rotate(pointToRotate, new Vector2(region.Center.X, region.Center.Y), -rotation);
      return region.Contains((int) vector2.X, (int) vector2.Y);
    }
  }
}
