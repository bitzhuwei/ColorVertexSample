using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// maintains bounding box that contains all models.
    /// </summary>
    internal class ModelContainer : SceneElement, IRenderable
    {
        BoundingBox boundingBox = new BoundingBox();
        BoundingBox expandedBoundingBox = new BoundingBox();

        private float expandFactor = 1.1f;

        /// <summary>
        /// Render expanded bounding box.
        /// <para>1 is for original size.</para>
        /// </summary>
        public float ExpandFactor
        {
            get { return expandFactor; }
            set
            {
                if (value != expandFactor)
                {
                    expandFactor = value;
                    UpdateExpandedBoudingBox();
                }
            }
        }

        /// <summary>
        /// Determins whether render the bounding box or not.
        /// </summary>
        public bool RenderBoundingBox { get; set; }

        /// <summary>
        /// Adds a child and extend bounding box.
        /// </summary>
        /// <param name="child"></param>
        internal void AddChild(ScientificModelElement child)
        {
            base.AddChild(child);
            BoundingBox boundingBox = this.boundingBox;
            IBoundingBox modelBoundingBox = child.Model.BoundingBox;
            if (base.Children.Count > 1)
            {
                boundingBox.Extend(modelBoundingBox.MinPosition);
                boundingBox.Extend(modelBoundingBox.MaxPosition);
            }
            else 
            {
                boundingBox.MinPosition = modelBoundingBox.MinPosition;
                boundingBox.MaxPosition = modelBoundingBox.MaxPosition;
            }
            UpdateExpandedBoudingBox();
        }
        
        /// <summary>
        /// Removes a chlid and updates expanded bounding box.
        /// </summary>
        /// <param name="child"></param>
        internal void RemoveChild(ScientificModelElement child)
        {
            base.RemoveChild(child);
            //TODO: improve efficiency with some algorithm.
            this.boundingBox.Reset();
            foreach (var item in this.Children)
            {
                ScientificModelElement element = item as ScientificModelElement;
                if (element != null)
                {
                    IBoundingBox modelBoundingBox = child.Model.BoundingBox;
                    this.boundingBox.Extend(modelBoundingBox.MinPosition);
                    this.boundingBox.Extend(modelBoundingBox.MaxPosition);
                }
            }
            UpdateExpandedBoudingBox();
        }

        /// <summary>
        /// Clears children, resets bounding box and updates expanded bounding box.
        /// </summary>
        internal void ClearChild()
        {
            base.Children.Clear();
            this.boundingBox.Reset();
            UpdateExpandedBoudingBox();
        }

        public ModelContainer()
        {
            this.RenderBoundingBox = true;
        }

        private void UpdateExpandedBoudingBox()
        {
            float x, y, z;
            this.boundingBox.GetCenter(out x, out y, out z);
            float xSize, ySize, zSize;
            this.boundingBox.GetBoundDimensions(out xSize, out ySize, out zSize);
            this.expandedBoundingBox.MinPosition = new Vertex(
                ExpandFactor * (-xSize / 2) + x,
                ExpandFactor * (-ySize / 2) + y,
                ExpandFactor * (-zSize / 2) + z);
            this.expandedBoundingBox.MaxPosition = new Vertex(
                ExpandFactor * (xSize / 2) + x,
                ExpandFactor * (ySize / 2) + y,
                ExpandFactor * (zSize / 2) + z);
        }

        /// <summary>
        /// Adjusts camera according to bounding box.
        /// </summary>
        /// <param name="openGL"></param>
        /// <param name="camera"></param>
        internal void AdjustCamera(OpenGL openGL, ScientificCamera camera)
        {
            IBoundingBox boundingBox = this.boundingBox;

            float xSize, ySize, zSize;
            boundingBox.GetBoundDimensions(out xSize, out ySize, out zSize);
            float size = Math.Max(Math.Max(xSize, ySize), zSize);

            float x, y, z;
            boundingBox.GetCenter(out x, out y, out z);
            Vertex target = new Vertex(x, y, z);

            Vertex target2Position = camera.Position - camera.Target;
            target2Position.Normalize();

            Vertex position = target + target2Position * (size * 2 + 1);
            //new Vertex(0.0f, 0.0f, 1.0f) * (size * 2);

            int[] viewport = new int[4];
            openGL.GetInteger(SharpGL.Enumerations.GetTarget.Viewport, viewport);
            int width = viewport[2]; int height = viewport[3];

            IPerspectiveCamera perspectiveCamera = camera;
            perspectiveCamera.FieldOfView = 60;
            perspectiveCamera.AspectRatio = (double)width / (double)height;
            perspectiveCamera.Near = 0.001;
            perspectiveCamera.Far = double.MaxValue;
            
            IOrthoCamera orthoCamera = camera;
            if (width > height)
            {
                orthoCamera.Left = -size * width / height;
                orthoCamera.Right = size * width / height;
                orthoCamera.Bottom = -size;
                orthoCamera.Top = size;
            }
            else
            {
                orthoCamera.Left = -size;
                orthoCamera.Right = size;
                orthoCamera.Bottom = -size * height / width;
                orthoCamera.Top = size * height / width;
            }
            orthoCamera.Near = 0.001;
            orthoCamera.Far = double.MaxValue;

            camera.Position = position;
            camera.Target = target;
            //camera.UpVector = new Vertex(0f, 1f, 0f);
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            if (renderMode == RenderMode.HitTest) { return; }
            if (!this.RenderBoundingBox) { return; }
            IBoundingBox boundingBox = this.expandedBoundingBox;
            //IBoundingBox boundingBox = this.boundingBox;
            if (boundingBox == null) { return; }

            boundingBox.Render(gl, renderMode);
        }

        #endregion
    }
}
