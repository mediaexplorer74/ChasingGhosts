// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.IContentManagerFactory
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Content;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  /// <summary>
  ///     Content Manager swapper. We do this to make unit testing less annoying.
  /// </summary>
  public interface IContentManagerFactory
  {
    /// <summary>
    ///     Creates a ContentManager, passing it the specified root path.
    /// </summary>
    /// <param name="rootPath">The root path.</param>
    /// <returns>
    ///     The <see cref="T:Microsoft.Xna.Framework.Content.ContentManager" />.
    /// </returns>
    ContentManager CreateAndUnloadCurrent();

    void UpdateContentPath(string combine);
  }
}
