using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ArrayHelper
    {
        /// <summary>
        /// Print array's elements in format 'element, element, element, ...'
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string PrintArray(this Array array)
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
