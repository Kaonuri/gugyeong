using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSettingView : View
{
    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.SettingView.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OnBackButtonClick();
        }
    }
}
