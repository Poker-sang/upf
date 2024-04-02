﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.UI.Input;
using Wodsoft.UI.Media;

namespace Wodsoft.UI.Threading
{
    public abstract class UIDispatcher : Dispatcher
    {
        #region Render

        private bool _updateRender;
        private int _continueFrames;

        public void UpdateRender()
        {
            _updateRender = true;
        }

        protected void RunRender()
        {
            if (_updateLayout)
            {
                _updateLayout = false;
                InvalidateLayout(RootElement);
                UpdateLayoutCore();
            }
            if (_updateRender || _continueFrames < 2)
            {
                if (_updateRender)
                {
                    _updateRender = false;
                    _continueFrames = 0;
                }
                else
                    _continueFrames++;
                RunRenderCore();
            }
        }

        protected abstract void RunRenderCore();

        #endregion

        #region Input

        private List<MouseInput> _mouseInputs = new List<MouseInput>();
        private List<MouseInput> _backMouseInputs = new List<MouseInput>();
        public void PushMouseInput(int messageTime, MouseActions actions, MouseButton? button, int x, int y, int wheel)
        {
            var inputs = _mouseInputs;
            lock (inputs)
            {
                if (inputs.Count != 0 && actions == MouseActions.Move)
                {
                    ref var lastInput = ref CollectionsMarshal.AsSpan(inputs)[inputs.Count - 1];
                    if (lastInput.Actions == MouseActions.Move)
                    {
                        lastInput.Point = new MousePoint(x, y);
                        return;
                    }
                }
                inputs.Add(new MouseInput
                {
                    MessageTime = messageTime,
                    Actions = actions,
                    Button = button,
                    Point = new MousePoint(x, y),
                    Wheel = wheel
                });
            }
        }

        protected void RunInput()
        {
            _backMouseInputs = Interlocked.Exchange(ref _mouseInputs, _backMouseInputs);
            if (_backMouseInputs.Count != 0)
            {
                var count = _backMouseInputs.Count;
                var inputs = CollectionsMarshal.AsSpan(_backMouseInputs);
                for (int i = 0; i < count; i++)
                {
                    ref var input = ref inputs[i];
                    MouseDevice.HandleInput(input);
                }
                _backMouseInputs.Clear();
            }
            RunInputCore();
        }

        protected virtual void RunInputCore()
        {

        }

        #endregion

        #region Layout

        private bool _updateLayout;

        protected abstract UIElement RootElement { get; }

        internal void UpdateLayout()
        {
            _updateLayout = true;
        }

        protected abstract void UpdateLayoutCore();

        private void InvalidateLayout(Visual visual)
        {
            if (visual is UIElement element)
            {
                element.InvalidateMeasure();
                element.InvalidateArrange();
            }
            var childrenCount = visual.VisualChildrenCount;
            for (int i = 0; i < childrenCount; i++)
            {
                var child = visual.GetVisualChild(i);
                if (child != null)
                    InvalidateLayout(child);
            }
        }

        #endregion

        #region Device

        protected internal abstract MouseDevice MouseDevice { get; }

        #endregion
    }
}