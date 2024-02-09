// ChasingGhosts.Windows.UI.SpeechBubble

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Loading;
using Sharp2D.Engine.Utility;

#nullable disable
namespace ChasingGhosts.Windows.UI
{
  public class SpeechBubble : GameObject
  {
    private string text = "Speech bubble";
    private Label lbl;
    private float opacity = 1f;
    private NinePatchSprite patch;

    public override void Initialize(IResolver resolver)
    {
      this.lbl = new Label(new FontDefinition("DefaultFont", 12f))
      {
        FontSize = 12f,
        Alignment = TextAlignment.Left,
        Tint = Color.White,
        Text = this.Text
      };
      this.Add((GameObject) this.lbl);
      this.patch = new NinePatchSprite((LoadInstruction<Texture2D>) new TextureAssetInstruction()
      {
        Asset = "ninepatch"
      }, new NinePatchInstruction()
      {
        TopLeft = new Rectangle(0, 0, 12, 12),
        TopCenter = new Rectangle(12, 0, 8, 12),
        TopRight = new Rectangle(20, 0, 12, 12),
        MiddleLeft = new Rectangle(0, 12, 12, 8),
        MiddleCenter = new Rectangle(12, 12, 16, 16),
        MiddleRight = new Rectangle(20, 12, 12, 8),
        BottomLeft = new Rectangle(0, 20, 12, 12),
        BottomCenter = new Rectangle(12, 20, 8, 12),
        BottomRight = new Rectangle(20, 20, 12, 12)
      });
      this.lbl.Components.Add((Component) this.patch);
      this.Opacity = this.Opacity;
      base.Initialize(resolver);
    }

    public string Text
    {
      get => this.text;
      set
      {
        this.text = value;
        if (this.lbl == null)
          return;
        this.lbl.Text = value;
      }
    }

    public float Opacity
    {
      get => this.opacity;
      set
      {
        this.opacity = value;
        if (this.lbl == null)
          return;
        this.lbl.Tint = Color.White * value;
        if (this.patch == null)
          return;
        this.patch.Tint = Color.White * value;
      }
    }
  }
}
