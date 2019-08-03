using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class Animate : IAnimate
{

    public void FadeOut(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }
    public void FadeIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }
    public void MoveXIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null)
    {
       
    }

    public void MoveXOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toRight = true, Action onComplete = null)
    {

    }

    public void MoveXOutInstant(CanvasGroup cg, bool toRight = true)
    {

    }

    public void MoveYIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }
    public void MoveYOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toBottom = true, Action onComplete = null) { }
    public void MoveYOutInstant(CanvasGroup cg, bool toBottom = true) { }

    public void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }

}
