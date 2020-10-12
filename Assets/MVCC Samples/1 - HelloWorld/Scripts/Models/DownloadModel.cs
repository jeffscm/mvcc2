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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;

namespace MVCSamples.HelloWorld.Model
{

    public class DownloadModel
    {


        public void GetProducts(Action<View.ARSimpleView.ARViewModel> onResult)
        {

            MVCC.httpHelper.Request("http://paralagames.public.cloudvps.com/jam/json.txt", BestHTTP.HTTPMethods.Get, null, (string data, int code) =>
            {

                if (string.IsNullOrEmpty(data))
                {
                    onResult(null);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<View.ARSimpleView.ARViewModel>(data);
                    onResult(obj);
                }
            });

        }


        public void DownloadImage(string url, RawImage imageObj, Action<bool> onResult)
        {
            imageObj.texture = null;

            MVCC.httpHelper.Request(url, BestHTTP.HTTPMethods.Get, null, (Texture2D data, int code) =>
            {
                if (data == null)
                {
                    onResult(false);
                }
                else
                {
                    imageObj.texture = data;
                    onResult(true);
                }
            });
        }

        public void Download3DAsset(string url, Action<GameObject> onResult)
        {
            // Dummy Stuff
            var temp = Resources.Load(url) as GameObject;
            onResult(temp);
        }
    }
}