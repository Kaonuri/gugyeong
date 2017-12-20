using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleVideoView : View
{
    public Toggle favoriteButton;
    public Text TitleLabel;
    public Text AddressLabel;

    private YoutubeData currentYoutubeData;   

    public void Load(YoutubeData youtubeData)
    {
        currentYoutubeData = youtubeData;

        gameObject.SetActive(true);

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

        YoutubeDataManager.PlaceInfo placeInfo = YoutubeDataManager.Instance.GetPlaceInfo(youtubeData);
        TitleLabel.text = placeInfo.Name;
        AddressLabel.text = placeInfo.Address;


        WebViewController.Instance.videoId = youtubeData.id;
        WebViewController.Instance.Load();
    }

    public void OnBackButtonClick()
    {
        WebViewController.Instance.Hide();
        UIManager.Instance.MainView.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnFavoriteButtonClick(bool value)
    {
        YoutubeDataManager.PlaceInfo placeInfo = YoutubeDataManager.Instance.GetPlaceInfo(currentYoutubeData);

        if (value)
        {
            foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
            {
                if (data.id == currentYoutubeData.id)
                    return;
            }

            AppManager.Instance.AppData.FavoriteVideos.Add(currentYoutubeData);
            AppManager.Instance.SaveData();
        }

        else
        {
            foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
            {
                if (data.id == currentYoutubeData.id)
                {
                    AppManager.Instance.AppData.FavoriteVideos.Remove(data);
                    AppManager.Instance.SaveData();
                    return;
                }
            }
        }
    }
}
