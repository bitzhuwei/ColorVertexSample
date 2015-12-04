using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab.GridSource.Factory
{
    public abstract class BufferDataFactory
    {

        public abstract BufferData Create(GridderSource source);
      
    }
}
