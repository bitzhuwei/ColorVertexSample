using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Helper class for array.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Print elements in format 'element, element, element, ...'
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string PrintArray(this System.Collections.IEnumerable array)
        {
            if (array == null) { return string.Empty; }

            StringBuilder builder = new StringBuilder();
            foreach (var item in array)
            {
                builder.Append(item);
                builder.Append(", ");
            }

            return builder.ToString();
        }

    }
}
