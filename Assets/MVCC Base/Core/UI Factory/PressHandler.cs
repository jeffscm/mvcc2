using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class PressHandler<T> : MonoBehaviour , IPointerDownHandler, IPointerUpHandler {

    
    public Animate _anim;

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

        if (_anim != null && useFade) _anim.FadeOut(cg, null, 0.6f);


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
        if (_anim != null && useFade) _anim.FadeIn(cg);


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
}
