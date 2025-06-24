using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;

public class TestActivity : WindowActivity
{
    // Testing Triangle
    int m_vao = -1, m_vbo = -1, m_ibo = -1, m_uvcb = -1;
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

        // Create the final Composite of mesh data.
        m_vao = GL.GenVertexArray();
        GL.BindVertexArray(m_vao);

        // Vertex Positions
        m_vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, CubeData.Vertices.Length * sizeof(float), CubeData.Vertices, BufferUsageHint.StaticDraw);

        // Indexed Positions
        m_ibo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ibo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, CubeData.Indices.Length * sizeof(uint), CubeData.Indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Testing up UV's
        m_uvcb = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_uvcb);
        GL.BufferData(BufferTarget.ArrayBuffer, CubeData.UVCoords.Length * sizeof(float), CubeData.UVCoords, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        t = new Transform(new Vector3(1f, 0f, -5f));
        t.Scale = new Vector3(1f, 1f, 1f);

        Utils.Log("Test Activity has been Loaded.\n");
    }

    public override void Update(FrameEventArgs args)
    {
        m_time += 25f * (float)args.Time;

        view = Matrix4.LookAt(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f));
        proj = Matrix4.CreatePerspectiveFieldOfView(m_fov, AspectRatio, 0.1f, 100f);
    }

    public override void Render(FrameEventArgs args)
    {
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Back);
        GL.FrontFace(FrontFaceDirection.Cw);

        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ibo);
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_uvcb);
        GL.BindVertexArray(m_vao);
        m_defaultShader?.Use();

        m_defaultShader?.SetModel(t.Model);
        m_defaultShader?.SetView(view);
        m_defaultShader?.SetProjection(proj);

        GL.DrawElements(PrimitiveType.Triangles, CubeData.Indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public override void Unload()
    {
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        m_defaultShader?.Dispose();
        GL.DeleteBuffer(m_vbo);
        GL.DeleteBuffer(m_ibo);
        GL.DeleteBuffer(m_uvcb);
        GL.DeleteVertexArray(m_vao);

        Utils.Log("Test Activity has been Unloaded.");
    }
}