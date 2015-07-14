using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public class HexahedronGridderElement : SceneElement, IRenderable
    {
        /// <summary>
        /// The model shown in <see cref="ScientificVisual3DControl"/>.
        /// </summary>
        public ScientificModel Model { get; set; }

        public bool RenderModel { get; set; }

        public HexahedronGridderElement(ScientificModel model, IScientificCamera camera, bool renderModel = true)
        {
            this.Model = model;
            this.Camera = camera;
            this.RenderModel = renderModel;
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
        private bool initialised;

        /// <summary>
        /// <para>Use <see cref="IHasObjectSpace"/> and <see cref="IScientificCamera"/> to update projection and view matrices.</para>
        /// </summary>
        public IScientificCamera Camera { get; set; }

        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void Initialise(OpenGL gl)
        {
            //  Set a blue clear colour.
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);

            {
                //  Create the shader program.
                var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"Model\Shader.vert");
                var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"Model\Shader.frag");
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                this.shaderProgram = shaderProgram;
            }
            {
                //  Create the picking shader program.
                var vertexShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.VertexShader);
                var fragmentShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.FragmentShader);
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                this.pickingShaderProgram = shaderProgram;
            }

            unsafe
            {
                //  Create the vertex array object.
                vertexBufferArray = new VertexBufferArray();
                vertexBufferArray.Create(gl);
                vertexBufferArray.Bind(gl);

                //  Create a vertex buffer for the vertex data.
                var vertexDataBuffer = new VertexBuffer();
                vertexDataBuffer.Create(gl);
                vertexDataBuffer.Bind(gl);
                vertexDataBuffer.SetData(gl, 0, this.Model.Positions, false, 3);

                //  Now do the same for the colour data.
                var colourDataBuffer = new VertexBuffer();
                colourDataBuffer.Create(gl);
                colourDataBuffer.Bind(gl);
                colourDataBuffer.SetData(gl, 1, this.Model.Colors, false, 3);

                //  Unbind the vertex array, we've finished specifying data for it.
                vertexBufferArray.Unbind(gl);
            }

            this.initialised = true;
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (!this.RenderModel) { return; }

            if (!initialised)
            {
                this.Initialise(gl);
            }
            // Update matrices.
            IScientificCamera camera = this.Camera;
            if (camera != null)
            {
                if (camera.CameraType == CameraTypes.Perspecitive)
                {
                    IPerspectiveViewCamera perspective = camera;
                    this.projectionMatrix = perspective.GetProjectionMat4();
                    this.viewMatrix = perspective.GetViewMat4();
                }
                else if (camera.CameraType == CameraTypes.Ortho)
                {
                    IOrthoViewCamera ortho = camera;
                    this.projectionMatrix = ortho.GetProjectionMat4();
                    this.viewMatrix = ortho.GetViewMat4();
                }
                else
                { throw new NotImplementedException(); }
            }

            modelMatrix = mat4.identity();

            //gl.PointSize(3);

            var shader = (renderMode == RenderMode.HitTest) ? pickingShaderProgram : shaderProgram;
            //  Bind the shader, set the matrices.
            shader.Bind(gl);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            if (renderMode == RenderMode.HitTest)
            {
                shader.SetUniform1(gl, "pickingBaseID", ((IColorCodedPicking)this).PickingBaseID);
            }

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            ScientificModel model = this.Model;
            if (model.First != null && model.Count != null && model.PrimitiveCount > 0)
            { gl.MultiDrawArrays((uint)model.Mode, model.First, model.Count, model.PrimitiveCount); }
            else
            { gl.DrawArrays((uint)this.Model.Mode, 0, this.Model.VertexCount); }

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shader.Unbind(gl);
        }

        #endregion
    }
}
