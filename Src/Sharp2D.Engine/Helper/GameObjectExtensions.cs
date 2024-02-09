// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Helper.GameObjectExtensions
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Helper
{
  /// <summary>The game object extensions.</summary>
  public static class GameObjectExtensions
  {
    /// <summary>
    /// Returns a collection of all the children of the specified <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" />.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <remarks>This is a recursive call.</remarks>
    /// <returns>
    /// The <see cref="!:IEnumerable" />.
    /// </returns>
    public static IEnumerable<GameObject> GetAllChildren(this GameObject source)
    {
      List<GameObject> allChildren = new List<GameObject>();
      foreach (GameObject child in source.Children)
      {
        allChildren.Add(child);
        allChildren.AddRange(child.GetAllChildren());
      }
      return (IEnumerable<GameObject>) allChildren;
    }

    /// <summary>Gets the root object.</summary>
    /// <param name="source">The source.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" />.
    /// </returns>
    public static GameObject GetRoot(this GameObject source)
    {
      GameObject gameObject = source.Parent == null ? source : source.Parent.GetRoot();
      return !(gameObject is IRootObject) ? (GameObject) null : gameObject;
    }

    /// <summary>
    /// Determines whether the specified <see cref="!:source" /> <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" /> is an ancestor of the
    ///     <see cref="!:child" />
    ///     .
    /// </summary>
    /// <param name="source">
    /// The source (is this an ancestor for <see cref="!:child" />?.
    /// </param>
    /// <param name="child">The child.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    public static bool IsAncestorOf(this GameObject source, GameObject child)
    {
      if (source == child)
        return false;
      bool flag = false;
      for (GameObject gameObject = child; gameObject != null; gameObject = gameObject.Parent)
      {
        if (source == gameObject)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Determines whether the specified <see cref="!:source" /> is a descendant
    ///     of the specified <see cref="!:parent" />
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="parent">The parent.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    public static bool IsDescendantOf(this GameObject source, GameObject parent)
    {
      return parent.IsAncestorOf(source);
    }

    /// <summary>
    /// Determines whether the specified <see cref="!:source" /> is valid parent for the specified <see cref="!:child" />
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="child">The child.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    public static bool IsValidParentFor(this GameObject source, GameObject child)
    {
      return !source.IsDescendantOf(child) | source.IsAncestorOf(child);
    }

    /// <summary>
    /// Recursively tests the predicate against all (grand)children of <see cref="!:root" />.
    /// </summary>
    /// <param name="root">The root.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>
    /// The <see cref="!:IEnumerable" />.
    /// </returns>
    public static IEnumerable<GameObject> RecursiveWhere(
      this GameObject root,
      Func<GameObject, bool> predicate)
    {
      return root.GetAllChildren().Where<GameObject>(predicate);
    }

    /// <summary>Backtraces to the source's root object.</summary>
    /// <param name="source">The source.</param>
    /// <param name="backtrace">The backtrace.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.ObjectSystem.GameObject" />.
    /// </returns>
    public static GameObject TraceRoot(this GameObject source, ref IList<GameObject> backtrace)
    {
      if (backtrace == null)
        backtrace = (IList<GameObject>) new List<GameObject>();
      GameObject gameObject;
      if (source.Parent == null)
      {
        gameObject = source;
      }
      else
      {
        backtrace.Add(source.Parent);
        gameObject = source.Parent.TraceRoot(ref backtrace);
      }
      return !(gameObject is IRootObject) ? (GameObject) null : gameObject;
    }
  }
}
