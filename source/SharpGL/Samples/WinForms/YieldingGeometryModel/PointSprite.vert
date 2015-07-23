#version 150 core

in vec3  in_Position;
in vec4  in_Color;
//in float in_visible;
//in float in_radius;
out vec4 pass_Color;
//out float pass_visible;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);
	//gl_PointSize = gl_VertexID % 10;//
	//if (in_radius >= 1)
	{
	//	gl_PointSize = in_radius;
	}
	//else
	{
	//	gl_PointSize = gl_VertexID % 10;
	}

	pass_Color = in_Color;
	//pass_visible = in_visible;
}