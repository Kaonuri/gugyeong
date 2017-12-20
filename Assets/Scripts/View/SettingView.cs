using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingView : View
{
    public void OnProfileSettingButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ProfileSettingView.gameObject.SetActive(true);
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainView.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OnBackButtonClick();
        }
    }
}
