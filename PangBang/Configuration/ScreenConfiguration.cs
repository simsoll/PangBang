using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PangBang.Configuration
{
    public class ScreenConfiguration : IScreenConfiguration
    {
        public ScreenConfiguration(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}
