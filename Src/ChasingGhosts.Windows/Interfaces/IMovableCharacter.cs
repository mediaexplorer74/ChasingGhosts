// ChasingGhosts.Windows.Interfaces.IMovableCharacter

using Microsoft.Xna.Framework;

#nullable disable
namespace ChasingGhosts.Windows.Interfaces
{
  public interface IMovableCharacter : IPhysicsEntity
  {
    ChasingGhosts.Windows.World.Movement Direction { get; }

    float MaxMovement { get; }

    Vector2 Movement { get; }
  }
}
