// Decompiled with JetBrains decompiler
// Type: Ninject.Infrastructure.Multimap`2
// Assembly: Ninject, Version=3.0.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7
// MVID: 38474FF7-7B1D-4207-94FE-C703C871E3C3
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Ninject.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Ninject.Infrastructure
{
  /// <summary>
  /// A data structure that contains multiple values for a each key.
  /// </summary>
  /// <typeparam name="K">The type of key.</typeparam>
  /// <typeparam name="V">The type of value.</typeparam>
  public class Multimap<K, V> : IEnumerable<KeyValuePair<K, ICollection<V>>>, IEnumerable
  {
    private readonly Dictionary<K, ICollection<V>> _items = new Dictionary<K, ICollection<V>>();

    /// <summary>
    /// Gets the collection of values stored under the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    public ICollection<V> this[K key]
    {
      get
      {
        Ensure.ArgumentNotNull((object) key, nameof (key));
        if (!this._items.ContainsKey(key))
          this._items[key] = (ICollection<V>) new List<V>();
        return this._items[key];
      }
    }

    /// <summary>Gets the collection of keys.</summary>
    public ICollection<K> Keys => (ICollection<K>) this._items.Keys;

    /// <summary>Gets the collection of collections of values.</summary>
    public ICollection<ICollection<V>> Values => (ICollection<ICollection<V>>) this._items.Values;

    /// <summary>Adds the specified value for the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void Add(K key, V value)
    {
      Ensure.ArgumentNotNull((object) key, nameof (key));
      Ensure.ArgumentNotNull((object) value, nameof (value));
      this[key].Add(value);
    }

    /// <summary>Removes the specified value for the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns><c>True</c> if such a value existed and was removed; otherwise <c>false</c>.</returns>
    public bool Remove(K key, V value)
    {
      Ensure.ArgumentNotNull((object) key, nameof (key));
      Ensure.ArgumentNotNull((object) value, nameof (value));
      return this._items.ContainsKey(key) && this._items[key].Remove(value);
    }

    /// <summary>Removes all values for the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <returns><c>True</c> if any such values existed; otherwise <c>false</c>.</returns>
    public bool RemoveAll(K key)
    {
      Ensure.ArgumentNotNull((object) key, nameof (key));
      return this._items.Remove(key);
    }

    /// <summary>Removes all values.</summary>
    public void Clear() => this._items.Clear();

    /// <summary>
    /// Determines whether the multimap contains any values for the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns><c>True</c> if the multimap has one or more values for the specified key; otherwise, <c>false</c>.</returns>
    public bool ContainsKey(K key)
    {
      Ensure.ArgumentNotNull((object) key, nameof (key));
      return this._items.ContainsKey(key);
    }

    /// <summary>
    /// Determines whether the multimap contains the specified value for the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns><c>True</c> if the multimap contains such a value; otherwise, <c>false</c>.</returns>
    public bool ContainsValue(K key, V value)
    {
      Ensure.ArgumentNotNull((object) key, nameof (key));
      Ensure.ArgumentNotNull((object) value, nameof (value));
      return this._items.ContainsKey(key) && this._items[key].Contains(value);
    }

    /// <summary>
    /// Returns an enumerator that iterates through a the multimap.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the multimap.</returns>
    public IEnumerator GetEnumerator() => (IEnumerator) this._items.GetEnumerator();

    IEnumerator<KeyValuePair<K, ICollection<V>>> IEnumerable<KeyValuePair<K, ICollection<V>>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<K, ICollection<V>>>) this._items.GetEnumerator();
    }
  }
}
