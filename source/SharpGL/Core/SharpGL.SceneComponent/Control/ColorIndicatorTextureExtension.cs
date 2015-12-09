using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
using SharpGL.SceneGraph;
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

        public static Bitmap CreateTextureImage(this SimpleUIColorIndicator colorIndicator, int width = 10000)

        {
            return colorIndicator.Data.ColorPalette.CreateTextureImage(width, 1);
        }
        ///// <summary>
        ///// 根据色板获取位图。
        ///// </summary>
        ///// <param name="colorPalette"></param>
        ///// <returns></returns>
        //private static Bitmap CreateTextureImage(this ColorPalette colorPalette, int width = 1000, int height = 20)
        //{
        //    Bitmap bmp = new Bitmap(width, height);
        //    //Graphics g = Graphics.FromImage(bitmap);
        //    // Lock the bitmap's bits.  
        //    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //    System.Drawing.Imaging.BitmapData bmpData =
        //        bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
        //        bmp.PixelFormat);

        //    // Get the address of the first line.
        //    IntPtr ptr = bmpData.Scan0;

        //    // Declare an array to hold the bytes of the bitmap.
        //    int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
        //    UnmanagedArray<byte> rgbValues = new UnmanagedArray<byte>(bytes);
        //    int index = 0;
        //    for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
        //    {
        //        int left = (int)(width * colorPalette.Coords[i]);
        //        int right = (int)(width * colorPalette.Coords[i + 1]);
        //        GLColor leftColor = colorPalette.Colors[i];
        //        GLColor rightColor = colorPalette.Colors[i + 1];
        //        for (int x = left; x < right; x++)
        //        {
        //            Color color = (leftColor * ((right - x) * 1.0f / (right - left)) + rightColor * ((x - left) * 1.0f / (right - left)));
        //            for (int y = 0; y < height; y++)
        //            {
        //                bmp.SetPixel(x, y, color);
        //                rgbValues[index++] = color.R;
        //                rgbValues[index++] = color.G;
        //                rgbValues[index++] = color.B;
        //            }
        //        }
        //    }

        //    // Copy the RGB values back to the bitmap
        //    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

        //    // Unlock the bits.
        //    bmp.UnlockBits(bmpData);

        //    // Draw the modified image.
        //    e.Graphics.DrawImage(bmp, 0, 150);


        //    //g.Dispose();
        //    return bmp;
        //}


        /// <summary>
        /// 根据色板获取位图。
        /// </summary>
        /// <param name="colorPalette"></param>
        /// <returns></returns>
        private static Bitmap CreateTextureImage(this ColorPalette colorPalette, int width = 1000, int height = 20)
        {
            Bitmap bitmap = new Bitmap(width, height);
            //Graphics g = Graphics.FromImage(bitmap);

            for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
            {
                int left = (int)(width * colorPalette.Coords[i]);
                int right = (int)(width * colorPalette.Coords[i + 1]);
                GLColor leftColor = colorPalette.Colors[i];
                GLColor rightColor = colorPalette.Colors[i + 1];
                for (int x = left; x < right; x++)
                {
                    Color color = (leftColor * ((right - x) * 1.0f / (right - left)) + rightColor * ((x - left) * 1.0f / (right - left)));
                    for (int y = 0; y < height; y++)
                    {
                        bitmap.SetPixel(x, y, color);
                    }
                }
            }
            //g.Dispose();
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

        //    for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
        //    {
        //        int left = (int)(width * colorPalette.Coords[i]);
        //        int right = (int)(width * colorPalette.Coords[i + 1]);
        //        //注意，最后一个色块的最后一个像素没有画到bitmap上。
        //        if (right == width) { right = width - 1; }
        //        Rectangle rect = new Rectangle(left, 0, right - left + 1, height);
        //        LinearGradientBrush brush = new LinearGradientBrush(
        //            rect,
        //            colorPalette.Colors[i], colorPalette.Colors[i + 1], LinearGradientMode.Horizontal);
        //        g.FillRectangle(brush, rect);
        //    }
        //    g.Dispose();
        //    return bitmap;
        //}

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
