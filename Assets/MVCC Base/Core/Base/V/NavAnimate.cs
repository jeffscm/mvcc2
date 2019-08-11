﻿using System.Collections;
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
    public List<MediaQueryItem> mediaQuery = new List<MediaQueryItem>();

    public CanvasGroup cg;

    public bool deactivateOnOut = false;
    public bool startOffScreen = false;

    Vector2 min, max;
    bool _hasMinMax = false;
    void Awake()
    {
        SetInitialMinMax();
        ScreenRotation.OnRotationChange += ProcessMediaQuery;
    }

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
                MVCC.animate.FadeOut(cg, deactivateOnOut, animateOut, onComplete);
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
        SetInitialMinMax();
        ProcessMediaQuery(ScreenRotation.CurrentRotation);

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

    void ProcessMediaQuery(DeviceOrientation newOrientation)
    {
        var rectTrans = transform as RectTransform;

        foreach (var mq in mediaQuery)
        {
            if (mq.orientation == DeviceOrientation.Unknown || newOrientation == mq.orientation)
            {
                if (mq.useOffset && ( string.IsNullOrEmpty(mq.searchGeneration) || Device.generation.ToString().ToLower().IndexOf(mq.searchGeneration.ToLower()) > -1))
                {
                    rectTrans.offsetMin = new Vector2(min.x + mq.offset.x, min.y + mq.offset.y);
                    rectTrans.offsetMax = new Vector2(max.x - mq.offset.x, max.y - mq.offset.y);
                }
            }
        }
    }

    void SetInitialMinMax()
    {
        if (_hasMinMax) return;

        _hasMinMax = true;
        var rectTrans = transform as RectTransform;
        min = rectTrans.offsetMin;
        max = rectTrans.offsetMax;

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

    public bool useFade = false;
    public float fade = 0f;

    //public bool useScale = false;
    public float scale = 0f;
    public bool cgActive = true;
}

[Serializable] 
public class MediaQueryItem
{
    public DeviceOrientation orientation = DeviceOrientation.Unknown;
    public float screenRatio = -1;
    public string searchGeneration = string.Empty;
    public Vector2 offset;
    public bool useOffset = false;
}