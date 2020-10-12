/*
Jefferson Scomacao
https://www.jscomacao.com

GitHub - Source Code
Project: MVCC2.0

Copyright (c) 2015 Jefferson Raulino Scomação

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if VUFORIA
using Vuforia;

namespace MVCSamples.HelloWorld
{
    public class ImageTracker : ImageTrackerBase<NOTIFYVUFORIA>
    {

        private bool _hasTracking = false;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnTrackingFound()
        {
            app.Notify(OnNotifyFound, this);
            _hasTracking = true;
        }

        protected override void OnTrackingLost()
        {
            app.NotifyGroup(OnNotifyLost, CONTROLLER_TYPE.PROXY, this);
            _hasTracking = false;
        }

        private void Update()
        {
            if (_hasTracking)
            {
                ImageTrackingProxy.OnRealTimeUpdate?.Invoke(this.transform);
            }
        }

    }
}
#endif