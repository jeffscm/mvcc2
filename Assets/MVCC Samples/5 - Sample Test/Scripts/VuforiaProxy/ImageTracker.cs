using System.Collections;
using System.Collections.Generic;
using MVC.Proxy;
using UnityEngine;
#if VUFORIA
using Vuforia;

public class ImageTracker : ImageTrackerBase<NOTIFYVUFORIA>
{

    private bool _hasTracking = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTrackingFound()
    {
        app.Notify(OnNotifyFound, this);
        _hasTracking = true;
    }

    protected override void OnTrackingLost()
    {
        app.NotifyGroup(OnNotifyLost, CONTROLLER_TYPE.PROXY, this);
        _hasTracking = false;
    }

    private void Update()
    {
        if (_hasTracking)
        {
            ImageTrackingProxy.OnRealTimeUpdate?.Invoke(this.transform);
        }
    }

}
#endif