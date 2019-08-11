using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NAVANIM { NONE, FADE, MOVELEFT, MOVERIGHT, MOVEBOTTOM, MOVEUP, SCALE }

public enum NOTIFY_TYPES 
{ 
    EVENT, SYSTEM, UI, JS, NAV, AR, VR, PROXY
}

public class App
{

    public static System.Action<string> OnNotificationLog;
    public static System.Action<UIViewControllerBase> OnNavLog;
    public static System.Action<UIView, int> OnUIViewSwitchLog;

    public AppModel model;
    public static Obsever<AppMonoController> monoObsever = new Obsever<AppMonoController>();
    public static Obsever<UIView> uiviewObsever = new Obsever<UIView>();
    public static Obsever<UI3DView> ui3dviewObsever = new Obsever<UI3DView>();

    static List<AppController> controller_list = new List<AppController>();
    static List<AppMonoController> monocontroller_list = new List<AppMonoController>();

    static Dictionary<CONTROLLER_TYPE, UIViewControllerBase> _currentNav = new Dictionary<CONTROLLER_TYPE, UIViewControllerBase>();

    public App()
    {
        model = new AppModel();

        monoObsever.registerCall = (newcontroller) =>
        {
            if (newcontroller.priority == 0)
            {
                monocontroller_list.Insert(0, newcontroller);
            }
            else
            {
                monocontroller_list.Add(newcontroller);
            }

        };
        monoObsever.Publish();

        uiviewObsever.registerCall = (uiview) =>
        {
            var t = uiview.GetType();
            if (!uiviewList.ContainsKey(t))
            {
                uiviewList.Add(t, uiview);
            }
        };
        uiviewObsever.Publish();

        ui3dviewObsever.registerCall = (uiview) =>
        {
            var t = uiview.GetType();
            if (!uiviewList.ContainsKey(t))
            {
                ui3dviewList.Add(t, uiview);
            }
        };
        ui3dviewObsever.Publish();
    }

    public void Notify<T>(T p_event_path, UnityEngine.Object p_target = null, params object[] p_data)
    {
        var temp = controller_list;
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] == null) continue;
            temp[i].OnNotification(p_event_path as object, p_target, p_data);

            OnNotificationLog?.Invoke(p_event_path.ToString());
        }

        var temp2 = monocontroller_list;
        for (int i = 0; i < temp2.Count; i++)
        {
            if (temp2[i] == null) continue;
            var response = temp2[i].OnNotification(p_event_path as object, p_target, p_data);

            if (!response && temp2[i].priority == 0) break;

            OnNotificationLog?.Invoke("MONO:" + p_event_path.ToString());
        }
    }

    public void NotifyMonoContext<T, T1>(T p_event_path, UnityEngine.Object p_target = null, params object[] p_data) where T1 : AppMonoController
    {
        var tempController = GetNav<T1>(CONTROLLER_TYPE.ALL);
        tempController.OnNotification(p_event_path as object, p_target, p_data);
    }

    public void NotifyGroup<T>(T p_event_path, CONTROLLER_TYPE group, UnityEngine.Object p_target = null, params object[] p_data)
    {
        var tempControllers = GetNavs(group);

        foreach(var controller in tempControllers)
        {
            controller.OnNotification(p_event_path as object, p_target, p_data);
        }
    }

    public void NotifyContext<T, T2>(T p_event_path, UnityEngine.Object p_target = null, params object[] p_data) where T2 : AppController
    {
        var tempController = GetController<T2>(CONTROLLER_TYPE.ALL);
        tempController.OnNotification(p_event_path as object, p_target, p_data);
    }

    public void SwitchNavController<T>(CONTROLLER_TYPE controllerType) where T : UIViewControllerBase
    {

        if (controllerType == CONTROLLER_TYPE.ALL)
        {
            foreach(var item in _currentNav)
            {
                if (item.Value != null)
                {
                    item.Value.OnNavigationExit();
                }
            }
        }
        else if (_currentNav.ContainsKey(controllerType) && _currentNav[controllerType] != null)
        {
            _currentNav[controllerType].OnNavigationExit();
        }

        var temp = GetNav<T>(controllerType);
        if (temp != null)
        {
            OnNavLog?.Invoke(temp);

            _currentNav[controllerType] = null;
            _currentNav[temp.controllerType] = temp;
            temp.OnNavigationEnter();
        }
        else
        {
            _currentNav[controllerType] = null;
        }
    }

    public UIViewControllerBase GetCurrentViewController(CONTROLLER_TYPE controllerType)
    {
        if (!_currentNav.ContainsKey(controllerType)) return null;
        return _currentNav[controllerType];
    }

    public void ActivateNavController<T>() where T : UIViewControllerBase
    {
        var temp = GetNav<T>(CONTROLLER_TYPE.ALL);
        if (temp != null)
        {
            if (!temp.enableSwitch)
            {
                temp.OnNavigationActivate();
            }
            OnNavLog?.Invoke(temp);
        }
    }

    public void DeactivateNavController<T>() where T : UIViewControllerBase
    {
        var temp = GetNav<T>(CONTROLLER_TYPE.ALL);
        if (temp != null)
        {
            if (!temp.enableSwitch)
            {
                temp.OnNavigationDeativate();
            }
            OnNavLog?.Invoke(temp);
        }
    }

    private T GetNav<T>(CONTROLLER_TYPE controllerType) where T : AppMonoController
    {
        var temp2 = monocontroller_list;
        for (int i = 0; i < temp2.Count; i++)
        {
            if (temp2[i] == null) continue;
            if ((controllerType == CONTROLLER_TYPE.ALL || temp2[i].controllerType == controllerType) && temp2[i].GetType() == typeof(T))
            {
                return temp2[i] as T;
            }
        }
        return default(T);
    }

    private List<AppMonoController> GetNavs(CONTROLLER_TYPE controllerType)
    {
        var result = new List<AppMonoController>();

        var temp2 = monocontroller_list;
        for (int i = 0; i < temp2.Count; i++)
        {
            if (temp2[i] == null) continue;
            if (temp2[i].controllerType == controllerType)
            {
                result.Add(temp2[i]);
            }
        }
        return result;
    }

    private T GetController<T>(CONTROLLER_TYPE controllerType) where T : AppController
    {
        var temp2 = monocontroller_list;
        for (int i = 0; i < temp2.Count; i++)
        {
            if (temp2[i] == null) continue;
            if ((controllerType == CONTROLLER_TYPE.ALL || temp2[i].controllerType == controllerType) && temp2[i].GetType() == typeof(T))
            {
                return temp2[i] as T;
            }
        }
        return default(T);
    }

    static Dictionary<System.Type, UIView> uiviewList = new Dictionary<System.Type, UIView>();
    static Dictionary<System.Type, UI3DView> ui3dviewList = new Dictionary<System.Type, UI3DView>();

    public void RegiterView<T>(T ui) where T : UIView
    {
        var temp = ui.GetType();
        if (!uiviewList.ContainsKey(temp))
        {
            uiviewList.Add(temp, ui);
        }
    }

    public T GetView<T>() where T : UIView
    {
        var temp = typeof(T);

        if (uiviewList.ContainsKey(temp))
        {
            return uiviewList[temp] as T;
        }
    
        return default;
    }

    public T Get3DView<T>() where T : UI3DView
    {
        var temp = typeof(T);

        if (ui3dviewList.ContainsKey(temp))
        {
            return ui3dviewList[temp] as T;
        }

        return default;
    }

    public void GetView<T>(Action<T> action) where T : UIView
    {
        var temp = typeof(T);

        if (uiviewList.ContainsKey(temp))
        {
            action?.Invoke(uiviewList[temp] as T);
        }
    }

    public bool IsCurrentView<T>(long controllerId) where T : UIView
    {
        foreach (var view in uiviewList)
        {
            Debug.Log($"IsCurrent: {view.Value.gameObject.name} {view.Value.IsCurrent}");
            if (view.Value != null && (view.Value.controllerId == controllerId || controllerId == -1) && view.Value.IsCurrent)
            {
                return view.Key == typeof(T);
            }
        }
        return false;
    }

    public List<UIView> GetViews(long controllerId)
    {
        var response = new List<UIView>();

        foreach(var view in uiviewList)
        {
            if (view.Value != null && view.Value.controllerId == controllerId)
            {
                response.Add(view.Value);
            }
        }

        return response;
    }

    public void HideViews<T>(T thisView, long controllerdId) where T : UIView
    {
        foreach(var ui in uiviewList)
        {
            if (ui.Value.GetType() != typeof(T) && ui.Value.controllerId == controllerdId)
            {
                ui.Value.Dismiss();
            }
        }
    }

    public void HideViews(long controllerdId)
    {
        foreach (var ui in uiviewList)
        {
            if (ui.Value.controllerId == controllerdId)
            {
                ui.Value.Dismiss();
            }
        }
    }

    public void RegisterController(AppController newcontroller)
    {
        controller_list.Add(newcontroller);
    }

    //public void NotifySystem(NOTIFYSYSTEM p_event_path, UnityEngine.Object p_target, params object[] p_data)
    //{
    //    var temp = controller_list;
    //    for (int i = 0; i < temp.Count; i++)
    //    {
    //        if (temp[i] == null) continue;
    //        temp[i].OnSystem(p_event_path, p_target, p_data);
    //    }
    //}

}

public class MVCC
{
    public static Action OnStart;

    static App _app;
    public static App app {  get { return _app; } }
    static bool _isReady = false;
    public static bool IsReady {  get { return _isReady; } }
    static IAnimate _anim;
    public static IAnimate animate { get { return _anim; } }

    static IAnimate _anim3d;
    public static IAnimate animate3d { get { return _anim3d; } }

    static HttpHelper _httpHelper;
    public static HttpHelper httpHelper { get { return _httpHelper; } }

    public static void RegisterApp(App newApp)
    {
        _app = newApp;
        _isReady = true;    
    }

    public static void RegisterAnim(IAnimate anim)
    {
        _anim = anim;
    }

    public static void RegisterAnim3d(IAnimate anim)
    {
        _anim3d = anim;
    }

    public static void RegisterHttpHelper(HttpHelper helper)
    {
        _httpHelper = helper;
    }
}

public interface IUIView
{
    bool IsCurrent { get; set; }
    void Present();
    void Dismiss();
    void Hide();
    void SetModel();
}


public class Obsever<T>
{

    List<T> subscriptions = new List<T>();

    public System.Action<T> registerCall;

    public void Publish()
    {
        foreach (var s in subscriptions)
        {
            registerCall?.Invoke(s);
        }
    }
      
    public void Subscribe(T newSub)
    {
        subscriptions.Add(newSub);
        registerCall?.Invoke(newSub);
    }
}



public class AppSystem : ISystem
{
    public void Setup() { }
    public void Start() { }
    public void Update() { }
    public void Destroy() { }
}

public interface ISystem
{

    void Setup();
    void Start();
    void Update();
    void Destroy();

}


