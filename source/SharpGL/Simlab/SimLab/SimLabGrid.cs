using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab
{


    /// <summary>
    /// 3D Visual Object
    /// </summary>
    public abstract class SimLabGrid : SceneElement
    {

        public bool RenderGridWireFrame { get; set; }
        public bool RenderGrid  { get; set; }

        /// <summary>
        /// 初始化点的vao
        /// </summary>
        /// <param name="gridCoords"></param>
        public void Init(MeshGeometry3D Geomtry)
        {
              //this.TraverseToRootElement().ParentScene.OpenGL;
        }


        public void SetTextureCoods(BufferData textureCoords)
        {
            
        }

        public void SetTexutre(Bitmap bitmap)
        {

        }
    }






   



}
