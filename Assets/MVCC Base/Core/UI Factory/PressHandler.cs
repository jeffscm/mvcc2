using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class PressHandler<T> : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public bool useFade = false;

    public bool Instant = false;

    public SOUNDTYPE playSound = SOUNDTYPE.NONE;

    public T notify;

    public T notifyOnUp;

    public CanvasGroup cg;

	public MonoBehaviour data;

	public bool reportOnUp = false;

    public bool detectDrag = false;

	public List<string> extraParams;


    Vector2 dragPos = Vector2.zero;

    public bool pushToTalk = false;

    public bool userFirstResponder = false;

    bool hasClickActive = false;

    UIResponder<T> _responder;

    [Space(10)]
    [Header("Is this a selection button?")]
    public bool useColor;
    public Color defaultColor;
    public Color selectedColor;
    public Image imageSelection;


    void OnEnable()
    {
        _responder = GetComponentInParent<UIResponder<T>>();
        if (_responder == null)
        {
            _responder = GetComponentInParent<UIResponder<T>>();
        }
    }

    void Execute(T eventNotify)
    {
        if (PressManager.instance.CanClick)
        {           
            if (useColor)
            {
                imageSelection.color = selectedColor;
            }

            if (userFirstResponder)
            {
                PressManager.instance.ProcessNotify(_responder.reponderNotify, data, extraParams);
            }
            else
            {
                PressManager.instance.ProcessNotify(eventNotify, data, extraParams);
            }
        }
        PressManager.instance.PointerExit();
    }


    public void OnPointerDown(PointerEventData eventData)
    {

        if (useFade) MVCC.animate.FadeOut(cg, false, new AnimateSettings() {
            time = 0.1f,
            fade = 0.5f
        });

        if (pushToTalk)
        {
            Execute(notify);
            return;
        }


        dragPos = eventData.position;
        if (reportOnUp){

            return;
        } 

        if (Instant)
        {
            if (playSound != SOUNDTYPE.NONE)
            {
                SoundManager.instance.Play(playSound);    
            }
            Execute(notify);
        }
        else 
        {
            hasClickActive = true;           
        }        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (useFade) MVCC.animate.FadeIn(cg, new AnimateSettings()
        {
            time = 0.1f,
            fade = 1f
        });


        if (pushToTalk)
        {
            Execute(notifyOnUp);
            return;
        }


		if (hasClickActive || reportOnUp)
        {

            if (detectDrag)
            {

                float f = Vector2.Distance(eventData.position, dragPos);
                //Debug.Log(f);
                if (f > 10)
                {
                    return;
                }

            }
            Execute(notify);
            if (playSound != SOUNDTYPE.NONE)
            {
                SoundManager.instance.Play(playSound);
            }
        }
    }

    public void ResetDefaultColor()
    {
        if (useColor)
        {
            imageSelection.color = defaultColor;
        }
    }
}
