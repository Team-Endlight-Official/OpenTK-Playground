using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

public class TestActivity : IWindowActivity
{
    readonly Window m_window;

    // Testing Triangle
    readonly float[] m_vertices = new float[24]
    {
        // front face
        -0.5f, -0.5f, 0.5f, // Left Bottom 0
        -0.5f, 0.5f, 0.5f,  // Left Top 1
        0.5f, 0.5f, 0.5f,   // Right Top 2
        0.5f, -0.5f, 0.5f,  // Right Bottom 3

        // back face
        -0.5f, -0.5f, -0.5f, // Left Bottom 4
        -0.5f, 0.5f, -0.5f,  // Left Top 5
        0.5f, 0.5f, -0.5f,   // Right Top 6
        0.5f, -0.5f, -0.5f   // Right Bottom 7
    };

    readonly uint[] m_indices = new uint[36]
    {
        // Front face
        0, 1, 2,
        2, 3, 0,

        // Back face
        7, 6, 5,
        5, 4, 7,

        // Top face
        1, 5, 6,
        6, 2, 1,

        // Bottom face
        7, 4, 0,
        0, 3, 7,

        // Left face
        4, 5, 1,
        1, 0, 4,

        // Right face
        3, 2, 6,
        6, 7, 3
    };

    readonly float[] m_uvCoords = new float[8]
    {
        // Front Face
        0.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f
    };

    int m_vao = -1, m_vbo = -1, m_ibo = -1, m_uvcb = -1;
    Shader? m_defaultShader;

    readonly float m_fov = MathHelper.DegreesToRadians(45f);
    Matrix4 model, view, proj, mvp;
    int m_mvpLoc;
    float m_time = 0;

    public TestActivity(Window window)
    {
        m_window = window;
    }

    public void ActivityLoad()
    {
        // Shader Creation
        m_defaultShader = new Shader("default");
        m_mvpLoc = m_defaultShader.GetUniformLocation("u_mvp");

        // Create the final Composite of mesh data.
        m_vao = GL.GenVertexArray();
        GL.BindVertexArray(m_vao);

        // Vertex Positions
        m_vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, m_vertices.Length * sizeof(float), m_vertices, BufferUsageHint.StaticDraw);

        // Indexed Positions
        m_ibo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ibo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, m_indices.Length * sizeof(uint), m_indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Testing up UV's
        m_uvcb = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_uvcb);
        GL.BufferData(BufferTarget.ArrayBuffer, m_uvCoords.Length * sizeof(float), m_uvCoords, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        Utils.Log("Test Activity has been Loaded.\n");
    }

    public void ActivityUpdate(double time)
    {
        m_time += 25f * (float)time;

        model = Matrix4.Identity * 
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(1 * m_time)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(2 * m_time)) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(3 * m_time)) *
            Matrix4.CreateTranslation(0f, 0f, -3f);
        view = Matrix4.LookAt(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f));
        proj = Matrix4.CreatePerspectiveFieldOfView(m_fov, m_window.AspectRatio, 0.1f, 100f);

        mvp = model * view * proj;
    }

    public void ActivityRender(double time)
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

        m_defaultShader?.SetUniformMatrix4(m_mvpLoc, mvp, true);

        GL.DrawElements(PrimitiveType.Triangles, m_indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void ActivityUnload()
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