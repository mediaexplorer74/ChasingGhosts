// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Input.InteractionManager
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.ObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Input
{
  public class InteractionManager : Component
  {
    private readonly IInteractionProvider interactor;
    private readonly List<GameObject> selectedItems = new List<GameObject>();

    public InteractionManager(IInteractionProvider interactor) => this.interactor = interactor;

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this.ClearSelected();
      foreach (Interaction interaction in this.interactor.GetInteractions())
      {
        GameObject selectedItem = this.GetSelectedItem(interaction);
        if (selectedItem != null)
        {
          selectedItem.Interactions.Add(interaction);
          if (!this.selectedItems.Contains(selectedItem))
            this.selectedItems.Add(selectedItem);
        }
      }
    }

    private GameObject GetSelectedItem(Interaction interaction)
    {
      Vector2 position = interaction.Position;
      return this.FindCollision((GameObject) Sharp2D.Engine.Common.Scene.Scene.CurrentScene.UiRoot, (int) Math.Round((double) position.X, 0), (int) Math.Round((double) position.Y, 0)) ?? this.FindCollision((GameObject) Sharp2D.Engine.Common.Scene.Scene.CurrentScene.WorldRoot, (int) Math.Round((double) position.X, 0), (int) Math.Round((double) position.Y, 0)) ?? (GameObject) null;
    }

    private GameObject FindCollision(GameObject item, int x, int y)
    {
      for (int index = item.Children.Count - 1; index >= 0; --index)
      {
        GameObject collision = this.FindCollision(item.Children[index], x, y);
        if (collision != null)
          return collision;
      }
      return item.Interactable && item.IsVisible && item.GlobalRegion.Contains(x, y) ? item : (GameObject) null;
    }

    private void ClearSelected()
    {
      if (!this.selectedItems.Any<GameObject>())
        return;
      foreach (GameObject selectedItem in this.selectedItems)
        selectedItem.Interactions.Clear();
      this.selectedItems.Clear();
    }
  }
}
