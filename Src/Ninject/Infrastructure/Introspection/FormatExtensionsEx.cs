// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Introspection.FormatExtensionsEx
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Activation;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

#nullable disable
namespace Ninject.Infrastructure.Introspection
{
  /// <summary>Provides extension methods for string formatting</summary>
  public static class FormatExtensionsEx
  {
    /// <summary>
    /// Formats the activation path into a meaningful string representation.
    /// </summary>
    /// <param name="request">The request to be formatted.</param>
    /// <returns>The activation path formatted as string.</returns>
    public static string FormatActivationPath(this IRequest request)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        for (IRequest request1 = request; request1 != null; request1 = request1.ParentRequest)
          stringWriter.WriteLine("{0,3}) {1}", (object) (request1.Depth + 1), (object) request1.Format());
        return stringWriter.ToString();
      }
    }

    /// <summary>
    /// Formats the given binding into a meaningful string representation.
    /// </summary>
    /// <param name="binding">The binding to be formatted.</param>
    /// <param name="context">The context.</param>
    /// <returns>The binding formatted as string</returns>
    public static string Format(this IBinding binding, IContext context)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        if (binding.Condition != null)
          stringWriter.Write("conditional ");
        if (binding.IsImplicit)
          stringWriter.Write("implicit ");
        IProvider provider = binding.GetProvider(context);
        switch (binding.Target)
        {
          case BindingTarget.Self:
            stringWriter.Write("self-binding of {0}", (object) binding.Service.Format());
            break;
          case BindingTarget.Type:
            stringWriter.Write("binding from {0} to {1}", (object) binding.Service.Format(), (object) provider.Type.Format());
            break;
          case BindingTarget.Provider:
            stringWriter.Write("provider binding from {0} to {1} (via {2})", (object) binding.Service.Format(), (object) provider.Type.Format(), (object) provider.GetType().Format());
            break;
          case BindingTarget.Method:
            stringWriter.Write("binding from {0} to method", (object) binding.Service.Format());
            break;
          case BindingTarget.Constant:
            stringWriter.Write("binding from {0} to constant value", (object) binding.Service.Format());
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        return stringWriter.ToString();
      }
    }

    /// <summary>
    /// Formats the specified request into a meaningful string representation.
    /// </summary>
    /// <param name="request">The request to be formatted.</param>
    /// <returns>The request formatted as string.</returns>
    public static string Format(this IRequest request)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        if (request.Target == null)
          stringWriter.Write("Request for {0}", (object) request.Service.Format());
        else
          stringWriter.Write("Injection of dependency {0} into {1}", (object) request.Service.Format(), (object) request.Target.Format());
        return stringWriter.ToString();
      }
    }

    /// <summary>
    /// Formats the specified target into a meaningful string representation..
    /// </summary>
    /// <param name="target">The target to be formatted.</param>
    /// <returns>The target formatted as string.</returns>
    public static string Format(this ITarget target)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        switch (target.Member.MemberType)
        {
          case MemberTypes.Constructor:
            stringWriter.Write("parameter {0} of constructor", (object) target.Name);
            break;
          case MemberTypes.Method:
            stringWriter.Write("parameter {0} of method {1}", (object) target.Name, (object) target.Member.Name);
            break;
          case MemberTypes.Property:
            stringWriter.Write("property {0}", (object) target.Name);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        stringWriter.Write(" of type {0}", (object) target.Member.ReflectedType.Format());
        return stringWriter.ToString();
      }
    }

    /// <summary>
    /// Formats the specified type into a meaningful string representation..
    /// </summary>
    /// <param name="type">The type to be formatted.</param>
    /// <returns>The type formatted as string.</returns>
    public static string Format(this Type type)
    {
      string friendlyName = FormatExtensionsEx.GetFriendlyName(type);
      if (friendlyName.Contains("AnonymousType"))
        return "AnonymousType";
      switch (friendlyName.ToLower(CultureInfo.InvariantCulture))
      {
        case "int16":
          return "short";
        case "int32":
          return "int";
        case "int64":
          return "long";
        case "string":
          return "string";
        case "object":
          return "object";
        case "boolean":
          return "bool";
        case "void":
          return "void";
        case "char":
          return "char";
        case "byte":
          return "byte";
        case "uint16":
          return "ushort";
        case "uint32":
          return "uint";
        case "uint64":
          return "ulong";
        case "sbyte":
          return "sbyte";
        case "single":
          return "float";
        case "double":
          return "double";
        case "decimal":
          return "decimal";
        default:
          Type[] genericArguments = type.GetGenericArguments();
          return genericArguments.Length > 0 ? FormatExtensionsEx.FormatGenericType(friendlyName, genericArguments) : friendlyName;
      }
    }

    private static string GetFriendlyName(Type type)
    {
      string friendlyName = type.FullName ?? type.Name;
      int length1 = friendlyName.IndexOf('[');
      if (length1 > 0)
        friendlyName = friendlyName.Substring(0, length1);
      int length2 = friendlyName.IndexOf(',');
      if (length2 > 0)
        friendlyName = friendlyName.Substring(0, length2);
      int num = friendlyName.LastIndexOf('.');
      if (num >= 0)
        friendlyName = friendlyName.Substring(num + 1);
      return friendlyName;
    }

    private static string FormatGenericType(string friendlyName, Type[] genericArguments)
    {
      StringBuilder sb = new StringBuilder(friendlyName.Length + 10);
      int start = 0;
      int startIndex = 0;
      for (int index = 0; index < friendlyName.Length; ++index)
      {
        if (friendlyName[index] == '`')
        {
          int count = (int) friendlyName[index + 1] - 48;
          sb.Append(friendlyName.Substring(startIndex, index - startIndex));
          FormatExtensionsEx.AppendGenericArguments(sb, genericArguments, start, count);
          start += count;
          startIndex = index + 2;
        }
      }
      if (startIndex < friendlyName.Length)
        sb.Append(friendlyName.Substring(startIndex));
      return sb.ToString();
    }

    private static void AppendGenericArguments(
      StringBuilder sb,
      Type[] genericArguments,
      int start,
      int count)
    {
      sb.Append("{");
      for (int index = 0; index < count; ++index)
      {
        if (index != 0)
          sb.Append(", ");
        sb.Append(genericArguments[start + index].Format());
      }
      sb.Append("}");
    }
  }
}
