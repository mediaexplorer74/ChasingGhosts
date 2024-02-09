// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Audio.AudioEffect
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Sharp2D.Engine.Drawing;
using Sharp2D.Engine.Infrastructure;
using System;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Audio
{
  /// <summary>
  ///     A general audio component that can manage a single audio instance.
  ///     Technically this could be used for any sound effect.
  /// </summary>
  public class AudioEffect : Component
  {
    /// <summary>The last state.</summary>
    protected SoundState LastState;
    /// <summary>The sound effect.</summary>
    protected SoundEffect SoundEffect;
    /// <summary>The sound effect instance.</summary>
    protected SoundEffectInstance SoundEffectInstance;
    /// <summary>The asset name.</summary>
    private string assetName;
    private ContentManager contentManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Audio.AudioEffect" /> class.
    /// </summary>
    /// <param name="assetName">Name of the asset.</param>
    public AudioEffect(string assetName)
      : this()
    {
      this.AssetName = assetName;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.Components.Audio.AudioEffect" /> class.
    /// </summary>
    public AudioEffect()
    {
      this.LastState = SoundState.Stopped;
      this.Volume = 1f;
    }

    /// <summary>Occurs when this audio effect is paused.</summary>
    public event EventHandler Paused;

    /// <summary>Occurs when this audio effect is resumed.</summary>
    public event EventHandler Resumed;

    /// <summary>Occurs when this audio effect is stopped.</summary>
    public event EventHandler Stopped;

    /// <summary>
    ///     Gets or sets the name of the asset.
    ///     <para>Setting this will also load the audio effect.</para>
    /// </summary>
    /// <value>The name of the asset.</value>
    public string AssetName
    {
      get => this.assetName;
      set
      {
        this.assetName = value;
        this.UpdateSoundEffect();
      }
    }

    /// <summary>
    ///     Gets or sets the pitch.
    ///     <para>Default value is 0</para>
    /// </summary>
    /// <value>The pitch.</value>
    public float Pitch { get; set; }

    /// <summary>
    ///     Gets or sets the volume of the sound effect.
    ///     <para>Goes from 0-1. Default is 1</para>
    /// </summary>
    /// <value>The volume.</value>
    public float Volume { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is playing.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
    /// </value>
    public bool IsPlaying { get; private set; }

    /// <summary>
    /// Loads and returns a new instance of <see cref="T:Sharp2D.Engine.Common.Components.Audio.AudioEffect" />, using audio loaded from the specified path.
    /// </summary>
    /// <param name="path">The content path.</param>
    /// <returns>
    /// The <see cref="T:Sharp2D.Engine.Common.Components.Audio.AudioEffect" />.
    /// </returns>
    public static AudioEffect Load(string path) => new AudioEffect(path);

    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.contentManager = resolver.Resolve<ContentManager>();
      this.UpdateSoundEffect();
    }

    /// <summary>You cannot draw an audio effect, dummy.</summary>
    /// <param name="batch">The batch.</param>
    /// <param name="time">The game time.</param>
    public override void Draw(SharpDrawBatch batch, GameTime time)
    {
    }

    /// <summary>Starts playing the loaded audio.</summary>
    /// <param name="loop">
    /// if set to <c>true</c> loops the audio until manually stopped. Useful for background music or similar.
    /// </param>
    public virtual void Play(bool loop = false)
    {
      this.SoundEffectInstance = this.SoundEffect != null ? this.SoundEffect.CreateInstance() : throw new ArgumentNullException("SoundEffect", "You cannot play a sound effect that has nothing loaded!");
      this.SoundEffectInstance.IsLooped = loop;
      this.SoundEffectInstance.Pitch = this.Pitch;
      this.SoundEffectInstance.Volume = this.Volume;
      this.SoundEffectInstance.Play();
      this.IsPlaying = true;
    }

    /// <summary>Stops the specified immediate.</summary>
    /// <param name="immediate">
    /// if set to <c>false</c> will not stop the audio until it has finished.
    ///     <para>
    /// This is used when looping sound effects
    ///     </para>
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// SoundEffectInstance;You cannot stop a sound effect that isn't playing!
    /// </exception>
    public virtual void Stop(bool immediate = true)
    {
      if (this.SoundEffectInstance == null)
        throw new ArgumentNullException("SoundEffectInstance", "You cannot stop a sound effect that isn't playing!");
      this.SoundEffectInstance.Stop(immediate);
    }

    /// <summary>
    /// Called every frame. Makes sure to keep track of the audios state, and fires any appropriate events.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    public override void Update(GameTime gameTime)
    {
      if (this.SoundEffectInstance == null)
        return;
      SoundState state = this.SoundEffectInstance.State;
      if (this.LastState == SoundState.Playing && state == SoundState.Stopped)
      {
        EventHandler stopped = this.Stopped;
        if (stopped != null)
          stopped((object) this, EventArgs.Empty);
        this.IsPlaying = false;
      }
      if (this.LastState == SoundState.Playing && state == SoundState.Paused)
      {
        EventHandler paused = this.Paused;
        if (paused != null)
          paused((object) this, EventArgs.Empty);
        this.IsPlaying = false;
      }
      if (this.LastState == SoundState.Paused && state == SoundState.Playing)
      {
        EventHandler resumed = this.Resumed;
        if (resumed != null)
          resumed((object) this, EventArgs.Empty);
        this.IsPlaying = true;
      }
      this.LastState = state;
    }

    private void UpdateSoundEffect()
    {
      if (this.contentManager == null || string.IsNullOrEmpty(this.assetName))
        return;
      this.SoundEffect = this.contentManager.Load<SoundEffect>(this.assetName);
    }
  }
}
