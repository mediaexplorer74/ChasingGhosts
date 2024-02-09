// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.ScrollableViewContainer
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>
  ///     The container used in the ScrollableView UI Control
  /// </summary>
  public class ScrollableViewContainer : GameObject
  {
    /// <summary>
    ///     Gets or sets the local position relative to it's parent object.
    /// </summary>
    /// <value>The local position.</value>
    /// <exception cref="T:System.InvalidCastException">ScrollableViewContainer must be a child of a ScrollableView control</exception>
    public override Vector2 LocalPosition => this.GetLocalPosition();

    /// <summary>Gets or sets the scrolled amount.</summary>
    /// <value>The scrolled amount.</value>
    public Vector2 ScrolledAmount { get; set; }

    /// <summary>
    ///     Gets the local position. Uses the parent <see cref="T:Sharp2D.Engine.Common.UI.Controls.ScrollableView" />'s <see cref="T:Sharp2D.Engine.Common.UI.Layout.Padding" /> to offset itself
    ///     from the parent.
    /// </summary>
    /// <returns>
    ///     The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///     ScrollableViewContainer must be a child of a ScrollableView control
    /// </exception>
    private Vector2 GetLocalPosition()
    {
      return !(this.Parent is ScrollableView parent) ? Vector2.Zero : new Vector2(parent.Padding.Left, parent.Padding.Top);
    }
  }
}
