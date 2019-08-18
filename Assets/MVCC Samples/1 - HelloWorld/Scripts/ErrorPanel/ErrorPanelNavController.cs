using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
{
    public class ErrorPanelNavController : UIViewController
    {

        private void Start()
        {
            app.GetView<View.ErrorView>().Dismiss();
        }

        public override void OnNavigationActivate()
        {
            base.OnNavigationActivate();
            app.GetView<View.ErrorView>().Present();
        }

        public override void OnNavigationDeativate()
        {
            base.OnNavigationDeativate();
            app.GetView<View.ErrorView>().Dismiss();
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYSYSTEM>(p_event_path);
            switch (path)
            {
                case NOTIFYSYSTEM.ERROR_MESSAGE:

                    app.GetView<View.ErrorView>().model.msg = p_data[0].ToString();
                    app.ActivateNavController<ErrorPanelNavController>();
                    Invoke(nameof(ErrorMessageHide), 3f);
                    break;
            }

            return true;
        }

        void ErrorMessageHide()
        {
            app.DeactivateNavController<ErrorPanelNavController>();
        }
    }
}