using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuforiaSample
{
    public class ARNavController : UIViewController
    {

        private void Start()
        {



        }
        public override void OnNavigationEnter()
        {
            Debug.Log("AR View active");
            base.OnNavigationEnter();


            if (GroundPlaneProxy.TrackingStatusIsTrackedAndNormal)
            {
                if (string.IsNullOrEmpty(app.model.GetModel<VuforiaStateModel>().currentPlaceID))
                {
                    app.GetView<ARFreeView>().Present();
                }
                else
                {
                    app.GetView<ARPlaceView>().Present();
                }
            }
            else
            {
                app.GetView<ARScanView>().Present();
            }

            GroundPlaneProxy.OnStartARGroundPlane += OnStartARGroundPlane;
            GroundPlaneProxy.OnScanStatus += OnScanState;
        }
        public override void OnNavigationExit()
        {
            base.OnNavigationExit();
            app.HideViews(this.controllerId);
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
               
                case NOTIFYVUFORIA.VUFORIA_PLACE_ON_GROUND:
                    app.model.GetModel<VuforiaStateModel>().currentPlaceID = string.Empty;
                    app.GetView<ARFreeView>().Present();
                    break;
                case NOTIFYVUFORIA.VUFORIA_CANCEL_PLACEMENT:
                    app.model.GetModel<VuforiaStateModel>().currentPlaceID = string.Empty;
                    app.SwitchNavController<SelectionController>(CONTROLLER_TYPE.NAV);
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
                                if (app.IsCurrentView<ARFreeView>(-1) || app.IsCurrentView<SelectView>(-1))
                                {
                                    _dragEnabled = true;
                                }
                            }
                        }
                    }                   
                    break;
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_BACK:

                    app.SwitchNavController<SelectionController>(CONTROLLER_TYPE.NAV);

                    break;
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_CANCEL:

                    app.Notify(NOTIFYVUFORIA.VUFORIA_CANCEL_PLACEMENT);

                    break;
            }
            return base.OnNotification(p_event_path, p_target, p_data);
        }

        void OnScanState(SCANSTATUS state)
        {
            Debug.Log($"Status Scan {state}");
            if (state == SCANSTATUS.SCANNED)
            {
                if (string.IsNullOrEmpty(app.model.GetModel<VuforiaStateModel>().currentPlaceID))
                {
                    app.GetView<ARFreeView>().Present();
                }
                else
                {
                    app.GetView<ARPlaceView>().Present();
                }
            }
            else
            {
                app.GetView<ARScanView>().Present();
            }
        }

        void OnStartARGroundPlane(bool enabled)
        {

        }


        GameObject ReturnClickedObject(out RaycastHit hit)
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                target = hit.collider.gameObject;
            }
            return target;
        }

        bool _dragEnabled = false;

       

        public GameObject target;
        bool isMouseDrag = false;
        Vector3 offset;
        Vector3 screenPosition;
        private void Update()
        {
            if (!_dragEnabled) return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                target = ReturnClickedObject(out hitInfo);
                if (target != null)
                {
                    isMouseDrag = true;
                    Debug.Log("target position :" + target.transform.position);
                    //Convert world position to screen position.
                    screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseDrag = false;
            }

            if (isMouseDrag)
            {
                //track mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

                //convert screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

                //It will update target gameobject's current postion.
                currentPosition.y = target.transform.position.y;
                target.transform.position = currentPosition;
            }
        }

    }
}
