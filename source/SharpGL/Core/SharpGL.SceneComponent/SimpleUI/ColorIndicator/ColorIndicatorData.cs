using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// Contains color palette, block count, min value, max value etc.
    /// <para>Works as model while <see cref="SimpleUIColorIndicator"/> works as view.</para>
    /// </summary>
    public class ColorIndicatorData
    {
        private long lastModified = 0;

        /// <summary>
        /// Get last time(in ticks) when this object's property is modified.
        /// </summary>
        internal long LastModified
        {
            get { return lastModified; }
        }

        ColorPalette colorPalette;

        /// <summary>
        /// Get or set color palette.
        /// </summary>
        public ColorPalette ColorPalette
        {
            get { return colorPalette; }
            set
            {
                if (value != colorPalette)
                {
                    colorPalette = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        public ColorIndicatorData(ColorPalette colorPalette)
        {
            this.ColorPalette = colorPalette;
        }

        private float step = 10.0f;

        public float Step
        {
            get { return step; }
            set
            {
                if (step != value)
                {
                    step = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        private string quantityLabel = string.Empty;
        /// <summary>
        /// Get or set label for the quantity.
        /// </summary>
        public string QuantityLabel
        {
            get { return quantityLabel; }
            set
            {
                if (quantityLabel != value)
                {
                    quantityLabel = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }


        private float minValue;
        /// <summary>
        /// Get or set minimum value.
        /// </summary>
        public float MinValue
        {
            get { return minValue; }
            set
            {
                if (minValue != value)
                {
                    minValue = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        private float maxValue;
        /// <summary>
        /// Get or set maximum value.
        /// </summary>
        public float MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;
                    lastModified = DateTime.Now.Ticks;
                }
            }
        }

        public GLColor MapToColor(float value)
        {
            return this.colorPalette.MapToColor(value, this.MinValue, this.MaxValue);
        }

        public int GetBlockCount()
        {
            if (this.step <= 0) { return -1; }

            int blockCount = (int)Math.Floor((this.MaxValue - this.MinValue) / this.Step) + 1;

            return blockCount;
        }

        //private GLColor[] colors;

        //public GLColor[] Colors
        //{
        //    get { return colors; }
        //    set
        //    {
        //        if (value == null || value.Length < 2)
        //        { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }
        //        colors = value;
        //    }
        //}

        //public ColorIndicatorData(GLColor[] colors, int minValue = 0, int maxValue = 0)
        //{
        //    if (colors == null || colors.Length < 2)
        //    { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }
        //    if (maxValue < minValue)
        //    { throw new Exception("minValue must less than or equal to maxValue."); }

        //    this.Colors = colors;
        //    this.minValue = minValue;
        //    this.maxValue = maxValue;
        //}

        //public GLColor MapToColor(float value)
        //{
        //    GLColor result = MapToColor(value, this.minValue, this.maxValue);
        //    return result;
        //}

        //public GLColor MapToColor(float value, float minValue, float maxValue)
        //{
        //    GLColor[] colors = this.Colors;
        //    if (colors == null || colors.Length < 2)
        //    { throw new ArgumentNullException("colors", "colors' count must greater than 1."); }

        //    float difference = maxValue - minValue;
        //    if (difference < 0.0f)
        //    { throw new ArgumentException("fault value range"); }

        //    if (difference == 0f)
        //    { return colors[0]; }

        //    if (value <= minValue) { return colors[0]; }
        //    if (value >= maxValue) { return colors[colors.Length - 1]; }

        //    float step = difference / (colors.Length - 1);

        //    int leftIndex = (int)Math.Floor(value - minValue / step);

        //    float leftValue = leftIndex * step;
        //    float rightValue = leftValue + step;

        //    GLColor leftColor = colors[leftIndex];
        //    GLColor rightColor = colors[leftIndex + 1];

        //    GLColor color = new GLColor();
        //    color = leftColor * ((value - leftValue) / step)
        //        + rightColor * ((rightValue - value) / step);

        //    return color;
        //}

        public override string ToString()
        {
            return string.Format("{0},min:{1}, max:{2}, step:{3}", colorPalette, minValue, maxValue, step);
        }
    }
}
