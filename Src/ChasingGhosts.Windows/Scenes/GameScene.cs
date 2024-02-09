// ChasingGhosts.Windows.Scenes.GameScene

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChasingGhosts.Windows.Interfaces;
using ChasingGhosts.Windows.UI;
using ChasingGhosts.Windows.ViewModels;
using ChasingGhosts.Windows.World;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Audio;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Common.World.Camera;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Utility;


#nullable disable
namespace ChasingGhosts.Windows.Scenes
{
  public class GameScene : Sharp2D.Engine.Common.Scene.Scene
  {
    private Player player;

    private PhysicsEngine physics;
    
    private PlayerViewModel playerVm;
    
    private IMusicManager musicManager;
    
    private Label lblScore;
    
    private int score;
    
    private const float MapSize = 5f;


    public GameScene(IResolver resolver) : base(resolver)
    {
      Sharp2DApplication.GameManager.BackgroundColor = Color.Gray;
    }

    public override async void Initialize(IResolver resolver)
    {
      GameScene gameScene = this; //?

      gameScene.playerVm = new PlayerViewModel();
      
      gameScene.InitWorld();
      gameScene.InitUi();
      
      gameScene.musicManager = resolver.Resolve<IMusicManager>();
      gameScene.musicManager.LoadSongs("Audio/LOOP_1", "Audio/LOOP_2",
          "Audio/LOOP_3", "Audio/LOOP_4", "Audio/LOOP_5");

      
      if (gameScene.musicManager is Component musicManager)
      {
        gameScene.WorldRoot.Components.Add(musicManager);
      }


        // ISSUE: reference to a compiler-generated method
        //gameScene.\u003C\u003En__0(resolver);
        base.Initialize(resolver);

        // ISSUE: explicit non-virtual call
        //__nonvirtual (gameScene.IsPaused) = true;
        this.IsPaused = true;


        // ISSUE: reference to a compiler-generated method
        //gameScene.playerVm.Dies += new EventHandler(gameScene.\u003CInitialize\u003Eb__7_0);

        //await Task.Delay(1500);

        // ISSUE: explicit non-virtual call
        //await __nonvirtual (gameScene.WaitForUpdate());
        // ISSUE: explicit non-virtual call
        //__nonvirtual (gameScene.IsPaused) = false;
        this.playerVm.Dies += async (s, e) =>
        {
            await this.WaitForUpdate();
            foreach (var npc in this.WorldRoot.Children.OfType<Npc>())
            {
                npc.PlayerDied();
            }

            this.musicManager.EndSongs();
            var shutdown = new AudioEffect("Audio/windows_shutdown")
            {
                Volume = .75f
            };
            this.UiRoot.Components.Add(shutdown);
            shutdown.Stopped += (_s, _e) => this.UiRoot.Components.Remove(shutdown);
            shutdown.Play();
            await this.player.PlayDeathAnimation();
            this.UiRoot.Insert(0, new DeathScreen());
        };
    }

    private void InitUi()
    {
      Label label = new Label(new FontDefinition("DefaultFont24", 24f));
      label.FontSize = 24f;
      label.Text = string.Format("Score: {0}", (object) this.score);
      label.Alignment = TextAlignment.Center;
      label.LocalPosition = new Vector2(Resolution.VirtualScreen.X / 2f, 50f);
      label.DropShadowOffset = new Vector2(2f, 1f);
      label.DropShadowTint = Color.White;
      label.DropShadowOpacity = 0.6f;
      label.HasDropShadow = true;
      this.lblScore = label;
      this.UiRoot.Add((GameObject) this.lblScore);
      this.UiRoot.Add((GameObject) new HealthBar(this.playerVm));
    }

    private void InitWorld()
    {
      this.player = new Player(this.playerVm);
      Sharp2D.Engine.Common.World.Camera.Camera camera 
                = new Sharp2D.Engine.Common.World.Camera.Camera()
      {
        MainCamera = true,
        EnableMovementTracking = true,
        Tracker = new CameraTracker()
        {
          Target = (GameObject) this.player,
          EnablePositionTracking = true
        }
      };
      this.WorldRoot.Add((GameObject) camera);
      this.AddFootprints();
      this.AddNpcs(camera);
      this.AddShoes(camera);
      this.WorldRoot.Add((GameObject) this.player);
      this.physics = new PhysicsEngine();
      this.physics.RemovedFootprint += (EventHandler) ((s, e) => this.score += 10);
      this.WorldRoot.Components.Add((Component) this.physics);
    }

    private void AddShoes(Sharp2D.Engine.Common.World.Camera.Camera camera)
    {
      ShoePowerup shoePowerup1 = new ShoePowerup();
      shoePowerup1.LocalPosition = Resolution.TransformPointToWorld(new Vector2(123f, 612f) 
          * 5f, camera);
      shoePowerup1.Powerup = ShoeType.Sneakers;
      this.WorldRoot.Add((GameObject) shoePowerup1);
      ShoePowerup shoePowerup2 = new ShoePowerup();
      shoePowerup2.LocalPosition = Resolution.TransformPointToWorld(new Vector2(468f, 460f)
          * 5f, camera);
      shoePowerup2.Powerup = ShoeType.Rollerblades;
      this.WorldRoot.Add((GameObject) shoePowerup2);
      ShoePowerup shoePowerup3 = new ShoePowerup();
      shoePowerup3.LocalPosition = Resolution.TransformPointToWorld(new Vector2(890f, 597f)
          * 5f, camera);
      shoePowerup3.Powerup = ShoeType.FlipFlops;
      this.WorldRoot.Add((GameObject) shoePowerup3);
    }

    private void AddNpcs(Sharp2D.Engine.Common.World.Camera.Camera camera)
    {
      Vector2[] source = new Vector2[12]
      {
        new Vector2(372f, 257f),
        new Vector2(72f, 464f),
        new Vector2(141f, 750f),
        new Vector2(175f, 712f),
        new Vector2(414f, 523f),
        new Vector2(708f, 551f),
        new Vector2(906f, 363f),
        new Vector2(674f, 119f),
        new Vector2(1102f, 171f),
        new Vector2(1164f, 427f),
        new Vector2(1249f, 598f),
        new Vector2(1164f, 550f)
      };
      foreach (Vector2 vector2 in ((IEnumerable<Vector2>) source).Select<Vector2, Vector2>((Func<Vector2, Vector2>) (v => Resolution.TransformPointToWorld(v * 5f, camera))).ToArray<Vector2>())
      {
        Npc npc = new Npc(this.player, this.playerVm);
        npc.LocalPosition = vector2;
        this.WorldRoot.Add((GameObject) npc);
      }
    }

    private void AddFootprints()
    {
      GamePath gamePath1 = new GamePath(((IEnumerable<Vector2>) new Vector2[56]
      {
        new Vector2(68f, 82f),
        new Vector2(103f, 143f),
        new Vector2(161f, 174f),
        new Vector2(229f, 183f),
        new Vector2(267f, 207f),
        new Vector2(277f, 249f),
        new Vector2(280f, 313f),
        new Vector2(263f, 391f),
        new Vector2(212f, 433f),
        new Vector2(182f, 467f),
        new Vector2(180f, 511f),
        new Vector2(141f, 530f),
        new Vector2(138f, 571f),
        new Vector2(105f, 631f),
        new Vector2(132f, 710f),
        new Vector2(257f, 731f),
        new Vector2(313f, 729f),
        new Vector2(373f, 702f),
        new Vector2(428f, 659f),
        new Vector2(452f, 593f),
        new Vector2(455f, 541f),
        new Vector2(428f, 510f),
        new Vector2(445f, 469f),
        new Vector2(495f, 449f),
        new Vector2(566f, 444f),
        new Vector2(651f, 450f),
        new Vector2(725f, 454f),
        new Vector2(770f, 500f),
        new Vector2(783f, 579f),
        new Vector2(768f, 640f),
        new Vector2(812f, 660f),
        new Vector2(854f, 645f),
        new Vector2(886f, 624f),
        new Vector2(902f, 557f),
        new Vector2(895f, 479f),
        new Vector2(868f, 416f),
        new Vector2(819f, 330f),
        new Vector2(745f, 273f),
        new Vector2(685f, 234f),
        new Vector2(679f, 186f),
        new Vector2(749f, 143f),
        new Vector2(838f, 102f),
        new Vector2(943f, 78f),
        new Vector2(1011f, 78f),
        new Vector2(1082f, 107f),
        new Vector2(1158f, 136f),
        new Vector2(1189f, 186f),
        new Vector2(1219f, 254f),
        new Vector2(1237f, 335f),
        new Vector2(1246f, 433f),
        new Vector2(1227f, 551f),
        new Vector2(1194f, 596f),
        new Vector2(1176f, 678f),
        new Vector2(1160f, 711f),
        new Vector2(1108f, 737f),
        new Vector2(1066f, 757f)
      }).Select<Vector2, Vector2>((Func<Vector2, Vector2>) (v => v * 5f)).ToArray<Vector2>());
      this.WorldRoot.Add((GameObject) gamePath1);
      Vector2 vector2 = this.player.GlobalPosition - gamePath1.GlobalPosition;
      GamePath gamePath2 = gamePath1;
      gamePath2.LocalPosition = gamePath2.LocalPosition + vector2;
    }

    public override void Update(GameTime gameTime)
    {
      this.CheckPowerups();
      this.lblScore.Text = string.Format("Score: {0}", (object) this.GetScore());
      base.Update(gameTime);
    }

    private float GetScore()
    {
      return (float) (this.score - (int) (100.0 - (double) this.playerVm.Health));
    }

    private void CheckPowerups()
    {
      foreach (ShoePowerup shoePowerup in this.WorldRoot.Children.OfType<ShoePowerup>().ToArray<ShoePowerup>())
      {
        Point point = shoePowerup.GlobalPosition.ToPoint();
        if (this.player.GlobalRegion.Contains(point.X, point.Y))
        {
          AudioEffect audio = new AudioEffect("Audio/newShoes");
          this.player.Components.Add((Component) audio);
          audio.Play();
          audio.Stopped += (EventHandler) ((s, e) => this.player.Components.Remove((Component) audio));
          this.player.EquipShoe(shoePowerup.Powerup);
          shoePowerup.Dismiss();
          break;
        }
      }
    }
  }
}
