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
using Utility;

namespace ColorVertexSample
{
    public class ColorIndicatorAttachment
    {
        private SceneControl control;
        private ColorTemplate colorTemplate;
        private Pen[] rectPens;
        private Pen whitePen = new Pen(Color.White);
        private RenderEventHandler renderEventHandler;
        public ColorTemplate ColorTemplate
        {
            get { return colorTemplate; }
            set
            {
                this.colorTemplate = value;
                if (value == null) { return; }

                //this.rectPens = new Pen[value.Colors.Length - 1];
                //for (int i = 0; i < value.Colors.Length -1 ; i++)
                //{
                //    this.rectPens[i]=new Pen(new LinearGradientBrush())
                //}
            }
        }

        public ColorIndicatorAttachment(ColorTemplate colorTemplate)
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
                var rect = new Rectangle(
                  colorTemplate.Margin.Left + i * blockWidth,
                  control.Height - colorTemplate.Height - colorTemplate.Margin.Bottom,
                  blockWidth, height);
                var brush = new LinearGradientBrush(rect,
                    colorTemplate.Colors[i], colorTemplate.Colors[i + 1],
                     LinearGradientMode.Horizontal);

                g.FillRectangle(brush, rect);
                g.DrawRectangle(whitePen, rect);
            }
        }
    }
}
