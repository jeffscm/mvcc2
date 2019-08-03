using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyAnim : IAnimate
{

    public void FadeOut(CanvasGroup cg, Action onComplete = null, float limit = 0f, float speed = 0.25f)
    {

        //if (deactivateOnOut)
        //{
            if (!cg.gameObject.activeInHierarchy) return;
        //}

        cg.gameObject.SetActive(true);
        LeanTween.cancel(cg.gameObject);

        LeanTween.alphaCanvas(cg, limit, speed).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();

            //if (deactivateOnOut)
            cg.gameObject.SetActive(false);

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
        //var v = rectTrans.anchoredPosition;
        //v.x = -Math.Abs(rectTrans.rect.width / 2f);
        //rectTrans.anchoredPosition = v;

        LeanTween.cancel(cg.gameObject);
        //if (cg != null) LeanTween.alphaCanvas(cg, 1f, speed);
        LeanTween.moveX(rectTrans, 0, speed).setDelay(delay).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });

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

    public void MoveXOut(CanvasGroup cg, bool onOut, bool toRight = true, Action onComplete = null, float speed = 0.35f, float delay = 0.1f)
    {

        if (onOut)
        {
            if (!cg.gameObject.activeInHierarchy) return;
        }

        var rectTrans = (RectTransform)cg.gameObject.transform;
        float f = Math.Abs(rectTrans.rect.width);

        cg.gameObject.SetActive(true);
        LeanTween.cancel(cg.gameObject);
        //if (cg != null) LeanTween.alphaCanvas(cg, 0f, speed);
        LeanTween.moveX(rectTrans, f, speed).setDelay(delay).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();

            if (onOut)
            cg.gameObject.SetActive(false);

        });

    }

    public void MoveYIn(CanvasGroup cg, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveYOut(CanvasGroup cg, bool onOut, bool toBottom = true, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveYOutInstant(CanvasGroup cg, bool toBottom = true) { }

    public void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
}
