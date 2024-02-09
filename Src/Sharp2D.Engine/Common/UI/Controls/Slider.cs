// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Controls.Slider
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Events;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Infrastructure.Input;
using Sharp2D.Engine.Utility;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Controls
{
  /// <summary>The slider.</summary>
  public class Slider : InteractableUiControl
  {
    /// <summary>The _current value.</summary>
    private float currentValue;
    /// <summary>The _slider button.</summary>
    private Button sliderButton;
    /// <summary>The _slider offset.</summary>
    private Vector2 sliderOffset;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Slider" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="sprite">The Sprite.</param>
    /// <param name="sliderButton">The slider button.</param>
    /// <param name="depth">The height.</param>
    /// <param name="direction">The direction for the slider to slide.</param>
    /// <param name="minValue">The minimum value on the slider.</param>
    /// <param name="startValue">The start value on the slider.</param>
    /// <param name="maxValue">The maximum value on the slider.</param>
    /// <param name="children">The children.</param>
    public Slider(
      Vector2 position,
      Sprite sprite,
      Button sliderButton,
      int depth,
      Direction direction = Direction.Vertical,
      int minValue = 0,
      int startValue = 0,
      int maxValue = 100,
      params GameObject[] children)
      : base(position, sprite, children)
    {
      this.SliderButton = sliderButton;
      this.MinValue = minValue;
      this.MaxValue = maxValue;
      this.Depth = depth;
      this.Direction = direction;
      this.CurrentValue = (float) startValue;
      this.Add((GameObject) this.SliderButton);
      this.CalculateSliderValue();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Slider" /> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="spriteSheet">The Sprite sheet.</param>
    /// <param name="sliderButton">The slider button.</param>
    /// <param name="depth">The height.</param>
    /// <param name="direction">The direction.</param>
    /// <param name="minValue">The minimum value on the slider.</param>
    /// <param name="startValue">The start value on the slider.</param>
    /// <param name="maxValue">The maximum value on the slider.</param>
    /// <param name="children">The children.</param>
    public Slider(
      Vector2 position,
      Sharp2D.Engine.Common.Components.Sprites.SpriteSheet<string> spriteSheet,
      Button sliderButton,
      int depth,
      Direction direction = Direction.Vertical,
      int minValue = 0,
      int startValue = 0,
      int maxValue = 100,
      params GameObject[] children)
      : base(position, spriteSheet, children)
    {
      this.SliderButton = sliderButton;
      this.MinValue = minValue;
      this.MaxValue = maxValue;
      this.Depth = depth;
      this.Direction = direction;
      this.CurrentValue = (float) startValue;
      this.Add((GameObject) this.SliderButton);
      this.CalculateSliderValue();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Controls.Slider" /> class.
    /// </summary>
    public Slider()
    {
    }

    /// <summary>Occurs when the value is changed.</summary>
    public event SliderChangedEventHandler ChangedValue;

    /// <summary>Gets or sets the current value.</summary>
    /// <value>The current value.</value>
    public float CurrentValue
    {
      get => this.currentValue;
      set
      {
        if ((double) value > (double) this.MaxValue)
          value = (float) this.MaxValue;
        if ((double) value < (double) this.MinValue)
          value = (float) this.MinValue;
        float currentValue = this.currentValue;
        this.currentValue = value;
        if ((double) currentValue == (double) value)
          return;
        this.OnChangedValue(new SliderChangedEventArgs(currentValue, value));
      }
    }

    /// <summary>
    ///     Gets the depth.
    ///     <para>
    ///         The depth is the length of the slider. Usually the Height when it is Vertical, and the Width when it's
    ///         Horizontal.
    ///     </para>
    /// </summary>
    /// <value>The depth.</value>
    public int Depth { get; set; }

    /// <summary>
    ///     Gets or sets the direction the slider is sliding..
    /// </summary>
    /// <value>The direction.</value>
    public Direction Direction { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the control is enabled (Accepts user input).
    /// </summary>
    /// <value>
    ///     <c>true</c> if enabled; otherwise, <c>false</c>.
    /// </value>
    public override bool Enabled
    {
      get => base.Enabled && this.SliderButton != null && this.SliderButton.Enabled;
      set
      {
        base.Enabled = value;
        if (this.SliderButton == null)
          return;
        this.SliderButton.Enabled = value;
      }
    }

    /// <summary>
    ///     Gets a value indicating whether it is currently sliding.
    /// </summary>
    /// <value>
    ///     <c>true</c> if currently sliding; otherwise, <c>false</c>.
    /// </value>
    public bool IsSliding { get; private set; }

    /// <summary>Gets or sets the maximum value.</summary>
    /// <value>The maximum value.</value>
    public int MaxValue { get; set; }

    /// <summary>Gets or sets the minimum value.</summary>
    /// <value>The minimum value.</value>
    public int MinValue { get; set; }

    /// <summary>Gets or sets the slider button.</summary>
    /// <value>The slider button.</value>
    public Button SliderButton
    {
      get => this.sliderButton;
      set
      {
        this.sliderButton = value;
        if (value == null)
          return;
        this.StoreSliderOffset(value);
      }
    }

    /// <summary>
    ///     Forces an update of the slider buttons position.
    ///     <para>
    ///         This is automatically being invoked every time you change the value,
    ///         so there's no reason to do so yourself, but feel free to anyway. Yeah, we're nice like that.
    ///     </para>
    /// </summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    public void CalculateSliderPosition()
    {
      float percentage = SharpMathHelper.GetPercentage((float) this.MinValue, this.CurrentValue, (float) this.MaxValue);
      float depth = (float) this.Depth;
      Vector2 localPosition = this.SliderButton.LocalPosition;
      float min;
      switch (this.Direction)
      {
        case Direction.Vertical:
          min = this.sliderOffset.Y;
          break;
        case Direction.Horizontal:
          min = this.sliderOffset.X;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      SharpMathHelper.SetPercentage(percentage, min, depth, out localPosition.Y);
      this.SliderButton.LocalPosition = localPosition;
    }

    /// <summary>
    /// Changes the value, and also updates the sliders current position to reflect <see cref="P:Sharp2D.Engine.Common.UI.Controls.Slider.CurrentValue" />.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void ChangeValue(float amount)
    {
      this.CurrentValue += amount;
      this.CalculateSliderPosition();
    }

    /// <summary>
    /// Updates this control, and runs the update method on all children. Also controls whether it is sliding (the slider
    ///     is currently being moved by the mouse).
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      if (this.SliderButton.InteractionState == UiControlInteractionState.Down)
        this.IsSliding = true;
      if (this.IsSliding)
        this.PerformSliding();
      base.Update(time);
    }

    /// <summary>Calculates the slider value.</summary>
    /// <summary>
    /// Raises the <see cref="E:ChangedValue" /> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="T:Sharp2D.Engine.Common.UI.Events.SliderChangedEventArgs" /> instance containing the event data.
    /// </param>
    protected virtual void OnChangedValue(SliderChangedEventArgs e)
    {
      SliderChangedEventHandler changedValue = this.ChangedValue;
      if (changedValue == null)
        return;
      changedValue((object) this, e);
    }

    /// <summary>Calculates the slider value.</summary>
    private void CalculateSliderValue()
    {
      float min = this.Direction == Direction.Vertical ? this.sliderOffset.Y : this.sliderOffset.X;
      float depth = (float) this.Depth;
      float current = this.Direction == Direction.Vertical ? this.SliderButton.LocalPosition.Y : this.SliderButton.LocalPosition.X;
      float percentage = SharpMathHelper.GetPercentage(min, current, depth);
      float num;
      if ((double) percentage != 0.0)
        SharpMathHelper.SetPercentage(percentage, (float) this.MinValue, (float) this.MaxValue, out num);
      else
        num = 0.0f;
      this.CurrentValue = num;
    }

    /// <summary>Calculates and performs all the sliding logic.</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
    private void PerformSliding()
    {
      if (InputManager.Mouse.LeftButton == ButtonState.Released)
        this.IsSliding = false;
      Vector2 vector2_1 = InputManager.MousePosition - this.SliderButton.GlobalPosition;
      switch (this.Direction)
      {
        case Direction.Vertical:
          float num1 = Math.Max(this.SliderButton.LocalPosition.Y + vector2_1.Y - this.sliderOffset.Y, 0.0f);
          Vector2 vector2_2 = new Vector2(this.SliderButton.LocalPosition.X, (float) this.Depth);
          Vector2 vector2_3 = new Vector2(this.SliderButton.LocalPosition.X, this.sliderOffset.Y + num1);
          this.SliderButton.LocalPosition = (double) this.sliderOffset.Y + (double) num1 >= (double) this.Depth ? vector2_2 : vector2_3;
          break;
        case Direction.Horizontal:
          float num2 = Math.Max(this.SliderButton.LocalPosition.X + vector2_1.X - this.sliderOffset.X, 0.0f);
          Vector2 vector2_4 = new Vector2((float) this.Depth, this.SliderButton.LocalPosition.Y);
          Vector2 vector2_5 = new Vector2(this.sliderOffset.X + num2, this.SliderButton.LocalPosition.Y);
          this.SliderButton.LocalPosition = (double) this.sliderOffset.X + (double) num2 >= (double) this.Depth ? vector2_4 : vector2_5;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.CalculateSliderValue();
    }

    /// <summary>The store slider offset.</summary>
    /// <param name="button">The button.</param>
    private void StoreSliderOffset(Button button) => this.sliderOffset = button.LocalPosition;
  }
}
