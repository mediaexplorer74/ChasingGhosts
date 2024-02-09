// ChasingGhosts.Windows.UI.NinePatchSprite


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Loading;

#nullable disable
namespace ChasingGhosts.Windows.UI
{
  public class NinePatchSprite : Sprite
  {
    private readonly SpriteFrame topLeft;
    private readonly SpriteFrame topCenter;
    private readonly SpriteFrame topRight;
    private readonly SpriteFrame middleLeft;
    private readonly SpriteFrame middleCenter;
    private readonly SpriteFrame middleRight;
    private readonly SpriteFrame bottomLeft;
    private readonly SpriteFrame bottomCenter;
    private readonly SpriteFrame bottomRight;
    private Vector2 area = new Vector2(100f, 50f);
    private Vector2 offset;

    public NinePatchSprite(LoadInstruction<Texture2D> instruction, NinePatchInstruction patch)
    {
      this.Tint = Color.White;
      this.topLeft = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.TopLeft
      };
      this.topCenter = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.TopCenter
      };
      this.topRight = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.TopRight
      };
      this.middleLeft = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.MiddleLeft
      };
      this.middleCenter = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.MiddleCenter
      };
      this.middleRight = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.MiddleRight
      };
      this.bottomLeft = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.BottomLeft
      };
      this.bottomCenter = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.BottomCenter
      };
      this.bottomRight = new SpriteFrame()
      {
        Instruction = instruction,
        Region = patch.BottomRight
      };
      this.Children.Add((Component) this.topLeft);
      this.Children.Add((Component) this.topCenter);
      this.Children.Add((Component) this.topRight);
      this.Children.Add((Component) this.middleLeft);
      this.Children.Add((Component) this.middleCenter);
      this.Children.Add((Component) this.middleRight);
      this.Children.Add((Component) this.bottomLeft);
      this.Children.Add((Component) this.bottomCenter);
      this.Children.Add((Component) this.bottomRight);
    }

    public override void Initialize(IResolver resolver)
    {
      if (this.Parent is Label parent)
        this.SetupAsLabel(parent);
      base.Initialize(resolver);
    }

    private void SetupAsLabel(Label lbl)
    {
      this.area = lbl.FontDefinition.GetFont().MeasureString(lbl.Text);
      this.offset = new Vector2(0.0f, 24f);
    }

    public override void Draw(
      SharpDrawBatch batch,
      GameTime time,
      Vector2 position,
      Color tint,
      float rotation,
      Vector2 scale,
      SpriteEffects effects,
      float depth)
    {
      int x = (int) this.area.X;
      int y = (int) this.area.Y;
      Rectangle region1 = this.topLeft.Region;
      Rectangle region2 = this.topCenter.Region;
      Rectangle region3 = this.topRight.Region;
      Rectangle region4 = this.middleLeft.Region;
      Rectangle region5 = this.middleCenter.Region;
      Rectangle region6 = this.middleRight.Region;
      Rectangle region7 = this.bottomLeft.Region;
      Rectangle region8 = this.bottomCenter.Region;
      Rectangle region9 = this.bottomRight.Region;

      Point point = (position - this.offset - new Vector2(
          (float) this.topLeft.Region.Width, 0.0f)).ToPoint();

      Texture2D texture1 = this.topLeft.Texture;

      batch.Draw(texture1, new Rectangle(
          point.X, point.Y, region1.Width, region1.Height),
          new Rectangle?(region1), tint);
      Texture2D texture2 = this.topCenter.Texture;
      batch.Draw(texture2, new Rectangle(
          point.X + region1.Width, point.Y, x, region2.Height), 
          new Rectangle?(region2), tint);
      Texture2D texture3 = this.topRight.Texture;
      batch.Draw(texture3, new Rectangle(
          point.X + region1.Width + x, point.Y, region3.Width, region3.Height), 
          new Rectangle?(region3), tint);
      Texture2D texture4 = this.middleLeft.Texture;
      batch.Draw(texture4, new Rectangle(
          point.X, point.Y + region1.Height, region4.Width, y), 
          new Rectangle?(region4), tint);
      Texture2D texture5 = this.middleCenter.Texture;
      int num1 = region1.Width - region4.Width + (region3.Width - region6.Width);
      int num2 = region1.Height - region2.Height + (region9.Height - region8.Height);
      batch.Draw(texture5, new Rectangle(
          point.X + region4.Width, point.Y + region2.Height, x + num1, y + num2),
          new Rectangle?(region5), tint);
      Texture2D texture6 = this.middleRight.Texture;
      batch.Draw(texture6, new Rectangle(
          point.X + region1.Width + x + (region1.Width - region6.Width), 
          point.Y + region3.Height, region6.Width, y), new Rectangle?(region6), tint);
      Texture2D texture7 = this.bottomLeft.Texture;
      batch.Draw(texture7, new Rectangle(
          point.X, point.Y + region3.Height + y, region7.Width, region7.Height), 
          new Rectangle?(region7), tint);
      Texture2D texture8 = this.bottomCenter.Texture;
      batch.Draw(texture8, new Rectangle(
          point.X + region7.Width, point.Y + region3.Height + y + (region7.Height - region8.Height),
          x, region8.Height), new Rectangle?(region8), tint);
      Texture2D texture9 = this.bottomRight.Texture;
      batch.Draw(texture9, new Rectangle(
          point.X + region7.Width + x, point.Y + region3.Height + y, 
          region9.Width, region9.Height), new Rectangle?(region9), tint);
    }
  }
}
