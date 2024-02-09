// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Language.ExtensionsForIEnumerableOfT
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Ninject.Infrastructure.Language
{
  /// <summary>
  /// Provides extension methods for see cref="IEnumerable{T}"/&gt;
  /// </summary>
  public static class ExtensionsForIEnumerableOfT
  {
    /// <summary>
    /// Executes the given action for each of the elements in the enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">The series.</param>
    /// <param name="action">The action.</param>
    public static void Map<T>(this IEnumerable<T> series, Action<T> action)
    {
      foreach (T obj in series)
        action(obj);
    }

    /// <summary>
    /// Converts the given enumerable type to prevent changed on the type behind.
    /// </summary>
    /// <typeparam name="T">The type of the enumerable.</typeparam>
    /// <param name="series">The series.</param>
    /// <returns>The input type as real enumerable not castable to the original type.</returns>
    public static IEnumerable<T> ToEnumerable<T>(this IEnumerable<T> series)
    {
      return series.Select<T, T>((Func<T, T>) (x => x));
    }
  }
}
