// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Events.SliderChangedEventArgs
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Events
{
  /// <summary>Slider Changed Event Args</summary>
  public class SliderChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Events.SliderChangedEventArgs" /> class.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    public SliderChangedEventArgs(float oldValue, float newValue)
    {
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }

    /// <summary>Gets the new value.</summary>
    /// <value>The new value.</value>
    public float NewValue { get; private set; }

    /// <summary>Gets the old value.</summary>
    /// <value>The old value.</value>
    public float OldValue { get; private set; }
  }
}
