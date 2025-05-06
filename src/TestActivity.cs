using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

public class TestActivity : IWindowActivity
{
    // Testing Triangle
    readonly float[] m_vertices = new float[9]
    {
        -0.5f, -0.5f, 0.0f, // Left Bottom
        0.0f, 0.5f, 0.0f,   // Top
        0.5f, -0.5f, 0.0f   // Right  Bottom
    };

    int m_vao = -1, m_vbo = -1;
    Shader? m_defaultShader;


    public void ActivityLoad()
    {
        // Shader Creation
        m_defaultShader = new Shader("default");

        // Vertex Positions
        m_vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, m_vertices.Length * sizeof(float), m_vertices, BufferUsageHint.StaticDraw);

        // Create the final Composite of mesh data.
        m_vao = GL.GenVertexArray();
        GL.BindVertexArray(m_vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        Utils.Log("Test Activity has been Loaded.");
    }

    public void ActivityUpdate(double time)
    {
        
    }

    public void ActivityRender(double time)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
        GL.BindVertexArray(m_vao);
        m_defaultShader?.Use();

        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
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