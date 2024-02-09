// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.ClippingHelper
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.ObjectSystem;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>The clipping helper.</summary>
  public static class ClippingHelper
  {
    /// <summary>
    /// Calculates whether or not to draw the scissor rectangle,
    ///     and returns a boolean value representing if it should be drawn.
    ///     <para>
    /// <see cref="!:scissorRectangle" /> will be modified to fit the new size, IF the method returns true.
    ///     </para>
    /// </summary>
    /// <param name="currentRect">
    /// The current rectangle of the GraphicsDevice Scissor.
    /// </param>
    /// <param name="scissorRectangle">
    /// The new rectangle to use to cut off from..
    /// </param>
    /// <returns>
    /// Returns a boolean value representing if the <see cref="!:scissorRectangle" /> is at least partially within the
    ///     rectangle of
    ///     <see cref="!:currentRect" />
    /// </returns>
    public static bool GetRectangle(Rectanglef currentRect, ref Rectanglef scissorRectangle)
    {
      bool flag1 = false;
      bool flag2 = false;
      if ((double) currentRect.Y <= (double) scissorRectangle.Y + (double) scissorRectangle.Height)
      {
        if ((double) currentRect.Y > (double) scissorRectangle.Y)
        {
          float num = currentRect.Y - scissorRectangle.Y;
          scissorRectangle.Y = currentRect.Y;
          scissorRectangle.Height -= num;
          flag1 = true;
        }
      }
      else
        flag2 = true;
      if ((double) currentRect.X <= (double) scissorRectangle.X + (double) scissorRectangle.Width)
      {
        if ((double) currentRect.X > (double) scissorRectangle.X)
        {
          float num = currentRect.X - scissorRectangle.X;
          scissorRectangle.X = currentRect.X;
          scissorRectangle.Width -= num;
          flag1 = true;
        }
      }
      else
        flag2 = true;
      if ((double) currentRect.Y + (double) currentRect.Height > (double) scissorRectangle.Y)
      {
        if ((double) currentRect.Y + (double) currentRect.Height < (double) scissorRectangle.Y + (double) scissorRectangle.Height)
        {
          float num = scissorRectangle.Height - currentRect.Height;
          scissorRectangle.Height = num;
          flag1 = true;
        }
      }
      else
        flag2 = true;
      if ((double) currentRect.X + (double) currentRect.Width > (double) scissorRectangle.X)
      {
        if ((double) currentRect.X + (double) currentRect.Width < (double) scissorRectangle.X + (double) scissorRectangle.Width)
        {
          float num = scissorRectangle.Width - currentRect.Width;
          scissorRectangle.Width = num;
          flag1 = true;
        }
      }
      else
        flag2 = true;
      return flag1 || !flag2;
    }
  }
}
