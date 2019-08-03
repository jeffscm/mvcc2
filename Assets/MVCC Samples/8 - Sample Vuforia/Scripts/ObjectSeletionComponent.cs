using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuforiaSample
{
    public class ObjectSeletionComponent : AppElement
    {
        public NOTIFYVUFORIA onUpEvent;

        public void OnPointerUp()
        {
            app.Notify(onUpEvent, null, true);
        }

        public void OnPointerDown()
        {
            Debug.Log("Down");
            app.Notify(onUpEvent, null, false);
        }

    }
}