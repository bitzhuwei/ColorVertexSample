using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample2
{
    public class GridProperty
    {

        public GridProperty()
        {
        }

        private string name = String.Empty;

        int[] gridIndexes;
        float[] values;

        float minValue;
        float maxValue;


        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int[] GridIndexes
        {
            get { return this.gridIndexes; }
            set { this.gridIndexes = value; } 
        }

        public float[] Values
        {
            get { return this.values; }
            set { this.values = value; }
        }

        public float MinValue
        {
            get { return this.minValue; }
            set { this.minValue = value; }
        }

        public float MaxValue
        {
            get { return this.maxValue; }
            set { this.maxValue = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
