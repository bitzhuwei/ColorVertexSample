using SharpGL.SceneComponent;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    public partial class PointSpriteFontElement
    {

        private void InitTexture(SharpGL.OpenGL openGL)
        {
            Bitmap bmp = ManifestResourceLoader.LoadBitmap("FontResources.LucidaTypewriterRegular.ttf.png");
            this.texture = new Texture();
            this.texture.Create(openGL, bmp);
            bmp.Dispose();

        }

    }
}
