#version 150 core

in vec3  in_Position;
in vec4  in_Color;
in float in_radius;
out vec4 pass_Color;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);
	gl_PointSize = in_radius;

	pass_Color = in_Color;
}