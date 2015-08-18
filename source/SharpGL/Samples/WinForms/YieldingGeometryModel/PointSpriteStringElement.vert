#version 150 core

in vec3  in_Position;
in vec4  in_Color;
in float in_visible;
in float in_radius;
out vec4 pass_Color;
out float pass_visible;
out vec2 pass_position;
out float pass_pointSize;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);
	gl_PointSize = in_radius;

	pass_Color = in_Color;
	pass_visible = in_visible;
	pass_position = vec2(gl_Position.x / gl_Position.w, gl_Position.y / gl_Position.w);
	pass_pointSize = gl_PointSize;
}