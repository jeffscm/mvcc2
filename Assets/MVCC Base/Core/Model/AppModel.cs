using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;
using BestHTTP;



public class AppModel
{
    
   
    List<object> models = new List<object>();

    public T RegisterModel<T>() where T : new()
    {
        var temp = new T();
        models.Add(temp);
        return temp;
    }

    public T GetModel<T>() where T : new()
    {
        for (int i = 0; i < models.Count; i++)
        {
            if (models[i].GetType() == typeof(T))
            {
                return (T)models[i];
            }
        }
        return RegisterModel<T>();
    }

}
