using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingPpm
{
    struct Point
    {
        public int x;
        public int y;
        public int z;

        public Point(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    class Example {
  
        static void Main() {

            int width = 1920;
            int height= 1080;
            Pixent c = new Pixent(width, height);
            c.FillBackGround(Colors.LIGHTGREEN);
            c.DrawLine(200,300,100,100, Colors.RED);
            c.FillCircle(50,50,50,Colors.GOLD);
            c.FillCircle(150,50,50,0xff181818);
            c.FillRect(500,400,500,100,Colors.FINE_RED);
            c.FillRect(500,600,500,100, Colors.GOLD);
            c.FillRect(500,500,500,100,Colors.FINE_BLUE);
            c.FillCircle(750,530,50,Colors.OLIVE);
            c.SaveAsPng("result.png");


        }
    }    
}
