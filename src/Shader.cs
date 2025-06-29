using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

/// <summary>
/// A Base class for Vertex and Fragment Shaders.
/// </summary>
public abstract class Shader : IDisposable
{
    private int _ID = 0;
    private int _V = 0, _F = 0;

    private bool _disposedValue = false;

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
        _V = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(_V, vertexCode);
        GL.CompileShader(_V);

        // Fragment Shader
        _F = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(_F, fragmentCode);
        GL.CompileShader(_F);

        // Create the Shader Program
        _ID = GL.CreateProgram();
        GL.AttachShader(_ID, _V);
        GL.AttachShader(_ID, _F);
        GL.LinkProgram(_ID);
    }

    ~Shader()
    {
        if (!_disposedValue)
        {
            Utils.Log("Shader Err: GPU resource leak! You forgot to call the Dispose Method!", ConsoleColor.Red);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            End();

            GL.DeleteProgram(_ID);
            GL.DeleteShader(_F);
            GL.DeleteShader(_V);

            _disposedValue = true;
        }
    }

    /// <summary>
    /// Enables the Shader Program.
    /// </summary>
    public void Begin()
    {
        GL.UseProgram(_ID);
    }

    /// <summary>
    /// Disables the Shader Program.
    /// </summary>
    public void End()
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
        int loc = GL.GetUniformLocation(_ID, name);
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
