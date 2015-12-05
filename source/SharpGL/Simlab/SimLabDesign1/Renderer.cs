using GlmNet;
using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    /// <summary>
    /// 渲染器，用于指定渲染方式。可选用IndexRenderer或NonIndexRenderer。
    /// </summary>
    public abstract class Renderer
    {
        /// <summary>
        /// OpenGL.TRIANGLES etc.
        /// </summary>
        public uint Mode { get; set; }

        public abstract void Render(OpenGL gl, RenderMode renderMode);
    }

    /// <summary>
    /// 用索引方式渲染。
    /// </summary>
    public class IndexRenderer : Renderer
    {
        /// <summary>
        /// count in DrawElements(uint mode, int count, uint type, IntPtr indices);
        /// <para>要渲染多少个索引。</para>
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// type in DrawElements(uint mode, int count, uint type, IntPtr indices);
        /// <para>OpenGL.GL_UNSIGNED_INT等</para>
        /// </summary>
        public uint Type { get; set; }


        public override void Render(OpenGL gl, RenderMode renderMode)
        {
            gl.DrawElements(this.Mode, this.Count, this.Type, IntPtr.Zero);
        }
    }

    /// <summary>
    /// 不用索引的方式渲染。
    /// </summary>
    public class NonIndexRenderer : Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertexCountPerPrim">VBO中每个单元（一个三角形、一个六面体等等）包含的顶点数。</param>
        /// <param name="PrimCount">VBO中包含的单元（一个三角形、一个六面体）的数目。</param>
        public NonIndexRenderer(int vertexCountPerPrim, int PrimCount)
        {
            this.First = new int[PrimCount];
            this.Count = new int[PrimCount];
            for (int i = 0; i < this.First.Length; i++)
            {
                this.First[i] = i * vertexCountPerPrim;
                this.Count[i] = vertexCountPerPrim;
            }
            this.PrimCount = PrimCount;
        }
        /// <summary>
        /// first in MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        /// </summary>
        public int[] First { get; set; }

        /// <summary>
        /// count in MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        /// </summary>
        public int[] Count { get; set; }

        /// <summary>
        /// primcount in MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        /// </summary>
        public int PrimCount { get; set; }

        public override void Render(OpenGL gl, RenderMode renderMode)
        {

            gl.MultiDrawArrays(this.Mode, this.First, this.Count, this.PrimCount);
        }


    }
}
