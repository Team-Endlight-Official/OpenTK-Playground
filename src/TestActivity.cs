using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;

public class TestActivity : WindowActivity
{
    // Testing Triangle
    VertexArray _vao;
    DefaultShader? _defaultShader;

    DebugCamera _debugCamera;
    Transform t;
    float _time = 0;

    public TestActivity(Window context) : base(context)
    {
        
    }

    public override void Load()
    {
        // Shader Creation
        _defaultShader = new DefaultShader();

        _vao = new VertexArray();
        _vao.Prepare(); // Creates and binds the VAO automatically!
        // Create the Position and Indexeding Order!
        _vao.AddVertexBuffer(new VertexBuffer()
            .Prepare(BufferTarget.ArrayBuffer)
            .InsertData(CubeData.Vertices.Length * sizeof(float), CubeData.Vertices, BufferUsageHint.StaticDraw));
        _vao.AddVertexBuffer(new VertexBuffer()
            .Prepare(BufferTarget.ElementArrayBuffer)
            .InsertData(CubeData.Indices.Length * sizeof(uint), CubeData.Indices, BufferUsageHint.StaticDraw)
            .Build(0, 3, VertexAttribPointerType.Float, false, 0, 0));

        // Create the UV Coordinates!
        _vao.AddVertexBuffer(new VertexBuffer()
            .Prepare(BufferTarget.ArrayBuffer)
            .InsertData(CubeData.UVCoords.Length * sizeof(float), CubeData.UVCoords, BufferUsageHint.StaticDraw)
            .Build(1, 2, VertexAttribPointerType.Float, false, 0, 0));

        _vao.Build(); // Finally build the fucking piece of shit! Crank it up just like what i did to your mother!s

        t = new Transform(new Vector3(1f, 0f, -5f));
        t.Scale = new Vector3(1f, 1f, 1f);

        _debugCamera = new DebugCamera(this);

        Utils.Log("Test Activity has been Loaded.\n");
    }

    public override void Update(FrameEventArgs args)
    {
        _time += 50f * (float)args.Time;
        t.Rotation += new Vector3(1f, 1f, 0f) * _time;

        _debugCamera.Update((float)args.Time);
    }

    public override void Render(FrameEventArgs args)
    {
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Back);
        GL.FrontFace(FrontFaceDirection.Cw);

        _defaultShader?.Begin();
        _vao.Begin();

        _defaultShader?.SetModel(t.Model);
        _defaultShader?.SetView(_debugCamera.View);
        _defaultShader?.SetProjection(_debugCamera.Projection);

        GL.DrawElements(PrimitiveType.Triangles, CubeData.Indices.Length, DrawElementsType.UnsignedInt, 0);
        _vao.End();
        _defaultShader?.End();
    }

    public override void Unload()
    {
        _vao.Dispose();
        _defaultShader?.Dispose();

        Utils.Log("Test Activity has been Unloaded.");
    }
}