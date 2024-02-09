// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.UiControl
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>Base UI Control for drawing a UI.</summary>
  public class UiControl : GameObject
  {
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.UiControl" /> class.
    /// </summary>
    public UiControl() => this.Enabled = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.UiControl" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="sprite">The Sprite.</param>
    /// <param name="children">The children.</param>
    protected UiControl(Vector2 position, Sprite sprite = null, params GameObject[] children)
      : base(position, sprite, children)
    {
      this.Enabled = true;
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the control is enabled (Accepts user input).
    /// </summary>
    /// <value>
    ///     <c>true</c> if enabled; otherwise, <c>false</c>.
    /// </value>
    public virtual bool Enabled { get; set; }

    /// <summary>
    ///     The draw tint. If you want to customize this - e.g. a button's hover state.
    /// </summary>
    protected Color? DrawTint { get; set; }

    /// <summary>
    /// Updates this control, and runs the update method on all children.
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time) => base.Update(time);
  }
}
