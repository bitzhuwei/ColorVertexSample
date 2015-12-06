#version 150 core
in float pass_uv;
out vec4 out_Color;

uniform sampler2D tex;
uniform float renderingWireframe;

void main(void) {
	if (renderingWireframe > 0.0)
	{
		if (0.0 <= pass_uv && pass_uv <= 1.0)
		{
			out_Color = vec4(1, 1, 1, 1);
		}
		else
		{
			discard;
		}
	}
	else
	{
	    if (0.0 <= pass_uv && pass_uv <= 1.0)
		{
			out_Color = texture(tex, vec2(pass_uv, 0.0));
		}
		else
		{
			discard;
		}
	}

}
