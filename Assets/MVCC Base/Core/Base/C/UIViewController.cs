using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewController : UIViewControllerBase
{
 
    public override void OnNavigationEnter() 
    {
        base.OnNavigationEnter();
        var list = app.GetViews(controllerId);
        foreach(var view in list)
        {
            view.OnNavigationEnter();
        }
    }

    public override void OnNavigationExit() 
    {
        base.OnNavigationExit();
        var list = app.GetViews(controllerId);
        foreach (var view in list)
        {
            view.OnNavigationExit();
        }
    }

    public override void OnNavigationActivate() 
    {
        base.OnNavigationActivate();
        var list = app.GetViews(controllerId);
        foreach (var view in list)
        {
            view.OnNavigationActivate();
        }
    }
        
    public override void OnNavigationDeativate()
    {
        base.OnNavigationDeativate();
        var list = app.GetViews(controllerId);
        foreach (var view in list)
        {
            view.OnNavigationDeactivate();
        }

    }

}

