using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VuforiaSample
{


    public class SelectionController : UIViewController
    {

        public AnimateSettings[] viewAnims;
        UIViewAnim _currentViewAnim = null;

        Dictionary<string, Color> _colors = new Dictionary<string, Color>() {
            {"green", Color.green},
            {"blue", Color.blue},
            {"white", Color.white}
        };

        bool _isFirstTime = true;

        private void Awake()
        {
            GroundPlaneProxy.OnStartARGroundPlane += (obj) => {
                //app.GetView<SelectView>().Present(); --> Show supported or not
            };
        }

        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();

            //if (_isFirstTime) return;
            _isFirstTime = false;
            app.GetView<SelectView>().Present();

            if (app.model.GetModel<VuforiaStateModel>().currentSelection == null)
            {
                app.GetView<SelectView>().editAnim.DoAnimateOut();
                app.GetView<SelectView>().selectAnim.DoAnimateIn();
            }
            else
            {
                app.GetView<SelectView>().editAnim.DoAnimateIn();
                app.GetView<SelectView>().selectAnim.DoAnimateOut();
            }

            app.ActivateNavController<RecordingController>();
        }

        public override void OnNavigationExit()
        {
            base.OnNavigationExit();
            app.HideViews(this.controllerId);
            app.DeactivateNavController<RecordingController>();
        }

        public override bool OnNotification(object p_event_path, UnityEngine.Object p_target, params object[] p_data)
        {
            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
                case NOTIFYVUFORIA.VUFORIA_START_PLACEMENT:
                    app.model.GetModel<VuforiaStateModel>().currentPlaceID = p_data[0].ToString();
                    app.SwitchNavController<ARNavController>(CONTROLLER_TYPE.NAV);

                    break;
                //case NOTIFYVUFORIA.VUFORIA_UI_CLICK_DRAWER:
                //    app.GetView<DrawerView>().Present();
                //    break;
                //case NOTIFYVUFORIA.VUFORIA_UI_CLICK_DRAWER_CLOSE:
                    //app.GetView<SelectView>().Present();
                    //break;
                case NOTIFYVUFORIA.VUFORIA_UI_SHOW_FIRST_OPTIONS:


                    if (_currentViewAnim != null) MVCC.animate.ScaleOut(_currentViewAnim.cg, false, viewAnims[3]);

                    app.GetView<SelectOptionsView>().Present();

                    _currentViewAnim = app.GetView<SelectOptionsView>().viewAnim;

                    _currentViewAnim.onAnimateIn?.Invoke();
                    MVCC.animate.ScaleIn(_currentViewAnim.cg, viewAnims[0]);

                    break;

                case NOTIFYVUFORIA.VUFORIA_UI_OPTIONS_BACK_MAIN:

                    if (_currentViewAnim != null) MVCC.animate.ScaleOut(_currentViewAnim.cg, false, viewAnims[2]);
                    _currentViewAnim = null;

                    app.GetView<SelectView>().Present();

                    break; 

                 case NOTIFYVUFORIA.VUFORIA_UI_CLICK_RESET:

                    if (app.model.GetModel<VuforiaStateModel>().currentSelection != null)
                    {
                        Destroy(app.model.GetModel<VuforiaStateModel>().currentSelection.gameObject);
                        app.model.GetModel<VuforiaStateModel>().currentSelection = null;
                    }

                    if (_currentViewAnim != null) MVCC.animate.ScaleOut(_currentViewAnim.cg, false, viewAnims[2]);
                    _currentViewAnim = null;

                    app.GetView<SelectView>().Present();
                    app.GetView<SelectView>().editAnim.DoAnimateOut();
                    app.GetView<SelectView>().selectAnim.DoAnimateIn();

                    break;

                case NOTIFYVUFORIA.VUFORIA_UI_SHOW_COLORS:

                    MVCC.animate.ScaleIn(_currentViewAnim.cg, viewAnims[1]);

                    app.GetView<SelectColorOptionView>().Present();

                    _currentViewAnim = app.GetView<SelectColorOptionView>().viewAnim;

                    MVCC.animate.ScaleIn(_currentViewAnim.cg, viewAnims[0]);

                    break;

                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_SELECT_COLOR:

                    ChangeColor(p_data[0].ToString());

                    break;

            }
            return true;
        }

        void ChangeColor(string color)
        {
            if (_colors.ContainsKey(color))
            {
                app.model.GetModel<VuforiaStateModel>().currentSelection.renderChangeColor.material.SetColor("_Color", _colors[color]);
            }
        }
    }

}