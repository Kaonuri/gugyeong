using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public UniWebView webview;

    private void Start()
    {
        var url = UniWebViewHelper.StreamingAssetURLForPath("local_www/test.html");
        webview.Load(url);
        webview.Show();
    }
}
