// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.InvalidGameComponentHierarchyException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>The invalid game component hierarchy exception.</summary>
  public class InvalidGameComponentHierarchyException : InvalidOperationException
  {
    /// <summary>The _message.</summary>
    private new const string _message = "A GameObject's Components cannot contain more than one reference to a single instance of a Component!";

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.InvalidGameComponentHierarchyException" /> class.
    /// </summary>
    public InvalidGameComponentHierarchyException()
      : base("A GameObject's Components cannot contain more than one reference to a single instance of a Component!")
    {
    }
  }
}
