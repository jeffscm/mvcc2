using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Nav3DAnimate : MonoBehaviour, INavAnimate
{

    public NAVANIM animateIn;
    public NAVANIM animateOut;

    public bool deactivateOnOut = false;

    public virtual void AnimateIn(Action onComplete = null)
    {
        //MVCC.animate3d.MoveXIn();
    }

    public virtual void AnimateOut(Action onComplete = null)
    {
        //MVCC.animate3d.MoveXOut();
    }

    public virtual void AnimateOutInstant()
    {
        //MVCC.animate3d.MoveXOutInstant(null);
    }

    public virtual void Setup(Action onComplete = null)
    {

    }
}