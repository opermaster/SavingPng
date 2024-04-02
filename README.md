# Library for making png images from byte array by using [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp).
## Methods

1. FillBackGround(Colors color) / FillBackGround(uint color) - filling background with selected color
2. FillCircle(int centerX, int centerY, int r, Colors color) / FillCircle(int centerX, int centerY, int r,uint color) - painting a circle and filling it with selectedcolor 
3. FillRect( int x, int y, int width, int height, Colors color) / FillRect( int x, int y, int width, int height,uint color) - painting a rectangle and filling it with selectedcolor 
4. FillPixel(int x, int y, Colors color) / FillPixel(int x, int y, uint color) - filling one pixel by color
5. DrawLine(int x1,int y1, int x2,int y2, Colors color) / DrawLine(int x1,int y1, int x2,int y2, uint color) - drawing a line with a selected color
6. SaveAsPng(string outputpath) - saving current bytes as image in png format 

## Usage
Firstly create an instance of Pixent class with width and height you want.
```
Pixent canvas=new Pixent(800,800);
```
And do what you want ...
And then save 
```
canvas.SaveAsPng("./output.png");
```
