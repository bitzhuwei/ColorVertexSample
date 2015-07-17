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

        protected override void CleanUnmanagedRes()
        {
            OpenGL gl = new OpenGL();// this is not cool.
            gl.InvalidateBufferData(this.positionBufferObj);
            gl.InvalidateBufferData(this.colorBufferObj);
            gl.InvalidateBufferData(this.ebo[0]);
        }

        protected override void CleanManagedRes()
        {
        }

    }
}
