// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.ObjectSystem.ChildComponentMovedArgs
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.Components;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.ObjectSystem
{
  /// <summary>Child Component Moved event arguments.</summary>
  public class ChildComponentMovedArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjectMovedArgs" /> class.
    /// </summary>
    /// <param name="child">The child.</param>
    /// <param name="newParent">The new parent.</param>
    /// <param name="oldParent">The old parent.</param>
    /// <param name="action">The action.</param>
    /// <param name="index">
    /// </param>
    public ChildComponentMovedArgs(
      Component child,
      GameObject newParent,
      GameObject oldParent,
      ChildObjectMoveAction action,
      int index = -1)
    {
      this.Child = child ?? throw new ArgumentNullException(nameof (child));
      this.NewParent = newParent;
      this.OldParent = oldParent;
      this.Action = action;
      this.Index = index;
    }

    /// <summary>Gets the move operation's action.</summary>
    /// <value>The action.</value>
    public ChildObjectMoveAction Action { get; private set; }

    /// <summary>Gets the child that was moved.</summary>
    /// <value>The child.</value>
    public Component Child { get; private set; }

    /// <summary>
    ///     Gets or sets the index of
    ///     the <see cref="F:Sharp2D.Engine.Common.ObjectSystem.ChildObjectMoveAction.Inserted" /> <see cref="P:Sharp2D.Engine.Common.ObjectSystem.ChildComponentMovedArgs.Child" />
    ///     in <see cref="P:Sharp2D.Engine.Common.ObjectSystem.ChildComponentMovedArgs.NewParent" />.
    /// </summary>
    /// <value>
    ///     The index. Will be -1 if <see cref="P:Sharp2D.Engine.Common.ObjectSystem.ChildComponentMovedArgs.Action" /> is not <see cref="F:Sharp2D.Engine.Common.ObjectSystem.ChildObjectMoveAction.Inserted" />.
    /// </value>
    public int Index { get; set; }

    /// <summary>Gets or sets the new parent.</summary>
    /// <value>The new parent.</value>
    public GameObject NewParent { get; set; }

    /// <summary>Gets or sets the old parent.</summary>
    /// <value>The old parent.</value>
    public GameObject OldParent { get; set; }
  }
}
