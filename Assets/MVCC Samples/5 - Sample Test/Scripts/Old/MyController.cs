using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class MyController : AppController
{
    public MyController(int id, Type thisContext) : base(id, thisContext)
    {
        MVCC.OnStart += InstantiateSystem;
    }

   
    void InstantiateSystem()
    {
//        var c = Resources.Load("maincanvas") as GameObject;
//        UnityEngine.MonoBehaviour.Instantiate(c);
//        var s = Resources.Load("someobject") as GameObject;
//        UnityEngine.MonoBehaviour.Instantiate(s);

//#if UNITY_INCLUDE_TESTS
//        UnityEngine.Assertions.Assert.IsTrue(c != null);
//        UnityEngine.Assertions.Assert.IsTrue(s != null);
//#endif
    }
}
