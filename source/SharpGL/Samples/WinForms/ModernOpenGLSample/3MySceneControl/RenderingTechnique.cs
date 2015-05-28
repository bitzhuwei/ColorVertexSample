using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernOpenGLSample._3MySceneControl
{
    /// <summary>
    /// The available techniques for rendering particles.
    /// </summary>
    enum RenderingTechnique
    {
        /// <summary>
        /// Use OpenGL point sprites to render imposter quads with a texture map.
        /// </summary>
        POINT_SPRITES,	
        /// <summary>
        /// Render explicit quad geometry made of two triangles.
        /// </summary>
        IMPOSTER_QUADS,	
        /// <summary>
        /// Render a cube for each particle (possibly using a raytracing fragment shader to make it look spherical).
        /// </summary>
        CUBE_GEOMETRY,
    };
}
