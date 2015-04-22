using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph.Effects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Core;

namespace SharpGL.SceneComponents
{
    /// <summary>
    /// The base class for all elements in a scene. Anything in
    /// the scene tree is a Scene Element. Scene elements can
    /// have children but are very lacking in functionality -
    /// implement 
    /// </summary>
    [XmlInclude(typeof(Folder2))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Primitives.Polygon))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Primitives.Cube))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Primitives.Axies))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Primitives.Grid))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Quadrics.Quadric))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Quadrics.Cylinder))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Quadrics.Disk))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Quadrics.Sphere))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Lighting.Light))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Effects.LinearTransformationEffect))]
    [XmlInclude(typeof(SharpGL.SceneGraph.Effects.OpenGLAttributesEffect))]
    public partial class SceneElement2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneElement2"/> class.
        /// </summary>
        public SceneElement2()
        {
            Name = "Scene Element";
            IsEnabled = true;
        }

        /// <summary>
        /// Adds a child.
        /// </summary>
        /// <param name="child">The child scene element.</param>
        public void AddChild(SceneElement2 child)
        {
            //  Throw an exception if the child already has a parent.
            if (child.Parent != null)
                throw new Exception("Cannot add the child element '" + child.Name +
                    "' - it is already in the Scene Tree.");

            //  Add the child and set the parent.
            children.Add(child);
            child.Parent = this;

            //  If the child has OpenGL context which is different to this, we must create it.
            if (child is IHasOpenGLContext)
            {
                //  Cast the chil.d
                IHasOpenGLContext contextChild = child as IHasOpenGLContext;

                //  Get the parent OpenGL.
                OpenGL gl = TraverseToRootElement().ParentScene.OpenGL;

                //  If we don't exist in this context, create in this context.
                if(contextChild.CurrentOpenGLContext != gl)
                    contextChild.CreateInContext(gl);
            }
        }

        /// <summary>
        /// Removes the child scene element.
        /// </summary>
        /// <param name="child">The child scene element.</param>
        public void RemoveChild(SceneElement2 child)
        {       
            //  If the child has OpenGL context which is not the current one, we must destroy it.
            if (child is IHasOpenGLContext)
                if(((IHasOpenGLContext)child).CurrentOpenGLContext != TraverseToRootElement().ParentScene.OpenGL)
                    ((IHasOpenGLContext)child).DestroyInContext(TraverseToRootElement().ParentScene.OpenGL);
  
            //  Throw an exception if the child is not a child of this element..
            if (child.Parent != this)
                throw new Exception("Cannot remove the child element '" + child.Name +
                    "' - it is not a child of '" + Name + "'.");

            //  Remove the child.
            children.Remove(child);
            child.Parent = null;
        }

        /// <summary>
        /// Adds an effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void AddEffect(Effect2 effect)
        {
            //  Add the effect.
            effects.Add(effect);
        }

        /// <summary>
        /// Removes the effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void RemoveEffect(Effect2 effect)
        {
            //  Remove the effect.
            effects.Remove(effect);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            //  Return the name.
            return GetType().Name + ": " + Name;
        }

        /// <summary>
        /// The children of the element.
        /// </summary>
        private ObservableCollection<SceneElement2> children = new ObservableCollection<SceneElement2>();

        /// <summary>
        /// The effects.
        /// </summary>
        private ObservableCollection<Effect2> effects = new ObservableCollection<Effect2>();

        /// <summary>
        /// Gets the children.
        /// </summary>
        [Browsable(false)]
        public ObservableCollection<SceneElement2> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Gets the effects.
        /// </summary>
        [Browsable(false)]
        public ObservableCollection<Effect2> Effects
        {
            get { return effects; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        [Description("The parent scene element."), Category("Scene Element")]
        [XmlIgnore]
        public SceneElement2 Parent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Description("The object name."), Category("Scene Element")]
        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        [Description("If true, the object participates in rendering."), Category("Scene Element")]
        [XmlAttribute]
        public bool IsEnabled
        {
            get;
            set;
        }
    }
}
