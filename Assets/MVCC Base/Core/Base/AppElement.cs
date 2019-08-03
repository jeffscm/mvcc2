using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppElement : MonoBehaviour {

   
	public App app 
    { 
        get 
        {
            return MVCC.app;
        }
    }
}
