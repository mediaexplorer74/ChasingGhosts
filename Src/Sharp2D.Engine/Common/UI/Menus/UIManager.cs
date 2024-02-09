// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Menus.UiManager
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Menus
{
  /// <summary>
  ///     Root UI manager object. Handles tabbing.
  ///     Should not be instantiated more than once.
  /// </summary>
  public class UiManager : GameObject, IRootObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Menus.UiManager" /> class.
    /// </summary>
    /// <param name="localPosition">
    /// The local position. This is the offset from the parent global position.
    /// </param>
    /// <param name="sprite">The texture.</param>
    /// <param name="children">The children.</param>
    public UiManager(Vector2 localPosition, Sprite sprite = null, params GameObject[] children)
      : base(localPosition, sprite, children)
    {
      this.IsGenerated = true;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Menus.UiManager" /> class.
    /// </summary>
    public UiManager()
      : this(Vector2.Zero, (Sprite) null)
    {
    }

    /// <summary>Focuses the next focusable UI control.</summary>
    public virtual void FocusNext() => UiContainerHelper.FindNextFocusableControl()?.SetFocus();

    /// <summary>Focuses the previous focusable UI control.</summary>
    public virtual void FocusPrevious()
    {
      UiContainerHelper.FindPreviousFocusableControl()?.SetFocus();
    }

    /// <summary>
    ///     Initializes this game object. Also initializes children.
    /// </summary>
    /// <param name="resolver"></param>
    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.Name = "User Interface";
    }

    /// <summary>
    /// If not paused, updates the specified game time. Else returns
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
      if (this.IsPaused)
        return;
      IUiInteractionProvider interactionProvider = this.Resolver.Resolve<IUiInteractionProvider>();
      if (interactionProvider.ShouldFocusNext())
        this.FocusNext();
      if (interactionProvider.ShouldFocusPrevious())
        this.FocusPrevious();
      base.Update(gameTime);
    }
  }
}
