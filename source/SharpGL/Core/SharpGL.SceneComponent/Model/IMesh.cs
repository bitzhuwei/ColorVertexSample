using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.Model
{
    public abstract class  TriangleMesh
    {
        public abstract Vertex3DArray Vertexes
        {
            get;
            set;
        }
        

        public abstract UIntArray StripTriangles
        {
            get;
            set;
        }

        public abstract FloatArray Visibles
        {
            get;
            set;
        }

        public abstract Vertex Min
        {
            get;
            set;
        }

        public abstract Vertex Max
        {
            get;
            set;
        }

        public abstract ColorFArray VertexColors
        {
            get;
            set;
        }
        
    }
}
