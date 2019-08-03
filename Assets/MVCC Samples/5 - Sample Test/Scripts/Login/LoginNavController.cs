using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using MVC.View;

namespace MVC.Controller
{

    public class LoginNavController : UIViewController
    {

        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();
            app.GetView<TopMenuNav.MenuView>().Hide();

            app.GetView<LoginView>().model.password = "FromModel";
            app.GetView<LoginView>().Present();
        }

        public override void OnNavigationExit()
        {
            base.OnNavigationExit();
            app.GetView<LoginView>().Dismiss();
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYUI>(p_event_path);

            switch (path)
            {
                case NOTIFYUI.UI_CLICK_LOGIN:

                    var s = app.GetView<LoginView>().model.password;

                    if (s == "123")
                    {
                        app.SwitchNavController<ARNavController>(this.controllerType);

                        app.SwitchNavController<ARController>(CONTROLLER_TYPE.THREED);

                        onStubExecute?.Invoke(true);
                    }
                    else
                    {
                        app.Notify(NOTIFYSYSTEM.ERROR_MESSAGE, null, "Invalid Password");
                        onStubExecute?.Invoke(false);
                    }
                    break;
            }

            return true;
        }
    }
}