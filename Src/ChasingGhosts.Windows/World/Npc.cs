// ChasingGhosts.Windows.World.Npc


using ChasingGhosts.Windows.Interfaces;
using ChasingGhosts.Windows.UI;
using ChasingGhosts.Windows.ViewModels;
using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Animations;
using Sharp2D.Engine.Common.Components.Audio;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;
using System;

#nullable disable
namespace ChasingGhosts.Windows.World
{
  public class Npc : GameObject, IMovableCharacter, IPhysicsEntity
  {
    private readonly Player player;
    private readonly PlayerViewModel viewModel;
    private GameTimer attackTimer;
    private MovementSprite spriteWalk;
    private bool hasSeenPlayer;
    private static readonly string[] Quotes = new string[5]
    {
      "Traitor!",
      "Why do you chase humans?!",
      "You horrible robot-being",
      "Life is futil-... eh machinery is futile!",
      "Human-lover!"
    };

    public Npc(Player player, PlayerViewModel viewModel)
    {
      this.player = player;
      this.viewModel = viewModel;
    }

    public ChasingGhosts.Windows.World.Movement Direction { get; private set; }

    public float MaxMovement { get; private set; } = 200f;

    public Vector2 Movement { get; private set; }

    public override void Initialize(IResolver resolver)
    {
      this.Width = 72f;
      this.Height = 72f;
      Sprite sprite = new Sprite(Color.GreenYellow * 0.25f, (int) this.Width, (int) this.Height);
      sprite.CenterObject();
      this.Components.Add((Component) sprite);
      sprite.IsVisible = false;
      this.spriteWalk = new MovementSprite("npc_walk", TimeSpan.FromSeconds(0.5));
      this.spriteWalk.Start();
      this.Components.Add((Component) this.spriteWalk);
      this.hasSeenPlayer = false;
      base.Initialize(resolver);
    }

    public override void Update(GameTime time)
    {
      base.Update(time);
      if (this.IsPaused)
      {
        this.Movement = Vector2.Zero;
      }
      else
      {
        this.UpdateSeenPlayer();
        if (!this.hasSeenPlayer)
        {
          this.Movement = Vector2.Zero;
        }
        else
        {
          this.HandlePlayerRange();
          this.HandleMovement();
          this.UpdateDirection();
        }
      }
    }

    private void UpdateSeenPlayer()
    {
      if (this.hasSeenPlayer)
        return;
      Vector2 vector2_1 = this.GlobalPosition - this.player.GlobalPosition;
      Vector2 vector2_2 = Resolution.VirtualScreen / 2f;
      if ((double) Math.Abs(vector2_1.X) > (double) vector2_2.X || (double) Math.Abs(vector2_1.Y) > (double) vector2_2.Y)
        return;
      this.hasSeenPlayer = true;
      this.AddTextBubble();
    }

    private void AddTextBubble()
    {
      Random random = new Random();
      SpeechBubble speechBubble = new SpeechBubble();
      speechBubble.LocalPosition = new Vector2(30f, -50f);
      speechBubble.Text = Npc.Quotes[random.Next(0, Npc.Quotes.Length)];
      SpeechBubble speech = speechBubble;
      this.Add((GameObject) speech);
      GameTimer timer = new GameTimer(TimeSpan.FromSeconds(5.0));
      timer.Expired += (EventHandler) ((s, e) =>
      {
        this.Components.Remove((Component) timer);
        ValueAnimator.PlayAnimation((GameObject) speech, (Action<float>) (f => speech.Opacity = 1f - f), TimeSpan.FromSeconds(0.5));
      });
      this.Components.Add((Component) timer);
    }

    private void UpdateDirection()
    {
      ChasingGhosts.Windows.World.Movement movement = MovementHelper.GetMovement(this.Movement);
      if (movement == ChasingGhosts.Windows.World.Movement.None)
        return;
      this.spriteWalk.Direction = movement;
    }

    private void HandlePlayerRange()
    {
      if (this.attackTimer != null || !this.IsCloseEnoughToHit())
        return;
      this.attackTimer = new GameTimer(TimeSpan.FromSeconds(0.75));
      this.attackTimer.Expired += (EventHandler) ((s, e) =>
      {
        this.Components.Remove((Component) this.attackTimer);
        this.Attack();
        this.StartCooldown();
      });
      this.Components.Add((Component) this.attackTimer);
    }

    private void StartCooldown()
    {
      this.MaxMovement = 50f;
      GameTimer cooldown = new GameTimer(TimeSpan.FromSeconds(1.0));
      cooldown.Expired += (EventHandler) ((s, e) =>
      {
        this.Components.Remove((Component) cooldown);
        this.attackTimer = (GameTimer) null;
        this.MaxMovement = 200f;
      });
      this.Components.Add((Component) cooldown);
    }

    private void Attack()
    {
      if (!this.IsCloseEnoughToHit() || this.viewModel.IsInvulnerable)
        return;
      this.viewModel.DamagePlayer(15f);
      AudioEffect hit = new AudioEffect(new Random().Next(2) == 0 ? "Audio/hit" : "Audio/hit2");
      this.Components.Add((Component) hit);
      hit.Play();
      hit.Stopped += (EventHandler) ((s, e) => this.Components.Remove((Component) hit));
    }

    private bool IsCloseEnoughToHit()
    {
      return (double) Vector2.Distance(this.GlobalPosition, this.player.GlobalPosition) <= 100.0;
    }

    private void HandleMovement()
    {
      Vector2 vector2 = this.player.GlobalPosition - this.GlobalPosition;
      vector2.Normalize();
      this.Movement = vector2;
    }

    public void PlayerDied()
    {
      this.IsPaused = true;
      this.Movement = Vector2.Zero;
    }
  }
}
