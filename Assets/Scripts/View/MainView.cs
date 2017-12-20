using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
    public View ProfileView;
    public Text Address;
    public InputField SearchBarInputField;

    private void Awake()
    {
    }

    private void Start()
    {        
    }

    public void OnGpsButtonClick()
    {
        float lat = LocationManager.Instance.LastLocationData.latitude;
        float lng = LocationManager.Instance.LastLocationData.longitude;
        GeocodingManager.Instance.ReverseGeocoding(lat, lng, UpdateAddress);
        List<YoutubeData> youtubeDataList = YoutubeDataManager.Instance.YoutubeDataList;
        List<YoutubeDataManager.PlaceInfo> placeInfoList = YoutubeDataManager.Instance.PlaceInfoList;

        AppManager.Instance.NearbyYoutubeDataList.Clear();

        for (int i = 0; i < placeInfoList.Count; i++)
        {
            float distance = LocationManager.Instance.CalculateDistanceBetweenPlaces(lat, lng, placeInfoList[i].Lat, placeInfoList[i].Lng);
            if (distance <= 100f)
            {                
                AppManager.Instance.NearbyYoutubeDataList.Add(youtubeDataList[i]);
            }
        }
    }

    private void UpdateAddress(string address)
    {
        AppManager.Instance.LastAddress = address;
        address = address.Replace("대한민국 ", "");
        Address.text = address;
    }

    public void OnProfileButtonClick()
    {
        ProfileView.gameObject.SetActive(true);
    }

    public void OnProfileExitButtonClick()
    {
        ProfileView.gameObject.SetActive(false);
    }

    public void OnNeighborhoodButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.NeighborhoodView.gameObject.SetActive(true);
    }

    public void OnRecommendButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void OnBookmarkButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.BookmarkView.gameObject.SetActive(true);
    }

    public void OnSettingButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.SettingView.gameObject.SetActive(true);
    }

    public void OnSearchButtonClick()
    {
//        GeocodingManager.Instance.Geocoding(SearchBarInputField.text, (f, f1) => );
    }
}
