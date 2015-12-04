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

      
       
        public abstract void SetVisibleBuffer(BufferData visibles);

        public abstract void SetTextureCoods(BufferData visibles);

        public abstract void SetTexutre(Bitmap bitmap);
    }






   



}
