using SimLab.GridSource.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.SimGrid.Factory
{
    public class DynamicUnstructureGridFactory : GridBufferDataFactory
    {


        public override MeshBase CreateMesh(GridSource.GridderSource source)
        {
            throw new NotImplementedException();
        }

        public override TextureCoordinatesBufferData CreateTextureCoordinates(GridSource.GridderSource source, int[] gridIndexes, float[] values, float minValue, float maxValue)
        {
            throw new NotImplementedException();
        }

        public override WireFrameBufferData CreateWireFrame(GridSource.GridderSource source)
        {
            throw new NotImplementedException();
        }
    }
}
