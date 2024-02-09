// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.InvalidGameObjectHierachyException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>
  ///     Occurs when attempting to add a parent as a child of it's own child.
  /// </summary>
  public class InvalidGameObjectHierachyException : InvalidOperationException
  {
    /// <summary>The _message.</summary>
    private new const string _message = "A parent cannot be a child of it's own (grand)children.\r\nTo put it in another way: A father cannot be the son of his own son. Damn that's some wise-ass shit.";

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.InvalidGameObjectHierachyException" /> class.
    /// </summary>
    public InvalidGameObjectHierachyException()
      : base("A parent cannot be a child of it's own (grand)children.\r\nTo put it in another way: A father cannot be the son of his own son. Damn that's some wise-ass shit.")
    {
    }
  }
}
