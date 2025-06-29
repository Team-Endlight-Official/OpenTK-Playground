using OpenTK.Mathematics;

public class DefaultShader : Shader
{
    private int _modelLoc;
    private int _viewLoc;
    private int _projLoc;

    public DefaultShader(string shaderPath = "default") : base(shaderPath)
    {
        _modelLoc = GetUniformLocation("u_model");
        _viewLoc = GetUniformLocation("u_view");
        _projLoc = GetUniformLocation("u_proj");
    }

    public void SetModel(in Matrix4 data)
    {
        SetUniformMatrix4(_modelLoc, data);
    }

    public void SetView(in Matrix4 data)
    {
        SetUniformMatrix4(_viewLoc, data);
    }

    public void SetProjection(in Matrix4 data)
    {
        SetUniformMatrix4(_projLoc, data);
    }
}
