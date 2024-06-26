﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.UI.Input
{
    /// <summary>
    ///     An enumeration of the various capture policies.
    /// </summary>
    public enum CaptureMode
    {
        /// <summary>
        ///     No Capture
        /// </summary>
        None,

        /// <summary>
        ///     Capture is constrained to a single element.
        /// </summary>
        Element,

        /// <summary>
        ///     Capture is constrained to the entire subtree of an element.
        /// </summary>
        SubTree
    }
}
