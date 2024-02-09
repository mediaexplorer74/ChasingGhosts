// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Utility.ConvertUnits
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;

#nullable disable
namespace Sharp2D.Engine.Utility
{
  /// <summary>
  ///     Convert units between display and simulation units.
  /// </summary>
  public static class ConvertUnits
  {
    private static float displayUnitsToSimUnitsRatio = 100f;
    private static float simUnitsToDisplayUnitsRatio = 1f / ConvertUnits.displayUnitsToSimUnitsRatio;

    public static void SetDisplayUnitToSimUnitRatio(float displayUnitsPerSimUnit)
    {
      ConvertUnits.displayUnitsToSimUnitsRatio = displayUnitsPerSimUnit;
      ConvertUnits.simUnitsToDisplayUnitsRatio = 1f / displayUnitsPerSimUnit;
    }

    public static float ToDisplayUnits(float simUnits)
    {
      return simUnits * ConvertUnits.displayUnitsToSimUnitsRatio;
    }

    public static float ToDisplayUnits(int simUnits)
    {
      return (float) simUnits * ConvertUnits.displayUnitsToSimUnitsRatio;
    }

    public static Vector2 ToDisplayUnits(Vector2 simUnits)
    {
      return simUnits * ConvertUnits.displayUnitsToSimUnitsRatio;
    }

    public static void ToDisplayUnits(ref Vector2 simUnits, out Vector2 displayUnits)
    {
      Vector2.Multiply(ref simUnits, ConvertUnits.displayUnitsToSimUnitsRatio, out displayUnits);
    }

    public static Vector3 ToDisplayUnits(Vector3 simUnits)
    {
      return simUnits * ConvertUnits.displayUnitsToSimUnitsRatio;
    }

    public static Vector2 ToDisplayUnits(float x, float y)
    {
      return new Vector2(x, y) * ConvertUnits.displayUnitsToSimUnitsRatio;
    }

    public static void ToDisplayUnits(float x, float y, out Vector2 displayUnits)
    {
      displayUnits = Vector2.Zero;
      displayUnits.X = x * ConvertUnits.displayUnitsToSimUnitsRatio;
      displayUnits.Y = y * ConvertUnits.displayUnitsToSimUnitsRatio;
    }

    public static float ToSimUnits(float displayUnits)
    {
      return displayUnits * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static float ToSimUnits(double displayUnits)
    {
      return (float) displayUnits * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static float ToSimUnits(int displayUnits)
    {
      return (float) displayUnits * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static Vector2 ToSimUnits(Vector2 displayUnits)
    {
      return displayUnits * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static Vector3 ToSimUnits(Vector3 displayUnits)
    {
      return displayUnits * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static void ToSimUnits(ref Vector2 displayUnits, out Vector2 simUnits)
    {
      Vector2.Multiply(ref displayUnits, ConvertUnits.simUnitsToDisplayUnitsRatio, out simUnits);
    }

    public static Vector2 ToSimUnits(float x, float y)
    {
      return new Vector2(x, y) * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static Vector2 ToSimUnits(double x, double y)
    {
      return new Vector2((float) x, (float) y) * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }

    public static void ToSimUnits(float x, float y, out Vector2 simUnits)
    {
      simUnits = Vector2.Zero;
      simUnits.X = x * ConvertUnits.simUnitsToDisplayUnitsRatio;
      simUnits.Y = y * ConvertUnits.simUnitsToDisplayUnitsRatio;
    }
  }
}
