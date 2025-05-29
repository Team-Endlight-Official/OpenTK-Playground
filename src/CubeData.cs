
/// <summary>
/// The Cube Data that holds the Vertices, UVs, Indices and other geometric data for rendering.
/// </summary>
public class CubeData
{
    /// <summary>
    /// The Vertex position data.
    /// </summary>
    public static readonly float[] Vertices = new float[24]
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

    /// <summary>
    /// The Indexing order data.
    /// </summary>
    public static readonly uint[] Indices = new uint[36]
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

    /// <summary>
    /// UV Coordinates data.
    /// </summary>
    public static readonly float[] UVCoords = new float[8]
    {
        // Front Face
        0.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 1.0f,
        1.0f, 0.0f
    };
}
