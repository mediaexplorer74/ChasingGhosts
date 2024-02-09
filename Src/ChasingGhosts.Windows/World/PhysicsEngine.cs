// ChasingGhosts.Windows.World.PhysicsEngine

using ChasingGhosts.Windows.Interfaces;
using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ChasingGhosts.Windows.World
{
  public class PhysicsEngine : Component
  {
    private readonly List<IWall> walls = new List<IWall>();
    private readonly List<IMovableCharacter> characters = new List<IMovableCharacter>();
    private readonly List<IShoePrint> shoeprints = new List<IShoePrint>();
    private IMusicManager musicManager;

    public override void Initialize(IResolver resolver)
    {
      this.musicManager = resolver.Resolve<IMusicManager>();
      base.Initialize(resolver);
      this.Hook(this.Parent);
    }

    private void Hook(GameObject obj)
    {
      obj.ChildObjectMoved -= new GameObjectMoved(this.Item_ChildObjectMoved);
      obj.ChildObjectMoved += new GameObjectMoved(this.Item_ChildObjectMoved);
      if (obj is IWall wall && !this.walls.Contains(wall))
        this.walls.Add(wall);
      if (obj is IMovableCharacter movableCharacter && !this.characters.Contains(movableCharacter))
        this.characters.Add(movableCharacter);
      if (obj is IShoePrint shoePrint && !this.shoeprints.Contains(shoePrint))
        this.shoeprints.Add(shoePrint);
      foreach (GameObject child in obj.Children)
        this.Hook(child);
    }

    private void Item_ChildObjectMoved(GameObject sender, ChildObjectMovedArgs args)
    {
      switch (args.Action)
      {
        case ChildObjectMoveAction.Added:
        case ChildObjectMoveAction.Inserted:
          this.Hook(args.Child);
          break;
        case ChildObjectMoveAction.Removed:
          args.Child.ChildObjectMoved -= new GameObjectMoved(this.Item_ChildObjectMoved);
          break;
      }
    }

    public override void Update(GameTime time)
    {
      base.Update(time);
      this.HandleMovement(time);
      this.HandlePlayerPrints();
    }

    private void HandlePlayerPrints()
    {
      Player player = this.characters.OfType<Player>().First<Player>();
      Rectanglef globalRegion = player.GlobalRegion;
      foreach (IShoePrint shoePrint in this.shoeprints.ToArray())
      {
        if (!shoePrint.IsActive)
        {
          this.shoeprints.Remove(shoePrint);
        }
        else
        {
          Vector2 center = shoePrint.GlobalRegion.Center;
          if (globalRegion.Contains((int) center.X, (int) center.Y))
          {
            player.RefreshMovement();
            shoePrint.Dismiss();
            this.musicManager.Transition(shoePrint.Level);
            EventHandler removedFootprint = this.RemovedFootprint;
            if (removedFootprint != null)
              removedFootprint((object) this, EventArgs.Empty);
          }
        }
      }
    }

    public event EventHandler RemovedFootprint;

    private void HandleMovement(GameTime time)
    {
      float totalSeconds = (float) time.ElapsedGameTime.TotalSeconds;
      foreach (IMovableCharacter character1 in this.characters)
      {
        IMovableCharacter character = character1;
        Vector2 movement1 = character.Movement;
        if (!(movement1 == Vector2.Zero))
        {
          movement1.Normalize();
          Vector2 movement2 = movement1 * character.MaxMovement * totalSeconds;
          if (!(movement2 == Vector2.Zero))
          {
            Rectanglef globalRegion = character.GlobalRegion;
            PhysicsEngine.CheckMovement((IEnumerable<IPhysicsEntity>) this.walls, 
                globalRegion, ref movement2);
            if (!(character is Player player) || !player.ViewModel.IsInvulnerable)
              PhysicsEngine.CheckMovement((IEnumerable<IPhysicsEntity>) 
                  this.characters.Where<IMovableCharacter>((Func<IMovableCharacter, bool>) 
                  (c => c != character)), globalRegion, ref movement2);

            IMovableCharacter movableCharacter = character;
            movableCharacter.LocalPosition = movableCharacter.LocalPosition + movement2;
          }
        }
      }
    }

    private static void CheckMovement(
      IEnumerable<IPhysicsEntity> list,
      Rectanglef startRegion,
      ref Vector2 movement)
    {
      foreach (IPhysicsEntity physicsEntity in list)
      {
        Rectanglef rectanglef = new Rectanglef(startRegion.X + movement.X,
            startRegion.Y + movement.Y, startRegion.Width, startRegion.Height);
        Rectanglef globalRegion = physicsEntity.GlobalRegion;
        if (globalRegion.Intersects(rectanglef))
        {
          ref Vector2 local1 = ref movement;
          int? nullable = PhysicsEngine.CheckRight(startRegion, rectanglef, globalRegion);
          Vector2 vector2_1 = new Vector2((float) (nullable ?? (double) movement.X), movement.Y);
          local1 = vector2_1;
          ref Vector2 local2 = ref movement;
          nullable = PhysicsEngine.CheckLeft(startRegion, rectanglef, globalRegion);
          Vector2 vector2_2 = new Vector2((float) (nullable ?? (double) movement.X), movement.Y);
          local2 = vector2_2;
          ref Vector2 local3 = ref movement;
          double x1 = (double) movement.X;
          nullable = PhysicsEngine.CheckBottom(startRegion, rectanglef, globalRegion);
          double y1 = nullable ?? (double) movement.Y;
          Vector2 vector2_3 = new Vector2((float) x1, (float) y1);
          local3 = vector2_3;
          ref Vector2 local4 = ref movement;
          double x2 = (double) movement.X;
          nullable = PhysicsEngine.CheckTop(startRegion, rectanglef, globalRegion);
          double y2 = nullable ?? (double) movement.Y;
          Vector2 vector2_4 = new Vector2((float) x2, (float) y2);
          local4 = vector2_4;
        }
      }
    }

    private static int? CheckTop(Rectanglef startRegion, Rectanglef endRegion, Rectanglef wallReg)
    {
      return (double) wallReg.Bottom >= (double) endRegion.Top &&
               (double) wallReg.Bottom <= (double) startRegion.Top
                    ? new int?((int) ((double) wallReg.Bottom - (double) startRegion.Top)) 
                    : new int?();
    }

    private static int? CheckBottom(
      Rectanglef startRegion,
      Rectanglef endRegion,
      Rectanglef wallReg)
    {
      return (double) wallReg.Top <= (double) endRegion.Bottom 
                && (double) wallReg.Top >= (double) startRegion.Bottom 
                    ? new int?((int) ((double) wallReg.Top - (double) startRegion.Bottom)) 
                    : new int?();
    }

    private static int? CheckRight(
      Rectanglef startRegion,
      Rectanglef endRegion,
      Rectanglef wallReg)
    {
      return (double) wallReg.Left <= (double) endRegion.Right 
                && (double) wallReg.Left >= (double) startRegion.Right 
                    ? new int?((int) ((double) wallReg.Left - (double) startRegion.Right))
                    : new int?();
    }

    private static int? CheckLeft(Rectanglef startRegion, Rectanglef endRegion, Rectanglef wallReg)
    {
      return (double) wallReg.Right >= (double) endRegion.Left
                && (double) wallReg.Right <= (double) startRegion.Left 
                ? new int?((int) ((double) wallReg.Right - (double) startRegion.Left))
                : new int?();
    }
  }
}
