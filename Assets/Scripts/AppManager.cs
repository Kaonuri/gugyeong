using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] class Datas { public string name; public List<string> likes; public int level; }

public class AppManager : MonoSingleton<AppManager>
{
    public AppData AppData = new AppData();

    public string LastAddress;
    public List<YoutubeData> NearbyYoutubeDataList = new List<YoutubeData>();

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        LocationManager.Instance.StartLocationService(OnStartLocationServiceComplete);
    }

    private void OnStartLocationServiceComplete()
    {
        UIManager.Instance.MainView.OnGpsButtonClick();
    }

    public void LoadData()
    {
        string appDataJson = PlayerPrefs.GetString("AppDataJson", "");

        if (appDataJson != "")
            AppData = JsonUtility.FromJson<AppData>(appDataJson);
    }

    public void SaveData()
    {
        string appDataJson = JsonUtility.ToJson(AppData);

        PlayerPrefs.SetString("AppDataJson", appDataJson);
        PlayerPrefs.Save();
    }
}
