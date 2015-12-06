using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public class GridBlockHelper
    {

        public static IList<int> CreateBlockCoords(int n)
        {
            IList<int> blockCoords = new List<int>(n);
            for(int i=1; i<=n; i++){
                blockCoords.Add(i);
            }
            return blockCoords;
        }

    }
}
