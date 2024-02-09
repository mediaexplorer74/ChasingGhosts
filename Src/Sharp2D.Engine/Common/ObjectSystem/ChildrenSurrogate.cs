// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.ObjectSystem.ChildrenSurrogate
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Common.ObjectSystem
{
  /// <summary>
  ///     Surrogate for JSON serialization. Should not be used in games.
  /// </summary>
  public class ChildrenSurrogate
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildrenSurrogate" /> class.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="owner">The owner.</param>
    public ChildrenSurrogate(IEnumerable<GameObject> items, GameObject owner)
    {
      this.Items = (IList<GameObject>) new List<GameObject>(items);
      this.Owner = owner;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildrenSurrogate" /> class.
    /// </summary>
    public ChildrenSurrogate() => this.Items = (IList<GameObject>) new List<GameObject>();

    /// <summary>Gets or sets the items.</summary>
    /// <value>The items.</value>
    public IList<GameObject> Items { get; set; }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    public GameObject Owner { get; set; }

    /// <summary>Unwraps this instance.</summary>
    /// <returns>
    ///     The <see cref="T:Sharp2D.Engine.Common.ObjectSystem.ChildObjects" />.
    /// </returns>
    public ChildObjects Unwrap()
    {
      foreach (GameObject gameObject in (IEnumerable<GameObject>) this.Items)
        this.Owner.Children.Add(gameObject);
      return this.Owner.Children;
    }
  }
}
