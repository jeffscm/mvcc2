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
namespace VuforiaSample
{
    public enum TOAST { NONE, OK_VIDEO, OK_PHOTO, FAIL };
    public class MessageNavController : UIViewController
    {

        public float timerDismiss = 2f;

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {

            var path = NotifyArg.ConvertTo<NOTIFYSYSTEM>(p_event_path);
            switch (path)
            {
                case NOTIFYSYSTEM.SHOW_TOAST:
                    var toast = (TOAST)p_data[0];

                    switch(toast)
                    {
                        case TOAST.OK_PHOTO:
                            app.GetView<OkPhotoView>().Present();
                            ScheduleHide();
                            break;
                        case TOAST.OK_VIDEO:
                            app.GetView<OkVideoView>().Present();
                            ScheduleHide();
                            break;
                        case TOAST.FAIL:
                            app.GetView<FailView>().Present();
                            ScheduleHide();
                            break;
                    }

                    break;
            }

            return base.OnNotification(p_event_path, p_target, p_data);
        }

        void ScheduleHide()
        {
            CancelInvoke(nameof(Deactivate));
            Invoke(nameof(Deactivate), timerDismiss);
        }

        void Deactivate()
        {
            app.HideViews(this.controllerId);
        }
    }
}