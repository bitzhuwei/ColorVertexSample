#version 150 core

in vec3  in_Position;
//in vec4  in_Color;
in uint in_Color;
in float in_visible;
out vec4 pass_Color;
out float pass_visible;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);

	pass_Color = vec4(
		(float(in_Color & 255u)) / 255.0,
		(float((in_Color >> 8) & 255u)) / 255.0,
		(float((in_Color >> 16) & 255u)) / 255.0,
		(float(in_Color >> 24)) / 255.0
		); //in_Color;
		
	pass_visible = in_visible;
}

