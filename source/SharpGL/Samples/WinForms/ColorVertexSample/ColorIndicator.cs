﻿using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ColorTemplate
    {

        private GLColor[] colors;
        public System.Windows.Forms.Padding Margin = new System.Windows.Forms.Padding(3, 3, 20, 40);
        public System.Windows.Forms.AnchorStyles Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
        public int Width = 100;
        public int Height = 40;

        public GLColor[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public ColorTemplate(GLColor[] colors)
        {
            if (colors == null || colors.Length < 2)
                throw new  ArgumentException("");
            this.colors = colors;
        }

        public GLColor MapToColor(float value, float minValue, float maxValue)
        {
           return  ColorMapHelper.MapToColor(value, this.colors, minValue, maxValue);
        }
    }

    public class ColorTemplateFactory
    {

        public static ColorTemplate CreateRainbow()
        {
           
            GLColor[] colors = new GLColor[5];
            colors[0] = System.Drawing.Color.FromArgb(255, 0, 22, 76);
            colors[1] = System.Drawing.Color.FromArgb(255, 0, 193, 136);
            colors[2] = System.Drawing.Color.FromArgb(255, 166, 255, 27);
            colors[3] = System.Drawing.Color.FromArgb(255, 255, 173, 0);
            colors[4] = System.Drawing.Color.FromArgb(255, 255, 8, 1);
            return new ColorTemplate(colors);
        }

    }
}
