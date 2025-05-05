#version 330 core

in vec3 _POS;


// OUTPUT
out vec4 OUT;

void main()
{
	OUT = vec4(_POS.x + 0.5, _POS.y + 0.5, _POS.z + 0.5, 1.0);
}