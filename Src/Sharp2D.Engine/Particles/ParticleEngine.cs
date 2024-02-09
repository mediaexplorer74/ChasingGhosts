// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Particles.ParticleEngine
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Sharp2D.Engine.Common.ObjectSystem;
using System.Collections.Generic;

#nullable disable
namespace Sharp2D.Engine.Particles
{
  /// <summary>The particle engine.</summary>
  public class ParticleEngine : GameObject
  {
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Particles.ParticleEngine" /> class.
    /// </summary>
    public ParticleEngine() => this.Emitters = new List<ParticleEmitter>();

    /// <summary>Gets the emitters.</summary>
    public List<ParticleEmitter> Emitters { get; private set; }

    /// <summary>The create emitter.</summary>
    /// <param name="target">The target.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Particles.ParticleEmitter" />.
    /// </returns>
    public ParticleEmitter CreateEmitter(GameObject target)
    {
      ParticleEmitter emitter = new ParticleEmitter(target, new List<string>());
      this.Emitters.Add(emitter);
      return emitter;
    }
  }
}
