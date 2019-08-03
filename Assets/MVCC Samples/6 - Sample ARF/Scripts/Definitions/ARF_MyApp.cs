using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARFSample
{
    public class ARF_MyApp : AppMonoController
    {

        public override void OnEnable()
        {
            base.OnEnable();

            MVCC.RegisterAnim(new ARF_MyAnim());

            MVCC.OnStart += () =>
            {
                //app.SwitchNavController<MVC.Controller.LoginNavController>(CONTROLLER_TYPE.ALL);
            };
        }


        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            // Handle Main Navigation Logic -> Should be generic and not tied to several sub systems
            var pathNav = NotifyArg.ConvertTo<NOTIFYNAV>(p_event_path);
            switch (pathNav)
            {
                case NOTIFYNAV.GOTO_LOGIN:
                    app.SwitchNavController<MVC.Controller.LoginNavController>(CONTROLLER_TYPE.ALL);
                    break;
            }
            return true;
        }
    }
}