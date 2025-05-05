#version 330 core

layout (location = 0) in vec3 POS;

out vec3 _POS;

void main()
{
	gl_Position = vec4(POS, 1.0);
	_POS = POS;
}