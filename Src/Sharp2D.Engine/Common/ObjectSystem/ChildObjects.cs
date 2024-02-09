// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.ObjectSystem.ChildObjects
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.Exceptions;
using Sharp2D.Engine.Helper;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Common.ObjectSystem
{
  /// <summary>
  ///     Child Objects collection. All of the <see cref="T:IList" /> methods that manipulate the
  ///     underlying collection will check for parent-child compatibility and possible
  ///     hierarchical conflicts.
  /// </summary>
  public class ChildObjects : 
    IList<GameObject>,
    ICollection<GameObject>,
    IEnumerable<GameObject>,
    IEnumerable
  {
    /// <summary>The incompatible child generic error</summary>
    private const string IncompatibleChildGenericError = "This game object is not a valid child for the desired parent.";
    /// <summary>The underlying list.</summary>
    private readonly IList<GameObject> list;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="initialChildren">The initial children.</param>
    /// <exception cref="T:System.ArgumentNullException">owner;Make sure that the object you're trying to deserialize, has a
    /// public parameterless constructor!</exception>
    public ChildObjects(GameObject owner, params GameObject[] initialChildren)
    {
      this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner), "Make sure that the object you're trying to deserialize, has a public parameterless constructor!");
      this.list = (IList<GameObject>) new List<GameObject>();
      this.SetupHierachy((IEnumerable<GameObject>) initialChildren);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" /> class.
    /// </summary>
    public ChildObjects() => this.list = (IList<GameObject>) new List<GameObject>();

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public int Count => this.list.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    public bool IsReadOnly => this.list.IsReadOnly;

    /// <summary>Gets or sets the owner of this collection.</summary>
    /// <value>The owner.</value>
    public GameObject Owner { get; private set; }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    /// Setting by index is not supported.
    /// </exception>
    public GameObject this[int index]
    {
      get => this.list[index];
      set => throw new NotSupportedException("Setting by index is not supported.");
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <exception cref="T:Sharp2D.Engine.Common.Exceptions.InvalidGameObjectHierachyException">
    /// </exception>
    public void Add(GameObject item)
    {
      if (!this.Owner.IsValidParentFor(item))
        throw new InvalidGameObjectHierachyException();
      this.ThrowIfNotValidChild(item);
      if (item.ActualParent != null)
        item.ActualParent.Children.Remove(item);
      GameObject actualParent = item.ActualParent;
      item.ActualParent = this.Owner;
      this.list.Add(item);
      this.Owner.OnChildObjectMoved(new ChildObjectMovedArgs(item, this.Owner, actualParent, ChildObjectMoveAction.Added));
    }

    /// <summary>Removes all game objects from this collection.</summary>
    /// <remarks>CAUTION! This is NOT recursive!</remarks>
    public void Clear()
    {
      for (int index = this.list.Count - 1; index >= 0 && this.list.Count > index; --index)
        this.Remove(this.list[index]);
    }

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
    /// </summary>
    /// <param name="item">
    /// The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <returns>
    /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />;
    ///     otherwise, false.
    /// </returns>
    public bool Contains(GameObject item) => this.list.Contains(item);

    /// <summary>Copies to.</summary>
    /// <param name="array">The array.</param>
    /// <param name="arrayIndex">Index of the array.</param>
    public void CopyTo(GameObject[] array, int arrayIndex) => this.list.CopyTo(array, arrayIndex);

    /// <summary>
    ///     Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<GameObject> GetEnumerator() => this.list.GetEnumerator();

    /// <summary>
    /// Determines the index of a specific item in the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" />.
    /// </summary>
    /// <param name="item">
    /// The object to locate in the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" />.
    /// </param>
    /// <returns>
    /// The index of <paramref name="item" /> if found in the list; otherwise, -1.
    /// </returns>
    public int IndexOf(GameObject item) => this.list.IndexOf(item);

    /// <summary>
    /// Inserts an item to the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" /> at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">
    /// The object to insert into the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">index</exception>
    /// <exception cref="T:Sharp2D.Engine.Common.Exceptions.InvalidGameObjectHierachyException">
    /// </exception>
    public void Insert(int index, GameObject item)
    {
      if (this.list.Count != 0 && this.list.Count <= index)
        throw new ArgumentOutOfRangeException(nameof (index), string.Format("Attemped to insert child object at index {0}, but collection only contains {1} elements.", new object[2]
        {
          (object) index,
          (object) this.list.Count
        }));
      if (this.Owner.IsDescendantOf(item))
        throw new InvalidGameObjectHierachyException();
      this.ThrowIfNotValidChild(item);
      GameObject actualParent = item.ActualParent;
      item.ActualParent = (GameObject) null;
      item.ActualParent = this.Owner;
      this.list.Insert(index, item);
      this.Owner.OnChildObjectMoved(new ChildObjectMovedArgs(item, this.Owner, actualParent, ChildObjectMoveAction.Inserted, index));
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the
    ///     <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <returns>
    /// true if <paramref name="item" /> was successfully removed from the
    ///     <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if
    ///     <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    public bool Remove(GameObject item)
    {
      if (!this.list.Contains(item))
        return false;
      GameObject actualParent = item.ActualParent;
      item.ActualParent = (GameObject) null;
      bool flag = this.list.Remove(item);
      this.Owner.OnChildObjectMoved(new ChildObjectMovedArgs(item, (GameObject) null, actualParent, ChildObjectMoveAction.Removed));
      return flag;
    }

    /// <summary>
    /// Removes the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" /> item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public void RemoveAt(int index) => this.Remove(this.list[index]);

    /// <summary>
    ///     Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.list.GetEnumerator();

    /// <summary>
    /// Sets up the initial hierachy when constructed with GameObject params.
    ///     In other words: Set's the parent-child relationships up.
    /// </summary>
    /// <param name="children">The children.</param>
    private void SetupHierachy(IEnumerable<GameObject> children)
    {
      if (children == null)
        return;
      foreach (GameObject child in children)
      {
        if (child.Parent != this.Owner)
          this.Add(child);
      }
    }

    /// <summary>
    /// Throws if the child is not a valid child of <see cref="P:Sharp2D.Engine.Common.ObjectSystem.ChildObjects.Owner" />.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <exception cref="T:Sharp2D.Engine.Common.Exceptions.IncompatibleChildException">
    /// </exception>
    private void ThrowIfNotValidChild(GameObject item)
    {
      if (!this.Owner.IsChildValid(item))
        throw new IncompatibleChildException("This game object is not a valid child for the desired parent.");
    }
  }
}
