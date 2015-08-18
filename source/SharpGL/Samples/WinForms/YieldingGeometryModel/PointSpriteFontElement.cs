using GlmNet;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染字符串。
    /// </summary>
    public partial class PointSpriteFontElement : SceneElement, IRenderable
    {
        private Texture texture;
        internal IScientificCamera camera;
        //private ShaderProgram pickingShader;
        //  The projection, view and model matrices.
        ShaderProgram shaderProgram;
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        uint primitiveMode;
        int primitiveCount;
        uint[] vao;

        /// <summary>
        /// 元素内的顶点数。（1）
        /// </summary>
        internal const int vertexCountPerElement = 1;

        //  Constants that specify the attribute indexes.
        internal uint attributeIndexPosition = 0;
        internal uint attributeIndexColour = 1;
        internal uint attributeIndexVisible = 2;
        internal uint attributeIndexRadius = 3;

        internal uint visualBuffer;

        /// <summary>
        /// 用于渲染字符串。
        /// Rendering gridder of hexadrons.
        /// </summary>
        public PointSpriteFontElement(string text, Vertex position, IScientificCamera camera)
        {
            this.text = text;
            this.position = position;
            this.camera = camera;
        }

        private string text;
        private Vertex position;


        public void Initialize(SharpGL.OpenGL openGL)
        {
            this.InitShader(openGL, out this.shaderProgram);

            this.InitVertexArrayBufferObject(openGL, out this.primitiveMode, out this.vao, out this.primitiveCount);
        }

        public void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {

            BeforeRendering(gl, renderMode);

            ShaderProgram shader = this.shaderProgram;// GetShader(gl, renderMode);
            shader.Bind(gl);

            // 用VAO+EBO进行渲染。
            //  Bind the out vertex array.
            gl.BindVertexArray(vao[0]);

            gl.DrawArrays(this.primitiveMode, 0, this.primitiveCount);

            //  Unbind our vertex array and shader.
            gl.BindVertexArray(0);

            shader.Unbind(gl);

            AfterRendering(gl, renderMode);
        }
    }
}
