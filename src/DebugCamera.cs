using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

public class DebugCamera : Camera
{
    // private members
    Vector3 cameraTarget;
    Vector3 cameraDirection;

    // public members
    public float Speed = 4.5f;

    public DebugCamera(WindowActivity activity) : base(activity)
    {
        UpdateView();
        UpdateProjection();
        UpdateTranform();
    }

    private void UpdateView()
    {
        View = Matrix4.LookAt(transform.Position, cameraTarget, transform.Up);
    }

    private void UpdateProjection()
    {
        Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), activity.AspectRatio, NearClip, FarClip);
    }

    private void UpdateInput(float time)
    {
        // Movement
        if (activity.Window.Keyboard.IsKeyDown(Keys.W)) 
            transform.Position += Speed * transform.Forward * time;
        if (activity.Window.Keyboard.IsKeyDown(Keys.S))
            transform.Position -= Speed * transform.Forward * time;
        if (activity.Window.Keyboard.IsKeyDown(Keys.D))
            transform.Position += Speed * transform.Right * time;
        if (activity.Window.Keyboard.IsKeyDown(Keys.A))
            transform.Position -= Speed * transform.Right * time;
    }

    private void UpdateTranform()
    {
        // Camera Direction;
        cameraTarget = transform.Position + transform.Forward;
        cameraDirection = Vector3.Normalize(transform.Position - cameraTarget);

        // Right Axis
        transform.Right = Vector3.Normalize(Vector3.Cross(transform.Up, cameraDirection));

        // Up Axis
        transform.Up = Vector3.Cross(cameraDirection, transform.Right);
    }

    public void Update(float time)
    {
        UpdateView();
        UpdateProjection();
        UpdateTranform();
        UpdateInput(time);
    }
}
