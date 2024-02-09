// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.Components.Animations.ScaleFunc
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Common.Components.Animations
{
  /// <summary>
  /// Takes in progress which is the percentage of the tween complete and returns
  /// the interpolation value that is fed into the lerp function for the tween.
  /// </summary>
  /// <remarks>
  /// Scale functions are used to define how the tween should occur. Examples would be linear,
  /// easing in quadratic, or easing out circular. You can implement your own scale function
  /// or use one of the many defined in the ScaleFuncs static class.
  /// </remarks>
  /// <param name="progress">The percentage of the tween complete in the range [0, 1].</param>
  /// <returns>The scale value used to lerp between the tween's start and end values</returns>
  public delegate float ScaleFunc(float progress);
}
