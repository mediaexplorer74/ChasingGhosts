// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.ObjectSystem.GameObject
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Helper;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.ObjectSystem
{
  /// <summary>
  ///     Game Object, all objects that are to be drawn in the game should inherit from this bad boii.
  /// </summary>
  /// <remarks>
  ///     If you are drawing a UI, you probably don't want to inherit from this.
  /// </remarks>
  public class GameObject : GameContract
  {
    /// <summary>
    ///     The actual parent, serving as a backing field for <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Parent" />
    /// </summary>
    protected internal GameObject ActualParent;
    private float width = -1f;
    private float height = -1f;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" /> class.
    /// </summary>
    /// <param name="localPosition">The local position. This is the offset from the parent global position.</param>
    /// <param name="sprite">The texture.</param>
    /// <param name="children">The children.</param>
    public GameObject(Vector2 localPosition, Sprite sprite = null, params GameObject[] children)
      : this(children)
    {
      this.LocalPosition = localPosition;
      if (sprite == null)
        return;
      this.Components.Add((Component) sprite);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" /> class.
    /// </summary>
    /// <param name="children">The children.</param>
    public GameObject(params GameObject[] children)
    {
      this.Components = new ChildComponents(this);
      this.Children = new ChildObjects(this, new GameObject[0]);
      if (children != null)
      {
        foreach (GameObject child in children)
          this.Children.Add(child);
      }
      this.LocalScale = Vector2.One;
      this.Name = this.GetType().Name;
      this.ChildObjectMoved += (GameObjectMoved) ((sender, args) =>
      {
        if (this.Resolver == null || args.Action != ChildObjectMoveAction.Added && args.Action != ChildObjectMoveAction.Inserted || args.Child.Parent == null || args.Child.Parent != this)
          return;
        args.Child.DoInitialize(this.Resolver);
      });
      this.ChildComponentMoved += (ComponentMoved) ((sender, args) =>
      {
        if (this.Resolver == null || args.Action != ChildObjectMoveAction.Added && args.Action != ChildObjectMoveAction.Inserted || args.Child.Parent == null || args.Child.Parent != this)
          return;
        args.Child.Initialize(this.Resolver);
      });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" /> class.
    /// </summary>
    public GameObject()
      : this((GameObject[]) null)
    {
    }

    /// <summary>Occurs when a child object is moved.</summary>
    public event GameObjectMoved ChildObjectMoved;

    /// <summary>Occurs when a child object is moved.</summary>
    public event ComponentMoved ChildComponentMoved;

    /// <summary>Gets the children.</summary>
    /// <value>The children.</value>
    public ChildObjects Children { get; }

    /// <summary>Gets or sets the components.</summary>
    /// <value>The components.</value>
    public ChildComponents Components { get; }

    /// <summary>
    /// Gets the number of elements contained in <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" />.
    /// </summary>
    /// <value>The count.</value>
    public int Count => this.Children.Count;

    /// <summary>
    /// Gets the global position of this object within the scene.
    /// </summary>
    /// <value>The global position.</value>
    public virtual Vector2 GlobalPosition => this.GetGlobalPosition();

    /// <summary>
    ///     Gets the region/area of this Game Object. This is (usually) the width and height of the texture
    ///     to be drawn, and the global position. Override for custom behavior.
    /// </summary>
    /// <value>The region.</value>
    public virtual Rectanglef GlobalRegion => this.GetGlobalRegion();

    /// <summary>Gets the global rotation in the scene.</summary>
    /// <value>The global rotation.</value>
    public virtual float GlobalRotation => this.GetGlobalRotation();

    /// <summary>Gets the global scale.</summary>
    /// <value>The global scale.</value>
    public virtual Vector2 GlobalScale => this.GetGlobalScale();

    /// <summary>
    /// Gets the height of the underlying texture (or 0 if Sprite is null).
    /// </summary>
    /// <value>The height.</value>
    public virtual float Height
    {
      get
      {
        return (double) this.height < 0.0 ? (this.Sprite == null ? 0.0f : (float) this.Sprite.Height * this.Sprite.Scale.Y) : this.height;
      }
      set => this.height = value;
    }

    /// <summary>
    /// Gets a value indicating whether <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" /> is read-only.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
    /// </value>
    public bool IsReadOnly => this.Children.IsReadOnly;

    /// <summary>
    ///     Gets or sets the local position relative to it's parent object.
    /// </summary>
    /// <value>The local position.</value>
    public virtual Vector2 LocalPosition { get; set; }

    /// <summary>
    /// Gets the region/area of this Game Object. This is (usually) the <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Width" /> and <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Height" /> of the texture
    /// to be drawn, and the <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.LocalPosition" />. Override for custom behavior.
    /// </summary>
    /// <value>The local region.</value>
    public virtual Rectanglef LocalRegion => this.GetLocalRegion();

    /// <summary>Gets or sets the rotation in degrees.</summary>
    /// <value>The rotation.</value>
    public float LocalRotation { get; set; }

    /// <summary>Gets or sets the local scale.</summary>
    public virtual Vector2 LocalScale { get; set; }

    /// <summary>
    ///     Gets or sets the name of this Game Object. This is not required. At all.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>Gets or sets the parent.</summary>
    /// <value>The parent.</value>
    public GameObject Parent
    {
      get => this.ActualParent;
      set => this.SetParent(value);
    }

    /// <summary>Gets or sets the Sprite texture.</summary>
    /// <value>The Sprite texture.</value>
    public virtual Sprite Sprite => this.Components.OfType<Sprite>().FirstOrDefault<Sprite>();

    /// <summary>
    ///     Gets the width of the underlying texture (or 0 if Sprite is null).
    /// </summary>
    /// <value>The width.</value>
    public virtual float Width
    {
      get
      {
        return (double) this.width < 0.0 ? (this.Sprite == null ? 0.0f : (float) this.Sprite.Width * this.Sprite.Scale.X) : this.width;
      }
      set => this.width = value;
    }

    public List<Interaction> Interactions { get; } = new List<Interaction>();

    public bool Interactable { get; set; } = false;

    /// <summary>
    /// Gets or sets the game object at the specified index (From the <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" /> collection).
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" />.
    /// </returns>
    public GameObject this[int index]
    {
      get => this.Children[index];
      set => this.Children[index] = value;
    }

    /// <summary>Helper to create a Vector2</summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public static Vector2 V2(float x, float y) => new Vector2(x, y);

    /// <summary>
    /// Adds an item to the <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" /> collection.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    public virtual void Add(GameObject item) => this.Children.Add(item);

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public void Clear() => this.Children.Clear();

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />;
    /// otherwise, false.
    /// </returns>
    public bool Contains(GameObject item) => this.Children.Contains(item);

    /// <summary>Copies to.</summary>
    /// <param name="array">The array.</param>
    /// <param name="arrayIndex">Index of the array.</param>
    public virtual void CopyTo(GameObject[] array, int arrayIndex)
    {
      this.Children.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Draws all <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Components" />, then all <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" />.
    /// If you override this method, you are responsible for calling base <see cref="M:Sharp2D.Engine.Common.ObjectSystem.GameObject.Draw(Sharp2D.Engine.Drawing.SharpDrawBatch,Microsoft.Xna.Framework.GameTime)" />
    /// if you want it to draw children as well.
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      foreach (GameContract gameContract in this.Components.ToArray<Component>())
        gameContract.Draw(batch, time);
      foreach (GameObject gameObject in this.Children.ToArray<GameObject>())
        gameObject.DoDraw(batch, time);
    }

    /// <summary>
    /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
    /// <returns>
    /// The index of <paramref name="item" /> if found in the list; otherwise, -1.
    /// </returns>
    public int IndexOf(GameObject item) => this.Children.IndexOf(item);

    /// <summary>Initializes this specific game object.</summary>
    /// <param name="resolver"></param>
    public override void Initialize(IResolver resolver)
    {
      if (this.Resolver != null)
        return;
      this.Resolver = resolver;
      foreach (GameContract gameContract in this.Components.ToArray<Component>())
        gameContract.Initialize(resolver);
      foreach (GameObject gameObject in this.Children.ToArray<GameObject>())
        gameObject.DoInitialize(resolver);
    }

    /// <summary>Gets the resolver.</summary>
    /// <value>The resolver.</value>
    protected IResolver Resolver { get; private set; }

    /// <summary>
    /// Inserts a game object to the <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" /> collection at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">
    /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </param>
    public virtual void Insert(int index, GameObject item) => this.Children.Insert(index, item);

    /// <summary>
    /// Determines whether the specified child is a valid child for this game object.
    /// </summary>
    /// <param name="child">The child.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    public virtual bool IsChildValid(GameObject child) => true;

    /// <summary>
    /// Moves the gameobject the specified <see cref="T:Microsoft.Xna.Framework.Vector2" /> distance.
    /// </summary>
    /// <param name="distance">Distance in <see cref="T:Microsoft.Xna.Framework.Vector2" />.</param>
    public void Move(Vector2 distance) => this.LocalPosition += distance;

    /// <summary>
    /// This is a shortcut for wrapping X and Y in a vector 2 and running
    /// <see cref="M:Sharp2D.Engine.Common.ObjectSystem.GameObject.Move(Microsoft.Xna.Framework.Vector2)" />
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public void Move(float x, float y) => this.Move(GameObject.V2(x, y));

    /// <summary>
    /// Moves the game object the specified <see cref="T:Microsoft.Xna.Framework.Vector2" /> distance over the given <see cref="!:duration" />.
    /// <example>Will move the object 500 units, if Distance is 250, and time is 0.5 seconds. Or 250, if Distance is 500, and duration is 2 seconds. You get the idea.</example>
    /// </summary>
    /// <param name="distance">Distance to move over specified time.</param>
    /// <param name="time">Game Time manager.</param>
    /// <param name="duration">Duration in floating point Seconds.</param>
    public void MoveDelta(Vector2 distance, GameTime time, float duration)
    {
      this.LocalPosition += distance * (float) time.ElapsedGameTime.TotalSeconds / duration;
    }

    /// <summary>
    /// This is a shortcut for wrapping X and Y in a vector 2 and running
    /// <see cref="M:Sharp2D.Engine.Common.ObjectSystem.GameObject.MoveDelta(Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.GameTime,System.Single)" />
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="time">Game Time manager.</param>
    /// <param name="duration">Duration in decimal Seconds.</param>
    public void MoveDelta(float x, float y, GameTime time, float duration)
    {
      this.MoveDelta(GameObject.V2(x, y), time, duration);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the
    /// <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" />.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> was successfully removed from the
    /// <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" />; otherwise, false. This method also returns false if
    /// <paramref name="item" /> is not found in the original <see cref="P:Sharp2D.Engine.Common.ObjectSystem.GameObject.Children" />.
    /// </returns>
    public virtual bool Remove(GameObject item) => this.Children.Remove(item);

    /// <summary>Removes the child object at the specified index.</summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public virtual void RemoveAt(int index) => this.Remove(this.Children[index]);

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this GameObject.
    /// <example>RandomObject (Local X: 25.89385, Local Y: 68.23847)</example>
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return string.Format("{0} (Local X: {1}, Local Y: {2})", new object[3]
      {
        (object) this.Name,
        (object) this.LocalPosition.X,
        (object) this.LocalPosition.Y
      });
    }

    /// <summary>
    /// Updates this object. On <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" />, does nothing.
    /// </summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      this.InvokeRunables();
      foreach (GameContract gameContract in this.Components.ToArray<Component>())
        gameContract.Update(time);
      foreach (GameObject gameObject in this.Children.ToArray<GameObject>())
        gameObject.DoUpdate(time);
    }

    /// <summary>
    /// Internal initialize call. Called only by internal classes, obviously.
    /// Ensures all children and child components are initialized.
    /// </summary>
    internal void DoInitialize(IResolver resolver) => this.Initialize(resolver);

    /// <summary>
    /// Internal update method. Ensures all children are updated as well.
    /// </summary>
    /// <param name="time">The time.</param>
    internal void DoUpdate(GameTime time)
    {
      if (this.IsPaused)
        return;
      this.Update(time);
    }

    /// <summary>
    /// Internal draw call. Called only by internal classes, obviously.
    /// Ensures all children and child components are drawn, but only if .
    /// </summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The time.</param>
    internal void DoDraw(SharpDrawBatch batch, GameTime time)
    {
      if (this.IsHidden)
        return;
      this.Draw(batch, time);
    }

    /// <summary>Called when a child object has been moved.</summary>
    /// <param name="args">The arguments.</param>
    internal virtual void OnChildObjectMoved(ChildObjectMovedArgs args)
    {
      GameObjectMoved childObjectMoved = this.ChildObjectMoved;
      if (childObjectMoved != null)
        childObjectMoved(this, args);
      Sharp2D.Engine.Common.Scene.Scene.OnObjectMoved(this, args);
    }

    /// <summary>Called when a child component has been moved.</summary>
    /// <param name="args">The arguments.</param>
    internal virtual void OnChildComponentMoved(ChildComponentMovedArgs args)
    {
      ComponentMoved childComponentMoved = this.ChildComponentMoved;
      if (childComponentMoved != null)
        childComponentMoved(this, args);
      Sharp2D.Engine.Common.Scene.Scene.OnComponentMoved(this, args);
    }

    /// <summary>Gets the global position of this object.</summary>
    /// <returns>
    /// Returns a Vector2, representing the position in World coordinates.
    /// </returns>
    private Vector2 GetGlobalPosition()
    {
      GameObject parent = this.Parent;
      if (parent == null)
        return this.LocalPosition;
      Vector2 globalPosition = parent.GlobalPosition;
      return SharpMathHelper.Rotate(globalPosition + this.LocalPosition * this.GlobalScale, globalPosition, parent.GlobalPosition == Vector2.Zero ? 0.0f : this.GlobalRotation - this.LocalRotation);
    }

    /// <summary>Gets the global rotation of this object.</summary>
    /// <returns>
    /// The <see cref="T:System.Single" />.
    /// </returns>
    private float GetGlobalRotation()
    {
      GameObject parent = this.Parent;
      return parent == null ? this.LocalRotation : parent.GlobalRotation + this.LocalRotation;
    }

    /// <summary>Gets the global region.</summary>
    /// <returns></returns>
    private Rectanglef GetGlobalRegion()
    {
      Vector2 globalPosition = this.GlobalPosition;
      float x = globalPosition.X;
      float y = globalPosition.Y;
      Vector2 globalScale = this.GlobalScale;
      Sprite sprite = this.Sprite;
      if (sprite != null)
        return new Rectanglef(x - sprite.TransformOrigin.X * (float) sprite.Width * globalScale.X * sprite.Scale.X, y - sprite.TransformOrigin.Y * (float) sprite.Height * globalScale.Y * sprite.Scale.Y, this.Width * globalScale.X, this.Height * globalScale.Y);
      return this.Components.FirstOrDefault<Component>((Func<Component, bool>) (k => k is SpriteSheet<int>)) is SpriteSheet<int> spriteSheet ? new Rectanglef(x - spriteSheet.TransformOrigin.X * (float) sprite.Width * globalScale.X * spriteSheet.Scale.X, y - spriteSheet.TransformOrigin.Y * (float) sprite.Height * globalScale.Y * spriteSheet.Scale.Y, this.Width, this.Height) : new Rectanglef(x, y, this.Width, this.Height);
    }

    /// <summary>Gets the global scale.</summary>
    /// <returns></returns>
    private Vector2 GetGlobalScale()
    {
      GameObject parent = this.Parent;
      if (parent == null)
        return this.LocalScale;
      Vector2 globalScale = parent.GlobalScale;
      return (double) globalScale.X == 1.0 && (double) globalScale.Y == 1.0 ? this.LocalScale : globalScale * this.LocalScale;
    }

    /// <summary>Gets the local region.</summary>
    /// <returns></returns>
    private Rectanglef GetLocalRegion()
    {
      float x = this.LocalPosition.X;
      float y = this.LocalPosition.Y;
      Sprite sprite = this.Sprite;
      if (sprite != null)
        return new Rectanglef(x - sprite.TransformOrigin.X * this.LocalScale.X, y - sprite.TransformOrigin.Y * this.LocalScale.Y, this.Width * this.LocalScale.X, this.Height * this.LocalScale.Y);
      return this.Components.FirstOrDefault<Component>((Func<Component, bool>) (k => k is SpriteSheet<int>)) is SpriteSheet<int> spriteSheet ? new Rectanglef(x - spriteSheet.TransformOrigin.X * this.LocalScale.X, y - spriteSheet.TransformOrigin.Y * this.LocalScale.Y, this.Width * this.LocalScale.X, this.Height * this.LocalScale.Y) : new Rectanglef(x, y, this.Width, this.Height);
    }

    /// <summary>
    /// Sets the parent of this game object, ensuring all references are up to date.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <remarks>
    /// If <see cref="!:value" /> is null,
    /// the current parent (if any) will remove the reference to this from it's child collection.
    /// </remarks>
    private void SetParent(GameObject value)
    {
      if (value == null)
        this.ActualParent?.Children.Remove(this);
      else
        value.Children.Add(this);
    }
  }
}
