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