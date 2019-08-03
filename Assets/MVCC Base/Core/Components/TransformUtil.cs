using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtil
{
    public static void ClearTransform(this Transform trans)
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            MonoBehaviour.Destroy(trans.GetChild(i).gameObject);
        }
    }
}