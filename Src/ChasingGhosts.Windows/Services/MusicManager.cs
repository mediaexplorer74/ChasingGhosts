// ChasingGhosts.Windows.Services.MusicManager

using ChasingGhosts.Windows.Interfaces;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.Components.Animations;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ChasingGhosts.Windows.Services
{
  public class MusicManager : Component, IMusicManager
  {
    private readonly ContentManager contentManager;
    private readonly List<SoundEffectInstance> songs = new List<SoundEffectInstance>();
    private int currentLevel;

    public MusicManager(ContentManager contentManager) => this.contentManager = contentManager;

    public void LoadSongs(params string[] songAssets)
    {
      foreach (SoundEffectInstance soundEffectInstance in this.songs.ToArray())
      {
        soundEffectInstance.Stop();
        soundEffectInstance.Dispose();
        this.songs.Remove(soundEffectInstance);
      }
      foreach (string songAsset in songAssets)
      {
        SoundEffectInstance instance = this.contentManager.Load<SoundEffect>(songAsset).CreateInstance();
        instance.IsLooped = true;
        instance.Volume = 0.0f;
        instance.Play();
        this.songs.Add(instance);
      }
      this.songs.First<SoundEffectInstance>().Volume = 0.8f;
    }

    public void Transition(int level)
    {
      if (this.currentLevel >= level)
        return;
      SoundEffectInstance current = this.songs[this.currentLevel];
      SoundEffectInstance next = this.songs[level];
      this.currentLevel = level;
      if (current == next)
      {
        current.Volume = 0.8f;
      }
      else
      {
        TimeSpan duration = TimeSpan.FromSeconds(1.0);
        ValueAnimator.PlayAnimation(this.Parent, (Action<float>) 
            (val => current.Volume = (float) ((1.0 - (double) val) * 0.800000011920929)), duration);
        ValueAnimator.PlayAnimation(this.Parent, (Action<float>) 
            (val => next.Volume = val * 0.8f), duration);
      }
    }

    public void EndSongs()
    {
      foreach (SoundEffectInstance song in this.songs)
        song.Volume = 0.0f;
    }
  }
}
