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

                if (Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x))
                {
                    e.z += eventData.delta.y;
                    _drag = Mathf.Clamp(eventData.delta.y, -1f, 1f);
                }
                else
                {
                    e.z += -eventData.delta.x;
                    _drag = -Mathf.Clamp(eventData.delta.x, -1f, 1f);
                }
                Debug.Log($"EZ: {e.z}");
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
            //_pressed = true;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            //_pressed = false;
        }
               
    }
}
