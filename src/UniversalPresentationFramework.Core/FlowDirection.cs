﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.UI
{
    public enum FlowDirection
    {
        /// <internalonly>
        /// Sets the primary text advance direction to left-to-right, and the line
        /// progression direction to top-to-bottom as is common in most Roman-based
        /// documents. For most characters, the current text position is advanced
        /// from left to right after each glyph is rendered. The 'direction' property
        /// is set to 'ltr'.
        /// </internalonly>
        LeftToRight,

        /// <internalonly>
        /// Sets the primary text advance direction to right-to-left, and the line
        /// progression direction to top-to-bottom as is common in Arabic or Hebrew
        /// scripts. The direction property is set to 'rtl'.
        /// </internalonly>
        RightToLeft,
    }
}
