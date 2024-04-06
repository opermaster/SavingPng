using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingPpm
{
    class Example {
  
        static void Main() {

            int width = 1920;
            int height= 1080;
            Pixent c = new Pixent(width, height);
            c.FillBackGround(Colors.LIGHTGREEN);
            c.DrawLine(200,300,100,100, Colors.RED);
            c.FillCircle(50,50,50,Colors.GOLD);
            c.FillCircle(150,50,50,0xff181818);
            c.FillCircle(0, 0, 50,Colors.TEAL);
            c.FillRect(500, 400, 500, 100, Colors.FINE_RED);
            c.FillRect(500, 600, 500, 100, Colors.GOLD);
            c.FillRect(500, 500, 500, 100, Colors.FINE_BLUE);

            c.FillCircle(1920, 1080, 60, Colors.OLIVE);
            c.FillRect(1900, 10, 50,50, Colors.OLIVE);
            c.SaveAsPng("result.png");


        }
    }    
}
