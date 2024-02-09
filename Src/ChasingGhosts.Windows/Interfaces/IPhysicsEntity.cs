// ChasingGhosts.Windows.Interfaces.IPhysicsEntity

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.ObjectSystem;

#nullable disable
namespace ChasingGhosts.Windows.Interfaces
{
  public interface IPhysicsEntity
  {
    Vector2 LocalPosition { get; set; }

    Rectanglef GlobalRegion { get; }
  }
}
