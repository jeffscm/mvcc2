using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewAnim : AppElement
{

    public UIView uiView;

    public AnimateSettings animateIn;
    public AnimateSettings animateOut;
    public CanvasGroup cg;
    public bool deactivateOnOut = false;

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
        }
    }

    public void DoAnimateOut()
    {
        switch (animateOut.animateType)
        {
            case NAVANIM.FADE:
                MVCC.animate.FadeOut(cg, animateOut, null);
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
        }
    }
}
