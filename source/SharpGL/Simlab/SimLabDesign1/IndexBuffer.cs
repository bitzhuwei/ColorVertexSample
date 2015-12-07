using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public class IndexBuffer : BufferBase
    {
        public override uint Target
        {
            get
            {
                return OpenGL.GL_ELEMENT_ARRAY_BUFFER;
            }
        }

        public int VertexCount { get; set; }

        public override void LayoutForVAO(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, this.BufferID);
        }

        public override string ToString()
        {
            return string.Format("{0}, VertexCount: {1}",
                base.ToString(), VertexCount);
            //return base.ToString();
        }
    }
}
