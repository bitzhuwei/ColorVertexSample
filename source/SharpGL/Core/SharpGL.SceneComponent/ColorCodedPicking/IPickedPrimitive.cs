using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// The color-coded picking result.
    /// <para>Representing a primitive.</para>
    /// </summary>
    public interface IPickedPrimitive
    {
        /// <summary>
        /// Gets or sets primitive's geometry type.
        /// </summary>
        GeometryTypes GeometryType { get; set; }

        /// <summary>
        /// Gets or sets positions of this primitive's vertices.
        /// </summary>
        float[] positions { get; set; }

        /// <summary>
        /// The scene's element from which this <see cref="IPickedPrimitive"/>'s instance is picked.
        /// </summary>
        IColorCodedPicking Element { get; set; }

#if DEBUG
        /// <summary>
        /// The last vertex's id that constructs the picked primitive.
        /// </summary>
        int StageVertexID { get; set; }
#endif
    }
}
