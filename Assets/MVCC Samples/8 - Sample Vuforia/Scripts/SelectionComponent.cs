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

namespace VuforiaSample
{
    public class SelectionComponent : AppElement, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public VuforiaPressHandler[] buttons;
        public UIViewAnim viewAnim;

        public bool useRotation = false;

        private void OnEnable()
        {
            if (viewAnim != null)
            {
                viewAnim.onAnimateIn += OnAnimateIn;
                viewAnim.onAnimateOut += OnAnimateOut;
            }
        }

        private void OnDisable()
        {
            if (viewAnim != null)
            {
                viewAnim.onAnimateIn -= OnAnimateIn;
                viewAnim.onAnimateOut -= OnAnimateOut;
            }
        }

        void OnAnimateIn()
        {
            foreach(var button in buttons)
            {
                button.ResetDefaultColor();
            }
            if (useRotation)
            {
                LeanTween.rotateLocal(this.gameObject, new Vector3(0, 0, initialAngle), 0.1f);
            }
        }
        void OnAnimateOut()
        {
            foreach (var button in buttons)
            {
                button.ResetDefaultColor();
            }
        }

        bool _canAnimate = true;

        float _drag = 0f;
        bool _dragValue = false;
        bool _pressed = false;
        float _lastDelta = 0f;

        public float startAngle, initialAngle, endAngle;

        void Update()
        {
            if (!useRotation) return;

            if (_dragValue && _canAnimate)
            {
                var e = this.transform.localEulerAngles;
                e.z += _drag;
                if (!(e.z > startAngle && e.z < endAngle))
                {
                    _dragValue = false;
                    return;
                }


                this.transform.localEulerAngles = e;
                _drag /= 1.1f;
                if (Mathf.Abs(_drag) < 0.2f)
                {
                    _dragValue = false;
                }
            }
        
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!useRotation) return;

            if (_canAnimate)
            {
                var e = this.transform.localEulerAngles;


                var v = eventData.position.normalized - (this.transform as RectTransform).anchoredPosition.normalized;

                var angle = Vector2.Angle(v, Vector2.right);
                var cross = Vector3.Cross(v, Vector2.right);

                Debug.Log($"A: {angle} {cross} {_lastDelta}");

                var delta = angle - _lastDelta;

                if (cross.z > 0) delta *= -1f;

                e.z += delta;

                _lastDelta = delta;

                if (!(e.z > startAngle && e.z < endAngle))
                {
                    _dragValue = false;

                    _canAnimate = false;

                    var ds = Mathf.Abs(startAngle - e.z);
                    var de = Mathf.Abs(endAngle - e.z);

                    var valueTarget = startAngle;
                    if (ds > de)
                    {
                        valueTarget = endAngle;
                    }

                    LeanTween.rotateLocal(this.gameObject, new Vector3(0, 0, valueTarget), 0.35f).setEaseInOutBounce().setOnComplete(() => { _canAnimate = true; });

                }
                else
                {
                    _dragValue = true;
                    this.transform.localEulerAngles = e;
                }


                Debug.Log(_drag);

            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_pressed)
            {
                //var v = eventData.position.normalized;
                //var angle = Vector2.Angle(v, Vector2.right);
                //var cross = Vector3.Cross(v, Vector2.right);
                //_lastDelta = angle;
            }
            _pressed = true;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
        }
               
    }
}
