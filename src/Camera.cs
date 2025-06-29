using OpenTK.Mathematics;
using System;

public abstract class Camera
{
    protected WindowActivity activity;
    public Transform transform;

    // Camera configurations
    public float FOV = 45f;
    public float NearClip = 0.1f;
    public float FarClip = 1000f;

    public Matrix4 View
    {
        get; protected set;
    }

    public Matrix4 Projection 
    { 
        get; protected set; 
    }

    public Camera(WindowActivity activity)
    {
        this.activity = activity;
        transform = new Transform(new Vector3(0f, 0f, 0f));
    }
}
