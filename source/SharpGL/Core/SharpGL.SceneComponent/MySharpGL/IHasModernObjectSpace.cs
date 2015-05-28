using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Pass shared information to <see cref="IHasModernObjectSpace"/> in a <see cref="MyScene"/>'s instance so that the elements can get useful information
    /// <para>like its rendering order, total previous VBOs' vertices count, etc.</para>
    /// <para>This interface takes place of <see cref="IHasObjectSpace"/></para>
    /// </summary>
    public interface IHasModernObjectSpace
    {
        /// <summary>
        /// Use this to update matrices, picking base id, etc.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        /// <param name="info"></param>
        void PushObjectSpace(OpenGL gl, SceneGraph.Core.RenderMode renderMode, SharedStageInfo info);
        /// <summary>
        /// Do opposite work to <see cref="PushObjectSpace()"/> if needed.
        /// </summary>
        /// <param name="gl"></param>
        /// <param name="renderMode"></param>
        /// <param name="info"></param>
        void PopObjectSpace(OpenGL gl, SceneGraph.Core.RenderMode renderMode, SharedStageInfo info);
    }
}
