// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.Keyboard.EventInput
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Sharp2D.Windows.Keyboard
{
  /// <summary>Static class for handling Win32 keyboard events.</summary>
  public static class EventInput
  {
    private static bool initialized;
    private static IntPtr prevWndProc;
    private static EventInput.WndProc hookProcDelegate;
    private static IntPtr hImc;
    private const int GwlWndproc = -4;
    private const int WmKeydown = 256;
    private const int WmKeyup = 257;
    private const int WmChar = 258;
    private const int WmImeSetcontext = 641;
    private const int WmInputlangchange = 81;
    private const int WmGetdlgcode = 135;
    private const int WmImeComposition = 271;
    private const int DlgcWantallkeys = 4;

    /// <summary>Event raised when a character has been entered.</summary>
    public static event CharEnteredHandler CharEntered;

    /// <summary>
    /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
    /// </summary>
    public static event KeyEventHandler KeyDown;

    /// <summary>Event raised when a key has been released.</summary>
    public static event KeyEventHandler KeyUp;

    [DllImport("Imm32.dll")]
    private static extern IntPtr ImmGetContext(IntPtr hWnd);

    [DllImport("Imm32.dll")]
    private static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hImc);

    [DllImport("user32.dll")]
    private static extern IntPtr CallWindowProc(
      IntPtr lpPrevWndFunc,
      IntPtr hWnd,
      uint msg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    /// <summary>Initialize the TextInput with the given GameWindow.</summary>
    /// <param name="window">The XNA window to which text input should be linked.</param>
    public static void Initialize(GameWindow window)
    {
      if (EventInput.initialized)
        throw new InvalidOperationException("TextInput.Initialize can only be called once!");
      EventInput.hookProcDelegate = new EventInput.WndProc(EventInput.HookProc);
      EventInput.prevWndProc = (IntPtr) EventInput.SetWindowLong(window.Handle, -4, (int) Marshal.GetFunctionPointerForDelegate((Delegate) EventInput.hookProcDelegate));
      EventInput.hImc = EventInput.ImmGetContext(window.Handle);
      EventInput.initialized = true;
    }

    private static IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
      IntPtr num = EventInput.CallWindowProc(EventInput.prevWndProc, hWnd, msg, wParam, lParam);
      switch (msg)
      {
        case 81:
          EventInput.ImmAssociateContext(hWnd, EventInput.hImc);
          num = (IntPtr) 1;
          break;
        case 135:
          num = (IntPtr) (num.ToInt32() | 4);
          break;
        case 256:
          if (EventInput.KeyDown != null)
          {
            EventInput.KeyDown((object) null, new KeyEventArgs((Keys) (int) wParam));
            break;
          }
          break;
        case 257:
          if (EventInput.KeyUp != null)
          {
            EventInput.KeyUp((object) null, new KeyEventArgs((Keys) (int) wParam));
            break;
          }
          break;
        case 258:
          if (EventInput.CharEntered != null)
          {
            EventInput.CharEntered((object) null, new CharacterEventArgs((char) (int) wParam, lParam.ToInt32()));
            break;
          }
          break;
        case 641:
          if (wParam.ToInt32() == 1)
          {
            EventInput.ImmAssociateContext(hWnd, EventInput.hImc);
            break;
          }
          break;
      }
      return num;
    }

    private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
  }
}
