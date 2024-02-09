// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Utility.Pools`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Drawing.Utility
{
  internal static class Pools<T> where T : new()
  {
    private static readonly Sharp2D.Engine.Drawing.Utility.Pool<T> _pool = new Sharp2D.Engine.Drawing.Utility.Pool<T>();

    public static Sharp2D.Engine.Drawing.Utility.Pool<T> Pool => Pools<T>._pool;

    public static T Obtain()
    {
      lock (Pools<T>._pool)
        return Pools<T>._pool.Obtain();
    }

    public static void Release(T obj)
    {
      lock (Pools<T>._pool)
        Pools<T>._pool.Release(obj);
    }

    public static void Release(IList<T> objects)
    {
      lock (Pools<T>._pool)
        Pools<T>._pool.Release(objects);
    }
  }
}
