using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMono : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        MVCC.OnStart?.Invoke();
    }
}
