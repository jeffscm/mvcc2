using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResponder<T> : UIView
{

    public T reponderNotify;

    public bool respondAtStart = false;

    public sealed override void OnNavigationActivate()
    {
        base.OnNavigationActivate();
        if (respondAtStart)
        {
            app.Notify(reponderNotify, null, null);
        }
    }

    public sealed override void OnNavigationEnter()
    {
        base.OnNavigationEnter();
        if (respondAtStart)
        {
            app.Notify(reponderNotify, null, null);
        }
    }

}
