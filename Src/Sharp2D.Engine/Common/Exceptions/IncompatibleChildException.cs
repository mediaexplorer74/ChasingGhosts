// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.IncompatibleChildException
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>
  ///     An exception that is thrown when attempting to add an object to a collection that allows the specific object in
  ///     terms of syntax, but not runtime.
  /// </summary>
  public class IncompatibleChildException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.IncompatibleChildException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="variableName">Name of the variable.</param>
    public IncompatibleChildException(string message, string variableName)
      : base(message)
    {
      this.VariableName = variableName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.IncompatibleChildException" /> class.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    public IncompatibleChildException(string variableName) => this.VariableName = variableName;

    /// <summary>
    /// Gets or sets the name of the variable that caused this exception.
    /// </summary>
    /// <value>The name of the variable.</value>
    public string VariableName { get; set; }
  }
}
