using SharpGL;
using SimLabDesign1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1_DataSources
{
    /// <summary>
    /// 描述六面体的数据源。
    /// </summary>
    public class HexahedronSource : DataSourceBase
    {
        /// <summary>
        /// 根据语法要求必须有此构造函数。
        /// </summary>
        /// <param name="renderableElement"></param>
        public HexahedronSource(IVertexBuffers renderableElement)
            : base(renderableElement)
        {

        }

        // TODO：下面是具体的Source内容。

    }
}
