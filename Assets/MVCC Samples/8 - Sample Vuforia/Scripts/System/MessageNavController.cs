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