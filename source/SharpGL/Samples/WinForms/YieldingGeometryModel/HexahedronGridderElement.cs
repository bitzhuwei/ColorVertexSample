﻿using GlmNet;
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
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    /// <summary>
    /// 用于渲染六面体网格。
    /// Rendering gridder of hexadrons.
    /// </summary>
    public partial class HexahedronGridderElement : IndexedVAOElement, IBoundingBox
    {
        internal HexahedronGridderSource source;
        internal IScientificCamera camera;
        //private ShaderProgram pickingShader;
        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        /// <summary>
        /// 元素内的顶点数。（8）
        /// </summary>
        internal const int vertexCountInHexahedron = 8;
        /// <summary>
        /// 顶点的分量数。（3）
        /// </summary>
        internal const int componentCountInVertex = 3;
        /// <summary>
        /// 用三角形带画六面体，需要14个顶点（索引值）
        /// </summary>
        internal const int triangleStrip = 14;

        //  Constants that specify the attribute indexes.
        internal const uint attributeIndexPosition = 0;
        internal const uint attributeIndexColour = 1;
        internal const uint attributeIndexVisible = 2;

        internal uint visualBuffer;

        /// <summary>
        /// 用于渲染六面体网格。
        /// Rendering gridder of hexadrons.
        /// </summary>
        public HexahedronGridderElement(HexahedronGridderSource source, IScientificCamera camera)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            this.source = source;
            this.camera = camera;
        }

    }
}