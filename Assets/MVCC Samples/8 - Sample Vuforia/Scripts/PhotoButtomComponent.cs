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
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VuforiaSample
{
    public class PhotoButtomComponent : AppElement, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {

        public NOTIFYVUFORIA onClick;
        public NOTIFYVUFORIA onHold;
        public NOTIFYVUFORIA onHoldStop;

        public float thresholdDown = 0.25f;
        public float thresholdUp = 1f;

        float _timeToPress = -1;
        bool _pressed = false;
        bool _hasOperation = false;

        void Update()
        {
            if (_pressed)
            {
                _timeToPress += Time.deltaTime;

                if (_timeToPress > thresholdUp)
                {
                    _pressed = false;
                    app.Notify(onHold);
                }

            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _timeToPress = 0f;
            _pressed = true;
            _hasOperation = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchUp();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TouchUp();
        }

        void TouchUp()
        {
            if (_hasOperation)
            {
                _hasOperation = false;
                _pressed = false;
                CheckResult();
            }
        }

        void CheckResult()
        {
            if (_timeToPress > 0f && _timeToPress < thresholdDown)
            {
                app.Notify(onClick);
            }
            else if (_timeToPress > thresholdUp)
            {
                app.Notify(onHoldStop);
            }
        }
    }
}