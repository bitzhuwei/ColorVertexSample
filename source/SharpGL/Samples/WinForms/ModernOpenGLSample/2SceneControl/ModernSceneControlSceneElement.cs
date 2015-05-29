using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernOpenGLSample._2SceneControl
{
    /// <summary>
    /// Render models with shader and VAO.
    /// <para>Use <see cref="IHasObjectSpace"/> and <see cref="IScientificCamera"/> to update projection and view matrices.</para>
    /// </summary>
    class ModernSceneControlSceneElement : SceneElement, IRenderable, IHasObjectSpace
    {
        /// <summary>
        /// Render models with shader and VAO.
        /// <para>Use <see cref="IHasObjectSpace"/> and <paramref name="camera"/> to update projection and view matrices.</para>
        /// </summary>
        /// <param name="camera"></param>
        public ModernSceneControlSceneElement(IScientificCamera camera = null)
        {
            this.Camera = camera;
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
        /// <para>Use <see cref="IHasObjectSpace"/> and <see cref="IScientificCamera"/> to update projection and view matrices.</para>
        /// </summary>
        public IScientificCamera Camera { get; set; }

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
                //  Create the picking shader program.
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
            //IPerspectiveCamera camera = this.cameraRotation.Camera;
            //projectionMatrix = camera.GetProjectionMat4();
            //viewMatrix = this.cameraRotation.Camera.GetViewMat4();
            //modelMatrix = mat4.identity();

            //  Now create the geometry for the square.
            CreateVertices(gl);
        }

        float[] vertices;//= new float[18];
        float[] colors;//= new float[18]; // Colors for our vertices  

        /// <summary>
        /// Creates the geometry for the square, also creating the vertex buffer array.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        private void CreateVertices(OpenGL gl)
        {
            //GeneratePoints(out vertices, out  colors);
            GenerateModel(out vertices, out colors);

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

        private void GenerateModel(out float[] vertices, out float[] colors)
        {
            const int length = 12 * 3;
            vertices = new float[length]; colors = new float[length];
            /*
            vertices[0] = 0; vertices[1] = 0; vertices[2] = 0;
            vertices[3] = 0; vertices[4] = 1; vertices[5] = 0;
            vertices[6] = 1; vertices[7] = 0; vertices[8] = 0;
            vertices[9] = 1; vertices[10] = 1; vertices[11] = 0;
            vertices[12] = 2; vertices[13] = 0; vertices[14] = 0;
            vertices[15] = 2; vertices[16] = 1; vertices[17] = 0;
             */
            for (int i = 0; i < length; i += 3)
            {
                vertices[i] = i / 6;
            }
            //vertices[length] = vertices[length - 3];

            for (int i = 1; i < length; i += 3)
            {
                vertices[i] = (i / 3) % 2;
            }
            //vertices[length + 1] = vertices[length - 3 + 1];

            for (int i = 0; i < length; i += 3)
            {
                colors[i] = (i / 3) % 2;
            }
            for (int i = 1; i < length; i += 3)
            {
                colors[i] = ((i - 1) / 6) % 2;
            }
            for (int i = 2; i < length; i += 3)
            {
                colors[i] = i / 12;
            }
        }

        //private void GeneratePoints(out float[] vertices, out float[] colors)
        //{
        //    const int length = 256 * 3;
        //    vertices = new float[length]; colors = new float[length];

        //    Random random = new Random();

        //    int direction = this.positiveGrowth ? 1 : -1;
        //    // points
        //    for (int i = 0; i < length; i++)
        //    {
        //        vertices[i] = direction * (float)i / (float)length;
        //        colors[i] = (float)((random.NextDouble() * 2 - 1) * 1);
        //        //colors[i] = (float)i / (float)length;
        //    }

        //    //// triangles
        //    //for (int i = 0; i < length / 9; i++)
        //    //{
        //    //    var x = random.NextDouble(); var y = random.NextDouble(); var z = random.NextDouble();
        //    //    for (int j = 0; j < 3; j++)
        //    //    {
        //    //        vertices[i * 9 + j * 3] = (float)(x + random.NextDouble() / 5 - 1);
        //    //    }
        //    //    for (int j = 0; j < 3; j++)
        //    //    {
        //    //        vertices[i * 9 + j * 3 + 1] = (float)(y + random.NextDouble() / 5 - 1);
        //    //    }
        //    //    for (int j = 0; j < 3; j++)
        //    //    {
        //    //        vertices[i * 9 + j * 3 + 2] = (float)(z + random.NextDouble() / 5 - 1);
        //    //    }
        //    //}
        //}

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            gl.PointSize(3);

            var shader = (renderMode == RenderMode.HitTest) ? pickingShaderProgram : shaderProgram;
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
        }

        #endregion

        #region IHasObjectSpace 成员

        void IHasObjectSpace.PushObjectSpace(OpenGL gl)
        {
            // Update matrices.
            IScientificCamera camera = this.Camera;
            if (camera == null) { return; }
            if (camera.CameraType == ECameraType.Perspecitive)
            {
                IPerspectiveViewCamera perspective = camera;
                this.projectionMatrix = perspective.GetProjectionMat4();
                this.viewMatrix = perspective.GetViewMat4();
            }
            else if (camera.CameraType == ECameraType.Ortho)
            {
                IOrthoViewCamera ortho = camera;
                this.projectionMatrix = ortho.GetProjectionMat4();
                this.viewMatrix = ortho.GetViewMat4();
            }
            else
            { throw new NotImplementedException(); }

            modelMatrix = mat4.identity();
        }

        void IHasObjectSpace.PopObjectSpace(OpenGL gl)
        {
        }

        SharpGL.SceneGraph.Transformations.LinearTransformation IHasObjectSpace.Transformation
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
