using SharpGL;
using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldingGeometryModel.FontResources;

namespace YieldingGeometryModel
{
    public partial class PointSpriteStringElement
    {
        private int textureWidth;

        /// <summary>
        /// TODO: 这里生成的中间贴图太大，有优化的空间
        /// </summary>
        /// <param name="content"></param>
        private void InitTexture(OpenGL gl, string content, int fontSize, int maxRowWidth, FontResource resource)
        {
            // step 1: get totalLength
            int totalLength = 0;
            {
                int glyphsLength = 0;
                for (int i = 0; i < content.Length; i++)
                {
                    char c = content[i];
                    CharacterInfo cInfo;
                    if (fontResource.CharInfoDict.TryGetValue(c, out cInfo))
                    {
                        int glyphWidth = cInfo.width;
                        glyphsLength += glyphWidth;
                    }
                    //else
                    //{ throw new Exception(string.Format("Not support for display the char [{0}]", c)); }
                }

                //glyphsLength = (glyphsLength * this.fontSize / FontResource.Instance.FontHeight);
                int interval = fontResource.FontHeight / 10; if (interval < 1) { interval = 1; }
                //interval = fontResource.CharInfoDict[' '].width / 10; if (interval < 1) { interval = 1; }
                totalLength = glyphsLength + interval * (content.Length - 1);
            }

            // step 2: setup contentBitmap
            Bitmap contentBitmap = null;
            {
                int interval = fontResource.FontHeight / 10; if (interval < 1) { interval = 1; }
                //int totalLength = glyphsLength + interval * (content.Length - 1);
                int currentTextureWidth = 0;
                int currentWidthPos = 0;
                int currentHeightPos = 0;
                if (totalLength * fontSize > maxRowWidth * fontResource.FontHeight)// 超过1行能显示的内容
                {
                    currentTextureWidth = maxRowWidth * fontResource.FontHeight / fontSize;

                    int lineCount = (totalLength - 1) / currentTextureWidth + 1;
                    // 确保整篇文字的高度在贴图中间。
                    currentHeightPos = (currentTextureWidth - fontResource.FontHeight * lineCount) / 2;
                    //- FontResource.Instance.FontHeight / 2;
                }
                else//只在一行内即可显示所有字符
                {
                    if (totalLength >= fontResource.FontHeight)
                    {
                        currentTextureWidth = totalLength;

                        // 确保整篇文字的高度在贴图中间。
                        currentHeightPos = (currentTextureWidth - fontResource.FontHeight) / 2;
                        //- FontResource.Instance.FontHeight / 2;
                    }
                    else
                    {
                        currentTextureWidth = fontResource.FontHeight;

                        currentWidthPos = (currentTextureWidth - totalLength) / 2;
                        //glyphsLength = fontResource.FontHeight;
                    }
                }

                //this.textureWidth = textureWidth * this.fontSize / FontResource.Instance.FontHeight;
                //currentWidthPosition = currentWidthPosition * this.fontSize / FontResource.Instance.FontHeight;
                //currentHeightPosition = currentHeightPosition * this.fontSize / FontResource.Instance.FontHeight;

                contentBitmap = new Bitmap(currentTextureWidth, currentTextureWidth);
                Graphics gContentBitmap = Graphics.FromImage(contentBitmap);
                Bitmap bigBitmap = fontResource.FontBitmap;
                for (int i = 0; i < content.Length; i++)
                {
                    char c = content[i];
                    CharacterInfo cInfo;
                    if (fontResource.CharInfoDict.TryGetValue(c, out cInfo))
                    {
                        if (currentWidthPos + cInfo.width > contentBitmap.Width)
                        {
                            currentWidthPos = 0;
                            currentHeightPos += fontResource.FontHeight;
                        }

                        gContentBitmap.DrawImage(bigBitmap,
                            new Rectangle(currentWidthPos, currentHeightPos, cInfo.width, fontResource.FontHeight),
                            new Rectangle(cInfo.xoffset, cInfo.yoffset, cInfo.width, fontResource.FontHeight),
                            GraphicsUnit.Pixel);

                        currentWidthPos += cInfo.width + interval;
                    }
                }
                gContentBitmap.Dispose();
                //contentBitmap.Save("PointSpriteStringElement-contentBitmap.png");
                System.Drawing.Bitmap bmp = null;
                if (totalLength * fontSize > maxRowWidth * fontResource.FontHeight)// 超过1行能显示的内容
                {
                    bmp = (System.Drawing.Bitmap)contentBitmap.GetThumbnailImage(
                        maxRowWidth, maxRowWidth, null, IntPtr.Zero);
                }
                else//只在一行内即可显示所有字符
                {
                    if (totalLength >= fontResource.FontHeight)
                    {
                        bmp = (System.Drawing.Bitmap)contentBitmap.GetThumbnailImage(
                            totalLength * fontSize / resource.FontHeight,
                            totalLength * fontSize / resource.FontHeight,
                            null, IntPtr.Zero);

                    }
                    else
                    {
                        bmp = (System.Drawing.Bitmap)contentBitmap.GetThumbnailImage(
                            fontSize, fontSize, null, IntPtr.Zero);
                    }
                }
                contentBitmap.Dispose();
                contentBitmap = bmp;
                //contentBitmap.Save("PointSpriteStringElement-contentBitmap-scaled.png");
            }

            // step 4: get texture's size 
            int targetTextureWidth;
            {

                ////	Get the maximum texture size supported by OpenGL.
                //int[] textureMaxSize = { 0 };
                //GL.GetInteger(GetTarget.MaxTextureSize, textureMaxSize);

                ////	Find the target width and height sizes, which is just the highest
                ////	posible power of two that'll fit into the image.

                //targetTextureWidth = textureMaxSize[0];
                ////System.Drawing.Bitmap bitmap = contentBitmap;
                //int scaledWidth = 8 * contentBitmap.Width * fontSize / fontResource.FontHeight;

                //for (int size = 1; size <= textureMaxSize[0]; size *= 2)
                //{
                //    if (scaledWidth < size)
                //    {
                //        targetTextureWidth = size / 2;
                //        break;
                //    }
                //    if (scaledWidth == size)
                //        targetTextureWidth = size;
                //}

                //this.textureWidth = targetTextureWidth;
                this.textureWidth = contentBitmap.Width;
                targetTextureWidth = contentBitmap.Width;
            }

            // step 5: scale contentBitmap to right size
            System.Drawing.Bitmap targetImage = contentBitmap;
            if (contentBitmap.Width != targetTextureWidth || contentBitmap.Height != targetTextureWidth)
            {
                //  Resize the image.
                targetImage = (System.Drawing.Bitmap)contentBitmap.GetThumbnailImage(
                    targetTextureWidth, targetTextureWidth, null, IntPtr.Zero);
            }

            // step 6: generate texture
            {
                //  Lock the image bits (so that we can pass them to OGL).
                BitmapData bitmapData = targetImage.LockBits(
                    new Rectangle(0, 0, targetImage.Width, targetImage.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                //gl.ActiveTexture(gl.GL_TEXTURE0);
                gl.GenTextures(1, texture);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, texture[0]);
                gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA,
                    targetImage.Width, targetImage.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE,
                    bitmapData.Scan0);
                //  Unlock the image.
                targetImage.UnlockBits(bitmapData);
                /* We require 1 byte alignment when uploading texture data */
                //gl.PixelStorei(OpenGL.GL_UNPACK_ALIGNMENT, 1);
                /* Clamping to edges is important to prevent artifacts when scaling */
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, (int)OpenGL.GL_CLAMP_TO_EDGE);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, (int)OpenGL.GL_CLAMP_TO_EDGE);
                /* Linear filtering usually looks best for text */
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, (int)OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, (int)OpenGL.GL_LINEAR);
            }

            // step 7: release images
            {
                //targetImage.Save("PointSpriteStringElement-TargetImage.png");
                if (targetImage != contentBitmap)
                {
                    targetImage.Dispose();
                }

                contentBitmap.Dispose();
            }
        }

    }
}
