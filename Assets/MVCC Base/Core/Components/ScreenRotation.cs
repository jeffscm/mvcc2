using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotation : MonoBehaviour
{
    static ScreenRotation _instance;

    public static DeviceOrientation CurrentRotation
    {
        get
        {
#if UNITY_EDITOR
            return _instance?.testScreenRotation ?? DeviceOrientation.Portrait;
#else
            return _currentRotation;
#endif
        }
    }

    static DeviceOrientation _currentRotation;

    public static Action<DeviceOrientation> OnRotationChange;

    public DeviceOrientation testScreenRotation;

    private void OnEnable()
    {
        _instance = this;
        _currentRotation = Input.deviceOrientation;
    }

    void Start()
    {
        InvokeRepeating(nameof(CheckRotation), 0.5f, 0.5f);
#if UNITY_EDITOR
        _currentRotation = testScreenRotation;
        OnRotationChange?.Invoke(_currentRotation);
#else
        _currentRotation = Input.deviceOrientation;
        OnRotationChange?.Invoke(_currentRotation);
#endif
    }

    void CheckRotation()
    {
#if !UNITY_EDITOR
        if (Input.deviceOrientation != _currentRotation)
        {
            Debug.Log($"New Rotation: {Input.deviceOrientation}");
            _currentRotation = Input.deviceOrientation;
            OnRotationChange?.Invoke(_currentRotation);
        }
#endif
    }
}
