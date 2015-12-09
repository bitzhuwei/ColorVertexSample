using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{


    public static class ColorIndicatorTextureExtension
    {

        public static Bitmap CreateTextureImage(this SimpleUIColorIndicator colorIndicator, int width = 1000)
        {
            return colorIndicator.Data.ColorPalette.CreateTextureImage(width, 20);
        }

        /// <summary>
        /// 根据色板获取位图。
        /// </summary>
        /// <param name="colorPalette"></param>
        /// <returns></returns>
        private static Bitmap CreateTextureImage(this ColorPalette colorPalette, int width = 1000, int height = 20)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
            {
                int left = (int)(width * colorPalette.Coords[i]);
                int right = (int)(width * colorPalette.Coords[i + 1]);
                if (right == width) { right = width - 1; }
                Rectangle rect = new Rectangle(left, 0, right - left + 1, height);
                LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    colorPalette.Colors[i], colorPalette.Colors[i + 1], LinearGradientMode.Horizontal);
                g.FillRectangle(brush, rect);
            }
            g.Dispose();
            return bitmap;
        }
        ///// <summary>
        ///// 根据色板获取位图。
        ///// </summary>
        ///// <param name="colorPalette"></param>
        ///// <returns></returns>
        //private static Bitmap CreateTextureImage(this ColorPalette colorPalette, int width = 1000, int height = 20)
        //{

        //    Bitmap bitmap = new Bitmap(width, height);
        //    Graphics g = Graphics.FromImage(bitmap);

        //    float[] intensities = colorPalette.Coords;



        //    for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
        //    {
        //        float left =   width * colorPalette.Coords[i];
        //        float right = width * colorPalette.Coords[i + 1];
        //        float widthF = right - left;
        //        RectangleF textureRect = new RectangleF(new PointF(left, 0), new SizeF(widthF, height));

        //        float  rWidth = colorPalette.Coords[i+1] - colorPalette.Coords[i];
        //        RectangleF rect = new RectangleF(new PointF(left, 0), new SizeF(widthF, height));
        //        Color color0 = colorPalette.Colors[i];
        //        Color color1 = colorPalette.Colors[i + 1];

        //        LinearGradientBrush brush = new LinearGradientBrush(
        //            rect,
        //            color0, color1,LinearGradientMode.Horizontal);

        //        g.FillRectangle(brush, textureRect);
        //    }
        //    g.Dispose();
        //    return bitmap;
        //}


    }
}
