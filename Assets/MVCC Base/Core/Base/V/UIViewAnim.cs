using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIViewAnim : AppElement
{

    public UIView uiView;

    public AnimateSettings animateIn;
    public AnimateSettings animateOut;
    public CanvasGroup cg;
    public bool deactivateOnOut = false;

    public Action onAnimateIn, onAnimateOut;

    private void Awake()
    {
        if (uiView != null)
        {
            uiView.OnDismiss += () =>
            {
                DoAnimateOut();
            };

            uiView.OnPresent += () =>
            {
                DoAnimateIn();
            };
        }
    }

    public void DoAnimateIn()
    {
        onAnimateIn?.Invoke();
        Debug.Log($"Anim in {animateIn.animateType}");
        switch (animateIn.animateType)
        {
            case NAVANIM.FADE:
                MVCC.animate.FadeIn(cg, animateIn, null);
                break;
            case NAVANIM.MOVEBOTTOM:
            case NAVANIM.MOVEUP:
                MVCC.animate.MoveYIn(cg, animateIn, null);
                break;
            case NAVANIM.MOVELEFT:
            case NAVANIM.MOVERIGHT:
                MVCC.animate.MoveXIn(cg, animateIn, null);
                break;
            case NAVANIM.SCALE:
                MVCC.animate.ScaleIn(cg, animateIn, null);
                break;
        }
    }

    public void DoAnimateOut()
    {
        onAnimateOut?.Invoke();
        switch (animateOut.animateType)
        {
            case NAVANIM.FADE:
                MVCC.animate.FadeOut(cg, deactivateOnOut, animateOut, null);
                break;
            case NAVANIM.MOVELEFT:
                MVCC.animate.MoveXOut(cg, deactivateOnOut, animateOut, false, null);
                break;
            case NAVANIM.MOVERIGHT:
                MVCC.animate.MoveXOut(cg, deactivateOnOut, animateOut, true, null);
                break;
            case NAVANIM.MOVEBOTTOM:
                MVCC.animate.MoveYOut(cg, deactivateOnOut, animateOut, true, null);
                break;
            case NAVANIM.MOVEUP:
                MVCC.animate.MoveYOut(cg, deactivateOnOut, animateOut, false, null);
                break;
            case NAVANIM.SCALE:
                MVCC.animate.ScaleOut(cg, deactivateOnOut, animateOut, null);
                break;
        }
    }

#if UNITY_EDITOR
    public void SetDefaultOut()
    {
        switch (animateOut.animateType)
        {
            case NAVANIM.FADE:
                cg.alpha = animateOut.fade;

                break;
            case NAVANIM.MOVELEFT:
                break;
            case NAVANIM.MOVERIGHT:
                break;
            case NAVANIM.MOVEBOTTOM:
                break;
            case NAVANIM.MOVEUP:
                break;
            case NAVANIM.SCALE:
                this.transform.localScale = animateOut.scale * Vector3.one;
                if (animateOut.useFade) cg.alpha = animateOut.fade;
                break;
        }

        cg.blocksRaycasts = animateOut.cgActive;
    }
#endif
}
