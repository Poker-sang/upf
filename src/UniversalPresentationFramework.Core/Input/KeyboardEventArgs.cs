﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.UI.Input
{
    /// <summary>
    ///     The KeyboardEventArgs class provides access to the logical
    ///     pointer device for all derived event args.
    /// </summary>
    /// <ExternalAPI/> 
    public class KeyboardEventArgs : InputEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the KeyboardEventArgs class.
        /// </summary>
        /// <param name="keyboard">
        ///     The logical keyboard device associated with this event.
        /// </param>
        /// <param name="timestamp">
        ///     The time when the input occurred.
        /// </param>
        public KeyboardEventArgs(KeyboardDevice keyboard, int timestamp) : base(keyboard, timestamp)
        {
        }

        /// <summary>
        ///     Read-only access to the logical keyboard device associated with
        ///     this event.
        /// </summary>
        public KeyboardDevice KeyboardDevice
        {
            get { return (KeyboardDevice)this.Device; }
        }

        /// <summary>
        ///     The mechanism used to call the type-specific handler on the
        ///     target.
        /// </summary>
        /// <param name="genericHandler">
        ///     The generic handler to call in a type-specific way.
        /// </param>
        /// <param name="genericTarget">
        ///     The target to call the handler on.
        /// </param>
        /// <ExternalAPI/> 
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            KeyboardEventHandler handler = (KeyboardEventHandler)genericHandler;

            handler(genericTarget, this);
        }
    }
}
