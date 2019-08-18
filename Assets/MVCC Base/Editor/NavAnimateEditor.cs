using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavAnimate), true)]
public class NavAnimateEditor : Editor
{

    NavAnimate _instance;

    void OnEnable()
    {
        _instance = target as NavAnimate;


        var src = _instance.GetComponent<CanvasGroup>();
        if (src == null)
        {
            var a = _instance.gameObject.AddComponent<CanvasGroup>();
            _instance.cg = a;
            var r = _instance.transform as RectTransform;
            r.anchorMin = Vector2.zero;
            r.anchorMax = Vector2.one;
            r.sizeDelta = Vector2.zero;
        }

        if (_instance.animateOut.animateType == NAVANIM.NO_ANIM)
        {
            _instance.deactivateOnOut = false;
        }

    }
}

