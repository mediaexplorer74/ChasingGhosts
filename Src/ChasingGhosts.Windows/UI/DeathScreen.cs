// ChasingGhosts.Windows.UI.DeathScreen

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;

#nullable disable
namespace ChasingGhosts.Windows.UI
{
  public class DeathScreen : GameObject
  {
    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.Add((GameObject) new AnimatingBackground(new Color(48, 48, 48), 0.85f));
    }
  }
}
