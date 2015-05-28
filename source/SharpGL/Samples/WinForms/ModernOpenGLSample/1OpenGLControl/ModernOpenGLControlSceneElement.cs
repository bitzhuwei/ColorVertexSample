using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernOpenGLSample._1OpenGLControl
{
    class ModernOpenGLControlSceneElement
    {
        SatelliteRotation cameraRotation = new SatelliteRotation();

        public ModernOpenGLControlSceneElement()
        {
            ScientificCamera camera = new ScientificCamera(ECameraType.Perspecitive);
            camera.Position = new SharpGL.SceneGraph.Vertex(0, 0, -2);
            camera.Target = new SharpGL.SceneGraph.Vertex();
            camera.UpVector = new SharpGL.SceneGraph.Vertex(0, 1, 0);
            this.cameraRotation.Camera = camera;
        }

        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        //  Constants that specify the attribute indexes.
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        //  The vertex buffer array which contains the vertex and colour buffers.
        VertexBufferArray vertexBufferArray;

        //  The shader program for our vertex and fragment shader.
        private ShaderProgram shaderProgram;
        private ShaderProgram pickingShaderProgram;

        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="width">The width of the screen.</param>
        /// <param name="height">The height of the screen.</param>
        public void Initialise(OpenGL gl, float width, float height)
        {
            //  Set a blue clear colour.
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);

            {
                //  Create the shader program.
                var vertexShaderSource = ManifestResourceLoader.LoadTextFile("Shader.vert");
                var fragmentShaderSource = ManifestResourceLoader.LoadTextFile("Shader.frag");
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                this.shaderProgram = shaderProgram;
            }
            {
                //  Create the shader program.
                var vertexShaderSource = ManifestResourceLoader.LoadTextFile("PickingShader.vert");
                var fragmentShaderSource = ManifestResourceLoader.LoadTextFile("PickingShader.frag");
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                this.pickingShaderProgram = shaderProgram;
            }

            //  Create a perspective projection matrix.
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, width / height, 0.1f, 100.0f);

            //  Create a view matrix to move us back a bit.
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(0.0f, 0.0f, -5.0f));

            //  Create a model matrix to make the model a little bigger.
            modelMatrix = glm.scale(new mat4(1.0f), new vec3(2.5f));
            IPerspectiveCamera camera = this.cameraRotation.Camera;
            projectionMatrix = camera.GetProjectionMat4();
            viewMatrix = this.cameraRotation.Camera.GetViewMat4();
            modelMatrix = mat4.identity();

            //  Now create the geometry for the square.
            CreateVertices(gl);
        }

        /// <summary>
        /// Draws the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void Draw(OpenGL gl, RenderMode renderMode = RenderMode.Render)
        {
            var shader = (renderMode == RenderMode.HitTest) ? pickingShaderProgram : shaderProgram;

            if (renderMode == RenderMode.HitTest)
            {
                gl.ClearColor(1, 1, 1, 1);
            }
            else
            {
                //  Set a blue clear colour.
                gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);
            }

            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            gl.PointSize(4);

            // Update matrices.
            IPerspectiveCamera camera = this.cameraRotation.Camera;
            projectionMatrix = camera.GetProjectionMat4();
            viewMatrix = this.cameraRotation.Camera.GetViewMat4();
            modelMatrix = mat4.identity();

            //  Bind the shader, set the matrices.
            shader.Bind(gl);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            //gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, vertices.Length);
            gl.DrawArrays(OpenGL.GL_POINTS, 0, vertices.Length);

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shader.Unbind(gl);

            gl.Flush();
        }

        float[] vertices;//= new float[18];
        float[] colors;//= new float[18]; // Colors for our vertices  

        /// <summary>
        /// Creates the geometry for the square, also creating the vertex buffer array.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        private void CreateVertices(OpenGL gl)
        {

            //GenerateSquare(out vertices, out colors);
            GeneratePoints(out vertices, out  colors);

            //  Create the vertex array object.
            vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            //  Create a vertex buffer for the vertex data.
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 0, vertices, false, 3);

            //  Now do the same for the colour data.
            var colourDataBuffer = new VertexBuffer();
            colourDataBuffer.Create(gl);
            colourDataBuffer.Bind(gl);
            colourDataBuffer.SetData(gl, 1, colors, false, 3);
            
            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);
        }

        private void GeneratePoints(out float[] vertices, out float[] colors)
        {
            const int length = 256 * 3;
            vertices = new float[length]; colors = new float[length];

            Random random = new Random();

            // points
            for (int i = 0; i < length; i++)
            {
                vertices[i] = (float)i / (float)length;
                colors[i] = (float)((random.NextDouble() * 2 - 1) * 1);
                //vertices[i] = (float)(random.NextDouble() * 2 - 1);
                //if (i % 2 == 0)
                //{
                //    vertices[i] = (i + 0.0f) / (float)(length);
                //}
                //else
                //{
                //    vertices[i] = -(i + 0.0f) / (float)(length);
                //}

                // triangles
            }

            //// triangles
            //for (int i = 0; i < length / 9; i++)
            //{
            //    var x = random.NextDouble(); var y = random.NextDouble(); var z = random.NextDouble();
            //    for (int j = 0; j < 3; j++)
            //    {
            //        vertices[i * 9 + j * 3] = (float)(x + random.NextDouble() / 5 - 1);
            //    }
            //    for (int j = 0; j < 3; j++)
            //    {
            //        vertices[i * 9 + j * 3 + 1] = (float)(y + random.NextDouble() / 5 - 1);
            //    }
            //    for (int j = 0; j < 3; j++)
            //    {
            //        vertices[i * 9 + j * 3 + 2] = (float)(z + random.NextDouble() / 5 - 1);
            //    }
            //}
        }

        internal void SetBound(int width, int height)
        {
            this.cameraRotation.SetBounds(width, height);
        }

        internal void MouseDown(int x, int y)
        {
            this.cameraRotation.MouseDown(x, y);
        }

        internal void MouseMove(int x, int y)
        {
            this.cameraRotation.MouseMove(x, y);
        }

        internal void MouseUp(int x, int y)
        {
            this.cameraRotation.MouseUp(x, y);
        }


    }
}
