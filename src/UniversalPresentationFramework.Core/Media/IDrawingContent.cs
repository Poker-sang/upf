﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.UI.Media
{
    public interface IDrawingContent
    {
        bool HitTest(in Point point);
    }
}
