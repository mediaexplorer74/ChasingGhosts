// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Brush
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Sharp2D.Engine.Drawing
{
  /// <summary>
  /// Objects used to fill the interiors of shapes and paths.
  /// </summary>
  public abstract class Brush : IDisposable
  {
    private float _alpha;
    private bool _disposed;

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Black { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Blue { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Brown { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Cyan { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkBlue { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkCyan { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkGray { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkGoldenrod { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkGreen { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkMagenta { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkOrange { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush DarkRed { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Goldenrod { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Gray { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Green { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush LightBlue { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush LightCyan { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush LightGray { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush LightGreen { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush LightPink { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush LightYellow { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Lime { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Magenta { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Orange { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Pink { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Purple { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Red { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Teal { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush White { get; private set; }

    /// <summary>A system-defined <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.</summary>
    public static Brush Yellow { get; private set; }

    static Brush()
    {
      Brush.Black = (Brush) new SolidColorBrush(Color.Black);
      Brush.Blue = (Brush) new SolidColorBrush(Color.Blue);
      Brush.Brown = (Brush) new SolidColorBrush(Color.Brown);
      Brush.Cyan = (Brush) new SolidColorBrush(Color.Cyan);
      Brush.DarkBlue = (Brush) new SolidColorBrush(Color.DarkBlue);
      Brush.DarkCyan = (Brush) new SolidColorBrush(Color.DarkCyan);
      Brush.DarkGoldenrod = (Brush) new SolidColorBrush(Color.DarkGoldenrod);
      Brush.DarkGray = (Brush) new SolidColorBrush(Color.DarkGray);
      Brush.DarkGreen = (Brush) new SolidColorBrush(Color.DarkGreen);
      Brush.DarkMagenta = (Brush) new SolidColorBrush(Color.DarkMagenta);
      Brush.DarkOrange = (Brush) new SolidColorBrush(Color.DarkOrange);
      Brush.DarkRed = (Brush) new SolidColorBrush(Color.DarkRed);
      Brush.Goldenrod = (Brush) new SolidColorBrush(Color.Goldenrod);
      Brush.Gray = (Brush) new SolidColorBrush(Color.Gray);
      Brush.Green = (Brush) new SolidColorBrush(Color.Green);
      Brush.LightBlue = (Brush) new SolidColorBrush(Color.LightBlue);
      Brush.LightCyan = (Brush) new SolidColorBrush(Color.LightCyan);
      Brush.LightGray = (Brush) new SolidColorBrush(Color.LightGray);
      Brush.LightGreen = (Brush) new SolidColorBrush(Color.LightGreen);
      Brush.LightPink = (Brush) new SolidColorBrush(Color.LightPink);
      Brush.LightYellow = (Brush) new SolidColorBrush(Color.LightYellow);
      Brush.Lime = (Brush) new SolidColorBrush(Color.Lime);
      Brush.Magenta = (Brush) new SolidColorBrush(Color.Magenta);
      Brush.Orange = (Brush) new SolidColorBrush(Color.Orange);
      Brush.Pink = (Brush) new SolidColorBrush(Color.Pink);
      Brush.Purple = (Brush) new SolidColorBrush(Color.Purple);
      Brush.Red = (Brush) new SolidColorBrush(Color.Red);
      Brush.Teal = (Brush) new SolidColorBrush(Color.Teal);
      Brush.White = (Brush) new SolidColorBrush(Color.White);
      Brush.Yellow = (Brush) new SolidColorBrush(Color.Yellow);
    }

    /// <summary>
    /// Initializes a new instance of a <see cref="T:Sharp2D.Engine.Drawing.Brush" /> class.
    /// </summary>
    protected Brush()
    {
      this.Color = Color.White;
      this.Transform = Matrix.Identity;
    }

    /// <summary>
    /// Initializes a new instance of a <see cref="T:Sharp2D.Engine.Drawing.Brush" /> class with a given alpha value.
    /// </summary>
    /// <param name="alpha">Alpha value of the brush.</param>
    protected Brush(float alpha)
      : this()
    {
      this._alpha = alpha;
    }

    /// <summary>The alpha value of the brush.</summary>
    public virtual float Alpha
    {
      get => this._alpha;
      set => this._alpha = value;
    }

    /// <summary>The color of the brush.</summary>
    protected internal Color Color { get; protected set; }

    /// <summary>The texture resource of the brush.</summary>
    protected internal Texture2D Texture { get; protected set; }

    /// <summary>Gets or sets the transformation to apply to brush.</summary>
    protected internal Matrix Transform { get; protected set; }

    /// <summary>
    /// Releases all resources used by the <see cref="T:Sharp2D.Engine.Drawing.Brush" /> object.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (this._disposed)
        return;
      if (disposing)
        this.DisposeManaged();
      this.DisposeUnmanaged();
      this._disposed = true;
    }

    /// <summary>Attempts to dispose unmanaged resources.</summary>
    ~Brush() => this.Dispose(false);

    /// <summary>
    /// Releases the managed resources used by the <see cref="T:Sharp2D.Engine.Drawing.Brush" />.
    /// </summary>
    protected virtual void DisposeManaged()
    {
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:Sharp2D.Engine.Drawing.Brush" />.
    /// </summary>
    protected virtual void DisposeUnmanaged()
    {
    }
  }
}
