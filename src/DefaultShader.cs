using OpenTK.Mathematics;

public class DefaultShader : Shader
{
    private int m_modelLoc;
    private int m_viewLoc;
    private int m_projLoc;

    public DefaultShader(string shaderPath = "default") : base(shaderPath)
    {
        m_modelLoc = GetUniformLocation("u_model");
        m_viewLoc = GetUniformLocation("u_view");
        m_projLoc = GetUniformLocation("u_proj");
    }

    public void SetModel(in Matrix4 data)
    {
        SetUniformMatrix4(m_modelLoc, data);
    }

    public void SetView(in Matrix4 data)
    {
        SetUniformMatrix4(m_viewLoc, data);
    }

    public void SetProjection(in Matrix4 data)
    {
        SetUniformMatrix4(m_projLoc, data);
    }
}
