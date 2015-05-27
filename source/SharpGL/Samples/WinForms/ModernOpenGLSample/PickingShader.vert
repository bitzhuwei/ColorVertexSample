#version 150 core

// Inputs from calling program:
uniform float basePointSize;
uniform mat4 modelview_matrix;
uniform mat4 projection_matrix;
uniform int pickingBaseID;

	// The input particle data:
	in vec3 position;
	in float particle_radius;
	
	// Output passed to fragment shader.
	flat out vec4 particle_color_fs;


void main()
{
	// Compute color from object ID.
	int objectID = pickingBaseID + gl_VertexID;

	particle_color_fs = vec4(
		float(objectID & 0xFF) / 255.0, 
		float((objectID >> 8) & 0xFF) / 255.0, 
		float((objectID >> 16) & 0xFF) / 255.0, 
		float((objectID >> 24) & 0xFF) / 255.0);
		
	// Transform and project particle position.
	vec4 eye_position = modelview_matrix * vec4(position, 1);

	// Transform and project particle position.
	gl_Position = projection_matrix * eye_position;

	// Compute sprite size.
	gl_PointSize = basePointSize * particle_radius / (eye_position.z * projection_matrix[2][3] + projection_matrix[3][3]);
}
