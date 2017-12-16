using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GeocodingManager : MonoSingleton<GeocodingManager>
{
    [Serializable]
    public class AddressComponent
    {
        public string long_name;
        public string short_name;
        public string[] types;
    }

    [Serializable]
    public class Coordinates
    {
        public float lat;
        public float lng;
    }

    [Serializable]
    public class Geometry
    {
        public Coordinates location;
        public string location_type;
        public Viewport viewport;
        public bool partial_match;
        public string place_id;
        public string[] types;
    }

    [Serializable]
    public class Viewport
    {
        public Coordinates northeast;
        public Coordinates southwest;
    }

    [Serializable]
    public class Data
    {
        public Result[] results;
        public string status;
    }

    [Serializable]
    public class Result
    {        
        public AddressComponent[] address_components;
        public string formatted_address;
        public Geometry geometry;
    }

    [SerializeField] private string ApiKey = "AIzaSyCEr1nTAojBOC8CLJo0tHTjt-45hoRVPv0";

    public void Geocoding(string address, Action<float, float> onComplete)
    {
        StartCoroutine(GeocodingCoroutine(address, onComplete));
    }

    private IEnumerator GeocodingCoroutine(string address, Action<float, float> onComplete)
    {
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogWarning("Location service is not running");
            yield break;
        }

        address = address.Replace(' ', '+');

        string url = string.Format("https://" + "maps.googleapis.com/maps/api/geocode/json?address={0}&region=kr&language=ko&key={1}", address, ApiKey);
        print(url);
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

            byte[] results = www.downloadHandler.data;
            Data data = GetData(Encoding.UTF8.GetString(results));

            if (data.status == "OK")
            {
                onComplete(data.results[0].geometry.location.lat, data.results[0].geometry.location.lng);
            }
        }
    }

    public void ReverseGeocoding(float latitude, float longitude, Action<string> onComplete)
    {
        StartCoroutine(ReverseGeocodingCoroutine(latitude, longitude, onComplete));
    }

    private IEnumerator ReverseGeocodingCoroutine(float latitude, float longitude, Action<string> onComplete)
    {
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogWarning("Location service is not running");
            yield break;
        }

        string url = string.Format("https://" + "maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}&language=ko", latitude, longitude, ApiKey);

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SendWebRequest();
        while (!www.isDone)
        {
            yield return null;
        }

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] results = www.downloadHandler.data;
            Data data = GetData(Encoding.UTF8.GetString(results));

            if (data.status == "OK")
            {
                onComplete(data.results[0].formatted_address);
            }
        }
    }

    private Data GetData(string json)
    {
        return JsonUtility.FromJson<Data>(json);
    }
}
