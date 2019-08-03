using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface INavAnimate
{
    void AnimateIn(Action onComplete = null);
    void AnimateOut(Action onComplete = null);
    void AnimateOutInstant();
    void Setup(Action onComplete = null);
}
