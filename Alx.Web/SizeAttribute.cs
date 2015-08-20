
using System;
using System.Drawing;

namespace Alx.Web
{
    public class SizeAttribute : Attribute
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size Size
        {
            get { return new Size(Width, Height); }
        }

        public SizeAttribute(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
