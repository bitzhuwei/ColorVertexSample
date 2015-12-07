using SharpGL.SceneGraph;
using SimLab.GridSource.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSource
{
    /// <summary>
    /// 块为六面体组成的模拟网格几何信息,支持切片分析
    /// </summary>
    public class PointGridderSource : GridderSource
    {

        protected override Factory.GridBufferDataFactory CreateFactory()
        {
            return new PointGridFactory();
        }

        /// <summary>
        /// 初始化切片可视性
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

    }
}
