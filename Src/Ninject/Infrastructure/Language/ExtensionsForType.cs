// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Language.ExtensionsForType
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Infrastructure.Language
{
  /// <summary>Extension methods for type</summary>
  /// <remarks></remarks>
  public static class ExtensionsForType
  {
    /// <summary>
    /// Gets an enumerable containing the given type and all its base types
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>An enumerable containing the given type and all its base types</returns>
    public static IEnumerable<Type> GetAllBaseTypes(this Type type)
    {
      for (; type != (Type) null; type = type.BaseType)
        yield return type;
    }
  }
}
