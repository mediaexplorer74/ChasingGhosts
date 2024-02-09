// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Utility.Buffer`1
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Drawing.Utility
{
  internal class Buffer<T> : IPoolable
  {
    private T[] _buffer;
    private int _index;

    public Buffer()
      : this(0)
    {
    }

    public Buffer(int initialCapacity) => this._buffer = new T[initialCapacity];

    public Buffer(T[] data) => this._buffer = data;

    public T this[int index]
    {
      get => this._buffer[index];
      set => this._buffer[index] = value;
    }

    public T[] Data => this._buffer;

    public int Capacity => this._buffer.Length;

    public int Index
    {
      get => this._index;
      set => this._index = value;
    }

    public void SetNext(T value) => this._buffer[this._index++] = value;

    public void EnsureCapacity(int capacity)
    {
      if (capacity <= this._buffer.Length)
        return;
      capacity = 1 << (int) Math.Ceiling(Math.Log((double) capacity, 2.0));
      T[] destinationArray = new T[capacity];
      Array.Copy((Array) this._buffer, (Array) destinationArray, this._buffer.Length);
      this._buffer = destinationArray;
    }

    public void Reset() => this._index = 0;
  }
}
