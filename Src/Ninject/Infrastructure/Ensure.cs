// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Ensure
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;

#nullable disable
namespace Ninject.Infrastructure
{
  internal static class Ensure
  {
    public static void ArgumentNotNull(object argument, string name)
    {
      if (argument == null)
        throw new ArgumentNullException(name, "Cannot be null");
    }

    public static void ArgumentNotNullOrEmpty(string argument, string name)
    {
      if (string.IsNullOrEmpty(argument))
        throw new ArgumentException("Cannot be null or empty", name);
    }
  }
}
