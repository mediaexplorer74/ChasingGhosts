// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.Keyboard.CharacterEventArgs
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using System;

#nullable disable
namespace Sharp2D.Windows.Keyboard
{
  /// <summary>Character Event Arguments</summary>
  public class CharacterEventArgs : EventArgs
  {
    private readonly char character;
    private readonly int lParam;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Windows.Keyboard.CharacterEventArgs" /> class.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="lParam">The l parameter.</param>
    public CharacterEventArgs(char character, int lParam)
    {
      this.character = character;
      this.lParam = lParam;
    }

    /// <summary>Gets the character.</summary>
    /// <value>The character.</value>
    public char Character => this.character;

    /// <summary>Gets the parameter.</summary>
    /// <value>The parameter.</value>
    public int Param => this.lParam;

    /// <summary>Gets the repeat count.</summary>
    /// <value>The repeat count.</value>
    public int RepeatCount => this.lParam & (int) ushort.MaxValue;

    /// <summary>Gets a value indicating whether [extended key].</summary>
    /// <value>
    ///   <c>true</c> if [extended key]; otherwise, <c>false</c>.
    /// </value>
    public bool ExtendedKey => (this.lParam & 16777216) > 0;

    /// <summary>Gets a value indicating whether [alt pressed].</summary>
    /// <value>
    ///   <c>true</c> if [alt pressed]; otherwise, <c>false</c>.
    /// </value>
    public bool AltPressed => (this.lParam & 536870912) > 0;

    /// <summary>Gets a value indicating whether [previous state].</summary>
    /// <value>
    ///   <c>true</c> if [previous state]; otherwise, <c>false</c>.
    /// </value>
    public bool PreviousState => (this.lParam & 1073741824) > 0;

    /// <summary>Gets a value indicating whether [transition state].</summary>
    /// <value>
    ///   <c>true</c> if [transition state]; otherwise, <c>false</c>.
    /// </value>
    public bool TransitionState => (this.lParam & int.MinValue) > 0;
  }
}
