using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.Model
{
    public class ModelSpaceObjectContainer:SceneElement
    {
        private ModelSpace modelSpace;

        public ModelSpaceObjectContainer(ModelSpace modelSpace)
        {
            this.modelSpace = modelSpace;
        }


        public ModelSpace ModelSpace
        {
            get
            {
                return this.modelSpace;
            }
        }

        public new void AddChild(ModelObject modelElement)
        {
            base.AddChild(modelElement);
        }

        public new void RemoveChild(ModelObject modelElement)
        {
            base.RemoveChild(modelElement);
        }

    }
}
