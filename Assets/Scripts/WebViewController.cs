using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewController : MonoSingleton<WebViewController>
{
    [HideInInspector] public UniWebView WebView;
    public string url;
    [HideInInspector] public string videoId;

    private void Awake()
    {
        var webViewGameObject = new GameObject("UniWebView");
        WebView = webViewGameObject.AddComponent<UniWebView>();
        WebView.ReferenceRectTransform = GetComponent<RectTransform>();
        WebView.SetVerticalScrollBarEnabled(false);
        WebView.BackgroundColor = Color.black;
        WebView.OnPageFinished += OnPageFinished;
        WebView.SetBackButtonEnabled(false);
        url = UniWebViewHelper.StreamingAssetURLForPath("local_www/" + url);
        WebView.Hide();
    }

    private void OnPageFinished(UniWebView webView, int statusCode, string url)
    {
        webView.EvaluateJavaScript(string.Format("changeIframeUrl('https://www.youtube.com/embed/" + videoId + "?autoplay=1')"), payload => WebView.Show());
    }

    public void Load()
    {
        WebView.Load(url);
    }

    public void Hide()
    {
        WebView.Stop();
        WebView.Hide();
    }
}
