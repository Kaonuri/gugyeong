using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public string url;

    UniWebView webView;

    void Start()
    {
        var webViewGameObject = new GameObject("UniWebView");
        webView = webViewGameObject.AddComponent<UniWebView>();
        webView.ReferenceRectTransform = GetComponent<RectTransform>();
        // webView.Frame = new Rect(0, 0, Screen.width, Screen.height/2f);        
        url = UniWebViewHelper.StreamingAssetURLForPath("local_www/" + url);
        webView.SetVerticalScrollBarEnabled(false);
        webView.OnPageFinished += OnPageFinished;
        webView.Load(url);        
    }

    private void OnPageFinished(UniWebView webView, int statusCode, string url)
    {
        webView.EvaluateJavaScript("changeIframeUrl('https://www.youtube.com/embed/-xNN-bJQ4vI?autoplay=1')");
        webView.Show();
    }
}