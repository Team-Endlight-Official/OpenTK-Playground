#version 330 core

in vec3 _POS;

uniform float u_gamma = 0.75;


// OUTPUT
out vec4 OUT;

void main()
{
	OUT = vec4((_POS.x + 0.5) * u_gamma, (_POS.y + 0.5) * u_gamma, (_POS.z + 0.5) * u_gamma, 1.0);
}