using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MyUIViewController : UIViewControllerBase
{

    public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        Debug.Log(p_event_path.ToString());
        var path = NotifyArg.ConvertTo<NOTIFYEVENT>(p_event_path);
        switch (path)
        {           
            case NOTIFYEVENT.TEST_BUTTON:

                app.GetView<MyUIView>().Present();

                var obj = app.GetView<MyUIView>().test;

                Assert.IsTrue(obj != null);
                obj.SetActive(!obj.activeInHierarchy);
                break;
        }

        return true;
    }

    public override void OnNavigationEnter()
    {
        base.OnNavigationEnter();
        Debug.Log("NavEnter");
        app.Notify(NOTIFYEVENT.TEST_BUTTON, null, null);
    }

    public override void OnNavigationExit()
    {
        base.OnNavigationExit();
    }
}
