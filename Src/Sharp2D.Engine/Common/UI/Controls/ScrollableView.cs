// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.ScrollableView
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Events;
using Sharp2D.Engine.Common.UI.Layout;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Engine.Utility;
using System;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>Scrollable view.</summary>
  public class ScrollableView : InteractableUiControl
  {
    /// <summary>The _rasterizer.</summary>
    private readonly RasterizerState rasterizer = new RasterizerState()
    {
      ScissorTestEnable = true
    };
    /// <summary>The _content container.</summary>
    private ScrollableViewContainer contentContainer;
    /// <summary>The _horizontal slider.</summary>
    private Slider horizontalSlider;
    /// <summary>The _scroll speed.</summary>
    private float scrollSpeed;
    /// <summary>The _vertical slider.</summary>
    private Slider verticalSlider;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.ScrollableView" /> class.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <param name="sprite">The Sprite.</param>
    /// <param name="verticalSlider">The vertical slider.</param>
    /// <param name="horizontalSlider">The horizontal slider.</param>
    /// <param name="children">The children.</param>
    public ScrollableView(
      Rectanglef region,
      Sprite sprite,
      Slider verticalSlider,
      Slider horizontalSlider,
      params GameObject[] children)
      : base(new Vector2(region.X, region.Y), sprite, children)
    {
      this.VerticalSlider = verticalSlider;
      this.HorizontalSlider = horizontalSlider;
      this.ContentContainer = new ScrollableViewContainer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.ScrollableView" /> class.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <param name="spriteSheet">The Sprite sheet.</param>
    /// <param name="verticalSlider">The vertical slider.</param>
    /// <param name="horizontalSlider">The horizontal slider.</param>
    /// <param name="children">The children.</param>
    public ScrollableView(
      Rectanglef region,
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet,
      Slider verticalSlider,
      Slider horizontalSlider,
      params GameObject[] children)
      : base(new Vector2(region.X, region.Y), spriteSheet, children)
    {
      this.VerticalSlider = verticalSlider;
      this.HorizontalSlider = horizontalSlider;
      this.ContentContainer = new ScrollableViewContainer();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.ScrollableView" /> class.
    /// </summary>
    public ScrollableView()
    {
      this.ContentContainer = new ScrollableViewContainer();
      this.ScrollSpeed = 51f;
    }

    /// <summary>Gets the content container.</summary>
    /// <value>The content container.</value>
    public ScrollableViewContainer ContentContainer
    {
      get => this.contentContainer;
      set => this.SetContentContainer(value);
    }

    /// <summary>Gets or sets the horizontal slider control.</summary>
    /// <value>The horizontal slider.</value>
    public Slider HorizontalSlider
    {
      get => this.horizontalSlider;
      set
      {
        if (value == null)
          return;
        this.SetHorizontalSlider(value);
      }
    }

    /// <summary>Gets the width of the verticalSlider container.</summary>
    /// <value>The width.</value>
    /// <summary>Gets the height of the verticalSlider container.</summary>
    /// <value>The height.</value>
    /// <summary>
    ///     Determines whether this control is focusable. <see cref="T:Sharp2D.Engine.Common.UI.Controls.ScrollableView" /> is not.
    /// </summary>
    /// <value></value>
    public override bool IsFocusable => false;

    /// <summary>Gets or sets the padding.</summary>
    /// <value>The padding.</value>
    public Padding Padding { get; set; }

    /// <summary>Gets or sets the scroll speed.</summary>
    public float ScrollSpeed
    {
      get => this.scrollSpeed * 100f;
      set
      {
        if ((double) value == 0.0)
          return;
        this.scrollSpeed = value / 100f;
      }
    }

    /// <summary>Gets or sets the vertical slider control.</summary>
    /// <value>The vertical Slider.</value>
    public Slider VerticalSlider
    {
      get => this.verticalSlider;
      set
      {
        if (value == null)
          return;
        this.SetVerticalSlider(value);
      }
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    public override void Add(GameObject item) => this.ContentContainer.Add(item);

    /// <summary>Copies to.</summary>
    /// <param name="array">The array.</param>
    /// <param name="arrayIndex">Index of the array.</param>
    public override void CopyTo(GameObject[] array, int arrayIndex)
    {
      this.ContentContainer.CopyTo(array, arrayIndex);
      this.RecalculateSize();
    }

    /// <summary>
    /// Draws a <see cref="!:SpriteSheet" /> if <see cref="!:SpriteType" /> is set to <see cref="!:Enums.SpriteType.Sheet" />.
    ///     Controls and draws all logic related to drawing the content of the verticalSlider container properly.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (this.IsHidden)
        return;
      foreach (GameContract component in this.Components)
        component.Draw(batch, time);
      this.VerticalSlider?.Draw(batch, time);
      this.HorizontalSlider?.Draw(batch, time);
      this.DrawContainerContent(batch, time);
    }

    /// <summary>
    /// Inserts a game object to the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" /> collection at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">
    /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </param>
    public override void Insert(int index, GameObject item)
    {
      this.ContentContainer.Insert(index, item);
    }

    /// <summary>The recalculate size.</summary>
    public void RecalculateSize()
    {
      if (this.ContentContainer == null || !this.ContentContainer.Children.Any<GameObject>())
        return;
      float num1 = (float) (int) this.ContentContainer.Children.Max<GameObject>((Func<GameObject, float>) (k => k.LocalPosition.X + k.Width + this.ContentContainer.ScrolledAmount.X));
      float num2 = (float) (int) this.ContentContainer.Children.Max<GameObject>((Func<GameObject, float>) (k => k.LocalPosition.Y + k.Height + this.ContentContainer.ScrolledAmount.Y));
      Padding padding;
      if ((double) num1 > (double) this.Width)
      {
        double num3 = (double) num1;
        double width = (double) this.Width;
        padding = this.Padding;
        double right = (double) padding.Right;
        double num4 = width - right;
        padding = this.Padding;
        double left = (double) padding.Left;
        double num5 = num4 - left;
        float num6 = (float) (num3 - num5);
        if (this.HorizontalSlider != null)
        {
          this.HorizontalSlider.Enabled = true;
          this.HorizontalSlider.Show();
          this.HorizontalSlider.MaxValue = (int) num6;
          this.HorizontalSlider.CalculateSliderPosition();
        }
      }
      else if (this.HorizontalSlider != null)
      {
        this.HorizontalSlider.Enabled = false;
        this.HorizontalSlider.Hide();
      }
      if ((double) num2 > (double) this.Height)
      {
        double num7 = (double) num2;
        double height = (double) this.Height;
        padding = this.Padding;
        double bottom = (double) padding.Bottom;
        double num8 = height - bottom;
        padding = this.Padding;
        double top = (double) padding.Top;
        double num9 = num8 - top;
        float num10 = (float) (num7 - num9);
        if (this.VerticalSlider == null)
          return;
        Debug.WriteLine((object) num10);
        this.VerticalSlider.Enabled = true;
        this.VerticalSlider.Show();
        this.VerticalSlider.MaxValue = (int) num10;
        this.VerticalSlider.CalculateSliderPosition();
      }
      else
      {
        if (this.VerticalSlider == null)
          return;
        this.VerticalSlider.Enabled = false;
        this.VerticalSlider.Hide();
      }
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the
    ///     <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <returns>
    /// true if <paramref name="item" /> was successfully removed from the
    ///     <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if
    ///     <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    public override bool Remove(GameObject item) => this.ContentContainer.Remove(item);

    /// <summary>Removes the child object at the specified index.</summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public override void RemoveAt(int index)
    {
      this.ContentContainer.RemoveAt(index);
      this.RecalculateSize();
    }

    /// <summary>The update.</summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      IUiInteractionProvider interactionProvider = this.Resolver.Resolve<IUiInteractionProvider>();
      IPointerDevice pointerDevice = this.Resolver.Resolve<IPointerDevice>();
      if (interactionProvider.GetUiControlInteractionState((InteractableUiControl) this) == UiControlInteractionState.Hover && pointerDevice.CurrentDeltaScroll != 0)
      {
        this.VerticalSlider?.ChangeValue((float) -pointerDevice.CurrentDeltaScroll * this.scrollSpeed);
        this.HorizontalSlider?.ChangeValue((float) -pointerDevice.CurrentDeltaScroll * this.scrollSpeed);
      }
      if (this.VerticalSlider != null && this.VerticalSlider.SliderButton.HasFocus)
      {
        if (InputManager.IsKeyPressed(new Keys?(Keys.Down)))
          this.VerticalSlider.ChangeValue(120f * this.scrollSpeed);
        else if (InputManager.IsKeyPressed(new Keys?(Keys.Up)))
          this.VerticalSlider.ChangeValue(-120f * this.scrollSpeed);
      }
      if (this.HorizontalSlider != null && this.HorizontalSlider.SliderButton.HasFocus)
      {
        if (InputManager.IsKeyPressed(new Keys?(Keys.Left)))
          this.HorizontalSlider.ChangeValue(120f * this.scrollSpeed);
        else if (InputManager.IsKeyPressed(new Keys?(Keys.Right)))
          this.HorizontalSlider.ChangeValue(-120f * this.scrollSpeed);
      }
      if (this.VerticalSlider != null)
        this.VerticalSlider.IsVisible = this.ContentContainer.Children.Any<GameObject>();
      if (this.HorizontalSlider != null)
        this.HorizontalSlider.IsVisible = this.ContentContainer.Children.Any<GameObject>();
      base.Update(time);
    }

    /// <summary>Changeds the slider value.</summary>
    /// <param name="deltaSlide">The deltaSlide.</param>
    public void ChangedSliderValue(Vector2 deltaSlide)
    {
      this.ContentContainer.ScrolledAmount += deltaSlide;
      foreach (GameObject child in this.ContentContainer.Children)
        child.LocalPosition += deltaSlide;
    }

    /// <summary>Draws the content of the container.</summary>
    /// <param name="spriteBatch">The batch.</param>
    /// <param name="time">The time.</param>
    private void DrawContainerContent(SharpDrawBatch batch, GameTime time)
    {
      SpriteBatch batch1 = batch.Batch;
      RasterizerState rasterizerState = batch1.GraphicsDevice.RasterizerState;
      Rectangle scissorRectangle = batch1.GraphicsDevice.ScissorRectangle;
      Vector2 drawScaling = Resolution.DetermineDrawScaling();
      Rectanglef rectanglef = this.Padding + this.GlobalRegion;
      rectanglef = new Rectanglef(rectanglef.X * drawScaling.X, rectanglef.Y * drawScaling.Y, rectanglef.Width * drawScaling.X, rectanglef.Height * drawScaling.Y);
      batch1.End();
      batch1.Begin(SpriteSortMode.Immediate, rasterizerState: this.rasterizer, transformMatrix: new Matrix?(Resolution.Scale));
      batch1.GraphicsDevice.ScissorRectangle = rectanglef.ToRect();
      foreach (GameObject gameObject in this.ContentContainer.Children.ToArray<GameObject>())
      {
        gameObject.Draw(batch, time);
        if (gameObject is UiControl uiControl)
        {
          int num = this.GlobalRegion.Intersects(gameObject.GlobalRegion - this.Padding) ? 1 : 0;
          uiControl.Enabled = num != 0;
        }
      }
      batch1.GraphicsDevice.ScissorRectangle = scissorRectangle;
      batch1.End();
      batch1.Begin(SpriteSortMode.Immediate, rasterizerState: rasterizerState, transformMatrix: new Matrix?(Resolution.Scale));
    }

    /// <summary>Sliders the on changed value.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">
    /// The <see cref="T:Sharp2D.Engine.Common.UI.Events.SliderChangedEventArgs" /> instance containing the event data.
    /// </param>
    private void HorizontalSliderOnChangedValue(object sender, SliderChangedEventArgs args)
    {
      this.ChangedSliderValue(new Vector2(args.OldValue - args.NewValue, 0.0f));
    }

    /// <summary>Sliders the on changed value.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">
    /// The <see cref="T:Sharp2D.Engine.Common.UI.Events.SliderChangedEventArgs" /> instance containing the event data.
    /// </param>
    private void VerticalSliderOnChangedValue(object sender, SliderChangedEventArgs args)
    {
      this.ChangedSliderValue(new Vector2(0.0f, args.OldValue - args.NewValue));
    }

    /// <summary>
    /// Triggered when the container's children have been modified.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// </exception>
    private void ContainerChildMoved(GameObject sender, ChildObjectMovedArgs args)
    {
      GameObject child = args.Child;
      switch (args.Action)
      {
        case ChildObjectMoveAction.Added:
          child.LocalPosition += this.ContentContainer.ScrolledAmount;
          Vector2 scrolledAmount = this.ContentContainer.ScrolledAmount;
          this.ChangedSliderValue(-scrolledAmount);
          this.RecalculateSize();
          this.ChangedSliderValue(scrolledAmount);
          break;
        case ChildObjectMoveAction.Removed:
          this.RecalculateSize();
          break;
        case ChildObjectMoveAction.Inserted:
          this.RecalculateSize();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    /// <summary>Sets the content container.</summary>
    /// <param name="value">The value.</param>
    private void SetContentContainer(ScrollableViewContainer value)
    {
      if (this.contentContainer != null)
      {
        this.contentContainer.ChildObjectMoved -= new GameObjectMoved(this.ContainerChildMoved);
        base.Remove((GameObject) this.contentContainer);
      }
      this.contentContainer = value;
      if (this.contentContainer != null)
      {
        this.contentContainer.ChildObjectMoved += new GameObjectMoved(this.ContainerChildMoved);
        base.Add((GameObject) this.contentContainer);
      }
      this.RecalculateSize();
    }

    /// <summary>Sets the horizontal slider.</summary>
    /// <param name="value">The value.</param>
    private void SetHorizontalSlider(Slider value)
    {
      if (this.horizontalSlider != null)
        this.horizontalSlider.ChangedValue -= new SliderChangedEventHandler(this.HorizontalSliderOnChangedValue);
      this.horizontalSlider = value;
      this.SetupSlider(this.horizontalSlider, this.LocalRegion);
      if (this.horizontalSlider != null)
        this.horizontalSlider.ChangedValue += new SliderChangedEventHandler(this.HorizontalSliderOnChangedValue);
      this.RecalculateSize();
    }

    /// <summary>Sets the vertical slider.</summary>
    /// <param name="value">The value.</param>
    private void SetVerticalSlider(Slider value)
    {
      if (this.verticalSlider != null)
        this.verticalSlider.ChangedValue -= new SliderChangedEventHandler(this.VerticalSliderOnChangedValue);
      this.verticalSlider = value;
      this.SetupSlider(this.verticalSlider, this.LocalRegion);
      if (this.verticalSlider != null)
        this.verticalSlider.ChangedValue += new SliderChangedEventHandler(this.VerticalSliderOnChangedValue);
      this.RecalculateSize();
    }

    /// <summary>Setups the slider.</summary>
    /// <param name="slider">The slider.</param>
    /// <param name="region">The region.</param>
    private void SetupSlider(Slider slider, Rectanglef region)
    {
      slider.Parent = (GameObject) this;
      if (!(slider.LocalPosition == Vector2.Zero))
        return;
      switch (slider.Direction)
      {
        case Direction.Vertical:
          slider.LocalPosition = new Vector2(region.Width - slider.Width, 0.0f);
          break;
        case Direction.Horizontal:
          slider.LocalPosition = new Vector2(0.0f, region.Height - slider.Height);
          break;
      }
    }
  }
}
