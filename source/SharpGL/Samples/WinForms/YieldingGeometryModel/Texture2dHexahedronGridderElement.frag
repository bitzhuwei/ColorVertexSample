#version 150 core
in vec2 pass_uv;
in float pass_visible;
out vec4 out_Color;

uniform sampler2D tex;

void main(void) {
	if (pass_visible == 1)
	{
		out_Color = texture(tex, pass_uv);
	}
	else
	{
		discard;
	}
}