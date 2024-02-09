// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.ChildComponents
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.Exceptions;
using Sharp2D.Engine.Common.ObjectSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.Components
{
  /// <summary>
  ///     A collection of ChildComponents, always connected to any <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" /> that has been created.
  ///     This is where you would usually add things such as Sprites, Audio, or other features you'd say would qualify as a
  ///     component.
  /// </summary>
  public class ChildComponents : 
    IList<Component>,
    ICollection<Component>,
    IEnumerable<Component>,
    IEnumerable
  {
    /// <summary>The _components.</summary>
    private readonly IList<Component> components;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.ChildComponents" /> class.
    /// </summary>
    /// <param name="owner">The parent.</param>
    public ChildComponents(GameObject owner)
    {
      this.Owner = owner;
      this.components = (IList<Component>) new List<Component>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.ChildComponents" /> class.
    /// </summary>
    public ChildComponents() => this.components = (IList<Component>) new List<Component>();

    /// <summary>
    ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
    public int Count => this.components.Count;

    /// <summary>
    ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
    public bool IsReadOnly => this.components.IsReadOnly;

    /// <summary>
    ///     Gets or sets the parent of this <see cref="T:Sharp2D.Engine.Common.Components.ChildComponents" /> collection.
    /// </summary>
    /// <value>The parent.</value>
    public GameObject Owner { get; private set; }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.Components.Component" />.
    /// </returns>
    public Component this[int index]
    {
      get => this.components[index];
      set
      {
        this.Remove(this.components[index]);
        this.components.Insert(index, value);
      }
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    public void Add(Component item)
    {
      if (this.Contains(item))
        return;
      GameObject parent = item.Parent;
      item.Parent = this.Owner;
      this.components.Add(item);
      this.Owner.OnChildComponentMoved(new ChildComponentMovedArgs(item, this.Owner, parent, ChildObjectMoveAction.Added));
    }

    /// <summary>
    ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public void Clear()
    {
      foreach (Component component in this.components.ToArray<Component>())
        this.Remove(component);
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
    public bool Contains(Component item) => this.components.Contains(item);

    /// <summary>Copies to.</summary>
    /// <param name="array">The array.</param>
    /// <param name="arrayIndex">Index of the array.</param>
    public void CopyTo(Component[] array, int arrayIndex)
    {
      this.components.CopyTo(array, arrayIndex);
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<Component> GetEnumerator() => this.components.GetEnumerator();

    /// <summary>
    /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </summary>
    /// <param name="item">
    /// The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </param>
    /// <returns>
    /// The index of <paramref name="item" /> if found in the list; otherwise, -1.
    /// </returns>
    public int IndexOf(Component item) => this.components.IndexOf(item);

    /// <summary>
    /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">
    /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </param>
    public void Insert(int index, Component item)
    {
      GameObject oldParent = !this.Contains(item) ? item.Parent : throw new InvalidGameComponentHierarchyException();
      item.Parent = this.Owner;
      this.components.Insert(index, item);
      this.Owner.OnChildComponentMoved(new ChildComponentMovedArgs(item, this.Owner, oldParent, ChildObjectMoveAction.Inserted, index));
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
    public bool Remove(Component item)
    {
      if (!this.components.Contains(item))
        return false;
      GameObject parent = item.Parent;
      item.Parent = (GameObject) null;
      bool flag = this.components.Remove(item);
      this.Owner.OnChildComponentMoved(new ChildComponentMovedArgs(item, (GameObject) null, parent, ChildObjectMoveAction.Removed));
      return flag;
    }

    /// <summary>
    /// Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public void RemoveAt(int index) => this.components.RemoveAt(index);

    /// <summary>
    ///     Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
