using SharpGL;
using SharpGL.SceneComponent;
using SimLab.SimGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2.VertexBuffers
{
    /// <summary>
    /// 用
    /// </summary>
    public class TriangleIndexBuffer : IndexBuffer
    {
        public TriangleIndexBuffer()
            : base(OpenGL.GL_TRIANGLES)
        { }

        public override void AllocMem(int elementCount)
        {
            this.array = new UnmanagedArray<TriangleIndex>(elementCount);

            this.ElementCount = elementCount * 3;
        }

        private unsafe void DoDump()
        {
            int trianglesCount = this.SizeInBytes / sizeof(TriangleIndex);
            TriangleIndex* triangles = (TriangleIndex*)this.Data;
            System.Console.WriteLine("Trinagles Count:{0}", trianglesCount);
            System.Console.WriteLine("====Triangles Indices==============");
            for (int i = 0; i < trianglesCount; i++)
            {
                System.Console.WriteLine(String.Format("{0}:({1},{2},{3})", i, triangles[i].dot0, triangles[i].dot1, triangles[i].dot2));
            }
            System.Console.WriteLine("====Triangles Indices  END==============");
        }

        public void Dump()
        {
            DoDump();
        }
    }
}
