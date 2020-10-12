/*
Jefferson Scomacao
https://www.jscomacao.com

GitHub - Source Code
Project: MVCC2.0

Copyright (c) 2015 Jefferson Raulino Scomação

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SafeArea : MonoBehaviour {


	#if UNITY_IOS
	[DllImport("__Internal")]
	private extern static void GetSafeAreaImpl(out float x, out float y, out float w, out float h);
	#endif

	private Rect GetSafeArea()
	{
		float x, y, w, h;
		#if UNITY_IOS && !UNITY_EDITOR
		GetSafeAreaImpl(out x, out y, out w, out h);
		#else
		x = 0;
		y = 0;
		w = Screen.width;
		h = Screen.height;
		#endif
		return new Rect(x, y, w, h);
	}

	public RectTransform canvas;
	public RectTransform panel;
	public Rect lastSafeArea = new Rect(0, 0, 0, 0);

	bool IsSafeAreaActive = true;

	void ApplySafeArea(Rect area)
	{

		var anchorMin = area.position;
		var anchorMax = area.position + area.size;
		anchorMin.x /= Screen.width;
		anchorMin.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;
		panel.anchorMin = anchorMin;
		panel.anchorMax = anchorMax;

		lastSafeArea = area;
	}

	void Start ()
	{
		IsSafeAreaActive = true;
		#if UNITY_EDITOR

		//Invoke ("TestSafe", 2f);

		#endif

		Invoke("DeactivateCheckSafe", 5f);

	}

	void DeactivateCheckSafe ()
	{
		IsSafeAreaActive = false;
	}



	void TestSafe ()
	{
		Rect safeArea = new Rect(0, 118, 1125, 2436-236);
		ApplySafeArea(safeArea);       
	}

	public float GetSafeOffset ()
	{
		return (Screen.height - lastSafeArea.size.y);
	}

	//	// Update is called once per frame
	void Update () 
	{       

		if (IsSafeAreaActive) {
			Rect safeArea = GetSafeArea (); // or Screen.safeArea if you use a version of Unity that supports it

			if (safeArea != lastSafeArea)
				ApplySafeArea (safeArea);

			Canvas.ForceUpdateCanvases ();
		}
	}
}
