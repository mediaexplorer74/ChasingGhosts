// Decompiled with JetBrains decompiler
// Type: Ninject.ActivationException
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject
{
  /// <summary>
  /// Indicates that an error occured during activation of an instance.
  /// </summary>
  public class ActivationException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.ActivationException" /> class.
    /// </summary>
    public ActivationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.ActivationException" /> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public ActivationException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ninject.ActivationException" /> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ActivationException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
