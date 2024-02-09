// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Component
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Common.Components
{
  /// <summary>
  /// A basic component. Anything that needs to be attached to a specific object,
  /// but isn't actually another game object, would be a component.
  /// <para>Audio, Sprite, Animation, (In relation to player character:) Inventory, Statistics, etc.</para>
  /// </summary>
  public abstract class Component : GameContract
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Component" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    protected Component(GameObject parent)
      : this()
    {
      this.Parent = parent;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Component" /> class.
    /// </summary>
    protected Component()
    {
      this.Name = this.GetType().Name;
      this.Children = new List<Component>();
    }

    protected IResolver Resolver { get; private set; }

    /// <summary>Gets the children.</summary>
    /// <value>The children.</value>
    public List<Component> Children { get; }

    /// <summary>
    ///     Gets or sets the name.
    ///     <para>
    ///         Used for identifying objects, as well as targeting a specific component by name. MAKE IT UNIQUE.
    ///         If you don't, any code targeting the Name, will use the first (unless you're looping,
    ///         in which case it will run on all components with this name. You get the idea.)
    ///     </para>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the parent object.
    /// <para>REQUIRED! If this is not set, you'll get an exception in initialize. Seriously, just pass the damn parent &gt;_&gt;</para>
    /// </summary>
    /// <value>The parent.</value>
    public GameObject Parent { get; set; }

    /// <summary>Initializes this component.</summary>
    /// <param name="resolver"></param>
    /// <exception cref="T:Sharp2D.Engine.Common.Components.MissingParentException"></exception>
    public override void Initialize(IResolver resolver)
    {
      this.Resolver = resolver;
      if (this.Parent == null)
        throw new MissingParentException(this.GetType().ToString());
      foreach (Component child in this.Children)
      {
        child.Parent = this.Parent;
        child.Initialize(resolver);
      }
    }

    /// <summary>
    /// Called every Frame. This is where you want to handle your logic.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
      this.InvokeRunables();
      if (this.IsPaused)
        return;
      foreach (GameContract gameContract in this.Children.ToArray())
        gameContract.Update(gameTime);
    }

    /// <summary>Draws this object to the screen.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      if (!this.IsVisible)
        return;
      foreach (GameContract child in this.Children)
        child.Draw(batch, time);
    }
  }
}
