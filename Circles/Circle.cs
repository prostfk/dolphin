using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles
{
    public class Circle
    {
        private int x;
        private int y;
        private int diameter;
        private int windowWidth;
        private int windowHeight;

        public Circle(
            int windowWidth,
            int windowHeight,
            int diameter = 50)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.diameter = diameter;
        }

        public int X
        {
            set
            {
                x = value + windowWidth / 2;
            }
            
            get { return x; }
        }

        public int Y
        {
            set
            {
                y = value + windowHeight / 2;
            }

            get { return y; }
        }

        public void Draw(Graphics gr, Color color)
        {
            gr.DrawEllipse(new Pen(color,5f),
                x - diameter / 2,
                y - diameter / 2,
                this.diameter,
                this.diameter);
        }
    }
}
