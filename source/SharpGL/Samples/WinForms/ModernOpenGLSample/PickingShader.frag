#version 150 core

	flat in vec4 particle_color_fs;
	out vec4 FragColor;

void main() 
{
	FragColor = particle_color_fs;
}