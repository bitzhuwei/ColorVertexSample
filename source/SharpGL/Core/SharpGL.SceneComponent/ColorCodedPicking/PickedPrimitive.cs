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
    public class PickedPrimitive : IPickedPrimitive
    {
        /// <summary>
        /// Gets or sets primitive's type.
        /// </summary>
        public PrimitiveType Type { get; set; }

        /// <summary>
        /// Gets or sets values of this primitive.
        /// </summary>
        public float[] positions { get; set; }

        /// <summary>
        /// The element that this picked primitive belongs to.
        /// </summary>
        public IColorCodedPicking Element { get; set; }

#if DEBUG
        public int StageVertexID { get; set; }
#endif

        public override string ToString()
        {
            string result = string.Format("{0}:{1}|belong to:{2}", Type, positions.PrintPositions(), Element);
            return result;
            //return base.ToString();
        }

    }
}
