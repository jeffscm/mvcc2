using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class NavAnimate : MonoBehaviour, INavAnimate
{

    public NAVANIM animateIn;
    public NAVANIM animateOut;
    public CanvasGroup cg;

    public bool deactivateOnOut = false;

    public bool startOffScreen = false;

    public bool doSetNewOffset = false;

    public Vector2 offsetRectAdd;

    public virtual void AnimateIn(Action onComplete = null)
    {
        MVCC.animate.MoveXIn(cg, onComplete);
        switch (animateIn)
        {
            case NAVANIM.MOVELEFT:
            case NAVANIM.MOVERIGHT:
               
                break;
            case NAVANIM.MOVEBOTTOM:
                break;
            case NAVANIM.MOVEUP:
                break;

        }
    }

    public virtual void AnimateOut(Action onComplete = null)
    {
        MVCC.animate.MoveXOut(cg, deactivateOnOut);
    }

    public virtual void AnimateOutInstant()
    {
        MVCC.animate.MoveXOutInstant(cg);
    }

    public virtual void Setup(Action onComplete = null)
    {
#if UNITY_IOS
        if (doSetNewOffset)
        {
#if !IPAD
            var rectTrans = transform as RectTransform;
            if (Device.generation.ToString().IndexOf("iPad") > -1)
#endif
            {
                rectTrans.offsetMin = new Vector2(rectTrans.offsetMin.x + offsetRectAdd.x, rectTrans.offsetMin.y + offsetRectAdd.y);
                rectTrans.offsetMax = new Vector2(rectTrans.offsetMax.x - offsetRectAdd.x, rectTrans.offsetMax.y - offsetRectAdd.y);
            }
        }   
#endif

        if (startOffScreen)
        {
            var rectTrans = transform as RectTransform;
            var v = rectTrans.anchoredPosition;
            v.x = rectTrans.rect.width;
            rectTrans.anchoredPosition = v;
            if (deactivateOnOut)
            {
                this.gameObject.SetActive(false);
            }
        }
        onComplete?.Invoke();
    }


}
