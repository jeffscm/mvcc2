using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
{
    public enum STATEVALUES { NONE, LOGIN, RESET, ARVIEW };

    public class Navigation : AppMonoController
    {

        State<STATEVALUES, NOTIFYUI> navState = new State<STATEVALUES, NOTIFYUI>();

        void Start()
        {
            navState.RegisterEvent(STATEVALUES.LOGIN, NOTIFYUI.UI_CLICK_LOGIN);


            //navState.onStateChange += (evt) =>
            //{
            //    app.Notify(evt);
            //};

            //navState.state = STATEVALUES.NONE;
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            var path = NotifyArg.ConvertTo<NOTIFYNAV>(p_event_path);

            switch (path)
            {
                case NOTIFYNAV.GOTO_LOGIN:

                    //if (app.GetCurrentViewController(CONTROLLER_TYPE.NAV).GetType() != typeof())
                    //{
                    //    return false;
                    //}

                    break;
                case NOTIFYNAV.SHOW_TOPMENU:

                    //if (navState.state != STATEVALUES.ARVIEW)
                    //{
                    //    return false;
                    //}

                    break;
            }

            return true;
        }
    }
}