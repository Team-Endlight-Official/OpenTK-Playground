using OpenTK.Mathematics;

/// <summary>
/// Transform holds the MVP matrix and the Position, Rotation, Scale and World Space vectors.
/// </summary>
public class Transform
{
    private Vector3 m_position;
    private Vector3 m_rotation;
    private Vector3 m_scale;

    public static readonly Vector4 LOCAL = new Vector4(0f, 0f, 0f, 0f);
    public static readonly Vector4 WORLD = new Vector4(0f, 0f, 0f, 1f);

    private Matrix4 m_model;

    /// <summary>
    /// Transform holds the MVP matrix and the Position, Rotation, Scale and World Space vectors.
    /// </summary>
    public Transform()
    {
        m_position = new Vector3(0f, 0f, 0f);
        m_rotation = new Vector3(0f, 0f, 0f);
        m_scale = new Vector3(1f, 1f, 1f);

        m_model = Matrix4.Identity;
    }

    /// <summary>
    /// Transform holds the MVP matrix and the Position, Rotation, Scale and World Space vectors.
    /// </summary>
    /// <param name="position">: Position of your Transform.</param>
    public Transform(Vector3 position)
    {
        m_position = position;
        m_rotation = new Vector3(0f, 0f, 0f);
        m_scale = new Vector3(1.0f, 1.0f, 1.0f);

        m_model = Matrix4.Identity;
    }

    /// <summary>
    /// The Position of the Transform.
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return m_position;
        }
        set
        {
            m_position = value;
        }
    }

    /// <summary>
    /// The Rotation of the Transform in Eulers.
    /// </summary>
    public Vector3 Rotation
    {
        get
        {
            return m_rotation;
        }
        set
        {
            m_rotation.X = MathHelper.DegreesToRadians(value.X);
            m_rotation.Y = MathHelper.DegreesToRadians(value.Y);
            m_rotation.Z = MathHelper.DegreesToRadians(value.Z);
        }
    }

    /// <summary>
    /// The Scale of the Transform.
    /// </summary>
    public Vector3 Scale
    {
        get
        {
            return m_scale;
        }
        set
        {
            m_scale = value;
        }
    }

    /// <summary>
    /// The finally calculated model matrix.
    /// </summary>
    public Matrix4 Model
    {
        get
        {
            m_model =
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z) *
                Matrix4.CreateTranslation(Position);
            return m_model;
        }
    }
}