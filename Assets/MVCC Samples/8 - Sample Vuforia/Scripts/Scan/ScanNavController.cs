using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuforiaSample
{
    public class ScanNavController : UIViewController
    {
        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();
            GroundPlaneProxy.OnStatusChange += OnScanState;
            app.GetView<ARScanView>().Present();
        }

        public override void OnNavigationExit()
        {
            base.OnNavigationExit();
            GroundPlaneProxy.OnStatusChange -= OnScanState;
            app.HideViews(this.controllerId);
        }

        void OnScanState(bool state)
        {
            Debug.Log($"Status Scan {state}");
            if (state)
            {
                if (string.IsNullOrEmpty(app.model.GetModel<VuforiaStateModel>().currentPlaceID))
                {
                    app.SwitchNavController<SelectionController>(CONTROLLER_TYPE.NAV);
                }
                else
                {
                    app.SwitchNavController<ARNavController>(CONTROLLER_TYPE.NAV);
                }
            }
        }

    }
}