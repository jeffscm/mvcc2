using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : AppElement, IUIView
{
    [HideInInspector]
    [SerializeField]
    public long controllerId;

    [HideInInspector]
    [SerializeField]
    public NavAnimate navAnimate;

    public virtual void Awake()
    {
        App.uiviewObsever.Subscribe(this);
        navAnimate.Setup();
        SetModel();
    }

    public void Present()
    {
        app.HideViews(this, controllerId);
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

    public virtual void OnNavigationDeactivate() { }

}
