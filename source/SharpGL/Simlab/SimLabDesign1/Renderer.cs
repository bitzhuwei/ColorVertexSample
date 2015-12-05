using SharpGL;
using SharpGL.SceneGraph.Core;
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
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// type in DrawElements(uint mode, int count, uint type, IntPtr indices);
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
        /// first in MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        /// </summary>
        int[] First { get; set; }

        /// <summary>
        /// count in MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        /// </summary>
        int[] Count { get; set; }

        /// <summary>
        /// primcount in MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        /// </summary>
        int PrimCount { get; set; }

        public override void Render(OpenGL gl, RenderMode renderMode)
        {
            gl.MultiDrawArrays(this.Mode, this.First, this.Count, this.PrimCount);
        }
    }
}
