#version 330 core

layout (location = 0) in vec3 POS;
layout (location = 1) in vec2 UV;

uniform mat4 u_model;
uniform mat4 u_view;
uniform mat4 u_proj;

out vec3 _POS;
out vec2 _UV;

void main()
{
	gl_Position = u_proj * u_view * u_model * vec4(POS, 1.0);

	_POS = POS;
	_UV = UV;
}