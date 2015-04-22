using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponents
{
    /// <summary>
    /// The bounding helper.
    /// </summary>
    internal class BoundingVolumeHelper2
    {
        /// <summary>
        /// The bounding volume.
        /// </summary>
        private BoundingVolume boundingVolume = new BoundingVolume();

        /// <summary>
        /// Gets the bounding volume.
        /// </summary>
        public BoundingVolume BoundingVolume
        {
            get { return boundingVolume; }
        }
    }
}
