using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLab2
{
    public class Disposable:IDisposable
    {
        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if (disposing)
                {
                    this.DisposeManagedResources();
                }
                // Call the appropriate methods to clean up 
                // unmanaged resources here. 
                // If disposing is false, 
                // only the following code is executed.

                this.DisposeUnmanagedResources();
                // Note disposing has been done.
                disposed = true;
            }
        }

        protected virtual void DisposeUnmanagedResources(){

        }

        protected virtual void DisposeManagedResources()
        {
        }





        public void Dispose()
        {
             Dispose(true);
             GC.SuppressFinalize(this);
        }

        ~Disposable()
        {
            // Do not re-create Dispose clean-up code here. 
            // Calling Dispose(false) is optimal in terms of 
            // readability and maintainability.
            Dispose(false);
        }

    }
}
