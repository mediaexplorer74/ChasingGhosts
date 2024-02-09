// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.WorldAlreadyContainsBodyException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>The world already contains body exception.</summary>
  public class WorldAlreadyContainsBodyException : Exception
  {
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.WorldAlreadyContainsBodyException" /> class.
    /// </summary>
    public WorldAlreadyContainsBodyException()
      : base("This body already exists in the world! A single body cannot exist several places at the same time.\nBasic logic u.u")
    {
    }
  }
}
