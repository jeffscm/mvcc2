using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class State<T1, T2> where T1 : IComparable
{
    private T1 _currentState = default;
    private Dictionary<T1, T2> _fireEvents = new Dictionary<T1, T2>();

    public Action<T2> onStateChange; 

    public T1 state 
    { 
    get
        {
            return _currentState;
        }
        set
        {
           
            bool diffState = value.CompareTo(_currentState) != 0;

            if (diffState)
            {
                _currentState = value;
                if (_fireEvents.ContainsKey(_currentState))
                {
                    onStateChange?.Invoke(_fireEvents[_currentState]);
                }
            }
        }
    }

    public void RegisterEvent(T1 ev, T2 notify)
    {
        _fireEvents[ev] = notify;
    }
}