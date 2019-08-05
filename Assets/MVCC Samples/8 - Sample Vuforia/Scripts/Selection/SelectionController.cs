using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VuforiaSample
{
    public class SelectionController : UIViewController
    {

        bool _isFirstTime = true;

        private void Awake()
        {
            GroundPlaneProxy.OnStartARGroundPlane += (obj) => {
                app.GetView<SelectView>().Present();
            };
        }

        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();

            //if (_isFirstTime) return;
            _isFirstTime = false;
            app.GetView<SelectView>().Present();
            app.GetView<SelectView>().editAnim.DoAnimateOut();
            app.GetView<SelectView>().selectAnim.DoAnimateIn();
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
                case NOTIFYVUFORIA.VUFORIA_START_PLACEMENT:
                    app.model.GetModel<VuforiaStateModel>().currentPlaceID = p_data[0].ToString();
                    app.SwitchNavController<ARNavController>(CONTROLLER_TYPE.NAV);

                    break;
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_DRAWER:
                    app.GetView<DrawerView>().Present();
                    break;
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_DRAWER_CLOSE:
                    app.GetView<SelectView>().Present();
                    break;
                case NOTIFYVUFORIA.VUFORIA_OBJ_SELECTED:
                    app.GetView<SelectEditView>().Present();
                    break;

                case NOTIFYVUFORIA.VUFORIA_OBJ_SELECTED_CLOSE:
                    app.GetView<SelectView>().Present();
                    break; 

                 case NOTIFYVUFORIA.VUFORIA_OBJ_EDIT:
                    if (p_data != null && p_data.Length > 0)
                    {
                        app.GetView<SelectView>().selectAnim.DoAnimateOut();
                        app.GetView<SelectView>().editAnim.DoAnimateIn();
                    }
                    else
                    {
                        app.GetView<SelectView>().editAnim.DoAnimateOut();
                        app.GetView<SelectView>().selectAnim.DoAnimateIn();
                    }
                    break; 

            }
            return true;
        }
    }
}