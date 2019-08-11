using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyAnim : IAnimate
{

    public void FadeOut(CanvasGroup cg, bool onOut, AnimateSettings settings, Action onComplete = null) { }
    public void FadeIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }


    public void MoveXIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null)
    {

    }

    public void MoveXOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toRight = true, Action onComplete = null)
    {

    }

    public void MoveXOutInstant(CanvasGroup cg, bool toRight = true)
    {
        if (!cg.gameObject.activeInHierarchy) return;
        var rectTrans = (RectTransform)cg.gameObject.transform;
        var v = rectTrans.anchoredPosition;
        v.x = Math.Abs(rectTrans.rect.width);
        LeanTween.cancel(cg.gameObject);
        rectTrans.anchoredPosition = v;
        cg.gameObject.SetActive(false);
    }

  

    public void MoveYIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }
    public void MoveYOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toBottom = true, Action onComplete = null) { }
    public void MoveYOutInstant(CanvasGroup cg, bool toBottom = true) { }

    public void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }

    public void ScaleOut(CanvasGroup cg, bool onOut, AnimateSettings settings, Action onComplete = null) { }
    public void ScaleIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }

}
