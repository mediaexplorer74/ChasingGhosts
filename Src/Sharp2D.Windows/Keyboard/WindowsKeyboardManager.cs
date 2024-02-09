// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.Keyboard.WindowsKeyboardManager
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Sharp2D.Engine.Common;
using Sharp2D.Engine.Common.UI.Controls;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Infrastructure.Input;

#nullable disable
namespace Sharp2D.Windows.Keyboard
{
  /// <summary>Windows Keyboard Manager.</summary>
  public class WindowsKeyboardManager : KeyboardManager
  {
    private readonly TextBox parent;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Sharp2D.Windows.Keyboard.WindowsKeyboardManager" /> class.
    /// </summary>
    /// <param name="parent"></param>
    public WindowsKeyboardManager(TextBox parent) => this.parent = parent;

    public static void InitializeManager()
    {
      if (Sharp2DApplication.GameManager?.Window != null)
        EventInput.Initialize(Sharp2DApplication.GameManager.Window);
      else
        Sharp2DApplication.GameCreated += (GameCreated) ((sender, args) => EventInput.Initialize(Sharp2DApplication.GameManager.Window));
    }

    public override void Initialize(IResolver resolver)
    {
      EventInput.CharEntered += (CharEnteredHandler) ((sender, args) =>
      {
        if (!this.parent.HasFocus)
          return;
        Label textField = this.parent.TextField;
        if (args.Character == '\b')
        {
          if (textField.Text.Length <= 0)
            return;
          textField.Text = textField.Text.Substring(0, textField.Text.Length - 1);
        }
        else
        {
          if (!textField.FontDefinition.GetFont().Characters.Contains(args.Character))
            return;
          textField.Text += args.Character.ToString();
        }
      });
      base.Initialize(resolver);
    }
  }
}
