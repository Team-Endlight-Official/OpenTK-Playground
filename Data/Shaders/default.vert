#version 330 core

layout (location = 0) in vec3 POS;
layout (location = 1) in vec2 UV;

uniform mat4 u_mvp;

out vec3 _POS;
out vec2 _UV;

void main()
{
	gl_Position = vec4(POS, 1.0) * u_mvp;
	_POS = POS;
	_UV = UV;
}