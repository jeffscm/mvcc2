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