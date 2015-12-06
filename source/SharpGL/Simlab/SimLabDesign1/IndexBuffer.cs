using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    class IndexBuffer : VBOInfoBase
    {
        public int VertexCount { get; set; }

        public IndexBuffer()
        {
            this.Target = OpenGL.GL_ELEMENT_ARRAY_BUFFER;
        }

        public override string ToString()
        {
            return string.Format("{0}, VertexCount: {1}",
                base.ToString(), VertexCount);
            //return base.ToString();
        }
    }
}
