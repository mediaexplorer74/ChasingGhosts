// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.DevelopmentMishapException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>The development mishap exception.</summary>
  public class DevelopmentMishapException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.DevelopmentMishapException" /> class.
    /// </summary>
    public DevelopmentMishapException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.DevelopmentMishapException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public DevelopmentMishapException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.DevelopmentMishapException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public DevelopmentMishapException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
