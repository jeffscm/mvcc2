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
    public class ARNavController : UIViewController
    {

        public override void OnNavigationEnter()
        {
            Debug.Log("AR View active");
            base.OnNavigationEnter();

            GroundPlaneProxy.OnStartARGroundPlane += OnStartARGroundPlane;
            GroundPlaneProxy.OnStatusChange += OnScanState;


            if (GroundPlaneProxy.TrackingStatusIsTrackedAndNormal)
            {
                if (string.IsNullOrEmpty(app.model.GetModel<VuforiaStateModel>().currentPlaceID))
                {
                    // this is wrong...   
                    app.SwitchNavController<SelectionController>(CONTROLLER_TYPE.NAV);
                }
                else
                {
                    app.GetView<ARPlaceView>().Present();
                }
            }
            else
            {
                app.SwitchNavController<ScanNavController>(CONTROLLER_TYPE.NAV);
            }
#if UNITY_EDITOR
            Invoke(nameof(Test), 2f);
#endif
        }
        public override void OnNavigationExit()
        {
            base.OnNavigationExit();

            GroundPlaneProxy.OnStartARGroundPlane -= OnStartARGroundPlane;
            GroundPlaneProxy.OnStatusChange -= OnScanState;

            app.HideViews(this.controllerId);
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
               
                case NOTIFYVUFORIA.VUFORIA_PLACE_ON_GROUND:
                    app.model.GetModel<VuforiaStateModel>().currentPlaceID = string.Empty;
                    app.SwitchNavController<SelectionController>(CONTROLLER_TYPE.NAV);
                    break;

                case NOTIFYVUFORIA.VUFORIA_CANCEL_PLACEMENT:
                    app.model.GetModel<VuforiaStateModel>().currentPlaceID = string.Empty;
                    app.SwitchNavController<SelectionController>(CONTROLLER_TYPE.NAV);
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

        void OnScanState(bool state)
        {
            Debug.Log($"Status Scan {state}");
            if (!state)
            {
                app.SwitchNavController<ScanNavController>(CONTROLLER_TYPE.NAV);
            }
        }

        void OnStartARGroundPlane(bool enabled)
        {

        }
#if UNITY_EDITOR
        void Test()
        {
            app.Notify(NOTIFYVUFORIA.VUFORIA_PLACE_ON_GROUND);
        }
#endif

    }
}
