// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Brushes.CheckerBrush
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Sharp2D.Engine.Drawing.Brushes
{
  /// <summary>
  /// A <see cref="T:Sharp2D.Engine.Drawing.Brush" /> that represents a two-color checkered texture.
  /// </summary>
  public class CheckerBrush : TextureBrush
  {
    private Color _color1;
    private Color _color2;
    private int _width;
    private int _height;

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Brushes.CheckerBrush" /> with the given <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" />, colors, and square cell size.
    /// </summary>
    /// <param name="device">The <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" /> that should be used to create a texture.</param>
    /// <param name="color1">The first checker color.</param>
    /// <param name="color2">The second checker color.</param>
    /// <param name="width">The size of the width and height of a single colored square.</param>
    public CheckerBrush(GraphicsDevice device, Color color1, Color color2, int width)
      : this(device, color1, color2, width, width, 1f)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Brushes.CheckerBrush" /> with the given <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" />, colors, square cell size, and opacity.
    /// </summary>
    /// <param name="device">The <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" /> that should be used to create a texture.</param>
    /// <param name="color1">The first checker color.</param>
    /// <param name="color2">The second checker color.</param>
    /// <param name="width">The size of the width and height of a single colored square.</param>
    /// <param name="opacity">The opacity to render the texture with.</param>
    public CheckerBrush(
      GraphicsDevice device,
      Color color1,
      Color color2,
      int width,
      float opacity)
      : this(device, color1, color2, width, width, opacity)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Brushes.CheckerBrush" /> with the given <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" />, colors, and cell dimensions.
    /// </summary>
    /// <param name="device">The <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" /> that should be used to create a texture.</param>
    /// <param name="color1">The first checker color.</param>
    /// <param name="color2">The second checker color.</param>
    /// <param name="width">The width of a single colored cell.</param>
    /// <param name="height">The height of a single colored cell.</param>
    public CheckerBrush(GraphicsDevice device, Color color1, Color color2, int width, int height)
      : this(device, color1, color2, width, height, 1f)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Sharp2D.Engine.Drawing.Brushes.CheckerBrush" /> with the given <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" />, colors, cell dimensions, and opacity.
    /// </summary>
    /// <param name="device">The <see cref="T:Microsoft.Xna.Framework.Graphics.GraphicsDevice" /> that should be used to create a texture.</param>
    /// <param name="color1">The first checker color.</param>
    /// <param name="color2">The second checker color.</param>
    /// <param name="width">The width of a single colored cell.</param>
    /// <param name="height">The height of a single colored cell.</param>
    /// <param name="opacity">The opacity to render the texture with.</param>
    public CheckerBrush(
      GraphicsDevice device,
      Color color1,
      Color color2,
      int width,
      int height,
      float opacity)
      : base(CheckerBrush.BuildCheckerTexture(device, color1, color2, width, height), opacity)
    {
      this._color1 = color1;
      this._color2 = color2;
      this._width = width;
      this._height = height;
      this.OwnsTexture = true;
      device.DeviceReset += new EventHandler<EventArgs>(this.HandleGraphicsDeviceReset);
    }

    /// <InheritDoc />
    protected override void DisposeManaged()
    {
      if (this.Texture != null && this.Texture.GraphicsDevice != null)
        this.Texture.GraphicsDevice.DeviceReset -= new EventHandler<EventArgs>(this.HandleGraphicsDeviceReset);
      base.DisposeManaged();
    }

    private void HandleGraphicsDeviceReset(object sender, EventArgs e)
    {
      if (!(sender is GraphicsDevice device))
        return;
      this.Texture = CheckerBrush.BuildCheckerTexture(device, this._color1, this._color2, this._width, this._height);
    }

    private static Texture2D BuildCheckerTexture(
      GraphicsDevice device,
      Color color1,
      Color color2,
      int blockWidth,
      int blockHeight)
    {
      int width = blockWidth * 2;
      int height = blockHeight * 2;
      byte[] data = new byte[width * height * 4];
      for (int y = 0; y < height / 2; ++y)
      {
        for (int x = 0; x < width / 2; ++x)
          CheckerBrush.SetColor(data, width, x, y, color1);
      }
      for (int y = 0; y < height / 2; ++y)
      {
        for (int x = width / 2; x < width; ++x)
          CheckerBrush.SetColor(data, width, x, y, color2);
      }
      for (int y = width / 2; y < height; ++y)
      {
        for (int x = 0; x < width / 2; ++x)
          CheckerBrush.SetColor(data, width, x, y, color2);
      }
      for (int y = width / 2; y < height; ++y)
      {
        for (int x = width / 2; x < width; ++x)
          CheckerBrush.SetColor(data, width, x, y, color1);
      }
      Texture2D texture2D = new Texture2D(device, width, height, false, SurfaceFormat.Color);
      texture2D.SetData<byte>(data);
      return texture2D;
    }

    private static void SetColor(byte[] data, int width, int x, int y, Color color)
    {
      int index = (y * width + x) * 4;
      data[index] = color.R;
      data[index + 1] = color.G;
      data[index + 2] = color.B;
      data[index + 3] = color.A;
    }
  }
}
