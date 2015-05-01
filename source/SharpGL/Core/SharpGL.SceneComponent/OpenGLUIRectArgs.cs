using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// pass viewport and UI's rect information.
    /// </summary>
    public class OpenGLUIRectArgs
    {
        /// <summary>
        /// viewport's width.
        /// </summary>
        public int viewWidth;
        /// <summary>
        /// viewport's height.
        /// </summary>
        public int viewHeight;
        /// <summary>
        /// UI's width in viewport.
        /// </summary>
        public int UIWidth;
        /// <summary>
        /// UI's height in viewHeight.
        /// </summary>
        public int UIHeight;
        /// <summary>
        /// left in gl.Ortho(left, right, bottom, top, zNear, zFar);
        /// </summary>
        public int left;
        /// <summary>
        /// bottom in gl.Ortho(left, right, bottom, top, zNear, zFar);
        /// </summary>
        public int bottom;
        /// <summary>
        /// right in gl.Ortho(left, right, bottom, top, zNear, zFar);
        /// </summary>
        public int right { get { return left + viewWidth; } }
        /// <summary>
        /// top in gl.Ortho(left, right, bottom, top, zNear, zFar);
        /// </summary>
        public int top { get { return bottom + viewHeight; } }
    }
}
