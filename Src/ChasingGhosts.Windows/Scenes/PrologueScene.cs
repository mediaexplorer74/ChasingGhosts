// ChasingGhosts.Windows.Scenes.PrologueScene

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.Components.Animations;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Common.UI.Enums;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Utility;


#nullable disable
namespace ChasingGhosts.Windows.Scenes
{
  public class PrologueScene : Sharp2D.Engine.Common.Scene.Scene
  {
    private readonly IResolver resolver;

    private CancellationTokenSource tokenSource;
    
    
    public PrologueScene(IResolver resolver)
      : base(resolver)
    {
      this.resolver = resolver;
      Sharp2DApplication.GameManager.BackgroundColor = Color.Black;
    }

    public override async void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.tokenSource = new CancellationTokenSource();
      try
      {
        await this.StartSequence(this.tokenSource.Token);
      }
      catch (TaskCanceledException ex)
      {
        await this.LoadGame();
      }
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (InputManager.GetHeldKeys().Length == 0)
        return;
      this.tokenSource.Cancel();
    }

    private async Task StartSequence(CancellationToken token)
    {
      PrologueScene prologueScene = this;
      await prologueScene.Delay(1000, token);
      Label lbl = prologueScene.CreateLabel("POST", Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.DelayDots(lbl, token);
      Label label1 = lbl;
      float labelLength1 = PrologueScene.GetLabelLength(label1);
      lbl = prologueScene.CreateLabel("SUCCESS", Color.Green);
      lbl.LocalPosition = new Vector2((float) ((double) label1.LocalPosition.X 
          + (double) labelLength1 + 10.0), label1.LocalPosition.Y);
      
      await prologueScene.Delay(500, token);
      
      lbl = prologueScene.CreateLabel("Initializing hardware drivers", Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.DelayDots(lbl, token);
      Label label2 = lbl;
      float labelLength2 = PrologueScene.GetLabelLength(label2);
      lbl = prologueScene.CreateLabel("SUCCESS", Color.Green);
      lbl.LocalPosition = new Vector2((float) ((double) label2.LocalPosition.X 
          + (double) labelLength2 + 10.0), label2.LocalPosition.Y);
      await prologueScene.Delay(500, token);
      
      SoundEffectInstance windowsLaunch = Load("Audio/windows_launch");
      windowsLaunch.Play();
      
      lbl = prologueScene.CreateLabel("Loading operation instructions", Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.DelayDots(lbl, token);
      
      SoundEffectInstance windowsError = Load("Audio/windows_error");
      Label label3 = lbl;
      float labelLength3 = PrologueScene.GetLabelLength(label3);
      lbl = prologueScene.CreateLabel("SUCC", Color.Green);
      lbl.LocalPosition = new Vector2((float) ((double) label3.LocalPosition.X 
          + (double) labelLength3 + 10.0), label3.LocalPosition.Y);
      await prologueScene.Delay(250, token);
      lbl.Text += ".-48tTQ#\"%rqawj912gGaf129'\"/";
      lbl.Tint = Color.IndianRed;
      windowsLaunch.Stop();
      windowsError.Play();
      await prologueScene.Delay(1500, token);
      lbl = prologueScene.CreateLabel(
          "Critical Error, restoring operating system \\\\ROBOT-OS\\backups\\shoe_obsession.bak", 
          Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.Delay(1500, token);
      lbl = prologueScene.CreateLabel("...", Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.Delay(1500, token);
      lbl = prologueScene.CreateLabel("...", Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.Delay(1500, token);
      lbl = prologueScene.CreateLabel("...", Color.Gray);
      prologueScene.InsertLabelLine(lbl);
      SoundEffectInstance windowsBsod = Load("Audio/windows_bsod");
      await prologueScene.Delay(500, token);
      lbl = prologueScene.CreateLabel("System restored!", Color.Green);
      prologueScene.InsertLabelLine(lbl);
      windowsError.Stop();
      windowsBsod.Volume = 0.25f;
      windowsBsod.Play();
      await prologueScene.Delay(3000, token);
      foreach (Label label4 in prologueScene.UiRoot.Children.OfType<Label>().ToArray<Label>())
        prologueScene.UiRoot.Children.Remove((GameObject) label4);
      await prologueScene.Delay(1500, token);
      lbl = prologueScene.CreateLabel("\"What are these? Foot prints? From actual humans?!\"",
          Color.YellowGreen);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.Delay(3000, token);
      lbl = prologueScene.CreateLabel("\"I must find this specimen!\"", Color.YellowGreen);
      prologueScene.InsertLabelLine(lbl);
      await prologueScene.Delay(5000, token);

        
        // ISSUE: reference to a compiler-generated method
        //       var target = token;
        // ValueAnimator.PlayAnimation((GameObject) prologueScene.UiRoot,
        //     new Action<float>(this.StartSequence(target)),//(prologueScene.\u003CStartSequence\u003Eb__5_1),
        //     TimeSpan.FromSeconds(1.0));
        ValueAnimator.PlayAnimation(
        this.UiRoot,
        val =>
        {
            foreach (var label in this.UiRoot.Children.OfType<Label>())
            {
                label.Tint = Color.YellowGreen * (1f - val);
            }
        },
        TimeSpan.FromSeconds(1f));


        await prologueScene.Delay(1500, token);
        await prologueScene.LoadGame();

      SoundEffectInstance Load(string asset)
      {
        SoundEffectInstance instance = this.resolver.Resolve<ContentManager>()
                    .Load<SoundEffect>(asset).CreateInstance();

        instance.Volume = 0.8f;
        return instance;
      }
    }

        private async Task LoadGame()
        {
            await Sharp2D.Engine.Common.Scene.Scene.Load(
                (Sharp2D.Engine.Common.Scene.Scene)new GameScene(this.resolver));
        }

        private async Task DelayDots(Label lbl, CancellationToken token)
    {
      await this.Delay(500, token);
      lbl.Text += ".";
      await this.Delay(500, token);
      lbl.Text += ".";
      await this.Delay(500, token);
      lbl.Text += ".";
      await this.Delay(500, token);
    }

    private static float GetLabelLength(Label label)
    {
      return label.FontDefinition.GetFont().MeasureString(label.Text).X;
    }

    private async Task Delay(int ms, CancellationToken token)
    {
      PrologueScene prologueScene = this;
      await Task.Delay(ms, token);


            // ISSUE: explicit non-virtual call
            //await __nonvirtual (prologueScene.WaitForUpdate());
            await Task.Delay(ms, token);
            await this.WaitForUpdate();
        }

    private void InsertLabelLine(Label lbl)
    {
      lbl.LocalPosition = new Vector2(30f, 30f);
      foreach (Label label1 in this.UiRoot.Children.OfType<Label>())
      {
        if (label1 != lbl)
        {
          Label label2 = label1;
          label2.LocalPosition = label2.LocalPosition + new Vector2(0.0f, 40f);
        }
      }
    }

    private Label CreateLabel(string text, Color textColor)
    {
      Label label = new Label(new FontDefinition("DefaultFont", 12f))
      {
        FontSize = 12f,
        Alignment = TextAlignment.Left,
        Tint = textColor,
        Text = text
      };
      this.UiRoot.Add((GameObject) label);
      return label;
    }
  }
}
