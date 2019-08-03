using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;
using MVC.View;

namespace MVC.Model
{

    public class DownloadModel
    {


        public void GetProducts(Action<ARSimpleView.ARViewModel> onResult)
        {

            MVCC.httpHelper.Request("http://paralagames.public.cloudvps.com/jam/json.txt", BestHTTP.HTTPMethods.Get, null, (string data, int code) =>
            {

                if (string.IsNullOrEmpty(data))
                {
                    onResult(null);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<ARSimpleView.ARViewModel>(data);
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