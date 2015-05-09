using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// A SceneElement can implement IMyHasObjectSpace to allow it to transform
    /// world space into object space.
    /// <para>This is a simpler version of <see cref="IHasObjectSpace"/>.</para>
    /// </summary>
    public interface IMyHasObjectSpace
    {
        /// <summary>
        /// Pushes us into Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        void PushObjectSpace(OpenGL gl);

        /// <summary>
        /// Pops us from Object Space using the transformation into the specified OpenGL instance.
        /// </summary>
        /// <param name="gl">The gl.</param>
        void PopObjectSpace(OpenGL gl);

    }
}
