using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class NavAnimate : MonoBehaviour, INavAnimate
{

    public AnimateSettings animateIn;
    public AnimateSettings animateOut;



    public CanvasGroup cg;

    public bool deactivateOnOut = false;

    public bool startOffScreen = false;

    public bool doSetNewOffset = false;

    public Vector2 offsetRectAdd;

    public virtual void AnimateIn(Action onComplete = null)
    {

        switch (animateIn.animateType)
        {
            case NAVANIM.FADE:
                MVCC.animate.FadeIn(cg, animateIn, onComplete);
                break;
            case NAVANIM.MOVEBOTTOM:
            case NAVANIM.MOVEUP:
                MVCC.animate.MoveYIn(cg, animateIn, onComplete);
                break;
            case NAVANIM.MOVELEFT:
            case NAVANIM.MOVERIGHT:
                MVCC.animate.MoveXIn(cg, animateIn, onComplete);
                break;
        }
    }

    public virtual void AnimateOut(Action onComplete = null)
    {
        switch (animateOut.animateType)
        {
            case NAVANIM.FADE:
                MVCC.animate.FadeOut(cg, animateOut, onComplete);
                break;
            case NAVANIM.MOVELEFT:
                MVCC.animate.MoveXOut(cg, deactivateOnOut, animateOut, false, onComplete);
                break;
            case NAVANIM.MOVERIGHT:
                MVCC.animate.MoveXOut(cg, deactivateOnOut, animateOut, true, onComplete);
                break;
            case NAVANIM.MOVEBOTTOM:
                MVCC.animate.MoveYOut(cg, deactivateOnOut, animateOut, true, onComplete);
                break;
            case NAVANIM.MOVEUP:
                MVCC.animate.MoveYOut(cg, deactivateOnOut, animateOut, false, onComplete);
                break;

        }
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
            float rectSize = 0;
            float f = 0;

            switch (animateOut.animateType)
            {
                case NAVANIM.MOVELEFT:
                    rectSize = -rectTrans.rect.width;
                    f = (animateOut.animateDistance == 0f) ? rectSize : animateOut.animateDistance;
                    v.x = f;
                    break;
                case NAVANIM.MOVERIGHT:
                    rectSize = rectTrans.rect.width;
                    f = (animateOut.animateDistance == 0f) ? rectSize : animateOut.animateDistance;
                    v.x = f;
                    break;
                case NAVANIM.MOVEBOTTOM:
                    rectSize = -rectTrans.rect.height;
                    f = (animateOut.animateDistance == 0f) ? rectSize : animateOut.animateDistance;
                    v.y = f;
                    break;
                case NAVANIM.MOVEUP:
                    rectSize = rectTrans.rect.height;
                    f = (animateOut.animateDistance == 0f) ? rectSize : animateOut.animateDistance;
                    v.y = f;
                    break;
            }

            rectTrans.anchoredPosition = v;
            if (deactivateOnOut)
            {
                this.gameObject.SetActive(false);
            }
        }
        onComplete?.Invoke();
    }


}


[Serializable]
public class AnimateSettings
{
    public NAVANIM animateType;
    public float animateDistance;
    public LeanTweenType tweenType = LeanTweenType.easeInOutQuad;
    public float time = 0.35f;
    public float delay = 0f;
    public float fade = 0f;
}