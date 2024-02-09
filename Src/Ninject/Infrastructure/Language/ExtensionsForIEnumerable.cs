// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Language.ExtensionsForIEnumerable
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace Ninject.Infrastructure.Language
{
  internal static class ExtensionsForIEnumerable
  {
    public static IEnumerable CastSlow(this IEnumerable series, Type elementType)
    {
      return typeof (Enumerable).GetMethod("Cast").MakeGenericMethod(elementType).Invoke((object) null, (object[]) new IEnumerable[1]
      {
        series
      }) as IEnumerable;
    }

    public static Array ToArraySlow(this IEnumerable series, Type elementType)
    {
      return typeof (Enumerable).GetMethod("ToArray").MakeGenericMethod(elementType).Invoke((object) null, (object[]) new IEnumerable[1]
      {
        series
      }) as Array;
    }

    public static IList ToListSlow(this IEnumerable series, Type elementType)
    {
      return typeof (Enumerable).GetMethod("ToList").MakeGenericMethod(elementType).Invoke((object) null, (object[]) new IEnumerable[1]
      {
        series
      }) as IList;
    }
  }
}
