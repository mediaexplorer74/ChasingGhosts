// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.UI.Events.TextChangedEventArgs
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.UI.Events
{
  /// <summary>Text Changed Event Args.</summary>
  public class TextChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.UI.Events.TextChangedEventArgs" /> class.
    /// </summary>
    /// <param name="oldText">The old text.</param>
    /// <param name="newText">The new text.</param>
    public TextChangedEventArgs(string oldText, string newText)
    {
      this.NewText = newText;
      this.OldText = oldText;
      this.UpdatePositioning = true;
    }

    /// <summary>Gets the new text.</summary>
    /// <value>The new text.</value>
    public string NewText { get; private set; }

    /// <summary>Gets the old text.</summary>
    /// <value>The old text.</value>
    public string OldText { get; private set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the internal code should automatically update the position in the textbox.
    /// </summary>
    /// <value>
    ///     <c>true</c> if it should update positioning; otherwise, <c>false</c>.
    /// </value>
    public bool UpdatePositioning { get; set; }
  }
}
