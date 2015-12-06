using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLabDesign1
{
    public abstract class VBOInfoBase
    {
        /// <summary>
        /// buffers[0] in GenBuffers(int n, uint[] buffers)
        /// <para>buffer in BindBuffer(uint target, uint buffer)</para>
        /// </summary>
        public uint BufferID { get; set; }

        /// <summary>
        /// target in BufferData(uint target, int size, IntPtr data, uint usage)
        /// </summary>
        public uint Target { get; set; }

        /// <summary>
        /// usage in BufferData(uint target, int size, IntPtr data, uint usage)
        /// </summary>
        public uint Usage { get; set; }


        public override string ToString()
        {
            return string.Format("BufferID: {0}, Target: {1}, Usage: {2}",
                BufferID, Target, Usage);
            //return base.ToString();
        }
    }
}
