﻿using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Use thsi for ortho projection * view matrix.
    /// <para>Typical usage: projection * view * model in GLSL.</para>
    /// </summary>
    public interface IOrthoViewCamera : IOrthoCamera, IViewCamera
    {
    }
}
