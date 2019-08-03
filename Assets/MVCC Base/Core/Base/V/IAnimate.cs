using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAnimate
{
    void FadeOut(CanvasGroup cg, AnimateSettings settings, Action onComplete = null);
    void FadeIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null);

    void MoveXIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null);
    void MoveXOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toRight = true, Action onComplete = null);
    void MoveXOutInstant(CanvasGroup cg, bool toRight = true);

    void MoveYIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null);
    void MoveYOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toBottom = true, Action onComplete = null);
    void MoveYOutInstant(CanvasGroup cg, bool toBottom = true);

    void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
    void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
}
