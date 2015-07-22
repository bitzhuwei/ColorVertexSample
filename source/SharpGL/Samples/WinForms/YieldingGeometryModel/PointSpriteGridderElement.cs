using GlmNet;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    public partial class PointSpriteGridderElement : VAOElement
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
        /// <summary>
        /// 顶点的分量数。（3）
        /// </summary>
        internal const int componentCountInVertex = 3;
        ///// <summary>
        ///// 用三角形带画六面体，需要14个顶点（索引值）
        ///// </summary>
        //internal const int triangleStrip = 14;

        //  Constants that specify the attribute indexes.
        internal const uint attributeIndexPosition = 0;
        internal const uint attributeIndexColour = 1;
        internal const uint attributeIndexVisible = 2;

        internal uint visualBuffer;

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons.
        /// </summary>
        public PointSpriteGridderElement(PointSpriteGridderSource source, IScientificCamera camera)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            this.Source = source;
            this.camera = camera;
        }


        public PointSpriteGridderSource Source { get; private set; }

    }
}
