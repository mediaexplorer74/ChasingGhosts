// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.MissingParentException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Components
{
  /// <summary>
  ///     Thrown in the event that a component does not have a Parent.
  /// </summary>
  public class MissingParentException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.MissingParentException" /> class.
    /// </summary>
    /// <param name="type">The type.</param>
    public MissingParentException(string type)
      : base(string.Format("ALL Sharp2D Game Components must have a parent! This component of type '{0}' does NOT have one.", new object[1]
      {
        (object) type
      }))
    {
    }
  }
}
