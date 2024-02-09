﻿// Decompiled with JetBrains decompiler
// Type: Ninject.Injection.DynamicMethodInjectorFactory
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using Ninject.Components;
using System;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace Ninject.Injection
{
  /// <summary>Creates injectors for members via DynamicMethods.</summary>
  public class DynamicMethodInjectorFactory : 
    NinjectComponent,
    IInjectorFactory,
    INinjectComponent,
    IDisposable
  {
    /// <summary>
    /// Gets or creates an injector for the specified constructor.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <returns>The created injector.</returns>
    public ConstructorInjector Create(ConstructorInfo constructor)
    {
      DynamicMethod dynamicMethod = new DynamicMethod(DynamicMethodInjectorFactory.GetAnonymousMethodName(), typeof (object), new Type[1]
      {
        typeof (object[])
      }, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      DynamicMethodInjectorFactory.EmitLoadMethodArguments(ilGenerator, (MethodBase) constructor);
      ilGenerator.Emit(OpCodes.Newobj, constructor);
      if (constructor.ReflectedType.IsValueType)
        ilGenerator.Emit(OpCodes.Box, constructor.ReflectedType);
      ilGenerator.Emit(OpCodes.Ret);
      return (ConstructorInjector) dynamicMethod.CreateDelegate(typeof (ConstructorInjector));
    }

    /// <summary>
    /// Gets or creates an injector for the specified property.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The created injector.</returns>
    public PropertyInjector Create(PropertyInfo property)
    {
      DynamicMethod dynamicMethod = new DynamicMethod(DynamicMethodInjectorFactory.GetAnonymousMethodName(), typeof (void), new Type[2]
      {
        typeof (object),
        typeof (object)
      }, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      DynamicMethodInjectorFactory.EmitUnboxOrCast(ilGenerator, property.DeclaringType);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      DynamicMethodInjectorFactory.EmitUnboxOrCast(ilGenerator, property.PropertyType);
      bool injectNonPublic = this.Settings.InjectNonPublic;
      DynamicMethodInjectorFactory.EmitMethodCall(ilGenerator, property.GetSetMethod(injectNonPublic));
      ilGenerator.Emit(OpCodes.Ret);
      return (PropertyInjector) dynamicMethod.CreateDelegate(typeof (PropertyInjector));
    }

    /// <summary>Gets or creates an injector for the specified method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>The created injector.</returns>
    public MethodInjector Create(MethodInfo method)
    {
      DynamicMethod dynamicMethod = new DynamicMethod(DynamicMethodInjectorFactory.GetAnonymousMethodName(), typeof (void), new Type[2]
      {
        typeof (object),
        typeof (object[])
      }, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      DynamicMethodInjectorFactory.EmitUnboxOrCast(ilGenerator, method.DeclaringType);
      DynamicMethodInjectorFactory.EmitLoadMethodArguments(ilGenerator, (MethodBase) method);
      DynamicMethodInjectorFactory.EmitMethodCall(ilGenerator, method);
      if (method.ReturnType != typeof (void))
        ilGenerator.Emit(OpCodes.Pop);
      ilGenerator.Emit(OpCodes.Ret);
      return (MethodInjector) dynamicMethod.CreateDelegate(typeof (MethodInjector));
    }

    private static void EmitLoadMethodArguments(ILGenerator il, MethodBase targetMethod)
    {
      ParameterInfo[] parameters = targetMethod.GetParameters();
      OpCode opcode = (object) (targetMethod as ConstructorInfo) != null ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1;
      for (int index = 0; index < parameters.Length; ++index)
      {
        il.Emit(opcode);
        il.Emit(OpCodes.Ldc_I4, index);
        il.Emit(OpCodes.Ldelem_Ref);
        DynamicMethodInjectorFactory.EmitUnboxOrCast(il, parameters[index].ParameterType);
      }
    }

    private static void EmitMethodCall(ILGenerator il, MethodInfo method)
    {
      OpCode opcode = method.IsFinal ? OpCodes.Call : OpCodes.Callvirt;
      il.Emit(opcode, method);
    }

    private static void EmitUnboxOrCast(ILGenerator il, Type type)
    {
      OpCode opcode = type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass;
      il.Emit(opcode, type);
    }

    private static string GetAnonymousMethodName()
    {
      return "DynamicInjector" + Guid.NewGuid().ToString("N");
    }
  }
}
