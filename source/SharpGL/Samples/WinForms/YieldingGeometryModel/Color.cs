using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldingGeometryModel
{
    struct Color
    {
        float r;  //red
        float g;  //green
        float b;  //blue
        float a;  //alpha

        public float Red
        {
            get
            {
                return r;
            }
        }

        public float Green
        {
            get
            {
                return g;
            }
        }
        public float A
        {
            get
            {
                return a;
            }
        }

        public static explicit operator Color(GLColor color)
        {
            Color c;
            c.r = color.R;
            c.g = color.G;
            c.b = color.B;
            c.a = color.A;
            return c;
        }


    }
}
