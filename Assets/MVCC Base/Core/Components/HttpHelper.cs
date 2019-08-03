using UnityEngine;
using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HttpHelper
{

    public virtual HTTPRequest Request(string url, HTTPMethods method, Dictionary<string, string> headers = null, Action<string, int> onFinish = null)
    {

        var request = new HTTPRequest(new Uri(url), HTTPMethods.Post, (req, resp) =>
            {
                if (req.State != HTTPRequestStates.Aborted)
                {
                    if (resp != null && resp.IsSuccess)
                    {
                        onFinish?.Invoke(resp.DataAsText, resp.StatusCode);
                    }
                    else if (resp != null)
                    {
                        onFinish?.Invoke(null, resp.StatusCode);
                    }
                    else
                    {
                        onFinish?.Invoke(null, 0);
                    }
                }
            }
        );

        if (headers != null)
        {
            foreach (var d in headers)
            {
                request.AddField(d.Key, d.Value);
            }
        }
        request.Send();

        return request;
    }

    public virtual HTTPRequest Request(string url, HTTPMethods method, Dictionary<string, string> headers = null, Action<Texture2D, int> onFinish = null)
    {

        var request = new HTTPRequest(new Uri(url), HTTPMethods.Post, (req, resp) =>
        {
            if (req.State != HTTPRequestStates.Aborted)
            {
                if (resp != null && resp.IsSuccess)
                {
                    onFinish?.Invoke(resp.DataAsTexture2D, resp.StatusCode);
                }
                else if (resp != null)
                {
                    onFinish?.Invoke(null, resp.StatusCode);
                }
                else
                {
                    onFinish?.Invoke(null, 0);
                }
            }
        }
        );

        if (headers != null)
        {
            foreach (var d in headers)
            {
                request.AddField(d.Key, d.Value);
            }
        }
        request.Send();

        return request;
    }

    public virtual void CancelDownload(HTTPRequest request)
    {
        if (request != null)
        {
            request.Abort();
        }
    }

}

