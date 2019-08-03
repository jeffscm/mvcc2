using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DView : AppElement, IUIView
{
    [HideInInspector]
    [SerializeField]
    public long controllerId;

    [HideInInspector]
    [SerializeField]
    public Nav3DAnimate navAnimate;

    public virtual void Awake()
    {
        Debug.Log("Reg View3d:" + name);
        App.ui3dviewObsever.Subscribe(this);
        navAnimate.Setup();
        SetModel();
    }

    public void Present()
    {
        navAnimate?.AnimateIn();
    }

    public void Dismiss()
    {
        navAnimate?.AnimateOut();
    }

    public void Hide()
    {
        navAnimate?.AnimateOutInstant();
    }

    public virtual void SetModel() { }

    public virtual void OnNavigationEnter() { }

    public virtual void OnNavigationExit() { }

    public virtual void OnNavigationActivate() { }

    public virtual void OnNavigationDeativate() { }

}
