using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{

    /// <summary>
    /// 正交网格数据源
    /// </summary>
    public class DemoPointSpriteGridderSource : PointSpriteGridderSource
    {

        public override Vertex GetPosition(int i, int j, int k)
        {
            float x = i * 1;// this.DX;
            float y = j * 1;// this.DY;
            float z = k * 1;// this.DZ;
            Vertex p = new Vertex(x, y, z);
            return p;
        }

        public override float GetRadius(int i, int j, int k)
        {
            return (float)(random.NextDouble() * 10);
        }

        static Random random = new Random();
    }
}
