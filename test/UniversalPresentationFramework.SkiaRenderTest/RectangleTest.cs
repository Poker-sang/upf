﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.UI.Media;
using UPF = Wodsoft.UI.Shapes;

namespace Wodsoft.UI.Test
{
    public class RectangleTest : RenderTest
    {
        [Fact]
        public void Draw()
        {
            var rect = new UPF.Rectangle();
            rect.Width = 200;
            rect.Height = 100;
            rect.Fill = new SolidColorBrush(Colors.Blue);
            rect.Arrange(new Rect(0,0, 200, 100));
            RenderToBitmap(rect);
        }
    }
}
