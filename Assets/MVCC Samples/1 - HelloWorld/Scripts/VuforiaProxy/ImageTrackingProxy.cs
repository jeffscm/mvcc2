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
using System;

namespace MVCSamples.HelloWorld.Controller
{
    public class ImageTrackingProxy : AppMonoController
    {

        public static Action<Transform> OnRealTimeUpdate;

        public Transform trackingObject;

        private void Start()
        {
            OnRealTimeUpdate += (trans) => {
                trackingObject.position = trans.position;
            };

            trackingObject.gameObject.SetActive(false);
        }

        public override bool OnNotification(object p_event_path, UnityEngine.Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
                case NOTIFYVUFORIA.TRACKING_FOUND:
                    trackingObject.gameObject.SetActive(true);
                    break;
                case NOTIFYVUFORIA.TRACKING_LOST:
                    trackingObject.gameObject.SetActive(false);
                    break;
            }

            return true;
        }
    }
}