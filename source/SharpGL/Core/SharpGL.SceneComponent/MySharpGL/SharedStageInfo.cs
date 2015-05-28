﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// This type's instance is passed to every <see cref="IHasModernObjectSpace"/> so that sceneElements can get useful information
    /// <para>like its rendering order, total VBOs' vertices count, etc.</para>
    /// </summary>
    public class SharedStageInfo
    {
        /// <summary>
        /// Specifies how many vertices have been rendered during hit test.
        /// </summary>
        public int pickingBaseID;

        /// <summary>
        /// Reset this instance's fields' values to initial state so that it can be used again during rendering.
        /// </summary>
        public virtual void Reset()
        {
            pickingBaseID = 0;
        }
    }
}