using GlmNet;
using SharpGL.SceneComponent;
using SharpGL.SceneComponent.Model;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    public partial class PointSpriteGridderElement : VAOElement<PointSpriteMesh>, IBoundingBox
    {
        //private Texture texture;
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
