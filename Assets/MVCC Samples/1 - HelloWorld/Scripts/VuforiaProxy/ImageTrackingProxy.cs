using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MVCSamples.HelloWorld.Controller
{
    public class ImageTrackingProxy : AppMonoController
    {

        public static Action<Transform> OnRealTimeUpdate;

        public Transform trackingObject;

        private void Start()
        {
            OnRealTimeUpdate += (trans) => {
                trackingObject.position = trans.position;
            };

            trackingObject.gameObject.SetActive(false);
        }

        public override bool OnNotification(object p_event_path, UnityEngine.Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYVUFORIA>(p_event_path);
            switch (path)
            {
                case NOTIFYVUFORIA.TRACKING_FOUND:
                    trackingObject.gameObject.SetActive(true);
                    break;
                case NOTIFYVUFORIA.TRACKING_LOST:
                    trackingObject.gameObject.SetActive(false);
                    break;
            }

            return true;
        }
    }
}