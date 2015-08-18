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
    public partial class PointSpriteFontElement
    {
        /// <summary>
        /// TODO: best practice value.
        /// </summary>
        private static int maxPointSize = 255;
        private int textureWidth;

        private void InitTexture(SharpGL.OpenGL openGL)
        {
            // test the font rendering procedure.
            //Bitmap bmp = ManifestResourceLoader.LoadBitmap("FontResources.LucidaTypewriterRegular.ttf.png");
            //this.texture = new Texture();
            //this.texture.Create(openGL, bmp);
            //bmp.Dispose();

            // this is not work.
            //int [] pointSize=new int[2];
            //openGL.GetInteger(SharpGL.Enumerations.GetTarget.PointSizeRange, pointSize);
            //maxPointSize = pointSize[1];

            //openGL.PointParameter(OpenGL.GL_POINT_SIZE_MAX_ARB, 255);

            //openGL.GetInteger(SharpGL.Enumerations.GetTarget.PointSizeRange, pointSize);
            //maxPointSize = pointSize[1];


            InitTexture(openGL, this.text);
        }

        private void InitTexture(SharpGL.OpenGL openGL, string content)
        {
            // step 1: get totalWidth
            int glyphsLength = 0;
            {
                for (int i = 0; i < content.Length; i++)
                {
                    char c = content[i];
                    CharacterInfo cInfo;
                    if (FontResource.Instance.CharInfoDict.TryGetValue(c, out cInfo))
                    {
                        int glyphWidth = cInfo.width;
                        glyphsLength += glyphWidth;
                    }
                    //else
                    //{ throw new Exception(string.Format("Not support for display the char [{0}]", c)); }
                }

                //glyphsLength = (glyphsLength * this.fontSize / FontResource.Instance.FontHeight);
            }

            // step 2: setup contentBitmap
            Bitmap contentBitmap = null;
            {
                int interval = FontResource.Instance.FontHeight / 5; if (interval < 1) { interval = 1; }
                int totalLength = glyphsLength + interval * (content.Length - 1);
                int currentTextureWidth = 0;
                int currentWidthPosition = 0;
                int currentHeightPosition = 0;
                if (totalLength * this.fontSize / FontResource.Instance.FontHeight > maxPointSize)// 超过1行能显示的内容
                {
                    currentTextureWidth = maxPointSize * FontResource.Instance.FontHeight / this.fontSize;

                    int lineCount = (glyphsLength - 1) / currentTextureWidth + 1;
                    // 确保整篇文字的高度在贴图中间。
                    currentHeightPosition = (currentTextureWidth - FontResource.Instance.FontHeight * lineCount) / 2
                        - FontResource.Instance.FontHeight / 2;
                }
                else//只在一行内即可显示所有字符
                {
                    currentTextureWidth = totalLength;

                    if (totalLength >= FontResource.Instance.FontHeight)
                    {
                        // 确保整篇文字的高度在贴图中间。
                        currentHeightPosition = (currentTextureWidth - FontResource.Instance.FontHeight) / 2;
                        //- FontResource.Instance.FontHeight / 2;
                    }
                    else
                    {
                        currentWidthPosition = (currentTextureWidth - glyphsLength) / 2;
                        glyphsLength = FontResource.Instance.FontHeight;
                    }
                }

                //this.textureWidth = textureWidth * this.fontSize / FontResource.Instance.FontHeight;
                //currentWidthPosition = currentWidthPosition * this.fontSize / FontResource.Instance.FontHeight;
                //currentHeightPosition = currentHeightPosition * this.fontSize / FontResource.Instance.FontHeight;

                contentBitmap = new Bitmap(currentTextureWidth, currentTextureWidth);
                Graphics gContentBitmap = Graphics.FromImage(contentBitmap);
                Bitmap bigBitmap = FontResource.Instance.FontBitmap;
                for (int i = 0; i < content.Length; i++)
                {
                    char c = content[i];
                    CharacterInfo cInfo;
                    if (FontResource.Instance.CharInfoDict.TryGetValue(c, out cInfo))
                    {
                        //for (int col = 0; col < cInfo.width; col++)
                        //{
                        //    for (int row = 0; row < FontResource.Instance.FontHeight; row++)
                        //    {
                        //        var color = bigBitmap.GetPixel(cInfo.xoffset + col, cInfo.yoffset + row);
                        //        contentBitmap.SetPixel(currentWidthPosition + col, currentHeightPosition + row, color);
                        //    }
                        //}

                        gContentBitmap.DrawImage(bigBitmap,
                            new Rectangle(currentWidthPosition, currentHeightPosition, cInfo.width, FontResource.Instance.FontHeight),
                            new Rectangle(cInfo.xoffset, cInfo.yoffset, cInfo.width, FontResource.Instance.FontHeight),
                            GraphicsUnit.Pixel);

                        currentWidthPosition += cInfo.width + interval;
                        if (currentWidthPosition >= contentBitmap.Width)
                        {
                            currentWidthPosition = 0;
                            currentHeightPosition += FontResource.Instance.FontHeight;
                        }
                    }
                }
                gContentBitmap.Dispose();
            }

            // step 4: get texture's size 
            int targetTextureWidth;
            {

                //	Get the maximum texture size supported by OpenGL.
                int[] textureMaxSize = { 0 };
                openGL.GetInteger(OpenGL.GL_MAX_TEXTURE_SIZE, textureMaxSize);

                //	Find the target width and height sizes, which is just the highest
                //	posible power of two that'll fit into the image.

                targetTextureWidth = textureMaxSize[0];
                //System.Drawing.Bitmap bitmap = contentBitmap;
                int scaledWidth = 8 * contentBitmap.Width * this.fontSize / FontResource.Instance.FontHeight;

                for (int size = 1; size <= textureMaxSize[0]; size *= 2)
                {
                    if (scaledWidth < size)
                    {
                        targetTextureWidth = size / 2;
                        break;
                    }
                    if (scaledWidth == size)
                        targetTextureWidth = size;
                }

                this.textureWidth = targetTextureWidth;
            }

            // step 5: scale contentBitmap to right size
            System.Drawing.Bitmap targetImage = contentBitmap;
            if (contentBitmap.Width != targetTextureWidth || contentBitmap.Height != targetTextureWidth)
            {
                //  Resize the image.
                targetImage = (System.Drawing.Bitmap)contentBitmap.GetThumbnailImage(targetTextureWidth, targetTextureWidth, null, IntPtr.Zero);
            }

            // step 6: generate texture
            {
                //  Lock the image bits (so that we can pass them to OGL).
                BitmapData bitmapData = targetImage.LockBits(new Rectangle(0, 0, targetImage.Width, targetImage.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                //GL.ActiveTexture(GL.GL_TEXTURE0);
                openGL.GenTextures(1, texture);
                openGL.BindTexture(OpenGL.GL_TEXTURE_2D, texture[0]);
                openGL.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA,
                    targetImage.Width, targetImage.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE,
                    bitmapData.Scan0);
                //  Unlock the image.
                targetImage.UnlockBits(bitmapData);
                /* We require 1 byte alignment when uploading texture data */
                //GL.PixelStorei(GL.GL_UNPACK_ALIGNMENT, 1);
                /* Clamping to edges is important to prevent artifacts when scaling */
                openGL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, (int)OpenGL.GL_CLAMP_TO_EDGE);
                openGL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, (int)OpenGL.GL_CLAMP_TO_EDGE);
                /* Linear filtering usually looks best for text */
                openGL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, (int)OpenGL.GL_LINEAR);
                openGL.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, (int)OpenGL.GL_LINEAR);
            }

            // step 7: release images
            {
                targetImage.Save("PointSpriteFontElement-TargetImage.png");
                if (targetImage != contentBitmap)
                {
                    targetImage.Dispose();
                }

                contentBitmap.Dispose();
            }
        }

    }
}
