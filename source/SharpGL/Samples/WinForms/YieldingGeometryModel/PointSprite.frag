#version 150 core

in vec4 pass_Color;
//in float pass_visible;
out vec4 out_Color;

void main(void) {
	//if (pass_visible == 1)
	{
		out_Color = pass_Color;
	}
	//else
	{
	//	discard;
	}
}