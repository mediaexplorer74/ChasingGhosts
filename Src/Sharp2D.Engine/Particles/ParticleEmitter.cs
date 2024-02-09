// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Particles.ParticleEmitter
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Sharp2D.Engine.Common.Components.Sprites;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Particles
{
  /// <summary>The particle emitter.</summary>
  public class ParticleEmitter : GameObject
  {
    /// <summary>The rnd.</summary>
    private readonly Random rnd;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Particles.ParticleEmitter" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="sprites">The sprites.</param>
    public ParticleEmitter(GameObject target, List<string> sprites)
    {
      this.Target = target;
      this.Sprites = sprites;
      this.rnd = new Random();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Particles.ParticleEmitter" /> class.
    /// </summary>
    public ParticleEmitter()
    {
      this.Sprites = new List<string>();
      this.rnd = new Random();
    }

    /// <summary>Gets or sets the area.</summary>
    public Rectangle? Area { get; set; }

    /// <summary>Gets or sets the how to particle.</summary>
    public Action<Particle> HowToParticle { get; set; }

    /// <summary>Gets the particles.</summary>
    public IEnumerable<Particle> Particles => this.Children.OfType<Particle>();

    /// <summary>Gets or sets the sprites.</summary>
    public List<string> Sprites { get; set; }

    /// <summary>Gets or sets the target.</summary>
    public GameObject Target { get; set; }

    /// <summary>The add particle.</summary>
    /// <returns>
    ///     The <see cref="T:Sharp2D.Engine.Particles.Particle" />.
    /// </returns>
    public Particle AddParticle()
    {
      Sprite texture = Sprite.Load(this.Sprites[this.rnd.Next(this.Sprites.Count)]);
      float x;
      float y;
      if (this.Area.HasValue)
      {
        Vector2 vector2 = SharpMathHelper.Rotate(GameObject.V2((float) this.rnd.Next(this.Area.Value.X, this.Area.Value.X + this.Area.Value.Width), (float) this.rnd.Next(this.Area.Value.Y, this.Area.Value.Y + this.Area.Value.Height)), Vector2.Zero, this.Target.GlobalRotation);
        x = this.Target.GlobalPosition.X + vector2.X;
        y = this.Target.GlobalPosition.Y + vector2.Y;
      }
      else
      {
        x = this.Target.GlobalPosition.X;
        y = this.Target.GlobalPosition.Y;
      }
      Particle particle = new Particle(new Vector2(x, y), texture);
      this.HowToParticle(particle);
      return particle;
    }

    /// <summary>The update.</summary>
    /// <param name="time">The time.</param>
    public override void Update(GameTime time)
    {
      for (int index = 0; index < 25; ++index)
        this.Children.Add((GameObject) this.AddParticle());
      base.Update(time);
      foreach (Particle particle in this.Particles.ToArray<Particle>())
      {
        if (particle.Duration <= TimeSpan.Zero)
          this.Children.Remove((GameObject) particle);
      }
    }
  }
}
