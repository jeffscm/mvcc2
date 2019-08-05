using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCanvasSetup : MonoBehaviour
{

    public Canvas canvas;

    private void Awake()
    {

        ScreenRotation.OnRotationChange += (newOrientation) => { 
            //canvas.
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
