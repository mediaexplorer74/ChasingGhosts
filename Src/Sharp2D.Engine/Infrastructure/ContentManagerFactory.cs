// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.ContentManagerFactory
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework.Content;
using Sharp2D.Engine.Common;
using System;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  /// <summary>Content Manager Factory</summary>
  public class ContentManagerFactory : IContentManagerFactory
  {
    private IResolver resolver;

    public ContentManagerFactory(IResolver resolver) => this.resolver = resolver;

    /// <summary>
    ///     Creates a ContentManager, passing it the specified root path.
    /// </summary>
    /// <param name="rootPath">The root path.</param>
    /// <returns>
    ///     The <see cref="T:Microsoft.Xna.Framework.Content.ContentManager" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///     Sharp2D Application Game was null.
    ///     or
    ///     Game Services container was null.
    /// </exception>
    public ContentManager CreateAndUnloadCurrent()
    {
      string contentRoot = Sharp2DApplication.ContentRoot;
      SharpGameManager sharpGameManager = this.resolver.TryResolve<SharpGameManager>();
      if (sharpGameManager == null)
        throw new InvalidOperationException("Sharp2D Application Game was null.");
      this.resolver.TryResolve<ContentManager>()?.Unload();
      ContentManager instance = new ContentManager(sharpGameManager.Service, contentRoot);
      sharpGameManager.Content = instance;
      this.resolver.Register<ContentManager>(instance);
      return instance;
    }

    public void UpdateContentPath(string combine)
    {
    }
  }
}
