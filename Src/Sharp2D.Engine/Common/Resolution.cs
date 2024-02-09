// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Resolution
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Sharp2D.Engine.Common
{
  /// <summary>Resolution</summary>
  public static class Resolution
  {
    /// <summary>The screen scale</summary>
    public static Vector2 ScreenAspectRatio = new Vector2(1f, 1f);
    /// <summary>
    ///     The virtual screen size. Default is 1280x800. See the non-existent documentation on how this works.
    /// </summary>
    public static Vector2 VirtualScreen = new Vector2(1280f, 800f);
    /// <summary>The _preferred back buffer height.</summary>
    private static int preferredBackBufferHeight;
    /// <summary>The _preferred back buffer width.</summary>
    private static int preferredBackBufferWidth;

    /// <summary>Gets or sets the margin.</summary>
    public static Vector2 Margin { get; set; }

    /// <summary>The scale used for beginning the DrawBatch.</summary>
    public static Matrix Scale { get; private set; }

    /// <summary>Gets the scaling factor.</summary>
    public static Vector3 ScalingFactor { get; private set; }

    /// <summary>Gets the window screen.</summary>
    public static Vector2 WindowScreen { get; private set; }

    /// <summary>
    ///     <para>
    ///         Determines the draw scaling.
    ///     </para>
    ///     <para>
    ///         Used to make the mouse scale correctly according to the virtual resolution,
    ///         no matter the actual resolution.
    ///     </para>
    ///     <para>
    ///         Example: 1920x1080 applied to 1280x800: new Vector2(1.5f, 1,35f)
    ///     </para>
    /// </summary>
    /// <returns>
    ///     The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public static Vector2 DetermineDrawScaling()
    {
      return new Vector2((float) Resolution.preferredBackBufferWidth / Resolution.VirtualScreen.X, (float) Resolution.preferredBackBufferHeight / Resolution.VirtualScreen.Y);
    }

    /// <summary>The transform point.</summary>
    /// <param name="original">The original.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public static Vector2 TransformPoint(Vector2 original)
    {
      return original / new Vector2(Resolution.ScalingFactor.X, Resolution.ScalingFactor.Y);
    }

    /// <summary>The transform point to world.</summary>
    /// <param name="original">The original.</param>
    /// <param name="camera">The camera.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public static Vector2 TransformPointToWorld(Vector2 original, Sharp2D.Engine.Common.World.Camera.Camera camera)
    {
      Vector2 vector2 = new Vector2(original.X - Resolution.WindowScreen.X / 2f, original.Y - Resolution.WindowScreen.Y / 2f);
      if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene.FixedScale)
        vector2 = Resolution.TransformPoint(vector2);
      Matrix scale = Matrix.CreateScale(1f / camera.Zoom, 1f / camera.Zoom, 1f);
      return Vector2.Transform(vector2, scale) + camera.LocalPosition;
    }

    /// <summary>Updates the specified preferred back buffer width.</summary>
    /// <param name="preferredBackBufferWidth">
    /// Width of the preferred back buffer.
    /// </param>
    /// <param name="preferredBackBufferHeight">
    /// Height of the preferred back buffer.
    /// </param>
    public static void Update(int preferredBackBufferWidth, int preferredBackBufferHeight)
    {
      if (preferredBackBufferWidth < 1)
        throw new ArgumentNullException(nameof (preferredBackBufferWidth));
      if (preferredBackBufferHeight < 1)
        throw new ArgumentNullException(nameof (preferredBackBufferHeight));
      Resolution.preferredBackBufferWidth = (int) ((double) preferredBackBufferWidth - (double) Resolution.Margin.X);
      Resolution.preferredBackBufferHeight = (int) ((double) preferredBackBufferHeight - (double) Resolution.Margin.Y);
      float x = (float) Resolution.preferredBackBufferWidth / Resolution.VirtualScreen.X;
      float y = (float) Resolution.preferredBackBufferHeight / Resolution.VirtualScreen.Y;
      Resolution.ScreenAspectRatio = new Vector2(x / y);
      Resolution.ScalingFactor = new Vector3(x, y, 1f);
      Resolution.Scale = Matrix.CreateScale(Resolution.ScalingFactor);
      if (Sharp2D.Engine.Common.Scene.Scene.CurrentScene != null && Sharp2D.Engine.Common.Scene.Scene.CurrentScene.MainCamera != null)
        Sharp2D.Engine.Common.Scene.Scene.CurrentScene.MainCamera.ResetCamera();
      Resolution.WindowScreen = new Vector2((float) preferredBackBufferWidth, (float) preferredBackBufferHeight);
    }
  }
}
