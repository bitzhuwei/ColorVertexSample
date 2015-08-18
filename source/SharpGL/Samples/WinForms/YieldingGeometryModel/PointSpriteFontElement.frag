#version 150 core

in vec4 pass_Color;
in float pass_visible;
in vec2 pass_position;
in float pass_pointSize;
out vec4 out_Color;

uniform sampler2D tex;
uniform float canvasWidth;
uniform float canvasHeight;

void main(void) {
	if (pass_visible == 1)
	{
		//out_Color = pass_Color;
		//out_Color = texture(tex, vec2(gl_FragCoord));
		//get screen coord from frag coord
		float screenX = (gl_FragCoord.x / canvasWidth - 0.5) * 2;
		float screenY = (gl_FragCoord.y / canvasHeight - 0.5) * 2;
		//find the difference between the fragments screen position and the points screen position then simply work out the point coord from the point size
		vec2 pointCoord = vec2(
			((screenX - pass_position.x) / (pass_pointSize / canvasWidth) + 1) / 2,
			1-((screenY - pass_position.y) / (pass_pointSize / canvasHeight) + 1) / 2);
		// remove this discard thing if you want to have a quad instead of a sphere
		if (length(pointCoord - vec2(0.5, 0.5)) > 0.3)
		{ discard; }
		out_Color = texture(tex, pointCoord);
	}
	else
	{
		discard;
	}
}