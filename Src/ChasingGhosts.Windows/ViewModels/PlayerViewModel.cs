// ChasingGhosts.Windows.ViewModels.PlayerViewModel


using ChasingGhosts.Windows.World;
using System;

#nullable disable
namespace ChasingGhosts.Windows.ViewModels
{
  public class PlayerViewModel
  {
    public float Health { get; private set; } = 100f;

    public event EventHandler Dies;

    public bool IsAlive => (double) this.Health > 0.0;

    public ShoeType ShoeType { get; set; }

    public bool IsInvulnerable { get; set; }

    public void DamagePlayer(float damage)
    {
      if (this.IsInvulnerable)
        return;
      float health = this.Health;
      this.Health -= damage;
      if ((double) this.Health > 0.0 || (double) health <= 0.0)
        return;
      EventHandler dies = this.Dies;
      if (dies == null)
        return;
      dies((object) this, EventArgs.Empty);
    }

    public void HealPlayer(float healing)
    {
      this.Health += healing;
      if ((double) this.Health < 100.0)
        return;
      this.Health = 100f;
    }
  }
}
