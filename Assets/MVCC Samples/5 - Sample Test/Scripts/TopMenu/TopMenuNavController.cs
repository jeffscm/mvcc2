using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.Controller
{

    public class TopMenuNavController : UIViewController
    {

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYNAV>(p_event_path);
            switch (path)
            {
                case NOTIFYNAV.HIDE_TOPMENU:
                    app.GetView<TopMenuNav.ButtonView>().Present();
                    app.GetView<TopMenuNav.MenuView>().Dismiss();
                    break;
                case NOTIFYNAV.SHOW_TOPMENU:
                    app.GetView<TopMenuNav.MenuView>().Present();
                    app.GetView<TopMenuNav.ButtonView>().Dismiss();
                    break;
            }

            return true;
        }

        public override void OnNavigationActivate()
        {
            base.OnNavigationActivate();

            app.GetView<TopMenuNav.MenuView>().Dismiss();
            app.GetView<TopMenuNav.MenuView>().gameObject.SetActive(true);


        }

        public override void OnNavigationDeativate()
        {
            base.OnNavigationDeativate();

        }

    }

}
