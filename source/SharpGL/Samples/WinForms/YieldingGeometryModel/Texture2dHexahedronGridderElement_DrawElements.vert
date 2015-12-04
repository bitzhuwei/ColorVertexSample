#version 150 core

in vec3  in_Position;
in vec2  in_uv;
in float in_visible;
out vec2 pass_uv;
out float pass_visible;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);

	pass_uv = in_uv;
	pass_visible = in_visible;
}