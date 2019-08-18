using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
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
                    app.GetView<View.ButtonView>().Present();
                    app.GetView<View.MenuView>().Dismiss();
                    break;
                case NOTIFYNAV.SHOW_TOPMENU:
                    app.GetView<View.MenuView>().Present();
                    app.GetView<View.ButtonView>().Dismiss();
                    break;
            }

            return true;
        }

        public override void OnNavigationActivate()
        {
            base.OnNavigationActivate();

            app.GetView<View.MenuView>().Dismiss();
            app.GetView<View.MenuView>().gameObject.SetActive(true);


        }

        public override void OnNavigationDeativate()
        {
            base.OnNavigationDeativate();

        }

    }

}
