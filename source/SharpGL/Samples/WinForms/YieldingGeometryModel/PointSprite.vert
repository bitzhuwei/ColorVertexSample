#if __VERSION__ >= 130
in vec3 in_Position;
in vec3 in_Color;
out vec3 pass_Color;
out vec2 pass_position;
out float pass_pointSize;

#else 
attribute vec3 in_Position;
attribute vec3 in_Color;  
varying vec3 pass_Color;
varying vec2 pass_position;
varying float pass_pointSize;

#endif

uniform float basePointSize;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
    gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);
	pass_Color = in_Color;
	pass_position = vec2(gl_Position.x / gl_Position.w, gl_Position.y / gl_Position.w);
	vec4 cameraPosition = viewMatrix * modelMatrix * vec4(in_Position, 1.0);
	gl_PointSize = basePointSize * 13 / (cameraPosition.z * projectionMatrix[2][3] + projectionMatrix[3][3]);
	pass_pointSize = gl_PointSize;
}