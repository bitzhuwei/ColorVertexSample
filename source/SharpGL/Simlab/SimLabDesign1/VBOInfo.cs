using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public class VBOInfo
    {
        public uint ID { get; set; }
        public uint Target { get; set; }
        public uint Usage { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}, Target: {1}, Usage: {2}", ID, Target, Usage);
            //return base.ToString();
        }
    }
}
