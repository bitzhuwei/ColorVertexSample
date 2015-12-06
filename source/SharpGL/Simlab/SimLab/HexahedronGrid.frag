#version 150 core
in float pass_uv;
out vec4 out_Color;

uniform sampler2D tex;

void main(void) {
	if (pass_uv < 0.0)
	{
	    out_Color = vec4(1, 1, 1, 1);
	}
	else if (pass_uv <= 1.0)
	{
		out_Color = texture(tex, vec2(pass_uv, 0.5));
	}
	else 
	{
		discard;
	}
}
