// ChasingGhosts.Windows.World.Player


using ChasingGhosts.Windows.Interfaces;
using ChasingGhosts.Windows.ViewModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Animations.Predefined;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Utility;
using System;
using System.Threading.Tasks;

#nullable disable
namespace ChasingGhosts.Windows.World
{
  public class Player : GameObject, IMovableCharacter, IPhysicsEntity
  {
    private Vector2 oldMovement;
    private MovementSprite spriteWalk;
    private MovementSprite spriteIdle;
    private EventValueAnimator speedReducer;
    private float topSpeed = 250f;
    private SpriteSheet<int> spriteDeath;
    private bool canDash = true;
    private const float RollerbladeDelay = 2f;

    public Player(PlayerViewModel viewModel)
    {
      this.ViewModel = viewModel;
      this.MaxMovement = this.topSpeed;
    }

    public override void Initialize(IResolver resolver)
    {
      this.Width = 56f;
      this.Height = 56f;
      Sprite sprite = new Sprite(Color.Red * 0.25f, (int) this.Width, (int) this.Height);
      sprite.CenterObject();
      sprite.IsVisible = false;
      this.Components.Add((Component) sprite);
      this.spriteIdle = new MovementSprite("player_walk", TimeSpan.FromSeconds(1.5))
      {
        Scale = 3f
      };
      this.spriteWalk = new MovementSprite("player_walk", TimeSpan.FromSeconds(0.5))
      {
        Scale = 3f
      };
      this.Components.Add((Component) this.spriteIdle);
      this.Components.Add((Component) this.spriteWalk);
      this.spriteIdle.Start();
      SpriteSheet<int> spriteSheet = new SpriteSheet<int>();
      SpriteRegions<int> spriteRegions = new SpriteRegions<int>();
      spriteRegions.Add(0, new SpriteFrame("player_dies", new Rectangle(0, 0, 48, 48)));
      spriteRegions.Add(1, new SpriteFrame("player_dies", new Rectangle(48, 0, 48, 48)));
      spriteRegions.Add(2, new SpriteFrame("player_dies", new Rectangle(96, 0, 48, 48)));
      spriteSheet.Regions = spriteRegions;
      spriteSheet.IsVisible = false;
      spriteSheet.Scale = new Vector2(3f);
      this.spriteDeath = spriteSheet;
      this.spriteDeath.CenterObject();
      this.Components.Add((Component) this.spriteDeath);
      this.speedReducer = new EventValueAnimator(TimeSpan.FromSeconds(5.0));
      this.speedReducer.ValueUpdated += (EventHandler<float>) ((s, perc) =>
      {
        float num = this.topSpeed / 2f;
        this.MaxMovement = num + num * (1f - perc);
      });
      this.Components.Add((Component) this.speedReducer);
      this.speedReducer.Start();
      base.Initialize(resolver);
      this.Direction = ChasingGhosts.Windows.World.Movement.Down;
      this.EquipShoe(this.ViewModel.ShoeType);
    }

    public ChasingGhosts.Windows.World.Movement Direction { get; private set; }

    public float MaxMovement { get; private set; }

    public Vector2 Movement { get; private set; }

    public PlayerViewModel ViewModel { get; }

    public override void Update(GameTime time)
    {
      base.Update(time);
      if (!this.ViewModel.IsAlive)
      {
        this.Movement = Vector2.Zero;
      }
      else
      {
        this.HandleMovement(time);
        this.HandleHealth(time);
        this.HandleDash(time);
      }
    }

    private void HandleDash(GameTime time)
    {
      if (this.ViewModel.ShoeType != ShoeType.Sneakers || !this.canDash || !Player.TriggerDash())
        return;
      this.canDash = false;
      this.ViewModel.IsInvulnerable = true;
      this.topSpeed = 700f;
      this.RefreshMovement();
      GameTimer duration = new GameTimer(TimeSpan.FromSeconds(0.34999999403953552));
      duration.Expired += (EventHandler) ((s, e) =>
      {
        this.ViewModel.IsInvulnerable = false;
        this.SetShoeSpeed();
        this.Components.Remove((Component) duration);
      });
      this.Components.Add((Component) duration);
      GameTimer cooldown = new GameTimer(TimeSpan.FromSeconds(3.0));
      cooldown.Expired += (EventHandler) ((s, e) =>
      {
        this.canDash = true;
        this.Components.Remove((Component) cooldown);
      });
      this.Components.Add((Component) cooldown);
    }

    private static bool TriggerDash()
    {
      return InputManager.IsKeyPressed(new Keys?(Keys.Space))
                || InputManager.IsRightButtonPressed 
                || GamePadManager.IsButtonPressed(Buttons.A);
    }

    private void HandleHealth(GameTime time)
    {
      if (this.ViewModel.ShoeType != ShoeType.FlipFlops || (double) this.ViewModel.Health >= 100.0)
        return;
      this.ViewModel.HealPlayer((float) (time.ElapsedGameTime.TotalSeconds * 4.0));
    }

    private void HandleMovement(GameTime time)
    {
      Vector2 movement = this.ViewModel.ShoeType == ShoeType.Rollerblades
                ? this.Movement : Vector2.Zero;
      this.HandleKeyboard(ref movement, time);
      this.HandleGamepad(ref movement, time);
      this.HandleCursor(ref movement, time);
      this.oldMovement = this.Movement;
      this.Movement = movement;
      if (this.ViewModel.ShoeType != ShoeType.Rollerblades 
                || !(movement != Vector2.Zero) 
                || (double) Vector2.Distance(movement, Vector2.Zero) <= 2.0)
        return;
      movement.Normalize();
      movement *= 2f;
      this.Movement = movement;
    }

    private void HandleCursor(ref Vector2 movement, GameTime time)
    {
      if (!InputManager.IsLeftButtonDown)
        return;
      Vector2 vector2 = Resolution.TransformPointToWorld(InputManager.MousePosition, 
          Sharp2D.Engine.Common.Scene.Scene.CurrentScene.MainCamera) - this.GlobalPosition;
      vector2.Normalize();
      if (vector2 == Vector2.Zero)
        return;
      if (this.ViewModel.ShoeType == ShoeType.Rollerblades)
        vector2 *= (float) (2.0 * time.ElapsedGameTime.TotalSeconds);
      movement += vector2;
    }

    private void HandleGamepad(ref Vector2 movement, GameTime time)
    {
      Vector2 vector2 = GamePadManager.LeftThumbstickMovement();
      vector2 = new Vector2(vector2.X, -vector2.Y);
      if (vector2 == Vector2.Zero)
        return;
      if (this.ViewModel.ShoeType == ShoeType.Rollerblades)
        vector2 *= (float) (2.0 * time.ElapsedGameTime.TotalSeconds);
      movement += vector2;
    }

    private void HandleKeyboard(ref Vector2 movement, GameTime time)
    {
      Vector2 zero = Vector2.Zero;
      if (InputManager.IsKeyDown(new Keys?(Keys.A)))
        zero -= new Vector2(1f, 0.0f);
      if (InputManager.IsKeyDown(new Keys?(Keys.W)))
        zero -= new Vector2(0.0f, 1f);
      if (InputManager.IsKeyDown(new Keys?(Keys.D)))
        zero += new Vector2(1f, 0.0f);
      if (InputManager.IsKeyDown(new Keys?(Keys.S)))
        zero += new Vector2(0.0f, 1f);
      if (zero == Vector2.Zero)
        return;
      zero.Normalize();
      if (this.ViewModel.ShoeType == ShoeType.Rollerblades)
        zero *= (float) (2.0 * time.ElapsedGameTime.TotalSeconds);
      movement += zero;
    }

    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
      this.UpdateDirection();
      this.UpdateSpritesheets();
      base.Draw(batch, time);
    }

    private void UpdateSpritesheets()
    {
      if (this.Movement == Vector2.Zero && this.oldMovement != Vector2.Zero)
      {
        this.spriteWalk.Stop();
        this.spriteIdle.Start();
      }
      else
      {
        if (!(this.Movement != Vector2.Zero) || !(this.oldMovement == Vector2.Zero))
          return;
        this.spriteWalk.Start();
        this.spriteIdle.Stop();
      }
    }

    private void UpdateDirection()
    {
      ChasingGhosts.Windows.World.Movement movement = MovementHelper.GetMovement(this.Movement);
      if (movement == ChasingGhosts.Windows.World.Movement.None)
        return;
      this.spriteIdle.Direction = movement;
      this.spriteWalk.Direction = movement;
    }

    public void RefreshMovement()
    {
      this.MaxMovement = this.topSpeed;
      this.speedReducer.Restart();
    }

    public void EquipShoe(ShoeType type)
    {
      this.ViewModel.ShoeType = type;
      this.SetShoeSpeed();
      this.RefreshMovement();
    }

    private void SetShoeSpeed()
    {
      switch (this.ViewModel.ShoeType)
      {
        case ShoeType.None:
          this.topSpeed = 225f;
          break;
        case ShoeType.Sneakers:
          this.topSpeed = 250f;
          break;
        case ShoeType.Rollerblades:
          this.topSpeed = 350f;
          break;
        case ShoeType.FlipFlops:
          this.topSpeed = 200f;
          break;
      }
    }

    public async Task PlayDeathAnimation()
    {
      this.spriteWalk.IsVisible = false;
      this.spriteIdle.IsVisible = false;
      this.Components.Remove(this.spriteWalk);
      this.Components.Remove(this.spriteIdle);

      this.spriteDeath.IsVisible = true;
      this.spriteDeath.RegionKey = 0;
     
      await Task.Delay(500);      
      await this.WaitForUpdate();
      this.spriteDeath.RegionKey = 1;
     
      await Task.Delay(500);
      await this.WaitForUpdate();
      this.spriteDeath.RegionKey = 2;

      await Task.Delay(1000);      
      await this.WaitForUpdate();
    }
  }
}
