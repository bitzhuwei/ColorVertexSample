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

        // 这是用LockBits方式计算的，速度较快。
        /// <summary>
        /// 根据色板获取位图。
        /// </summary>
        /// <param name="colorPalette"></param>
        /// <returns></returns>
        private static Bitmap CreateTextureImage(this ColorPalette colorPalette, int width = 1000, int height = 1)
        {
            System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format32bppRgb;
            System.Drawing.Imaging.ImageLockMode lockMode = System.Drawing.Imaging.ImageLockMode.WriteOnly;
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height, format);
            Rectangle bitmapRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(bitmapRect, lockMode, format);

            int length = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] bitmapBytes = new byte[length];

            for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
            {
                int left = (int)(width * colorPalette.Coords[i]);
                int right = (int)(width * colorPalette.Coords[i + 1]);
                GLColor leftColor = colorPalette.Colors[i];
                GLColor rightColor = colorPalette.Colors[i + 1];
                for (int col = left; col < right; col++)
                {
                    Color color = (leftColor * ((right - col) * 1.0f / (right - left)) + rightColor * ((col - left) * 1.0f / (right - left)));
                    for (int row = 0; row < height; row++)
                    {
                        bitmapBytes[row * bmpData.Stride + col * 4 + 0] = color.B;
                        bitmapBytes[row * bmpData.Stride + col * 4 + 1] = color.G;
                        bitmapBytes[row * bmpData.Stride + col * 4 + 2] = color.R;
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(bitmapBytes, 0, bmpData.Scan0, length);

            bitmap.UnlockBits(bmpData);

            return bitmap;
        }

        // 这是用SetPixel方式计算的，速度较慢。
        ///// <summary>
        ///// 根据色板获取位图。
        ///// </summary>
        ///// <param name="colorPalette"></param>
        ///// <returns></returns>
        //private static Bitmap CreateTextureImage(this ColorPalette colorPalette, int width = 1000, int height = 20)
        //{
        //    Bitmap bitmap = new Bitmap(width, height);
        //    //Graphics g = Graphics.FromImage(bitmap);

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
        //                bitmap.SetPixel(x, y, color);
        //            }
        //        }
        //    }
        //    //g.Dispose();
        //    return bitmap;
        //}

    }
}
