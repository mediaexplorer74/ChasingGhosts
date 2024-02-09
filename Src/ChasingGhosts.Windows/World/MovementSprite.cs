// ChasingGhosts.Windows.World.MovementSprite

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Animations;
using Sharp2D.Engine.Common.Components.Animations.Predefined;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ChasingGhosts.Windows.World
{
  public class MovementSprite : Component
  {
    private readonly string assetName;
    private readonly TimeSpan duration;
    private FragmentedSpriteSheet sheet;
    private EventValueAnimator anim;
    private bool started;
    private float scale = 2f;

    public MovementSprite(string assetName, TimeSpan duration)
    {
      this.assetName = assetName;
      this.duration = duration;
    }

    public float Scale
    {
      get => this.scale;
      set
      {
        this.scale = value;
        if (this.sheet == null)
          return;
        this.sheet.Scale = new Vector2(value);
      }
    }

    public override void Initialize(IResolver resolver)
    {
      this.sheet = new FragmentedSpriteSheet(this.assetName, new SpriteSheetFragment()
      {
        Groups = new List<SpriteSheetFragmentGroup>()
        {
          MovementSprite.CreateFragmentGroup("down", 0, 4),
          MovementSprite.CreateFragmentGroup("right", 1, 4),
          MovementSprite.CreateFragmentGroup("left", 2, 4),
          MovementSprite.CreateFragmentGroup("top", 3, 4)
        }
      });
      this.sheet.Scale = new Vector2(this.Scale);
      this.sheet.IsVisible = true;
      this.Children.Add((Component) this.sheet);
      this.anim = new EventValueAnimator(this.duration, true);
      this.anim.ValueUpdated += (EventHandler<float>) ((s, val) =>
      {
        string animationKey = this.GetAnimationKey();
        if (string.IsNullOrEmpty(animationKey))
          return;
        List<int> group = this.sheet.Groups[animationKey];
        int val2 = (int) ((double) val * (double) group.Count);
        int index = Math.Min(group.Count - 1, val2);
        this.sheet.RegionKey = group[index];
      });
      this.anim.Easing = AnimationEase.Linear;
      this.anim.Loop = true;
      this.anim.IsPaused = false;
      this.Children.Add((Component) this.anim);
      base.Initialize(resolver);
      if (!this.started)
        return;
      this.Start();
    }

    private string GetAnimationKey()
    {
      switch (this.Direction)
      {
        case Movement.Right:
          return "right";
        case Movement.Down:
          return "down";
        case Movement.Left:
          return "left";
        case Movement.Top:
          return "top";
        default:
          return (string) null;
      }
    }

    public Movement Direction { get; set; }

    private static SpriteSheetFragmentGroup CreateFragmentGroup(
      string groupName,
      int yStart,
      int frameCount)
    {
      return new SpriteSheetFragmentGroup()
      {
        TransformOrigin = new Vector2?(new Vector2(0.5f, 0.6f)),
        GroupName = groupName,
        Frames = Enumerable.Range(0, frameCount).Select<int, Rectangle>((Func<int, Rectangle>) (i => new Rectangle(i * 48, yStart * 48, 48, 48))).ToList<Rectangle>()
      };
    }

    public void Stop()
    {
      this.started = false;
      this.IsVisible = false;
      this.IsPaused = true;
      this.anim.Stop();
    }

    public void Start()
    {
      this.started = true;
      this.IsVisible = true;
      this.IsPaused = false;
      this.anim?.Restart();
    }
  }
}
