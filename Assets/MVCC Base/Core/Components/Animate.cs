﻿/*
Jefferson Scomacao
https://www.jscomacao.com

GitHub - Source Code
Project: MVCC2.0

Copyright (c) 2015 Jefferson Raulino Scomação

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class Animate : IAnimate
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

    }

    public void MoveYIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }
    public void MoveYOut(CanvasGroup cg, bool onOut, AnimateSettings settings, bool toBottom = true, Action onComplete = null) { }
    public void MoveYOutInstant(CanvasGroup cg, bool toBottom = true) { }

    public void MoveX(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }
    public void MoveY(CanvasGroup cg, float add, Action onComplete = null, float speed = 0.35f, float delay = 0.1f) { }

    public void ScaleOut(CanvasGroup cg, bool onOut, AnimateSettings settings, Action onComplete = null) { }
    public void ScaleIn(CanvasGroup cg, AnimateSettings settings, Action onComplete = null) { }


}
