using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL.SceneGraph.Core;
using SharpGL.RenderContextProviders;
using System.Drawing;

namespace SharpGL.SceneComponent
{
    public class OrthoColorIndicatorNumber : SceneElement, IRenderable, IDisposable
    {
        private double minValue;
        private double maxValue;
        private Control viewControl;
        private System.Drawing.Graphics graphics;
        private System.Drawing.Font font = new System.Drawing.Font("Courier New", 8.5f);

        public void SetBound(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public void SetControl(Control viewControl)
        {
            this.viewControl = viewControl;
        }

        public ColorTemplate colorTemplate { get; set; }

        public void Render(OpenGL gl, RenderMode renderMode)
        {
            ColorTemplate colorTemplate = this.colorTemplate;
            if (colorTemplate == null) { return; }

            IRenderContextProvider rc = gl.RenderContextProvider;
            Debug.Assert(rc != null);

            int width = 0;
            int height = 0;

            if (rc != null)
            {
                width = rc.Width;
                height = rc.Height;
            }
            else
            {
                int[] viewport = new int[4];
                gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
                width = viewport[2];
                height = viewport[3];
            }

            if (graphics == null && viewControl != null)
            { graphics = viewControl.CreateGraphics(); }

            int blockWidth = (width - colorTemplate.Margin.Left - colorTemplate.Margin.Right) / (colorTemplate.Colors.Length - 1);
            //draw numbers
            for (int i = 0; i < colorTemplate.Colors.Length; i++)
            {
                string value = (minValue * (double)(colorTemplate.Colors.Length - 1 - i) / (colorTemplate.Colors.Length - 1)
                    + maxValue * (double)i / (colorTemplate.Colors.Length - 1)).ToString();
                float valueLength = 0f;
                if (graphics != null)
                { valueLength = graphics.MeasureString(value, font).Width; }
                float x = colorTemplate.Margin.Left + i * blockWidth - valueLength / 2;
                int y = colorTemplate.Margin.Bottom - 20;
                gl.DrawText((int)x, (int)y, 1, 1, 1, "Courier New", 12.0f, value);
            }
        }

        public void Dispose()
        {
            Graphics g = this.graphics;
            this.graphics = null;
            if (g != null)
            {
                g.Dispose();
            }
        }

        ~OrthoColorIndicatorNumber()
        {
            Dispose();
        }
    }
}
