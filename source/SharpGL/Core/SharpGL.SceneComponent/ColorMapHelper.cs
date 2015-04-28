using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class ColorMapHelper
    {


        /// <summary>
        /// 如果两点重合，则斜率为NAN.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        private static double LineSlope(double y2, double y1, double step)
        {
            return (y2 - y1) / step;
        }

        public static GLColor MapToColor(double value, GLColor[] template, double minValue, double maxValue)
        {
            if (template.Length < 2)
                throw new ArgumentException("template colors size error");

            double d = maxValue - minValue;
            if (d < 0.0f)
                throw new ArgumentException("fault value range");

            if (value < minValue)
              value = minValue;
            if (value > maxValue)
              value = maxValue;

            if (d == 0.0d)
              return template[0];
              
            double step = d / template.Length;

            double d0 = value - minValue;
            int leftIndex = (int)Math.Floor(d0/step);

            double x0 = leftIndex * step;
            double x = value;
            
            double dx = x - x0;
            if (dx == 0.0d)
                return template[leftIndex];

            GLColor y0 = template[leftIndex];
            GLColor y2 = template[leftIndex + 1];

            GLColor color = new GLColor();
            double k = LineSlope(y2.R, y0.R, step);
            double r = y0.R + k * (x - x0);
            double g = y0.G + k * (x - x0);
            double b = y0.B + k * (x - x0);
            double total = r + g + b;

            color.Set((float)(r/total), (float)(g/total), (float)(b/total));
            return color;

        }
    }
}
