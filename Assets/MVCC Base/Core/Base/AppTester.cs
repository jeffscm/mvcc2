using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppTester : MonoBehaviour
{

    public string lastNotification, lastSwiftNavigation;

    // Start is called before the first frame update
    void Awake()
    {
        App.OnNotificationLog += (lastNotify) => { lastNotification = lastNotify; }; 

        App.OnNavLog += (obj) => { lastSwiftNavigation = obj.GetType().ToString(); };
    }

}
