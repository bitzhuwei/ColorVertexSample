#version 150 core

in vec4 pass_Color;
in float pass_visible;
in vec2 pass_position;
in float pass_pointSize;
out vec4 out_Color;

uniform sampler2D tex;

void main(void) {
	float transparency = texture2D(tex, gl_PointCoord).r;
	if (transparency == 0.0f)
	{
		discard;
	}
	else
	{
		out_Color = vec4(1, 1, 1, transparency);
	}
}