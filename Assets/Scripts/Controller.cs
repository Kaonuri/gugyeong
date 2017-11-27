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

        webView.Frame = new Rect(0, 0, Screen.width, Screen.height/2f);        
        url = UniWebViewHelper.StreamingAssetURLForPath("local_www/" + url);
        webView.SetVerticalScrollBarEnabled(false);
        webView.Load(url);
        webView.Show();
    }
}