using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections;
using System.Text;
using System.Diagnostics;

namespace SavingPpm
{
    enum Colors : uint
    {
        RED = 0xff0000ff,
        BLUE = 0xffff0000,
        GREEN = 0xffff0000,
        SALMON = 0xff7280FA,
        TEAL = 0xff808000,
        YELLOW = 0xff00FFFF,
        BLACK = 0xff000000,
        WHITE = 0xffffffff,
        OLIVE = 0xff008080,
        NAVY = 0xff800000,
        PURPLE = 0xff800080,
        GOLD = 0xff00BFFF,
        SKYBLUE = 0xffFAE4D8,
        LIGHTGREEN = 0xff9FE2BF,
        FINE_RED = 0xff3440eb,
        FINE_BLUE = 0xffbe9625,
    }
    class Pixent {
        private int width { get; set; } 
        private int height { get; set; }
        private uint[,] pixels;
        
        public Pixent(int width,int height) {
            this.width = width;
            this.height = height;
            pixels = new uint[height, width];
        }
        //public void SaveIntoPpm(int width, int height) {
        //    using (FileStream fs = new FileStream("test.ppm", FileMode.Create)) {
        //        using (StreamWriter br = new StreamWriter(fs)) {
        //            br.Write("P3\n");
        //            br.Write($"{width} {height}\n");
        //            br.Write("255\n");
        //            for (int i = 0; i < height; i++) {
        //                for (int j = 0; j < width; j++) {
        //                    br.Write(((pixels[i, j] >> 0) & 0xff) + " ");
        //                    br.Write(((pixels[i, j] >> 8) & 0xff) + " ");
        //                    br.Write(((pixels[i, j] >> 16) & 0xff) + " ");
        //                }
        //            }
        //        }
        //    }
        //}
        public void FillBackGround(Colors color) {
            for (int i = 0; i < pixels.GetLength(0); i++) {
                for (int j = 0; j < pixels.GetLength(1); j++) {
                    pixels[i, j] = blend_colors(pixels[i, j], (uint)color);
                }
            }
        }
        public void FillBackGround(uint color) {
            for (int i = 0; i < pixels.GetLength(0); i++) {
                for (int j = 0; j < pixels.GetLength(1); j++) {
                    pixels[i, j] = blend_colors(pixels[i, j], color);
                }
            }
        }
        public void SaveAsPng(string outputpath) {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);
            var image = new Image<Rgba32>(pixels.GetLength(1), pixels.GetLength(0));
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    var temp = BitConverter.GetBytes(pixels[i, j]);
                    image[j, i] = new Rgba32(temp[0], temp[1], temp[2], temp[3]);
                }
            }
            image.Save(outputpath);
        }
        private byte mix_coms(byte c1, byte c2, byte alpha) {
            return (byte)(c1 + (c2 - c1) * alpha / 255);
        }
        private uint blend_colors(uint c1, uint c2) {
            byte[] comp1 = BitConverter.GetBytes(c1);
            byte[] comp2 = BitConverter.GetBytes(c2);
            for (int i = 0; i < 4; i++) {
                comp1[i] = mix_coms(comp1[i], comp2[i], comp2[3]);
            }
            return BitConverter.ToUInt32(comp1);
        }
        private void swap_int(ref int x, ref int y) {
            int temp = x;
            x = y;
            y = temp;
        }
        public void FillCircle(int centerX, int centerY, int r, Colors color) {
            int sx = centerX - r;
            int sy = centerY - r;
            int ex = centerX + r;
            int ey = centerY + r;
            for (int i = sy; i < ey; i++) {
                for (int j = sx; j < ex; j++) {
                    if (Math.Pow((centerX - j), 2) + Math.Pow((centerY - i), 2) < r * r) pixels[i, j] = blend_colors(pixels[i, j], (uint)color);
                    else continue;
                }
            }
        }
        public void FillCircle(int centerX, int centerY, int r, uint color) {
            int sx = centerX - r;
            int sy = centerY - r;
            int ex = centerX + r;
            int ey = centerY + r;
            for (int i = sy; i < ey; i++) {
                for (int j = sx; j < ex; j++) {
                    if (Math.Pow((centerX - j), 2) + Math.Pow((centerY - i), 2) < r * r) pixels[i, j] = blend_colors(pixels[i, j], color);
                    else continue;
                }
            }
        }
        public void FillRect( int x, int y, int width, int height, Colors color) {
            for (int i = y; i < height + y; i++) {
                for (int j = x; j < width + x; j++) {
                    pixels[i, j] = blend_colors(pixels[i, j], (uint)color);
                }
            }
        }
        public void FillRect(int x, int y, int width, int height, uint color) {
            for (int i = y; i < height + y; i++) {
                for (int j = x; j < width + x; j++) {
                    pixels[i, j] = blend_colors(pixels[i, j], color);
                }
            }
        }
        public void FillPixel(int x, int y, Colors color) {
            pixels[y, x] = blend_colors(pixels[y, x], (uint)color);
        }
        public void FillPixel(int x, int y, uint color) {
            pixels[y, x] = blend_colors(pixels[y, x], color);
        }
        public void DrawLine(int x1,int y1, int x2,int y2, Colors color) {
            int dx = x2 - x1;
            int dy = y2 - y1;
            if (dx != 0) {
                int c = y1 - dy*x1 / dx ;
                if (x1 > x2) swap_int(ref x1,ref x2);
                for (int x=x1;x<=x2;x++) {
                    if (0 <= x && x < width) {
                        int sy2 = dy *( x + 1) / dx + c;
                        int sy1 = dy*x / dx  + c;
                        if (sy1 > sy2) swap_int(ref sy1, ref sy2);
                        for(int y = sy1; y <= sy2; ++y) {
                            if (0 <= y && y < height) {
                            pixels[y, x] = blend_colors(pixels[y, x], (uint)color);
                            }
                            
                        }
                            
                    }
                    
                }
            }
            else {
                int x = x1;
                if(0<=x && x < width) {
                    if (y1 > y2) swap_int(ref y1,ref y2);
                    for(int y = y1; y <= y2; ++y) {
                        if (0 <= y && y < height) {
                            pixels[y, x] = 0xff000000;
                        }
                    }
                }
            }
        }
        public void DrawLine(int x1, int y1, int x2, int y2, uint color) {
            int dx = x2 - x1;
            int dy = y2 - y1;
            if (dx != 0) {
                int c = y1 - dy * x1 / dx;
                if (x1 > x2) swap_int(ref x1, ref x2);
                for (int x = x1; x <= x2; x++) {
                    if (0 <= x && x < width) {
                        int sy2 = dy * (x + 1) / dx + c;
                        int sy1 = dy * x / dx + c;
                        if (sy1 > sy2) swap_int(ref sy1, ref sy2);
                        for (int y = sy1; y <= sy2; ++y) {
                            if (0 <= y && y < height) {
                                pixels[y, x] = blend_colors(pixels[y, x], color);
                            }

                        }

                    }

                }
            }
            else {
                int x = x1;
                if (0 <= x && x < width) {
                    if (y1 > y2) swap_int(ref y1, ref y2);
                    for (int y = y1; y <= y2; ++y) {
                        if (0 <= y && y < height) {
                            pixels[y, x] = 0xff000000;
                        }
                    }
                }
            }
        }
    }
}
