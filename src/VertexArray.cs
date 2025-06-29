using OpenTK.Graphics.OpenGL;
using System;

/// <summary>
/// The VAO in a simplified matter. You can define countless of Vertex Buffers and tends to be more readable and friendlier to work with!
/// </summary>
public class VertexArray : IDisposable
{
    private bool _disposedValue = false;
    private bool _built = false;

    private int _handle = -1;
    private List<VertexBuffer> _vertexBuffers;

    ~VertexArray()
    {
        if (!_disposedValue)
        {
            Utils.Log("Vertex Book Err: GPU resource leak! You forgot to call the Dispose Method!", ConsoleColor.Red);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            End();

            foreach (VertexBuffer buffer in _vertexBuffers)
            {
                buffer.Dispose();
            }

            GL.DeleteVertexArray(_handle);

            _built = false;
            _disposedValue = true;
        }
    }

    /// <summary>
    /// Creates the starting point for the VAO.
    /// </summary>
    public void Prepare()
    {
        _handle = GL.GenVertexArray();
        GL.BindVertexArray(_handle);

        _vertexBuffers = new List<VertexBuffer>();
        _built = false;
    }

    /// <summary>
    /// Adds a Vertex Buffer Data to this VAO.
    /// </summary>
    /// <param name="vertexBuffer">A vertex buffer object data.</param>
    public void AddVertexBuffer(VertexBuffer vertexBuffer)
    {
        int count = _vertexBuffers.Count;
        _vertexBuffers.Add(vertexBuffer);

        Utils.Log($"Vertex Buffer Nr. {count} has been added to the Vertex Book!");
    }

    /// <summary>
    /// Builds the final VAO product and is ready to be used!
    /// </summary>
    /// <exception cref="ArgumentNullException">Crashes when there are no VBO's in this VAO. A VAO must have a VBO for setting up the rendering order!</exception>
    public void Build()
    {
        // get all defined Vertex Buffers and build/create them.
        if (_vertexBuffers.Count <= 0) throw new ArgumentNullException("There are no VBO's defined in this Vertex Book.");

        GL.BindVertexArray(0);
        _built = true;
    }

    /// <summary>
    /// Begin the rendering preparation. Call this before any draw call!
    /// </summary>
    public void Begin()
    {
        if (!_built) throw new Exception("This VAO has not been built!");

        GL.BindVertexArray(_handle);
        foreach (VertexBuffer buffer in _vertexBuffers)
        {
            buffer.Begin();
        }
    }

    /// <summary>
    /// End the rendering/drawing. Call this after any draw call!
    /// </summary>
    public void End()
    {
        if (!_built) throw new Exception("This VAO has not been built!");

        foreach (VertexBuffer buffer in _vertexBuffers)
        {
            buffer.End();
        }
        GL.BindVertexArray(0);
    }

    /// <summary>
    /// Disposes the VAO with it's VBO's!
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        Utils.Log("Vertex Book has been Disposed", ConsoleColor.DarkGray);
        GC.SuppressFinalize(this);
    }
}
