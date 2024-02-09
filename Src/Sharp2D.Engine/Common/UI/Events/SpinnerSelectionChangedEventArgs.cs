// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Events.SpinnerSelectionChangedEventArgs
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Events
{
  /// <summary>The spinner selection changed event args.</summary>
  public class SpinnerSelectionChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Events.SpinnerSelectionChangedEventArgs" /> class.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="oldIndex">The old index.</param>
    /// <param name="value">The value.</param>
    internal SpinnerSelectionChangedEventArgs(int index, int oldIndex, object value)
    {
      this.Index = index;
      this.OldIndex = oldIndex;
      this.Value = value;
    }

    /// <summary>Gets the index.</summary>
    public int Index { get; private set; }

    /// <summary>Gets the old index.</summary>
    public int OldIndex { get; private set; }

    /// <summary>Gets the value.</summary>
    public object Value { get; private set; }
  }
}
