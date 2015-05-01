using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// shows axis at left bottom corner in a fixed position.
    /// <para>supports arcball rotation in a moving camera</para>
    /// </summary>
    public class OrthoAxisElement : SceneElement
    {
        public OrthoArcBallEffect orthoArcBallEffect { get; set; }
    }
}
