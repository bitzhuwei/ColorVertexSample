using GlmNet;
using SharpGL.SceneComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnmanagedArrayTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInSubRoutine_Click(object sender, EventArgs e)
        {
            int count = 1000000;
            // 测试vec3类型
            {
                var vec3Array = new UnmanagedArray<vec3>(count);
                long startTick = DateTime.Now.Ticks;
                for (int i = 0; i < count; i++)
                {
                    vec3Array[i] = new vec3(i * 3 + 0, i * 3 + 1, i * 3 + 2);
                }
                for (int i = 0; i < count; i++)
                {
                    var item = vec3Array[i];
                    var old = new vec3(i * 3 + 0, i * 3 + 1, i * 3 + 2);
                    if (item.x != old.x || item.y != old.y || item.z != old.z)
                    { throw new Exception(); }
                }
                long interval = DateTime.Now.Ticks - startTick;

                unsafe
                {
                    startTick = DateTime.Now.Ticks;
                    vec3* header = (vec3*)vec3Array.FirstElement();
                    vec3* last = (vec3*)vec3Array.LastElement();
                    vec3* tailAddress = (vec3*)vec3Array.TailAddress();
                    int i = 0;
                    for (vec3* ptr = header; ptr <= last/*or: ptr < tailAddress*/; ptr++)
                    {
                        *ptr = new vec3(i * 3 + 0, i * 3 + 1, i * 3 + 2);
                        i++;
                    }
                    i = 0;
                    for (vec3* ptr = header; ptr <= last/*or: ptr < tailAddress*/; ptr++, i++)
                    {
                        var item = *ptr;
                        var old = new vec3(i * 3 + 0, i * 3 + 1, i * 3 + 2);
                        if (item.x != old.x || item.y != old.y || item.z != old.z)
                        { throw new Exception(); }
                    }
                }
                long interval2 = DateTime.Now.Ticks - startTick;

                Console.WriteLine();
                MessageBox.Show(string.Format("Ticks: safe: {0} vs unsafe: {1}", interval, interval2), "result");

            }
        }
    }
}
