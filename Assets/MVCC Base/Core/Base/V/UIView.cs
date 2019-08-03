using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIView : AppElement, IUIView
{
    public bool IsCurrent { get; set; } = false;

    [HideInInspector]
    [SerializeField]
    public long controllerId;

    [HideInInspector]
    [SerializeField]
    public NavAnimate navAnimate;

    public Action OnPresent, OnDismiss;

    public virtual void Awake()
    {
        App.uiviewObsever.Subscribe(this);
        navAnimate.Setup();
        SetModel();
    }

    public void Present()
    {
        Debug.Log($"This UIView {IsCurrent}");
        app.HideViews(this, controllerId);
        navAnimate?.AnimateIn( () => {
            OnPresent?.Invoke();
        });
        IsCurrent = true;

    }

    public void Dismiss()
    {
        OnDismiss?.Invoke();
        navAnimate?.AnimateOut();
        IsCurrent = false;
    }

    public void Hide()
    {
        navAnimate?.AnimateOutInstant();
        IsCurrent = false;
        OnDismiss?.Invoke();
    }

    private void OnDestroy()
    {
        IsCurrent = false;
    }

    public virtual void SetModel() { }

    public virtual void OnNavigationEnter() { }

    public virtual void OnNavigationExit() { }

    public virtual void OnNavigationActivate() { }

    public virtual void OnNavigationDeactivate() { }

}
