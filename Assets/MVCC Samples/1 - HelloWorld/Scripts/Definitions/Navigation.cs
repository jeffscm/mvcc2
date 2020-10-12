/*
Jefferson Scomacao
https://www.jscomacao.com

GitHub - Source Code
Project: MVCC2.0

Copyright (c) 2015 Jefferson Raulino Scomação

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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