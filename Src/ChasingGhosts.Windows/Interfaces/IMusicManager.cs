// ChasingGhosts.Windows.Interfaces.IMusicManager

#nullable disable
namespace ChasingGhosts.Windows.Interfaces
{
  public interface IMusicManager
  {
    void LoadSongs(params string[] songAssets);

    void Transition(int level);

    void EndSongs();
  }
}
