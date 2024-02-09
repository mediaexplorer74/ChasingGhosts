// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Language.ExtensionsForICustomAttributeProvider
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System;
using System.Reflection;

#nullable disable
namespace Ninject.Infrastructure.Language
{
  internal static class ExtensionsForICustomAttributeProvider
  {
    public static bool HasAttribute(this ICustomAttributeProvider member, Type type)
    {
      MemberInfo member1 = member as MemberInfo;
      return member1 != (MemberInfo) null ? ExtensionsForMemberInfo.HasAttribute(member1, type) : member.IsDefined(type, true);
    }

    public static object[] GetCustomAttributesExtended(
      this ICustomAttributeProvider member,
      Type attributeType,
      bool inherit)
    {
      MemberInfo member1 = member as MemberInfo;
      return member1 != (MemberInfo) null ? ExtensionsForMemberInfo.GetCustomAttributesExtended(member1, attributeType, inherit) : member.GetCustomAttributes(attributeType, inherit);
    }
  }
}
