using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColorVertexSample.Model;
using SharpGL;
using SharpGL.SceneGraph.Core;

namespace ColorVertexSample.Visual
{
    class PointModelElement : SceneElement, IRenderable, IDisposable
    {
        private PointModel _model;

        public PointModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public PointModelElement(PointModel model)
        {
            this._model = model;
        }

        public void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            if (this._model.PointCount <= 0)
                return;

            unsafe
            {
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.Enable(0X8861);

                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, (IntPtr)this._model.Positions);
                gl.ColorPointer(3, OpenGL.GL_BYTE, 0, (IntPtr)this._model.Colors);

                gl.DrawArrays(OpenGL.GL_POINTS, 0, this._model.PointCount);

                gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);
            }
        }

        public void Dispose()
        {
        }

        protected void Dispose(bool disposing)
        {
        }

        public SharpGL.SceneComponent.MyArcBallEffect modelArcBallEffect { get; set; }
    }
}
