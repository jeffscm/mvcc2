
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MVCCSetup
{
    public App app;

    [RuntimeInitializeOnLoadMethod]
    static void StartMVC()
    {
        var newMvc = new MVCCSetup();

        newMvc.Initialize();

    }

    public void Initialize()
    {
        app = new App();
        MVCC.RegisterApp(app);
       
        var mvccbaseobj = Resources.Load("MVCC") as GameObject;
        UnityEngine.MonoBehaviour.Instantiate(mvccbaseobj);


    }
}
