#version 420 core

in vec3  in_Position;
out vec4 pass_Color;

// only 'u' is stored and passed as 'v' is always the same value.
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform float minAlpha = 0.1f;

void main(void)
{
    gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0f);

	//vec3 color = in_Position - vec3((2336535 + 2339212) / 2,
	//	(3680033 + 3682446) / 2, (-194.0594 + 1923.169) / 2);
	vec3 color = in_Position - vec3((0 + 300) / 2,
		(0 + 300) / 2, (12000 + 12300) / 2);

    if (color.r < 0) { color.r = -color.r; }
    if (color.g < 0) { color.g = -color.g; }
    if (color.b < 0) { color.b = -color.b; }
	vec3 normalized = normalize(color);
	float variance = (normalized.r - normalized.g) * (normalized.r - normalized.g);
	variance += (normalized.g - normalized.b) * (normalized.g - normalized.b);
	variance += (normalized.b - normalized.r) * (normalized.b - normalized.r);
	variance = variance / 2.0f;// range from 0.0f - 1.0f
	float a = (0.75f - minAlpha) * (1.0f - variance) + minAlpha;
    pass_Color = vec4(normalized, a);
}
