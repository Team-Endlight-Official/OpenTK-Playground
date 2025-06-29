using OpenTK.Graphics.OpenGL;
using System;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// The VBO in a simplified matter. Put any type of data in this sub container.
/// </summary>
public class VertexBuffer : IDisposable
{
    private bool _disposedValue = false;
    private int _handle = -1;

    // Vertex Buffer Data
    private BufferTarget _target;
    private int _index;
    private bool _built = false;

    ~VertexBuffer()
    {
        if (!_disposedValue)
        {
            Utils.Log("Vertex Buffer Err: GPU resource leak! You forgot to call the Dispose Method!", ConsoleColor.Red);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            End();
            Clear();
            Utils.Log("Vertex Buffer has been Disposed", ConsoleColor.DarkGray);

            _disposedValue = true;
        }
    }

    /// <summary>
    /// [CHAINED METHOD] Creates a starting point for the VBO.
    /// </summary>
    /// <param name="target">Your Buffer target for the VBO.</param>
    public VertexBuffer Prepare(BufferTarget target)
    {
        _target = target;
        _handle = GL.GenBuffer();
        GL.BindBuffer(_target, _handle);
        _built = false;
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Insert any type of data into the VBO.
    /// </summary>
    /// <param name="size">Size of the data.</param>
    /// <param name="data">Raw float data.</param>
    /// <param name="usage">Buffer usage hinting.</param>
    public VertexBuffer InsertData(int size, float[] data, BufferUsageHint usage)
    {
        GL.BufferData(_target, size, data, usage);
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Insert any type of data into the VBO.
    /// </summary>
    /// <param name="size">Size of the data.</param>
    /// <param name="data">Raw int data.</param>
    /// <param name="usage">Buffer usage hinting.</param>
    public VertexBuffer InsertData(int size, int[] data, BufferUsageHint usage)
    {
        GL.BufferData(_target, size, data, usage);
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Insert any type of data into the VBO.
    /// </summary>
    /// <param name="size">Size of the data.</param>
    /// <param name="data">Raw unsigned data.</param>
    /// <param name="usage">Buffer usage hinting.</param>
    public VertexBuffer InsertData(int size, uint[] data, BufferUsageHint usage)
    {
        GL.BufferData(_target, size, data, usage);
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Sign the final VBO product, letting the VAO know that it is a part of it and ready to use.
    /// </summary>
    /// <param name="index">Index location of the product data in the GLSL shader.</param>
    /// <param name="size">Maximum data number per row.</param>
    /// <param name="type">Vertex attribute pointer type.</param>
    /// <param name="normalized">Is it all normalized?</param>
    /// <param name="stride">Stride of the data.</param>
    /// <param name="offset">Offset of the data..</param>
    public VertexBuffer Build(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
    {
        _index = index;
        GL.VertexAttribPointer(_index, size, type, normalized, stride, offset);
        GL.EnableVertexAttribArray(_index);
        _built = true;
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Enables the VBO attribute pointer, hinting ready to use. (THIS IS ALL CALLED AUTOMATICALLY WITHIN 'Vertex Book')
    /// </summary>
    public VertexBuffer Begin()
    {
        if (!_built) return this;

        GL.EnableVertexAttribArray(_index);
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Disables the VBO attribute pointer, right after the drawcall making place for other VAO. (THIS IS ALL CALLED AUTOMATICALLY WITHIN 'Vertex Book')
    /// </summary>
    public VertexBuffer End()
    {
        if (!_built) return this;

        GL.DisableVertexAttribArray(_index);
        return this;
    }

    /// <summary>
    /// [CHAINED METHOD] Deletes the Data of the VBO (THIS IS ALL CALLED AUTOMATICALLY WITHIN THE DISPOSE METHOD. DO NOT CALL THIS!)
    /// </summary>
    public VertexBuffer Clear()
    {
        GL.BindBuffer(_target, 0);
        GL.DeleteBuffer(_handle);
        return this;
    }

    /// <summary>
    /// Deletes and disables all data within this VBO. Call this always before the Game is closed to prevent leaking!
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        //Utils.Log("Vertex Buffer has been Disposed", ConsoleColor.DarkGray);
        GC.SuppressFinalize(this);
    }
}
