#version 460 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aUv;
layout (location = 2) in vec4 aCol;

out vec3 fUv;
out vec4 fCol;

uniform mat4 uTransform;
uniform mat4 uProjection;
uniform mat4 uView;

void main()
{
	fUv = aUv;
	fCol = aCol;
    gl_Position = uProjection * uView * (uTransform * vec4(aPosition, 1.0));
}