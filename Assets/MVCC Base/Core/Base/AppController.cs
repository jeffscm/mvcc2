using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AppController {

    public AppController(int id, Type thisContext)
    {
        controllerId = id;
        context = thisContext;
    }
    public Type context = null;
    public int controllerId;

    public App app
    {
        get
        {
            return MVCC.app;
        }
    }

	public virtual void OnNotification(object p_event_path,UnityEngine.Object p_target,params object[] p_data)
	{
		
	}

    //public virtual void OnSystem(NOTIFYSYSTEM p_event_path, UnityEngine.Object p_target, params object[] p_data)
    //{

    //}

}

public class NotifyArg
{
    public static T ConvertTo<T>(object inputObject) where T : System.Enum 
    {

        var names = System.Enum.GetNames(typeof(T));
        var n = inputObject.ToString();
        if (!names.Contains(n)) return default;

        return (T)inputObject;
    }
}


