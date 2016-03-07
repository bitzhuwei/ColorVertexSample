using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpGL.SceneComponent
{
    internal class ColorIndicatorNumber : SceneElement, IRenderable
    {
        public ColorIndicatorData Data { get; set; }

        //private System.Drawing.Font font = new System.Drawing.Font("Courier New", fontSize);
        const float fontSize = 12f;


        /// <summary>
        /// 计算格式化精度。
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private string MinorFormatString(float step)
        {

            if (step == 0.0f)
                return "F0";

            double p = Math.Log10(step);
            int sign = Math.Sign(p);
            double absP = Math.Abs(p);
            int precision;
            if (absP >= 1.0)
            {
                precision = (int)Math.Round(Math.Floor(Math.Abs(absP)));
            }
            else
            {
                precision=(int)Math.Round(Math.Ceiling(Math.Abs(absP)));
            }

            if (sign < 0)
            {
                //小数
                return String.Format("F{0}", precision);
            }
            else
            {
                return String.Format("F{0}", 0);
            }
        }


        public void Render(OpenGL gl, RenderMode renderMode)
        {
            SimpleUIRectArgs lastArgs = this.CurrentArgs;
            if (lastArgs == null) { return; }
            ColorIndicatorData data = this.Data;
            if (data == null) { return; }

            int blockCount = data.GetBlockCount();
            if (blockCount <= 0) { return; }

            String formatStr = MinorFormatString(data.Step);

            GLColor[] colors = data.ColorPalette.Colors;
            int blockWidth = 0;
            if (data.MaxValue - data.MinValue == 0)
            {
                blockWidth = lastArgs.UIWidth;
            }
            else
            {
                blockWidth = (int)(lastArgs.UIWidth * (data.Step / (data.MaxValue - data.MinValue)));
            }
            //draw numbers
            for (int i = 0; i <= blockCount; i++)
            {
                string value = null;
                if (i == blockCount)
                {
                    if (!data.UseLogarithmic)
                    {
                        value = data.MaxValue.ToString(formatStr,CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        value = Math.Pow(data.LogBase, data.MaxValue).ToString(formatStr,CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    float tickValue = data.MinValue + data.Step * i;
                    if (!data.UseLogarithmic)
                        value = tickValue.ToString(formatStr,CultureInfo.InvariantCulture);
                    else
                        value = Math.Pow(data.LogBase, tickValue).ToString(formatStr,CultureInfo.InvariantCulture);
                }
                double valueLength = 100.0 * value.Length / fontSize;
                double x = 0;
                if (i == blockCount)
                { x = -(double)lastArgs.UIWidth / 2 - lastArgs.left + lastArgs.UIWidth - valueLength / 2; }
                else
                { x = -(double)lastArgs.UIWidth / 2 - lastArgs.left + i * blockWidth - valueLength / 2; }
                double y = -(double)lastArgs.UIHeight / 2 - lastArgs.bottom - 14;
                gl.DrawText((int)x, (int)y, 1, 1, 1, "Courier New", fontSize, value);
            }
        }

        public SimpleUIRectArgs CurrentArgs { get; set; }
    }
}
