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
        public int viewWidth;
        public int viewHeight;
        public int UIWidth;
        public int UIHeight;
        public int left;
        public int bottom;
        public int right { get { return left + viewWidth; } }
        public int top { get { return bottom + viewHeight; } }
    }
}
