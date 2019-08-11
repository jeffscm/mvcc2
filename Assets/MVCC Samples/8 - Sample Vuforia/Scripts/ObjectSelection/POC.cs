using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class POC : AppElement
{

    public GameObject[] defaults;
    public AnchorBehaviour stage;

    public GameObject RealObject
    {
        get
        {
            return defaults[1];
        }
    }


    public Renderer renderChangeColor;

    bool _isPlaced = false;
    public bool IsPlaced { get { return _isPlaced; } }
       
    public void PlaceReal()
    {
        _isPlaced = true;
        defaults[0].SetActive(false);
        defaults[1].SetActive(true);
    }

    public void StartPlacement()
    {
        defaults[0].SetActive(true);
        defaults[1].SetActive(false);
    }
}
