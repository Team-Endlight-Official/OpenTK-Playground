using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

/// <summary>
/// A Base class for Vertex and Fragment Shaders.
/// </summary>
public class Shader : IDisposable
{
    private int m_ID = 0;
    private int m_V = 0, m_F = 0;

    private bool m_disposedValue = false;

    /// <summary>
    /// A Base class for Vertex and Fragment Shaders.
    /// </summary>
    /// <param name="shaderPath">: Your specified shader path. NOTE: '.vert' and '.frag' files must have the same nanme.</param>
    public Shader(string shaderPath)
    {
        // Reading the Text Files
        string vertexCode = Utils.ReadFileContentsFromData($"Shaders/{shaderPath}.vert");
        string fragmentCode = Utils.ReadFileContentsFromData($"Shaders/{shaderPath}.frag");

        // Vertex Shader
        m_V = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(m_V, vertexCode);
        GL.CompileShader(m_V);

        // Fragment Shader
        m_F = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(m_F, fragmentCode);
        GL.CompileShader(m_F);

        // Create the Shader Program
        m_ID = GL.CreateProgram();
        GL.AttachShader(m_ID, m_V);
        GL.AttachShader(m_ID, m_F);
        GL.LinkProgram(m_ID);
    }

    ~Shader()
    {
        if (!m_disposedValue)
        {
            Utils.Log("Shader Err: GPU resource leak! You forgot to call the Dispose Method!", ConsoleColor.Red);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!m_disposedValue)
        {
            Unuse();

            GL.DeleteProgram(m_ID);
            GL.DeleteShader(m_F);
            GL.DeleteShader(m_V);

            m_disposedValue = true;
        }
    }

    /// <summary>
    /// Enables the Shader Program.
    /// </summary>
    public void Use()
    {
        GL.UseProgram(m_ID);
    }

    /// <summary>
    /// Disables the Shader Program.
    /// </summary>
    public void Unuse()
    {
        GL.UseProgram(0);
    }

    // Public Methods

    /// <summary>
    /// Gets the location of an value in your GLSL codes.
    /// </summary>
    /// <param name="name">: Uniform name in your GLSL codes.</param>
    public int GetUniformLocation(string name)
    {
        int loc = GL.GetUniformLocation(m_ID, name);
        if (loc == -1)
        {
            Utils.Log($"Shader Err: Uniform name of '{name}' does not exist.");
            return -1;
        }

        return loc;
    }

    /// <summary>
    /// Sets an uniform float to your desires.
    /// </summary>
    /// <param name="loc">: The uniform location within your GLSL codes.</param>
    /// <param name="value">: Your desired value.</param>
    public void SetUniformFloat(int loc, float value)
    {
        GL.Uniform1(loc, value);
    }

    /// <summary>
    /// Sets an uniform int to your desires.
    /// </summary>
    /// <param name="loc">: The uniform location within your GLSL codes.</param>
    /// <param name="value">: Your desired value.</param>
    public void SetUniformInt(int loc, int value)
    {
        GL.Uniform1(loc, value);
    }

    /// <summary>
    /// Sets an uniform Matrix4x4 to your desires.
    /// </summary>
    /// <param name="loc">: The uniform location within your GLSL codes.</param>
    /// <param name="value">: Your desired value.</param>
    public void SetUniformMatrix4(int loc, Matrix4 value, bool transpose = false)
    {
        GL.UniformMatrix4(loc, transpose, ref value);
    }

    /// <summary>
    /// Disposes the Shader. a MUST to prevent GPU resource leaking.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        Utils.Log("Shader Program has been Disposed", ConsoleColor.DarkGray);
        GC.SuppressFinalize(this);
    }
}
