using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// replace of <see cref="SharpGL.SceneGraph.Scene"/>
    /// </summary>
    public class MyScene : SharpGL.SceneGraph.Scene
    {
        public override void Draw(SceneGraph.Cameras.Camera camera = null)
        {
            var gl = OpenGL;
            if (gl == null) { return; }

            //  TODO: we must decide what to do about drawing - are 
            //  cameras completely outside of the responsibility of the scene?
            //  If no camera has been provided, use the current one.
            if (camera == null)
                camera = CurrentCamera;

            //	Set the clear color.
            float[] clear = (SharpGL.SceneGraph.GLColor)ClearColor;

            gl.ClearColor(clear[0], clear[1], clear[2], clear[3]);

            //  Reproject.
            if (camera != null)
                camera.Project(gl);

            //	Clear.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT |
                OpenGL.GL_STENCIL_BUFFER_BIT);

            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            //  Render the root element, this will then render the whole
            //  of the scene tree.
            RenderElement(SceneContainer, gl, RenderMode.Design);

            //  TODO: Adding this code here re-enables textures- it should work without it but it
            //  doesn't, look into this.
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            //gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Flush();
        }

        /// <summary>
        /// Renders the element.
        /// </summary>
        /// <param name="gl">The gl.</param>
        /// <param name="renderMode">The render mode.</param>
        public void RenderElement(SceneElement sceneElement, OpenGL gl, RenderMode renderMode)
        {
            //  If the element is disabled, we're done.
            if (sceneElement.IsEnabled == false)
                return;

            //  Push each effect.
            foreach (var effect in sceneElement.Effects)
                if (effect.IsEnabled)
                    effect.Push(gl, sceneElement);

            //  If the element can be bound, bind it.
            if (sceneElement is IBindable)
                ((IBindable)sceneElement).Push(gl);

            //  If the element has an object space, transform into it.
            if (sceneElement is IHasObjectSpace)
                ((IHasObjectSpace)sceneElement).PushObjectSpace(gl);

            //  If the element has a material, push it.
            if (sceneElement is IHasMaterial && ((IHasMaterial)sceneElement).Material != null)
                ((IHasMaterial)sceneElement).Material.Push(gl);

            //  If the element can be rendered, render it.
            if (sceneElement is IRenderable)
                ((IRenderable)sceneElement).Render(gl, renderMode);

            //  If the element has a material, pop it.
            if (sceneElement is IHasMaterial && ((IHasMaterial)sceneElement).Material != null)
                ((IHasMaterial)sceneElement).Material.Pop(gl);

            //  IF the element is volume bound and we are rendering volumes, render the volume.
            if (RenderBoundingVolumes && sceneElement is IVolumeBound)
                ((IVolumeBound)sceneElement).BoundingVolume.Render(gl, renderMode);

            //  Recurse through the children.
            foreach (var childElement in sceneElement.Children)
                RenderElement(childElement, renderMode);

            //  If the element has an object space, transform out of it.
            if (sceneElement is IHasObjectSpace)
                ((IHasObjectSpace)sceneElement).PopObjectSpace(gl);

            //  If the element can be bound, bind it.
            if (sceneElement is IBindable)
                ((IBindable)sceneElement).Pop(gl);

            //  Pop each effect.
            for (int i = sceneElement.Effects.Count - 1; i >= 0; i--)
                if (sceneElement.Effects[i].IsEnabled)
                    sceneElement.Effects[i].Pop(gl, sceneElement);
        }
    }
}