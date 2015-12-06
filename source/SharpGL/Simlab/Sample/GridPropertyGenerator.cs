using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public  class GridPropertyGenerator
    {


        
        

        public static GridProperty CreateRandomProperty(int dimsize, string name, float minValue , float maxValue)
        {

            GridProperty prop = new GridProperty();
            int[] gridIndexes = new int[dimsize];
            float[] values = new float[dimsize];
            Random random = new Random((int)(DateTime.Now.Ticks/1000));
            for(int i=0; i<dimsize; i++){
                   gridIndexes[i] = i;
                   double norm = random.NextDouble();
                   values[i] = (float)(minValue + (maxValue - minValue) * norm);
            }
            prop.Name = name;
            prop.MinValue = minValue;
            prop.MaxValue = maxValue;
            prop.Values = values;
            prop.GridIndexes = gridIndexes;
            return prop;
        }

        public static GridProperty CreateGridIndexProperty(int dimsize, string name)
        {
            GridProperty prop = new GridProperty();
            int[] gridIndexes = new int[dimsize];
            float[] values = new float[dimsize];

            for (int i = 0; i < dimsize; i++)
            {
                gridIndexes[i] = i;
                values[i] = i+1;
            }

            prop.Name = name;
            prop.Values = values;
            prop.GridIndexes = gridIndexes;
            prop.MinValue = 1;
            prop.MaxValue = dimsize;
            return prop;
        }

    }
}
