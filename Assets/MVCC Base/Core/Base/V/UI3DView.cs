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

    public bool IsCurrent { get; set; }

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
        IsCurrent = true;
    }

    public void Dismiss()
    {
        navAnimate?.AnimateOut();
        IsCurrent = false;
    }

    public void Hide()
    {
        navAnimate?.AnimateOutInstant();
        IsCurrent = false;
    }

    public virtual void SetModel() { }

    public virtual void OnNavigationEnter() { }

    public virtual void OnNavigationExit() { }

    public virtual void OnNavigationActivate() { }

    public virtual void OnNavigationDeativate() { }

}
