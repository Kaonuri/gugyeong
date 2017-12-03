using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class LocationManager : MonoSingleton<LocationManager>
{
    public bool UseLocationService = true;

    [SerializeField] private int _timeOut = 5;
    [SerializeField] private string _apiKey = "AIzaSyCEr1nTAojBOC8CLJo0tHTjt-45hoRVPv0";

    private bool _isUpdating = false;
    
    public LocationInfo LastData { get; private set; }
    public string LastAddress { get; private set; }

    public void UpdateAddress()
    {
        if (!_isUpdating)
        {
            _isUpdating = true;
            StartCoroutine(UpdateAddressProcess());
        }
    }

    private IEnumerator UpdateAddressProcess()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();

        int maxWait = _timeOut;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            LastData = Input.location.lastData;

            yield return GetAddressProcess(LastData.latitude, LastData.longitude, _apiKey);
        }

        Input.location.Stop();
        _isUpdating = false;
    }

    private IEnumerator GetAddressProcess(float latitude, float longitude, string apiKey)
    {
        string url = String.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}&language=ko", latitude, longitude, apiKey);

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SendWebRequest();
        while (!www.isDone)
        {
            Debug.Log(www.downloadProgress);
            yield return null;
        }

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Done");
//            Debug.Log("Downloaded: " + www.downloadHandler.text);

            byte[] results = www.downloadHandler.data;
            Data data = JsonManager.Instance.GetData(Encoding.UTF8.GetString(results));

            if (data.status == "OK")
            {
                LastAddress = data.results[0].formatted_address.Replace("대한민국 ", "");
            }
        }
    }
}