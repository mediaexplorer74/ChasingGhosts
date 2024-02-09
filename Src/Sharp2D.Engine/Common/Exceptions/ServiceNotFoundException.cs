// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Exceptions.ServiceNotFoundException`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Exceptions
{
  /// <summary>Service Not Found exception.</summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class ServiceNotFoundException<T> : Exception
  {
    /// <summary>The _message.</summary>
    private new const string _message = "An instance of type {0} not found in the service container. Did you forget to register it?";

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Exceptions.ServiceNotFoundException`1" /> class.
    /// </summary>
    public ServiceNotFoundException()
      : base(string.Format("An instance of type {0} not found in the service container. Did you forget to register it?", new object[1]
      {
        (object) typeof (T).Name
      }))
    {
    }
  }
}
