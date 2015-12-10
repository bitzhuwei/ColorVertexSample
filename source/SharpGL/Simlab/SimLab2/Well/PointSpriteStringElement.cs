using GlmNet;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SimLab2.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlab.Well
{
    /// <summary>
    /// 用于渲染字符串。
    /// </summary>
    public partial class PointSpriteStringElement : SceneElement, IRenderable
    {

        private string content;
        private Vertex position;
        private int fontSize;
        private vec3 textColor;
        private int maxRowWidth = 255;
        FontResource fontResource;

        uint[] texture = new uint[1];
        internal IScientificCamera camera;
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

        internal uint attributeIndexPosition = 0;

        /// <summary>
        /// 用于渲染字符串。
        /// Rendering gridder of hexadrons.
        /// </summary>
        public PointSpriteStringElement(
            //IScientificCamera camera, string text, Vertex position, int fontSize = 32, GLColor textColor = null)
            IScientificCamera camera,
            string content, Vertex position, GLColor textColor = null, int fontSize = 32, int maxRowWidth = 256)
        {
            if (fontSize >= 256) { throw new ArgumentException(); }

            this.camera = camera;
            this.content = content;
            this.position = position;
            this.fontSize = fontSize;
            if (textColor == null)
            {
                this.textColor = new vec3(1, 1, 1);
            }
            else
            {
                this.textColor = new vec3(textColor.R, textColor.G, textColor.B);
            }

            if (0 < maxRowWidth && maxRowWidth < 257)
            {
                this.maxRowWidth = maxRowWidth;
            }
            else
            {
                throw new ArgumentOutOfRangeException("max row width must between 0 and 257(not include 0 or 257)");
            }

            this.fontResource = FontResource.Instance;
        }


        public void Initialize(SharpGL.OpenGL openGL)
        {
            this.InitTexture(openGL, this.content, this.fontSize, this.maxRowWidth, this.fontResource);

            this.InitShaderProgram(openGL, out this.shaderProgram);

            this.InitVAO(openGL, out this.primitiveMode, out this.vao, out this.primitiveCount);
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
