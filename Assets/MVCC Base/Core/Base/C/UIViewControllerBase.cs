using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewControllerBase : AppMonoController
{
    public bool enableSwitch = true;

    public sealed override bool OnNavigationSwitch(int controllerId)
    {
        if (enableSwitch)
        {
            if (this.controllerId == controllerId)
            {
                OnNavigationEnter();
            }
            else
            {
                OnNavigationExit();
            }
        }

        return true;
    }

    public virtual void OnNavigationEnter() 
    {    

    }

    public virtual void OnNavigationExit() 
    {

    }

    public virtual void OnNavigationActivate() 
    {

    }

    public virtual void OnNavigationDeativate()
    {
       

    }

}

