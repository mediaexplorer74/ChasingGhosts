// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.ObjectSystem.GameObjectMoved
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Common.ObjectSystem
{
  /// <summary>
  ///     Delegate used for notifying of a game object hierachy change.
  /// </summary>
  /// <param name="sender">The game object that is the parent of the child that was moved.</param>
  /// <param name="args">The arguments - contains information about the action as well as the new parent.</param>
  public delegate void GameObjectMoved(GameObject sender, ChildObjectMovedArgs args);
}
