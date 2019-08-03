using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAnimate
{
    void FadeOut(CanvasGroup cg, Action onComplete = null, float limit = 0f, float speed = 0.25f);
    void FadeIn(CanvasGroup cg, Action onComplete = null, float limit = 1f, float speed = 0.25f);
    void MoveXIn(CanvasGroup cg, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
    void MoveXOut(CanvasGroup cg, bool onOut, bool toRight = true, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
    void MoveXOutInstant(CanvasGroup cg, bool toRight = true);

    void MoveYIn(CanvasGroup cg, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
    void MoveYOut(CanvasGroup cg, bool onOut, bool toBottom = true, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
    void MoveYOutInstant(CanvasGroup cg, bool toBottom = true);

    void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);
    void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f);


}
