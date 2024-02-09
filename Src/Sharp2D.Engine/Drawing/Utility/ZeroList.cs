// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Drawing.Utility.ZeroList
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Drawing.Utility
{
  internal class ZeroList : IList<float>, ICollection<float>, IEnumerable<float>, IEnumerable
  {
    public int IndexOf(float item) => 0;

    public void Insert(int index, float item)
    {
    }

    public void RemoveAt(int index)
    {
    }

    public float this[int index]
    {
      get => 0.0f;
      set
      {
      }
    }

    public void Add(float item)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(float item) => false;

    public void CopyTo(float[] array, int arrayIndex)
    {
    }

    public int Count => 0;

    public bool IsReadOnly => true;

    public bool Remove(float item) => false;

    public IEnumerator<float> GetEnumerator()
    {
      yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      yield break;
    }
  }
}
