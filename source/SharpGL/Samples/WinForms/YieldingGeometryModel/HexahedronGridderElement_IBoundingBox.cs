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
using System.Threading.Tasks;
using YieldingGeometryModel.Builder;
using YieldingGeometryModel.GLPrimitive;


namespace YieldingGeometryModel
{
    public partial class HexahedronGridderElement
    {
        //BoundingBox boundingBox = new BoundingBox();

        #region IBoundingBox 成员
        /*
        Vertex IBoundingBox.MaxPosition
        {
            get { return boundingBox.MaxPosition; }
        }

        Vertex IBoundingBox.MinPosition
        {
            get { return boundingBox.MinPosition; }
        }

        void IBoundingBox.GetCenter(out float x, out float y, out float z)
        {
            boundingBox.GetCenter(out x, out y, out z);
        }

        void IBoundingBox.GetBoundDimensions(out float xSize, out float ySize, out float zSize)
        {
            boundingBox.GetBoundDimensions(out xSize, out ySize, out zSize);
        }

        void IBoundingBox.Render(OpenGL gl, RenderMode renderMode)
        {
            boundingBox.Render(gl, renderMode);
        }

        void IBoundingBox.Set(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            boundingBox.Set(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public void SetBoundingBox(Vertex min, Vertex max)
        {
            boundingBox.SetBounds(min, max);
        }
        */
        #endregion

    }
}
