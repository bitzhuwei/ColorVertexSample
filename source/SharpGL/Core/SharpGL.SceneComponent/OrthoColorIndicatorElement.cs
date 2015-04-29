using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponent
{
    public class OrthoColorIndicatorElement : SceneElement
    {
        public OrthoColorIndicatorBar bar { get; set; }

        public OrthoColorIndicatorNumber number { get; set; }
    }
}
