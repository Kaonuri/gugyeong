using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Text text;
    public UniWebView webview;

    private void Start()
    {
        var url = UniWebViewHelper.StreamingAssetURLForPath("local_www/test.html");
        webview.Load(url);
        webview.Show();
    }
}
