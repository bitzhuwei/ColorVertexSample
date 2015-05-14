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
            IBoundingBox modelBoundingBox = child.Model.BoundingBox;
            this.boundingBox.Extend(modelBoundingBox.MinPosition);
            this.boundingBox.Extend(modelBoundingBox.MaxPosition);
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
        internal void AdjustCamera(OpenGL openGL, SceneGraph.Cameras.LookAtCamera camera)
        {
            IBoundingBox boundingBox = this.boundingBox;
            float xSize, ySize, zSize;
            boundingBox.GetBoundDimensions(out xSize, out ySize, out zSize);
            float x, y, z;
            boundingBox.GetCenter(out x, out y, out z);
            Vertex center = new Vertex(x, y, z);

            float size = Math.Max(Math.Max(xSize, ySize), zSize);

            Vertex position = center + new Vertex(0.0f, 0.0f, 1.0f) * (size * 2);

            int[] viewport = new int[4];
            openGL.GetInteger(SharpGL.Enumerations.GetTarget.Viewport, viewport);
            int width = viewport[2]; int height = viewport[3];
            camera.Position = position;
            camera.Target = center;
            camera.UpVector = new Vertex(0f, 1f, 0f);
            camera.FieldOfView = 60;
            camera.AspectRatio = (double)width / (double)height;
            camera.Near = 0.001;
            camera.Far = float.MaxValue;
        }

        #region IRenderable 成员

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            if (renderMode == RenderMode.HitTest) { return; }
            if (!this.RenderBoundingBox) { return; }
            IBoundingBox boundingBox = this.expandedBoundingBox;
            if (boundingBox == null) { return; }

            boundingBox.Render(gl, renderMode);
        }

        #endregion
    }
}
