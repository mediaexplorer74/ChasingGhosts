// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Utility.Pool`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Drawing.Utility
{
  internal sealed class Pool<T> : Pool where T : new()
  {
    private Stack<T> _free;

    public Pool()
      : this(16, int.MaxValue)
    {
    }

    public Pool(int initialCapacity)
      : this(initialCapacity, int.MaxValue)
    {
    }

    public Pool(int initialCapacity, int max)
    {
      this.MaxReserve = max;
      this._free = default;//new Stack<T>(initialCapacity);
    }

    public override int MaxReserve { get; set; }

    public override int Peak { get; set; }

    public override int Count => this._free.Count;

    public T Obtain() => this._free.Count == 0 ? new T() : this._free.Pop();

    protected override void ReleaseCore(object obj)
    {
      if (!(obj is T obj1))
        return;
      this.Release(obj1);
    }

    public void Release(T obj)
    {
      if ((object) obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (this._free.Count < this.MaxReserve)
      {
        this._free.Push(obj);
        this.Peak = Math.Max(this.Peak, this._free.Count);
      }
      if (!((object) obj is IPoolable))
        return;
      ((object) obj as IPoolable).Reset();
    }

    public void Release(IList<T> objects)
    {
      if (objects == null)
        throw new ArgumentNullException(nameof (objects));
      foreach (T obj in (IEnumerable<T>) objects)
      {
        if ((object) obj != null)
        {
          if (this._free.Count < this.MaxReserve)
            this._free.Push(obj);
          if ((object) obj is IPoolable)
            ((object) obj as IPoolable).Reset();
        }
      }
      this.Peak = Math.Max(this.Peak, this._free.Count);
    }

    public override void Clear()
    {
      this._free.Clear();
      this.Peak = 0;
    }
  }
}
