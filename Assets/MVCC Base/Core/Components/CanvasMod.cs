using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
public class CanvasMod : MonoBehaviour {


    [SerializeField]
    CanvasScaler cs;

	void Awake () 
    {
        #if UNITY_IOS
#if !IPAD

        if (Device.generation.ToString().IndexOf("iPad") > -1)

#endif
        {

            var v = cs.referenceResolution * 1.6f;

            cs.referenceResolution = v;
           

        }
#endif
	}
	
	
}
