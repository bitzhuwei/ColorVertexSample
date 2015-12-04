using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.TextureHelper
{
    public static class ColorPaletteHelper
    {
        /// <summary>
        /// 根据色板获取位图。
        /// </summary>
        /// <param name="colorPalette"></param>
        /// <returns></returns>
        public static Bitmap GenBitmap(this ColorPalette colorPalette)
        {
            const int length = 256;
            Bitmap bitmap = new Bitmap(length, length);
            Graphics g = Graphics.FromImage(bitmap);

            for (int i = 0; i < colorPalette.Colors.Length - 1; i++)
            {
                int left = (int)(length * colorPalette.Coords[i]);
                int right = (int)(length * colorPalette.Coords[i + 1]);
                LinearGradientBrush brush = new LinearGradientBrush(
                    new Point(left, 0), new Point(right, 0),
                    colorPalette.Colors[i], colorPalette.Colors[i + 1]);
                g.FillRectangle(brush, new Rectangle(left, 0, right - left, length));
            }
            g.Dispose();

            return bitmap;
        }
    }
}
