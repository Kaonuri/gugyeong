using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{   
    public MainView MainView;
    public NeighborhoodView NeighborhoodView;
    public BookmarkView BookmarkView;
    public SettingView SettingView;
    public ProfileSettingView ProfileSettingView;
    public JoinView JoinView;
    public LoginView LoginView;

    private void Awake()
    {
        MainView.gameObject.SetActive(true);
        NeighborhoodView.gameObject.SetActive(false);
        BookmarkView.gameObject.SetActive(false);
        SettingView.gameObject.SetActive(false);
        ProfileSettingView.gameObject.SetActive(false);
        JoinView.gameObject.SetActive(false);
        LoginView.gameObject.SetActive(false);
    }
}
