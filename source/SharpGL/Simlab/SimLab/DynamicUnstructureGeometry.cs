using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2
{
    public class DynamicUnstructureGeometry:MeshBase
    {

        private MatrixIndicesBufferData matrixIndices;
        private FracturePositionBufferData fracturePositions;

        /// <summary>
        /// 基质的模型
        /// </summary>
        /// <param name="matrixPositions"></param>
        public DynamicUnstructureGeometry(PositionsBufferData matrixPositions)
            : base(matrixPositions)
        {

        }


        /// <summary>
        /// 基质基质的索引
        /// </summary>
        public MatrixIndicesBufferData MatrixIndices
        {
            get { return this.matrixIndices; }
            set { this.matrixIndices = value; }
        }


        /// <summary>
        /// 基质的位置信息描述
        /// </summary>
        private new MatrixPositionBufferData Positions
        {
            get
            {
                return (MatrixPositionBufferData)base.Positions;
            } 
        }

        public MatrixPositionBufferData MatrixPositions
        {
            get
            {
                return (MatrixPositionBufferData)base.Positions;
            }
        }


        public FracturePositionBufferData FracturePositions
        {
            get
            {
                return this.fracturePositions;
            }
            internal set
            {
                this.fracturePositions = value;
            }

        }

    }
}
