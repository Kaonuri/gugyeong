using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborhoodView : View
{
    private int currentIndex;
    public Toggle favoriteButton;

    private void OnEnable()
    {
        float lat = LocationManager.Instance.LastLocationData.latitude;
        float lng = LocationManager.Instance.LastLocationData.longitude;

        GeocodingManager.Instance.ReverseGeocoding(lat, lng, s => AppManager.Instance.LastAddress = s);

        currentIndex = 0;

        Load(currentIndex);
    }

    public void OnBackButtonClick()
    {
        WebViewController.Instance.Hide();
        UIManager.Instance.MainView.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnFavoriteButtonClick(bool value)
    {
        YoutubeData youtubeData = AppManager.Instance.NearbyYoutubeDataList[currentIndex];
        YoutubeDataManager.PlaceInfo placeInfo = YoutubeDataManager.Instance.GetPlaceInfo(youtubeData);

        if (value)
        {
            foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
            {
                if (data.id == youtubeData.id)
                    return;
            }

            AppManager.Instance.AppData.FavoriteVideos.Add(youtubeData);
            AppManager.Instance.SaveData();
        }

        else
        {
            foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
            {
                if (data.id == youtubeData.id)
                {
                    AppManager.Instance.AppData.FavoriteVideos.Remove(data);
                    AppManager.Instance.SaveData();
                    return;
                }
            }
        }
    }

    public void OnPreviousButtonClick()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = AppManager.Instance.NearbyYoutubeDataList.Count - 1;

        Load(currentIndex);
    }

    public void OnNextButtonClick()
    {
        currentIndex++;
        if (currentIndex == AppManager.Instance.NearbyYoutubeDataList.Count)
            currentIndex = 0;

        Load(currentIndex);
    }

    public void Load(int index)
    {
        YoutubeData youtubeData = AppManager.Instance.NearbyYoutubeDataList[index];

        bool isOn = false;
        foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
        {
            if (data.id == youtubeData.id)
            {
                isOn = true;
                break;
            }
        }

        favoriteButton.isOn = isOn; 

        WebViewController.Instance.videoId = youtubeData.id;
        WebViewController.Instance.Load();
    }
}
