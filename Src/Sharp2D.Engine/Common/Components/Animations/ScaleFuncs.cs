// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.ScaleFuncs
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System;

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations
{
  /// <summary>
  /// Defines a set of premade scale functions for use with tweens.
  /// </summary>
  /// <remarks>
  /// To avoid excess allocations of delegates, the public members of ScaleFuncs are already
  /// delegates that reference private methods.
  /// 
  /// Implementations based on http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c
  /// which are based on http://www.robertpenner.com/easing/
  /// </remarks>
  public static class ScaleFuncs
  {
    /// <summary>A linear progress scale function.</summary>
    public static readonly ScaleFunc Linear = new ScaleFunc(ScaleFuncs.LinearImpl);
    /// <summary>
    /// A quadratic (x^2) progress scale function that eases in.
    /// </summary>
    public static readonly ScaleFunc QuadraticEaseIn = (ScaleFunc) (p => ScaleFuncs.EaseInPower(p, 2));
    /// <summary>
    /// A quadratic (x^2) progress scale function that eases out.
    /// </summary>
    public static readonly ScaleFunc QuadraticEaseOut = (ScaleFunc) (p => ScaleFuncs.EaseOutPower(p, 2));
    /// <summary>
    /// A quadratic (x^2) progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc QuadraticEaseInOut = (ScaleFunc) (p => ScaleFuncs.EaseInOutPower(p, 2));
    /// <summary>A cubic (x^3) progress scale function that eases in.</summary>
    public static readonly ScaleFunc CubicEaseIn = (ScaleFunc) (p => ScaleFuncs.EaseInPower(p, 3));
    /// <summary>A cubic (x^3) progress scale function that eases out.</summary>
    public static readonly ScaleFunc CubicEaseOut = (ScaleFunc) (p => ScaleFuncs.EaseOutPower(p, 3));
    /// <summary>
    /// A cubic (x^3) progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc CubicEaseInOut = (ScaleFunc) (p => ScaleFuncs.EaseInOutPower(p, 3));
    /// <summary>
    /// A quartic (x^4) progress scale function that eases in.
    /// </summary>
    public static readonly ScaleFunc QuarticEaseIn = (ScaleFunc) (p => ScaleFuncs.EaseInPower(p, 4));
    /// <summary>
    /// A quartic (x^4) progress scale function that eases out.
    /// </summary>
    public static readonly ScaleFunc QuarticEaseOut = (ScaleFunc) (p => ScaleFuncs.EaseOutPower(p, 4));
    /// <summary>
    /// A quartic (x^4) progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc QuarticEaseInOut = (ScaleFunc) (p => ScaleFuncs.EaseInOutPower(p, 4));
    /// <summary>
    /// A quintic (x^5) progress scale function that eases in.
    /// </summary>
    public static readonly ScaleFunc QuinticEaseIn = (ScaleFunc) (p => ScaleFuncs.EaseInPower(p, 5));
    /// <summary>
    /// A quintic (x^5) progress scale function that eases out.
    /// </summary>
    public static readonly ScaleFunc QuinticEaseOut = (ScaleFunc) (p => ScaleFuncs.EaseOutPower(p, 5));
    /// <summary>
    /// A quintic (x^5) progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc QuinticEaseInOut = (ScaleFunc) (p => ScaleFuncs.EaseInOutPower(p, 5));
    /// <summary>A sextic (x^6) progress scale function that eases in.</summary>
    public static readonly ScaleFunc SexticEaseIn = (ScaleFunc) (p => ScaleFuncs.EaseInPower(p, 6));
    /// <summary>
    /// A sextic (x^6) progress scale function that eases out.
    /// </summary>
    public static readonly ScaleFunc SexticEaseOut = (ScaleFunc) (p => ScaleFuncs.EaseOutPower(p, 6));
    /// <summary>
    /// A sextic (x^6) progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc SexticEaseInOut = (ScaleFunc) (p => ScaleFuncs.EaseInOutPower(p, 6));
    /// <summary>A septic (x^7) progress scale function that eases in.</summary>
    public static readonly ScaleFunc SepticEaseIn = (ScaleFunc) (p => ScaleFuncs.EaseInPower(p, 7));
    /// <summary>
    /// A septic (x^7) progress scale function that eases out.
    /// </summary>
    public static readonly ScaleFunc SepticEaseOut = (ScaleFunc) (p => ScaleFuncs.EaseOutPower(p, 7));
    /// <summary>
    /// A septic (x^7) progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc SepticEaseInOut = (ScaleFunc) (p => ScaleFuncs.EaseInOutPower(p, 7));
    /// <summary>A sinusoidal progress scale function that eases in.</summary>
    public static readonly ScaleFunc SineEaseIn = new ScaleFunc(ScaleFuncs.SineEaseInImpl);
    /// <summary>A sinusoidal progress scale function that eases out.</summary>
    public static readonly ScaleFunc SineEaseOut = new ScaleFunc(ScaleFuncs.SineEaseOutImpl);
    /// <summary>
    /// A sinusoidal progress scale function that eases in and out.
    /// </summary>
    public static readonly ScaleFunc SineEaseInOut = new ScaleFunc(ScaleFuncs.SineEaseInOutImpl);
    private const float Pi = 3.14159274f;
    private const float HalfPi = 1.57079637f;

    private static float LinearImpl(float progress) => progress;

    private static float EaseInPower(float progress, int power)
    {
      return (float) Math.Pow((double) progress, (double) power);
    }

    private static float EaseOutPower(float progress, int power)
    {
      int num = power % 2 == 0 ? -1 : 1;
      return (float) num * ((float) Math.Pow((double) progress - 1.0, (double) power) + (float) num);
    }

    private static float EaseInOutPower(float progress, int power)
    {
      progress *= 2f;
      if ((double) progress < 1.0)
        return (float) Math.Pow((double) progress, (double) power) / 2f;
      int num = power % 2 == 0 ? -1 : 1;
      return (float) ((double) num / 2.0 * (Math.Pow((double) progress - 2.0, (double) power) + (double) (num * 2)));
    }

    private static float SineEaseInImpl(float progress)
    {
      return (float) Math.Sin((double) progress * 1.5707963705062866 - 1.5707963705062866) + 1f;
    }

    private static float SineEaseOutImpl(float progress)
    {
      return (float) Math.Sin((double) progress * 1.5707963705062866);
    }

    private static float SineEaseInOutImpl(float progress)
    {
      return (float) (Math.Sin((double) progress * 3.1415927410125732 - 1.5707963705062866) + 1.0) / 2f;
    }
  }
}
