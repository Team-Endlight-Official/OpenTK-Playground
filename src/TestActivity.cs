using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

public class TestActivity : IWindowActivity
{
    // Testing Triangle
    readonly float[] m_vertices = new float[24]
    {
        // front face
        -0.5f, -0.5f, 0.5f, // Left Bottom 0
        -0.5f, 0.5f, 0.5f,  // Left Top 1
        0.5f, 0.5f, 0.5f,   // Right Top 2
        0.5f, -0.5f, 0.5f,    // Right Bottom 3

        // back face
        -0.5f, 0.5f, -0.5f, // Left Top 4
        -0.5f, -0.5f, -0.5f,  // Left Bottom 5
        0.5f, -0.5f, -0.5f,   // Right Bottom 6
        0.5f, 0.5f, -0.5f   // Right Top 7
    };

    readonly uint[] m_indices = new uint[12]
    {
        // Front face
        0, 1, 2,
        2, 3, 0,

        // Back Face
        4, 5, 6,
        6, 7, 4
    };

    int m_vao = -1, m_vbo = -1, m_ibo = -1;
    Shader? m_defaultShader;

    int m_uGammaLoc;
    float gamma = 1.0f;

    public void ActivityLoad()
    {
        // Shader Creation
        m_defaultShader = new Shader("default");

        // Vertex Positions
        m_vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, m_vertices.Length * sizeof(float), m_vertices, BufferUsageHint.StaticDraw);

        // Indexed Positions
        m_ibo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ibo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, m_indices.Length * sizeof(uint), m_indices, BufferUsageHint.StaticDraw);

        // Create the final Composite of mesh data.
        m_vao = GL.GenVertexArray();
        GL.BindVertexArray(m_vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        m_uGammaLoc = m_defaultShader.GetUniformLocation("u_gamma");

        Utils.Log("Test Activity has been Loaded.");
    }

    public void ActivityUpdate(double time)
    {
        gamma -= 0.2f * (float)time;
        if (gamma <= 0f) gamma = 1.0f;
    }

    public void ActivityRender(double time)
    {
        //GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Back);
        GL.FrontFace(FrontFaceDirection.Cw);

        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ibo);
        GL.BindVertexArray(m_vao);
        m_defaultShader?.Use();

        // Some little test to see if my shader uniform tweakin' works :D
        m_defaultShader?.SetUniformFloat(m_uGammaLoc, gamma);

        GL.DrawElements(PrimitiveType.Triangles, m_indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void ActivityUnload()
    {
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        m_defaultShader?.Dispose();
        GL.DeleteBuffer(m_vbo);
        GL.DeleteVertexArray(m_vao);

        Utils.Log("Test Activity has been Unloaded.");
    }
}