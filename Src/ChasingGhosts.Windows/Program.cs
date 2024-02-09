// ChasingGhosts.Windows.Program


using Sharp2D.Engine.Infrastructure;
using System;

#nullable disable
namespace ChasingGhosts.Windows
{
  public static class Program
  {
    [STAThread]
    private static void Main() => new MainApp((IResolver) new NinjectResolver()).Start();
  }
}
