using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
{
    public class MyApp : AppMonoController
    {
        public override void OnEnable() // First thing that happens in your code will go here
        {
            base.OnEnable();

            //1 - Register your UI animation system
            MVCC.RegisterAnim(new MyAnim()); 
            //2 - Register other systems like Http Connector
            MVCC.RegisterHttpHelper(new HttpHelper());
            //3 - Start your system -> All anims and other scripts will behave accordingly
            MVCC.OnStart += () =>
            {
                //4 - Which controller is the "main"
                app.SwitchNavController<LoginNavController>(CONTROLLER_TYPE.ALL);
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
                    app.SwitchNavController<LoginNavController>(CONTROLLER_TYPE.ALL);
                    break;
            }

            return true;
        }
    }
}

