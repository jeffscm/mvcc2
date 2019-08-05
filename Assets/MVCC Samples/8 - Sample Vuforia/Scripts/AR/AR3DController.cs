using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuforiaSample
{
    public class AR3DController : AppMonoController
    {
        bool _dragEnabled = false;
        public GameObject target;
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
            //selectionObject.gameObject.SetActive(state);
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
                        target = null;
                    }
                    else
                    {
                        if (GroundPlaneProxy.TrackingStatusIsTrackedAndNormal)
                        {
                            if (string.IsNullOrEmpty(app.model.GetModel<VuforiaStateModel>().currentPlaceID))
                            {
                                if (app.IsCurrentView<SelectView>(-1))
                                {
                                    _dragEnabled = true;
                                }
                            }
                        }
                    }
                    break;

            }
            return base.OnNotification(p_event_path, p_target, p_data);
        }

        GameObject ReturnClickedObject(out RaycastHit hit)
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, 99f, LayerMask.GetMask("Default")))
            {
                target = hit.collider.gameObject;
            }
            return target;
        }


        private void Update()
        {
            if (!_dragEnabled) return;

            if (Input.GetMouseButtonDown(0) && target == null)
            {
                RaycastHit hitInfo;
                var temp = ReturnClickedObject(out hitInfo);

                CheckSelectedObject(temp);

                target = temp;
                if (target != null)
                {
                    isMouseDrag = true;
                    Debug.Log("target position : " + target.transform.position);
                    //Convert world position to screen position.
                    screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseDrag = false;
            }

            if (isMouseDrag && target != null)
            {
                //track mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

                //convert screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

                //It will update target gameobject's current postion.
                currentPosition.y = target.transform.position.y;
                target.transform.position = currentPosition;
                selectionObject.position = currentPosition;
            }
        }

        void CheckSelectedObject(GameObject newSelection)
        {

            Debug.Log($"SELECT: {_currentTarget?.GetInstanceID() ?? -1} {target?.GetInstanceID() ?? -1} {newSelection?.GetInstanceID() ?? -1}");

            var currId = _currentTarget?.GetInstanceID() ?? -1;
            var newId = newSelection?.GetInstanceID() ?? -1;

            if (newSelection == null)
            {
                selectionObject.gameObject.SetActive(false);
                _currentTarget = null;
                app.Notify(NOTIFYVUFORIA.VUFORIA_OBJ_EDIT, null, null);
            }
            else if (currId != newId)
            {
                selectionObject.gameObject.SetActive(true);
                _currentTarget = newSelection;
                app.Notify(NOTIFYVUFORIA.VUFORIA_OBJ_EDIT, null, _currentTarget);
            }
            else if (currId == newId)
            {
                selectionObject.gameObject.SetActive(false);
                app.Notify(NOTIFYVUFORIA.VUFORIA_OBJ_EDIT, null, null);
                _currentTarget = null;
            }
        }
    }
}