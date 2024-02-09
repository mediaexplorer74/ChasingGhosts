// ChasingGhosts.Windows.UI.HealthBar

using ChasingGhosts.Windows.ViewModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;

#nullable disable
namespace ChasingGhosts.Windows.UI
{
  public class HealthBar : GameObject
  {
    private readonly PlayerViewModel viewModel;
    private Texture2D text;
    private float animHealth;

    public HealthBar(PlayerViewModel viewModel)
    {
      this.viewModel = viewModel;
      this.animHealth = this.viewModel.Health;
    }

    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.text = new Texture2D(resolver.Resolve<GraphicsDevice>(), 1, 1);
      this.text.SetData<Color>(new Color[1]{ Color.White });
    }

    public float Health => this.viewModel.Health;

    public override void Update(GameTime time)
    {
      base.Update(time);
      if ((double) this.animHealth == (double) this.Health)
        return;
      float num = (float) (20.0 * time.ElapsedGameTime.TotalSeconds);
      if ((double) this.animHealth > (double) this.Health && (double) this.animHealth 
                - (double) num <= (double) this.Health)
        this.animHealth = this.Health;
      else if ((double) this.animHealth < (double) this.Health
                && (double) this.animHealth - (double) num >= (double) this.Health)
        this.animHealth = this.Health;
      else
        this.animHealth += (double) this.animHealth > (double) this.Health ? -num : num;
    }

    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      base.Draw(batch, time);
      Vector2 virtualScreen = Resolution.VirtualScreen;
      float num = (float) (1.0 - (double) this.animHealth / 100.0);
      batch.Draw(this.text, new Rectangle(0, 0, (int) virtualScreen.X, 
          (int) virtualScreen.Y), Color.DarkRed * num * 0.7f);
    }
  }
}
