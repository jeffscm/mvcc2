using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class Animate : IAnimate
{

    public void FadeOut(CanvasGroup cg, Action onComplete = null, float limit = 0f, float speed = 0.25f)
    {

        //if (deactivateOnOut)
        //{
        //    if (!gameObject.activeInHierarchy) return;
        //}

        cg.gameObject.SetActive(true);
        LeanTween.cancel(cg.gameObject);

        LeanTween.alphaCanvas(cg, limit, speed).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();

            //if (deactivateOnOut)
                //this.gameObject.SetActive(false);

        });

    }
    public void FadeIn(CanvasGroup cg, Action onComplete = null, float limit = 1f, float speed = 0.25f)
    {
        cg.gameObject.SetActive(true);
        LeanTween.cancel(cg.gameObject);
       
        LeanTween.alphaCanvas(cg, limit, speed).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });

    }

  
 

    public void MoveXIn(CanvasGroup cg, Action onComplete = null, float speed = 0.35f, float delay = 0.1f)
    {
        cg.gameObject.SetActive(true);
        var rectTrans = (RectTransform)cg.gameObject.transform;
        var v = rectTrans.anchoredPosition;
        v.x = Screen.width * 2f;
        rectTrans.anchoredPosition = v;

        LeanTween.cancel(cg.gameObject);
        if (cg != null) LeanTween.alphaCanvas(cg, 1f, speed);
        LeanTween.moveX(rectTrans, 0, speed).setDelay(delay).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });

    }

    public void MoveXOut(CanvasGroup cg, bool onOut, bool toRight = true, Action onComplete = null, float speed = 0.35f, float delay = 0.1f)
    {

        //if (deactivateOnOut)
        //{
        //    if (!gameObject.activeInHierarchy) return;
        //}

        var rectTrans = (RectTransform)cg.gameObject.transform;
        float f = -Screen.width;

        cg.gameObject.SetActive(true);
        LeanTween.cancel(cg.gameObject);
        if (cg != null) LeanTween.alphaCanvas(cg, 0f, speed);
        LeanTween.moveX(rectTrans, f, speed).setDelay(delay).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();

            //if (deactivateOnOut)
                //this.gameObject.SetActive(false);

        });

    }

    public void MoveXOutInstant(CanvasGroup cg, bool toRight = true)
    {

    }

    public void MoveYIn(CanvasGroup cg, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveYOut(CanvasGroup cg, bool onOut, bool toBottom = true, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveYOutInstant(CanvasGroup cg, bool toBottom = true) { }

    public void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }

}
