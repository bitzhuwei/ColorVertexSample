using SharpGL;
using SharpGL.SceneGraph.Assets;
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

        uint[] positionBuffer;
        uint[] colorBuffer;
        uint[] indexBuffer;
        uint[] wireframeIndexBuffer;

        /// <summary>
        /// 初始化顶点位置和索引
        /// </summary>
        /// <param name="gridCoords"></param>
        public void Init(MeshGeometry3D Geomtry)
        {
            //TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;
            positionBuffer=new uint[1];
            gl.GenBuffers(1, positionBuffer);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, positionBuffer[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, Geomtry.Positions.GLSize, Geomtry.Positions.Data, OpenGL.GL_STATIC_DRAW);
            
            indexBuffer = new uint[1];
            gl.GenBuffers(1, indexBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, Geomtry.TriangleIndices.GLSize, Geomtry.TriangleIndices.Data, OpenGL.GL_STATIC_DRAW);
        }


        public void SetTextureCoods(BufferData textureCoords)
        {
            //TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;
            positionBuffer = new uint[1];
            gl.GenBuffers(1, positionBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, positionBuffer[0]);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, textureCoords.GLSize, textureCoords.Data, OpenGL.GL_STATIC_DRAW);
        }

        public void SetTexutre(Bitmap bitmap)
        {
            //TODO:如果用此方式，则必须先将此对象加入scene树，然后再调用Init
            OpenGL gl = this.TraverseToRootElement().ParentScene.OpenGL;

            Texture texture = new Texture();
            texture.Create(gl, bitmap);
        }
    }






   



}
