using SharpGL;
using SharpGL.Enumerations;
using SharpGL.OpenGLAttributes;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraSample.Model
{
    public class BoundingBox:SceneElement,IRenderable
    {
        private GLColor lineColor = new GLColor(1.0f, 1.0f, 1.0f, 1.0f);

        //left bottom near
        private Vertex lbn = new Vertex(-1, -1, 1);

        //right top far
        private Vertex rtf = new Vertex(1, 1, -1.0f);

        public void Render(SharpGL.OpenGL gl, RenderMode renderMode)
        {
            gl.PushAttrib(AttributeMask.Lighting);
            LightingAttributes lighting = new LightingAttributes();
            lighting.Enable = false;
            lighting.SetAttributes(gl);


            gl.Color(lineColor);
            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(lbn.X, lbn.Y, lbn.Z);
            gl.Vertex(rtf.X, lbn.Y, lbn.Z);
            gl.Vertex(rtf.X, lbn.Y, rtf.Z);
            gl.Vertex(lbn.X, lbn.Y, rtf.Z);
            gl.End();

            gl.Begin(BeginMode.LineLoop);
            gl.Vertex(lbn.X, rtf.Y, lbn.Z);
            gl.Vertex(rtf.X, rtf.Y, lbn.Z);
            gl.Vertex(rtf.X, rtf.Y, rtf.Z);
            gl.Vertex(lbn.X, rtf.Y, rtf.Z);
            gl.End();

            gl.Begin(BeginMode.Lines);
            gl.Vertex(lbn.X, lbn.Y, lbn.Z);
            gl.Vertex(lbn.X, rtf.Y, lbn.Z);
            gl.Vertex(rtf.X, lbn.Y, lbn.Z);
            gl.Vertex(rtf.X, rtf.Y, lbn.Z);
            gl.Vertex(rtf.X, lbn.Y, rtf.Z);
            gl.Vertex(rtf.X, rtf.Y, rtf.Z);
            gl.Vertex(lbn.X, lbn.Y, rtf.Z);
            gl.Vertex(lbn.X, rtf.Y, rtf.Z);
            gl.End();

            gl.PopAttrib();
        }


        public GLColor LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
            }
        }

        public Vertex LBN
        {
            get
            {
                return this.lbn;
            }
            set
            {
                this.lbn = value;
            }
        }

        public Vertex RTF
        {
            get
            {
                return this.rtf;
            }
            set
            {
                this.rtf = value;
            }
        }



    }
}
