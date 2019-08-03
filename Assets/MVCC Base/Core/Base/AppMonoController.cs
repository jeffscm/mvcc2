using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;

public enum CONTROLLER_TYPE { NONE, ALL, NAV, THREED, ERROR, WARN, PROXY};

public class AppMonoController : AppElement
{
    public Type context = null;

    public static Action<bool> onStubExecute;

    public int priority = -1;

    [HideInInspector]
    [SerializeField]
    public long controllerId;

    public CONTROLLER_TYPE controllerType;

    public virtual bool OnNotification(object p_event_path, UnityEngine.Object p_target, params object[] p_data)
    {
        return true;
    }

    //public virtual void OnSystem(NOTIFYSYSTEM p_event_path, UnityEngine.Object p_target, params object[] p_data)
    //{

    //}


    public virtual bool OnNavigationSwitch(int controllerId)
    {
        return true;
    }


    bool isRegistered = false;

    public virtual void OnEnable()
    {

        if (!isRegistered)
        {
            isRegistered = true;
            App.monoObsever.Subscribe(this);
        }
    }
       
}