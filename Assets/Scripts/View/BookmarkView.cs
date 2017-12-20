using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkView : View
{
    public GameObject cell;
    public Transform scrollContent;

    private void Awake()
    {
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainView.gameObject.SetActive(true);
    }
}
