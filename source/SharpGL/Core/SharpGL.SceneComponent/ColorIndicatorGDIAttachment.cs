using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Draw color indicator to SceneControl with GDI+
    /// </summary>
    public class ColorIndicatorGDIAttachment
    {
        private SceneControl control;
        private ColorTemplate colorTemplate;
        private Pen whitePen = new Pen(Color.White);
        private Brush whiteBrush = new SolidBrush(Color.White);
        private Font font = new Font("宋体", 10);
        private RenderEventHandler renderEventHandler;
        private float minValue;
        private float maxValue;
        public ColorTemplate ColorTemplate
        {
            get { return colorTemplate; }
            set
            {
                if(value == null)
                { throw new ArgumentNullException(); }

                this.colorTemplate = value;
            }
        }

        public ColorIndicatorGDIAttachment(ColorTemplate colorTemplate)
        {
            if (colorTemplate == null)
            { throw new ArgumentNullException("colorTemplate"); }

            this.ColorTemplate = colorTemplate;
            this.renderEventHandler = new RenderEventHandler(SceneControl_GDIDraw);
        }

        public void AttachTo(SceneControl control)
        {
            if (control == null)
            { throw new ArgumentNullException("control"); }

            this.control = control;
            control.GDIDraw += this.renderEventHandler;
        }

        public void Dettach()
        {
            var control = this.control;
            if (control == null) { return; }

            control.GDIDraw -= this.renderEventHandler;

            this.control = null;
        }

        /// <summary>
        /// Draw axis at corner of view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void SceneControl_GDIDraw(object sender, RenderEventArgs args)
        {
            var colorTemplate = this.ColorTemplate;
            if (colorTemplate == null) { return; }
            var control = this.control;
            if (control == null) { return; }

            var g = args.Graphics;
            var blockWidth = (control.Width - colorTemplate.Margin.Left - colorTemplate.Margin.Right) / (colorTemplate.Colors.Length - 1);
            var height = colorTemplate.Height;
            //draw rectangles
            for (int i = 0; i < colorTemplate.Colors.Length - 1; i++)
            {
                var x = colorTemplate.Margin.Left + i * blockWidth;
                var y = control.Height - (colorTemplate.Height + colorTemplate.Margin.Bottom);
                var rect = new Rectangle(x, y, blockWidth, height);
                var brush = new LinearGradientBrush(rect,
                    colorTemplate.Colors[i], colorTemplate.Colors[i + 1],
                     LinearGradientMode.Horizontal);

                g.FillRectangle(brush, rect);
                g.DrawRectangle(whitePen, rect);
            }
            //draw dropping lines
            for (int i = 0; i < colorTemplate.Colors.Length; i++)
            {
                var x = colorTemplate.Margin.Left + i * blockWidth;
                var y = control.Height - (colorTemplate.Margin.Bottom);
                g.DrawLine(whitePen, x, y, x, y + 6);
            }
            //draw numbers
            for (int i = 0; i < colorTemplate.Colors.Length; i++)
            {
                var value = (minValue * (double)(colorTemplate.Colors.Length - 1 - i) / (colorTemplate.Colors.Length - 1)
                    + maxValue * (double)i / (colorTemplate.Colors.Length - 1)).ToString();
                var size = g.MeasureString(value, font);
                var x = colorTemplate.Margin.Left + i * blockWidth - size.Width / 2;
                var y = control.Height - (colorTemplate.Margin.Bottom - 9);
                g.DrawString(value, font, whiteBrush, x, y);
            }
        }

        public void SetBound(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}
