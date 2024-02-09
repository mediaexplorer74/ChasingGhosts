// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Language.ExtensionsForTargetInvocationException
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Reflection;

#nullable disable
namespace Ninject.Infrastructure.Language
{
  internal static class ExtensionsForTargetInvocationException
  {
    public static void RethrowInnerException(this TargetInvocationException exception)
    {
      Exception innerException = exception.InnerException;
      typeof (Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) innerException, (object) innerException.StackTrace);
      throw innerException;
    }
  }
}
