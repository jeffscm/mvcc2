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
using NUnit.Framework;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
{

    public class LoginNavController : UIViewController
    {

        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();
            app.GetView<View.MenuView>().Hide();

            app.GetView<View.LoginView>().model.password = "FromModel";
            app.GetView<View.LoginView>().Present();
        }

        public override void OnNavigationExit()
        {
            base.OnNavigationExit();
            app.GetView<View.LoginView>().Dismiss();
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYUI>(p_event_path);

            switch (path)
            {
                case NOTIFYUI.UI_CLICK_LOGIN:

                    var s = app.GetView<View.LoginView>().model.password;

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