// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Scene.SceneData
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.UI.Menus;
using Sharp2D.Engine.Common.World;

#nullable disable
namespace Sharp2D.Engine.Common.Scene
{
  /// <summary>
  ///     Scene Data container.
  ///     TODO: Wrappers in Scene to access these.
  /// </summary>
  public class SceneData
  {
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Scene.SceneData" /> class.
    /// </summary>
    public SceneData()
    {
      this.WorldRoot = new WorldManager();
      this.UiRoot = new UiManager();
    }

    /// <summary>Gets or sets the ui root.</summary>
    public UiManager UiRoot { get; set; }

    /// <summary>
    ///     Gets or sets the world root.
    ///     <para>Can only contains World Objects or objects derived from it.</para>
    /// </summary>
    /// <value>The world root.</value>
    public WorldManager WorldRoot { get; set; }
  }
}
