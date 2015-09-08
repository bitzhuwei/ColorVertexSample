using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel.DataSource
{
    public class dfmPointSpriteGridderSource : PointSpriteGridderSource, IDisposable
    {
        StreamReader reader;
        public dfmPointSpriteGridderSource(float maxRadius = 1000)
        {
            this.maxRadius = maxRadius;
            this.reader = new StreamReader("dfm_cvxyz.in");
        }

        static char[] separator = new char[] { ' ', '\t', '\n' };
        public override SharpGL.SceneGraph.Vertex GetPosition(int i, int j, int k)
        {
            if (!this.reader.EndOfStream)
            {
                var line = this.reader.ReadLine();
                var parts = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var x = float.Parse(parts[0]);
                var y = float.Parse(parts[1]);
                var z = float.Parse(parts[2]);
                return new SharpGL.SceneGraph.Vertex(x, y, z);
            }
            else
            {
                return new SharpGL.SceneGraph.Vertex();
            }
        }

        public override float GetRadius(int i, int j, int k)
        {
            return (float)(random.NextDouble() * maxRadius);
        }

        static Random random = new Random();


        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        } // end sub

        /// <summary>
        /// Destruct instance of the class.
        /// </summary>
        ~dfmPointSpriteGridderSource()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Backing field to track whether Dispose has been called.
        /// </summary>
        private bool disposedValue = false;
        private float maxRadius;

        /// <summary>
        /// Dispose managed and unmanaged resources of this instance.
        /// </summary>
        /// <param name="disposing">If disposing equals true, managed and unmanaged resources can be disposed. If disposing equals false, only unmanaged resources can be disposed. </param>
        protected virtual void Dispose(bool disposing)
        {

            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // TODO: Dispose managed resources.
                    this.reader.Dispose();
                } // end if

                // TODO: Dispose unmanaged resources.
            } // end if

            this.disposedValue = true;
        } // end sub

        #endregion
    }
}
