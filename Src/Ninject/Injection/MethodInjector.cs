// Decompiled with JetBrains decompiler
// Type: Ninject.Injection.MethodInjector
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

#nullable disable
namespace Ninject.Injection
{
  /// <summary>A delegate that can inject values into a method.</summary>
  public delegate void MethodInjector(object target, params object[] arguments);
}
