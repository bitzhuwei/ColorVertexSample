#version 150 core
in float pass_uv;
out vec4 out_Color;

uniform sampler2D tex;

void main(void) {
	if (0.0 <= pass_uv && pass_uv <= 1.0)
	{
		out_Color = texture(tex, vec2(pass_uv, 0.5));
	}
	else
	{
		discard;
	}
}
