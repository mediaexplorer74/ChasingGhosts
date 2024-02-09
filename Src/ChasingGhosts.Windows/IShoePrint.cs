// ChasingGhosts.Windows.IShoePrint

using Sharp2D.Engine.Common.ObjectSystem;

#nullable disable
namespace ChasingGhosts.Windows
{
  public interface IShoePrint
  {
    bool IsActive { get; }

    void Dismiss();

    Rectanglef GlobalRegion { get; }

    int Level { get; set; }
  }
}
