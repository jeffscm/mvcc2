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

namespace VuforiaSample
{
    public class AR3DController : AppMonoController
    {
        bool _dragEnabled = false;

        bool isMouseDrag = false;
        Vector3 offset;
        Vector3 screenPosition;


        public Transform selectionObject;
        GameObject _currentTarget = null;


        private void Start()
        {
            GroundPlaneProxy.OnStatusChange += OnScanState;
        }

        void OnScanState(bool state)
        {
            Debug.Log($"Status Scan {state}");




        }


        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
                case NOTIFYVUFORIA.VUFORIA_START_PLACEMENT:
                    selectionObject.gameObject.SetActive(false);
                    _currentTarget = null;

                    break;

                case NOTIFYVUFORIA.VUFORIA_HIT_TEST:
                    var up = (bool)p_data[0];
                    if (up)
                    {
                        _dragEnabled = false;
                    }
                    else
                    {
                        if (GroundPlaneProxy.TrackingStatusIsTrackedAndNormal)
                        {
                            if (string.IsNullOrEmpty(app.model.GetModel<VuforiaStateModel>().currentPlaceID))
                            {
                                if (app.IsCurrentView<SelectView>(-1))
                                {
                                    _currentTarget = app.model.GetModel<VuforiaStateModel>().currentSelection?.RealObject ?? null;
                                    if (_currentTarget != null)
                                    {
                                        _down = false;
                                        _dragEnabled = true;
                                    }
                                }
                            }
                        }
                    }
                    break;

            }
            return base.OnNotification(p_event_path, p_target, p_data);
        }

        GameObject ReturnClickedObject()//out RaycastHit hit)
        {
            return _currentTarget;

            //GameObject target = null;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, 99f, LayerMask.GetMask("Default")))
            //{
            //    target = hit.collider.gameObject;
            //}
            //return target;
        }


        private void Update()
        {
            if (!_dragEnabled) return;


            if (Input.touchCount == 2)
            {
                if (_currentTarget != null)
                {
                    RotateObject();
                }
                else
                {
                    _down = false;
                }
                return;
            }

            _down = false;

            if (Input.GetMouseButtonDown(0) && _currentTarget != null)
            {
                //RaycastHit hitInfo;
                var temp = ReturnClickedObject();//out hitInfo);

                CheckSelectedObject(temp);

                isMouseDrag = true;
                Debug.Log("target position : " + _currentTarget.transform.position);
                //Convert world position to screen position.
                screenPosition = Camera.main.WorldToScreenPoint(_currentTarget.transform.position);
                offset = _currentTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseDrag = false;
            }

            if (isMouseDrag && _currentTarget != null)
            {
                //track mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

                //convert screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

                //It will update target gameobject's current postion.
                currentPosition.y = _currentTarget.transform.position.y;
                _currentTarget.transform.position = currentPosition;
                selectionObject.position = currentPosition;
            }
        }

        void CheckSelectedObject(GameObject newSelection)
        {

            Debug.Log($"SELECT: {_currentTarget?.GetInstanceID() ?? -1} {newSelection?.GetInstanceID() ?? -1}");

            var currId = _currentTarget?.GetInstanceID() ?? -1;
            var newId = newSelection?.GetInstanceID() ?? -1;

            if (newSelection == null)
            {
                selectionObject.gameObject.SetActive(false);
                //_currentTarget = null;
                //app.Notify(NOTIFYVUFORIA.VUFORIA_UI_CLICK_RESET, null, null);
            }
            else //if (currId != newId)
            {
                selectionObject.gameObject.SetActive(true);
                //_currentTarget = newSelection;
                //app.Notify(NOTIFYVUFORIA.VUFORIA_OBJ_EDIT, null, _currentTarget);
            }
            //else if (currId == newId)
            //{
            //    selectionObject.gameObject.SetActive(false);
            //    app.Notify(NOTIFYVUFORIA.VUFORIA_OBJ_EDIT, null, null);
            //    _currentTarget = null;
            //}
        }

        bool _down = false;

        float _lastAngleDelta = 0;

        void RotateObject()
        {
            Debug.Log("Rotate");
            var points = GetTouchPositions();
            var center = (points[1] + points[0]) / 2f;
            var zero = center - points[0];
            zero = zero.normalized;

            var angle = Vector2.Angle(new Vector2(1, 0), zero);// * GetLeftMost();
            var cross = Vector3.Cross(new Vector2(1, 0), zero);

            //Debug.Log(angle);

            if (!_down) _lastAngleDelta = angle;
            var delta = angle - _lastAngleDelta;

            if (cross.z > 0) delta = delta * -1;

            //Debug.Log($"Delta {angle - _lastAngleDelta} {cross.z}");

            _currentTarget.transform.Rotate(new Vector3(0, delta, 0));

            _lastAngleDelta = angle;

            _down = true;
        }

        Vector2[] touchPos = new Vector2[2];

        Vector2[] GetTouchPositions()
        {
            touchPos[0] = Input.GetTouch(0).position;
            touchPos[1] = Input.GetTouch(1).position;
            return touchPos;
        }
    }
}