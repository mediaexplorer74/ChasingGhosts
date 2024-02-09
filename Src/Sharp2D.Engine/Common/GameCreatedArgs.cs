// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.GameCreatedArgs
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common
{
  /// <summary>Game Created event args.</summary>
  public class GameCreatedArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.GameCreatedArgs" /> class.
    /// </summary>
    /// <param name="game">The game.</param>
    public GameCreatedArgs(SharpGameManager game) => this.GameManager = game;

    /// <summary>Gets or sets the game.</summary>
    /// <value>The game.</value>
    public SharpGameManager GameManager { get; set; }
  }
}
