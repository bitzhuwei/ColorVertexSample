#version 150 core

in vec3  in_Position;
in float  in_uOfUV;
out vec4 pass_uOfUV;
out float pass_visible;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);

	pass_Color = in_Color;
	pass_visible = in_visible;
}