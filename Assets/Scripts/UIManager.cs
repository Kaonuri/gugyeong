using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{   
    public MainView MainView;
    public NeighborhoodView NeighborhoodView;
    public SettingView SettingView;

    private void Awake()
    {
        MainView.gameObject.SetActive(true);
        NeighborhoodView.gameObject.SetActive(false);
        SettingView.gameObject.SetActive(false);
    }    
}
