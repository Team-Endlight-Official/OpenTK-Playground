using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;

public class TestActivity : WindowActivity
{
    // Testing Triangle
    VertexBook _vao;
    DefaultShader? m_defaultShader;

    readonly float m_fov = MathHelper.DegreesToRadians(45f);
    Matrix4 view, proj;
    Transform t;
    float m_time = 0;

    public TestActivity(Window context) : base(context)
    {
        
    }

    public override void Load()
    {
        // Shader Creation
        m_defaultShader = new DefaultShader();

        _vao = new VertexBook();
        _vao.Define(); // Creates and binds the VAO automatically!
        // Create the Position and Indexeding Order!
        _vao.AddVertexBuffer(new VertexBuffer()
            .Define(BufferTarget.ArrayBuffer)
            .InsertData(CubeData.Vertices.Length * sizeof(float), CubeData.Vertices, BufferUsageHint.StaticDraw));
        _vao.AddVertexBuffer(new VertexBuffer()
            .Define(BufferTarget.ElementArrayBuffer)
            .InsertData(CubeData.Indices.Length * sizeof(uint), CubeData.Indices, BufferUsageHint.StaticDraw)
            .Sign(0, 3, VertexAttribPointerType.Float, false, 0, 0));

        // Create the UV Coordinates!
        _vao.AddVertexBuffer(new VertexBuffer()
            .Define(BufferTarget.ArrayBuffer)
            .InsertData(CubeData.UVCoords.Length * sizeof(float), CubeData.UVCoords, BufferUsageHint.StaticDraw)
            .Sign(1, 2, VertexAttribPointerType.Float, false, 0, 0));
        _vao.Build(); // Finally build the fucking piece of shit! Crank it up just like what i did to your mother!s

        t = new Transform(new Vector3(1f, 0f, -5f));
        t.Scale = new Vector3(1f, 1f, 1f);

        Utils.Log("Test Activity has been Loaded.\n");
    }

    public override void Update(FrameEventArgs args)
    {
        m_time += 50f * (float)args.Time;

        view = Matrix4.LookAt(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f));
        proj = Matrix4.CreatePerspectiveFieldOfView(m_fov, AspectRatio, 0.1f, 100f);

        t.Rotation += new Vector3(1f, 1f, 0f) * m_time;
    }

    public override void Render(FrameEventArgs args)
    {
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Back);
        GL.FrontFace(FrontFaceDirection.Cw);

        _vao.Begin();
        m_defaultShader?.Use();

        m_defaultShader?.SetModel(t.Model);
        m_defaultShader?.SetView(view);
        m_defaultShader?.SetProjection(proj);

        GL.DrawElements(PrimitiveType.Triangles, CubeData.Indices.Length, DrawElementsType.UnsignedInt, 0);
        _vao.End();
    }

    public override void Unload()
    {
        _vao.Dispose();
        m_defaultShader?.Dispose();

        Utils.Log("Test Activity has been Unloaded.");
    }
}