using GlmNet;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
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
    public partial class PointSpriteFontElement : VAOElement<PointSpriteMesh>//, IBoundingBox
    {
        private Texture texture;
        internal IScientificCamera camera;
        //private ShaderProgram pickingShader;
        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

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

    }
}
