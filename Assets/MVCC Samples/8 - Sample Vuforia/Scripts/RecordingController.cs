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
    public class RecordingController : UIViewController
    {

        public override void OnNavigationActivate()
        {
            base.OnNavigationActivate();
            LeanTween.cancel(app.GetView<RecordingView>().recordingIcon.gameObject);
            app.GetView<RecordingView>().recordingIcon.gameObject.SetActive(false);
            app.GetView<RecordingView>().Present();
        }

        public override void OnNavigationDeativate()
        {
            base.OnNavigationDeativate();
            app.HideViews(this.controllerId);
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {

            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_PHOTO:
                    Debug.Log("Photo");
                    app.Notify(NOTIFYSYSTEM.SHOW_TOAST, null, TOAST.OK_PHOTO);
                    break;
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_START_REC:
                    Debug.Log("Start Video");
                    LeanTween.cancel(app.GetView<RecordingView>().recordingIcon.gameObject);
                    app.GetView<RecordingView>().recordingIcon.alpha = 1f;
                    LeanTween.alphaCanvas(app.GetView<RecordingView>().recordingIcon, 0.5f, 0.5f).setLoopPingPong().setRepeat(-1);

                    app.GetView<RecordingView>().recordingIcon.gameObject.SetActive(true);
                    break;
                case NOTIFYVUFORIA.VUFORIA_UI_CLICK_STOP_REC:
                    Debug.Log("Stop Video");
                    LeanTween.cancel(app.GetView<RecordingView>().recordingIcon.gameObject);
                    app.GetView<RecordingView>().recordingIcon.alpha = 1f;
                    app.GetView<RecordingView>().recordingIcon.gameObject.SetActive(false);
                    app.Notify(NOTIFYSYSTEM.SHOW_TOAST, null, TOAST.OK_VIDEO);
                    break;
            }
            return base.OnNotification(p_event_path, p_target, p_data);
        }
    }
}