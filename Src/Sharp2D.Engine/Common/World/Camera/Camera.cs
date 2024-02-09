// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Common.World.Camera.Camera
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sharp2D.Engine.Common.Components;
using Sharp2D.Engine.Common.ObjectSystem;
using Sharp2D.Engine.Infrastructure;
using Sharp2D.Engine.Utility;
using System;
using System.Linq;

#nullable disable
namespace Sharp2D.Engine.Common.World.Camera
{
  /// <summary>
  ///     Game Camera. Allows you to pan around in the world, instead of being restricted to the window resolution.
  /// </summary>
  public class Camera : GameObject
  {
    /// <summary>The max zoom.</summary>
    private const float MaxZoom = 10f;
    /// <summary>The min zoom.</summary>
    private const float MinZoom = 1f;
    /// <summary>The _graphics.</summary>
    private static GraphicsDevice graphics;
    /// <summary>The _current zoom.</summary>
    private float currentZoom;
    /// <summary>The _translate center.</summary>
    private Vector2 translateCenter;

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Sharp2D.Engine.Common.World.Camera.Camera" /> class.
    ///     The constructor for the Camera class.
    /// </summary>
    public Camera()
    {
      this.currentZoom = 1f;
      this.EnableMovementTracking = true;
      this.EnableRotationTracking = false;
    }

    /// <summary>
    ///     Gets or sets a value indicating whether movement tracking is enabled.
    /// </summary>
    /// <value>
    ///     <c>true</c> if movement tracking is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool EnableMovementTracking { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether rotation tracking is enabled.
    /// </summary>
    /// <value>
    ///     <c>true</c> if rotation tracking is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool EnableRotationTracking { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this is the main camera of the scene.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this is the main camera; otherwise, <c>false</c>.
    /// </value>
    public bool MainCamera { get; set; }

    /// <summary>Gets or sets the projection.</summary>
    /// <value>The projection.</value>
    public Matrix Projection { get; set; }

    /// <summary>Gets or sets the simulation projection matrix.</summary>
    /// <value>The simulation projection.</value>
    public Matrix SimProjection { get; set; }

    /// <summary>Gets or sets the simulation view matrix.</summary>
    /// <value>The simulation view.</value>
    public Matrix SimView { get; set; }

    /// <summary>
    ///     Gets the tracker, if one exists in the Component collection.
    ///     <para>If one does not exist, returns null.</para>
    /// </summary>
    /// <value>The tracker.</value>
    public CameraTracker Tracker
    {
      get
      {
        return this.Components.FirstOrDefault<Component>((Func<Component, bool>) (k => k is CameraTracker)) as CameraTracker;
      }
      set
      {
        if (this.Tracker != null)
          this.Components.Remove((Component) this.Tracker);
        if (value == null)
          return;
        this.Components.Add((Component) value);
      }
    }

    /// <summary>Gets or sets the view.</summary>
    /// <value>The view.</value>
    public Matrix View { get; private set; }

    /// <summary>The current rotation of the camera in radians.</summary>
    public float Zoom
    {
      get => this.currentZoom;
      set => this.currentZoom = MathHelper.Clamp(value, 1f, 10f);
    }

    /// <summary>
    /// Converts a provided screen coordinate to world coordinates, based on the current camera location.
    ///     <para>
    /// Used (for instance) to use the mouse for placing something in the world.
    ///     </para>
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public Vector2 ConvertScreenToWorld(Vector2 location)
    {
      Vector3 source = new Vector3(location, 0.0f);
      Vector3 vector3 = Sharp2D.Engine.Common.World.Camera.Camera.graphics.Viewport.Unproject(source, this.SimProjection, this.SimView, Matrix.Identity);
      return new Vector2(vector3.X, vector3.Y);
    }

    /// <summary>
    /// Converts a provided world coordinate to screen coordinates, based on the current camera location.
    ///     <para>
    /// This is used to get the position of the object on the screen.
    ///         Used (for instance) to detecting if the mouse is clicking on a World Object.
    ///     </para>
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Xna.Framework.Vector2" />.
    /// </returns>
    public Vector2 ConvertWorldToScreen(Vector2 location)
    {
      Vector3 source = new Vector3(location, 0.0f);
      Vector3 vector3 = Sharp2D.Engine.Common.World.Camera.Camera.graphics.Viewport.Project(source, this.SimProjection, this.SimView, Matrix.Identity);
      return new Vector2(vector3.X, vector3.Y);
    }

    /// <summary>
    ///     Initializes this instance, and invokes <see cref="M:Sharp2D.Engine.Common.World.Camera.Camera.SetCameraSettings" />.
    /// </summary>
    /// <param name="resolver"></param>
    public override void Initialize(IResolver resolver)
    {
      base.Initialize(resolver);
      this.SetCameraSettings();
      if (!this.MainCamera)
        return;
      Sharp2D.Engine.Common.Scene.Scene.CurrentScene.MainCamera = this;
    }

    /// <summary>Moves the camera.</summary>
    /// <param name="amount">The amount.</param>
    public void MoveCamera(Vector2 amount) => this.LocalPosition = this.LocalPosition + amount;

    /// <summary>
    ///     Resets the camera to default values, and invokes <see cref="M:Sharp2D.Engine.Common.World.Camera.Camera.SetView" />.
    /// </summary>
    public void ResetCamera()
    {
      this.LocalPosition = Vector2.Zero;
      this.LocalRotation = 0.0f;
      this.currentZoom = 1f;
      this.SetView();
    }

    /// <summary>Rotates the camera.</summary>
    /// <param name="amount">The amount.</param>
    public void RotateCamera(float amount) => this.LocalRotation += amount;

    /// <summary>
    ///     Sets up default camera information (using the current property values).
    /// </summary>
    public void SetCameraSettings()
    {
      SharpGameManager sharpGameManager = this.Resolver.Resolve<SharpGameManager>();
      Sharp2D.Engine.Common.World.Camera.Camera.graphics = sharpGameManager.Graphics.GraphicsDevice;
      this.SimProjection = Matrix.CreateOrthographicOffCenter(0.0f, ConvertUnits.ToSimUnits(sharpGameManager.VirtualResolution.X), ConvertUnits.ToSimUnits(sharpGameManager.VirtualResolution.Y), 0.0f, 0.0f, 1f);
      this.Projection = Matrix.CreateOrthographicOffCenter(0.0f, ConvertUnits.ToSimUnits(sharpGameManager.WindowResolution.X), ConvertUnits.ToSimUnits(sharpGameManager.WindowResolution.Y), 0.0f, 0.0f, 1f);
      this.SimView = Matrix.Identity;
      this.View = Matrix.Identity;
      float num1 = sharpGameManager.WindowResolution.X / 2f;
      float num2 = sharpGameManager.WindowResolution.Y / 2f;
      float num3 = sharpGameManager.VirtualResolution.X / 2f;
      float num4 = sharpGameManager.VirtualResolution.Y / 2f;
      this.translateCenter = new Vector2(ConvertUnits.ToSimUnits(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.FixedScale ? num3 : num1), ConvertUnits.ToSimUnits(Sharp2D.Engine.Common.Scene.Scene.CurrentScene.FixedScale ? num4 : num2));
      this.SetView();
    }

    /// <summary>
    ///     Recalculates the view matrix, based on all the current information stored in this instance.
    /// </summary>
    public void SetView()
    {
      Matrix rotationZ = Matrix.CreateRotationZ(this.EnableRotationTracking ? -MathHelper.ToRadians(this.GlobalRotation) : 0.0f);
      Matrix scale = Matrix.CreateScale(this.currentZoom, this.currentZoom, 1f);
      Vector3 vector3_1 = new Vector3(this.translateCenter, 0.0f);
      Vector3 vector3_2 = new Vector3(this.EnableMovementTracking ? -ConvertUnits.ToSimUnits(this.GlobalPosition) : Vector2.Zero, 0.0f);
      this.SimView = Matrix.CreateTranslation(vector3_2) * rotationZ * scale * Matrix.CreateTranslation(vector3_1);
      Vector3 displayUnits = ConvertUnits.ToDisplayUnits(vector3_1);
      this.View = Matrix.CreateTranslation(ConvertUnits.ToDisplayUnits(vector3_2)) * rotationZ * scale * Matrix.CreateTranslation(displayUnits);
    }

    /// <summary>
    /// Post <see cref="M:Sharp2D.Engine.Common.World.Camera.Camera.Update(Microsoft.Xna.Framework.GameTime)" />, will invokes <see cref="M:Sharp2D.Engine.Common.World.Camera.Camera.SetView" />.
    /// </summary>
    /// <param name="gameTime">The game Time.</param>
    public override void Update(GameTime gameTime)
    {
      if (this.IsPaused)
        return;
      base.Update(gameTime);
      this.SetView();
    }
  }
}
