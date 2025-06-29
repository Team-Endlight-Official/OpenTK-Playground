using OpenTK.Mathematics;
using System.Data.SqlTypes;

/// <summary>
/// Transform holds the MVP matrix and the Position, Rotation, Scale and World Space vectors.
/// </summary>
public class Transform
{
    private Vector3 _position;
    private Vector3 _rotation;
    private Vector3 _scale;

    public Vector3 Forward = -Vector3.UnitZ;
    public Vector3 Right = Vector3.UnitX;
    public Vector3 Up = Vector3.UnitY;

    public static readonly Vector4 LOCAL = new Vector4(0f, 0f, 0f, 0f);
    public static readonly Vector4 WORLD = new Vector4(0f, 0f, 0f, 1f);

    private Matrix4 _model;

    /// <summary>
    /// Transform holds the MVP matrix and the Position, Rotation, Scale and World Space vectors.
    /// </summary>
    public Transform()
    {
        _position = new Vector3(0f, 0f, 0f);
        _rotation = new Vector3(0f, 0f, 0f);
        _scale = new Vector3(1f, 1f, 1f);

        _model = Matrix4.Identity;
    }

    /// <summary>
    /// Transform holds the MVP matrix and the Position, Rotation, Scale and World Space vectors.
    /// </summary>
    /// <param name="position">: Position of your Transform.</param>
    public Transform(Vector3 position)
    {
        _position = position;
        _rotation = new Vector3(0f, 0f, 0f);
        _scale = new Vector3(1.0f, 1.0f, 1.0f);

        _model = Matrix4.Identity;
    }

    /// <summary>
    /// The Position of the Transform.
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

    /// <summary>
    /// The Rotation of the Transform in Eulers.
    /// </summary>
    public Vector3 Rotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            _rotation.X = MathHelper.DegreesToRadians(value.X);
            _rotation.Y = MathHelper.DegreesToRadians(value.Y);
            _rotation.Z = MathHelper.DegreesToRadians(value.Z);
        }
    }

    /// <summary>
    /// The Scale of the Transform.
    /// </summary>
    public Vector3 Scale
    {
        get
        {
            return _scale;
        }
        set
        {
            _scale = value;
        }
    }

    /// <summary>
    /// The finally calculated model matrix.
    /// </summary>
    public Matrix4 Model
    {
        get
        {
            _model =
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z) *
                Matrix4.CreateTranslation(Position);
            return _model;
        }
    }

    public static Transform operator +(Transform left, Transform right)
    {
        Transform t = new Transform();
        t.Position = left.Position + right.Position;
        t.Rotation = left.Rotation + right.Rotation;
        t.Scale = left.Scale + right.Scale;
        t._model = left._model + right._model;

        return t;
    }

    public static Transform operator -(Transform left, Transform right)
    {
        Transform t = new Transform();
        t.Position = left.Position - right.Position;
        t.Rotation = left.Rotation - right.Rotation;
        t.Scale = left.Scale - right.Scale;
        t._model = left._model - right._model;

        return t;
    }

    public static Transform operator *(Transform left, Transform right)
    {
        Transform t = new Transform();
        t.Position = left.Position * right.Position;
        t.Rotation = left.Rotation * right.Rotation;
        t.Scale = left.Scale * right.Scale;
        t._model = left._model * right._model;

        return t;
    }
}