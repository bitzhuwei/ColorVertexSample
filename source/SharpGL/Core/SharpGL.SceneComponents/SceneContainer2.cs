using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponents
{
    /// <summary>
    /// The Scene Container is the top-level object in a scene graph.
    /// </summary>
    public class SceneContainer2 : SceneElement2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainer2"/> class.
        /// </summary>
        public SceneContainer2()
        {
            Name = "Scene Container";
        }

        /// <summary>
        /// Gets or sets the parent scene.
        /// </summary>
        /// <value>
        /// The parent scene.
        /// </value>
        [XmlIgnore]
        public Scene2 ParentScene
        {
            get;
            set;
        }
    }
}
