using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

/// <summary>
/// A Base class for Vertex and Fragment Shaders.
/// </summary>
public class Shader : IDisposable
{
    private int m_ID = 0;
    private int m_V = 0, m_F = 0;

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

    public void Dispose()
    {
        Unuse();

        GL.DeleteProgram(m_ID);
        GL.DeleteShader(m_F);
        GL.DeleteShader(m_V);

        GC.SuppressFinalize(this);
    }
}
