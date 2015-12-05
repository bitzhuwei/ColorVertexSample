using SimLabDesign1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1_Elements
{
    /// <summary>
    /// 可渲染多个六面体的场景元素。
    /// </summary>
    public class HexahedronElement : IVertexBuffers
    {
        Dictionary<string, uint> vboDict = new Dictionary<string, uint>();


        void IVertexBuffers.SetupVertexBuffer(string key)
        {
            throw new NotImplementedException();
        }

        void IVertexBuffers.SetupVertexBuffer<T>(string key, SharpGL.SceneComponent.UnmanagedArray<T> newValue, int startIndex)
        {
            throw new NotImplementedException();
        }

        void IVertexBuffers.UpdateVertexBuffer<T>(string key, SharpGL.SceneComponent.UnmanagedArray<T> newValue, int startIndex)
        {
            throw new NotImplementedException();
        }

        void IVertexBuffers.DeleteVertexBuffer(string key)
        {
            throw new NotImplementedException();
        }
    }
}
