using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace VuforiaSample
{
    public class Vuforia_MyAnim : IAnimate
    {

        public void FadeOut(CanvasGroup cg, bool onOut, AnimateSettings settings, Action onComplete = null)
        {

            //if (deactivateOnOut)
            //{
            if (!cg.gameObject.activeInHierarchy) return;
            //}
            cg.blocksRaycasts = settings.cgActive;
            cg.gameObject.SetActive(true);
            LeanTween.cancel(cg.gameObject);

            LeanTween.alphaCanvas(cg, settings.fade, settings.time).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();

                if (onOut) cg.gameObject.SetActive(false);
            }).setDelay(settings.delay).setEase(settings.tweenType);
        }

        public void FadeIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) 
        {
            cg.blocksRaycasts = settings.cgActive;
            cg.gameObject.SetActive(true);
            LeanTween.cancel(cg.gameObject);

            LeanTween.alphaCanvas(cg, settings.fade, settings.time).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();
            }).setDelay(settings.delay).setEase(settings.tweenType);

        }

        public void MoveXIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null)
        {
            cg.blocksRaycasts = settings.cgActive;
            cg.gameObject.SetActive(true);
            var rectTrans = (RectTransform)cg.gameObject.transform;
            //var v = rectTrans.anchoredPosition;
            //v.x = -Math.Abs(rectTrans.rect.width / 2f);
            //rectTrans.anchoredPosition = v;

            LeanTween.cancel(cg.gameObject);
            //if (cg != null) LeanTween.alphaCanvas(cg, 1f, speed);
            LeanTween.moveX(rectTrans, settings.animateDistance, settings.time).setDelay(settings.delay).setEase(settings.tweenType).setOnComplete(() =>
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

        public void MoveXOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toRight = true, Action onComplete = null)
        {

            if (onOut)
            {
                if (!cg.gameObject.activeInHierarchy) return;
            }
            cg.blocksRaycasts = settings.cgActive;
            var rectTrans = (RectTransform)cg.gameObject.transform;

            float rectWidth = (toRight) ? rectTrans.rect.width : -rectTrans.rect.width;

            float f = (settings.animateDistance == 0f) ? rectWidth : settings.animateDistance;

            cg.gameObject.SetActive(true);
            LeanTween.cancel(cg.gameObject);
            //if (cg != null) LeanTween.alphaCanvas(cg, 0f, speed);
            LeanTween.moveX(rectTrans, f, settings.time).setDelay(settings.delay).setEase(settings.tweenType).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();

                if (onOut)
                    cg.gameObject.SetActive(false);

            });

        }

        public void MoveYIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null)
        {
            cg.gameObject.SetActive(true);
            var rectTrans = (RectTransform)cg.gameObject.transform;
            //var v = rectTrans.anchoredPosition;
            //v.x = -Math.Abs(rectTrans.rect.width / 2f);
            //rectTrans.anchoredPosition = v;
            cg.blocksRaycasts = settings.cgActive;
            LeanTween.cancel(cg.gameObject);
            //if (cg != null) LeanTween.alphaCanvas(cg, 1f, speed);
            LeanTween.moveY(rectTrans, settings.animateDistance, settings.time).setDelay(settings.delay).setEase(settings.tweenType).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();
            });
        }

        public void MoveYOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toBottom = true, Action onComplete = null) 
        {
            if (onOut)
            {
                if (!cg.gameObject.activeInHierarchy) return;
            }
            cg.blocksRaycasts = settings.cgActive;
            var rectTrans = (RectTransform)cg.gameObject.transform;

            float rectHeight = (toBottom) ? rectTrans.rect.height : -rectTrans.rect.height;

            float f = (settings.animateDistance == 0f) ? rectHeight : settings.animateDistance;

            cg.gameObject.SetActive(true);
            LeanTween.cancel(cg.gameObject);
            //if (cg != null) LeanTween.alphaCanvas(cg, 0f, speed);
            LeanTween.moveY(rectTrans, f, settings.time).setDelay(settings.delay).setEase(settings.tweenType).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();

                if (onOut)
                    cg.gameObject.SetActive(false);

            });

        }

        public void MoveYOutInstant(CanvasGroup cg, bool toBottom = true) { }

        public void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
        public void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }

        public void ScaleOut(CanvasGroup cg, bool onOut, AnimateSettings settings, Action onComplete = null) {

            //if (deactivateOnOut)
            //{
            if (!cg.gameObject.activeInHierarchy) return;
            //}

            LeanTween.cancel(cg.gameObject);

            cg.blocksRaycasts = settings.cgActive;

            LeanTween.scale(cg.gameObject, settings.scale * Vector3.one, settings.time).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();

                if (onOut) cg.gameObject.SetActive(false);

            }).setDelay(settings.delay).setEase(settings.tweenType);

            if (settings.useFade)
            {
                LeanTween.alphaCanvas(cg, settings.fade, settings.time).setDelay(settings.delay);
            }
        }
        public void ScaleIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null)
        {
            cg.gameObject.SetActive(true);

            cg.blocksRaycasts = settings.cgActive;

            LeanTween.cancel(cg.gameObject);

            LeanTween.scale(cg.gameObject, settings.scale * Vector3.one, settings.time).setOnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();
            }).setDelay(settings.delay).setEase(settings.tweenType);

            if (settings.useFade)
            {
                LeanTween.alphaCanvas(cg, settings.fade, settings.time).setDelay(settings.delay);
            }

        }

    }
}