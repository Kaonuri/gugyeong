using UnityEngine;

[System.Serializable]
public class Data
{
    public Result[] results;
    public string status;
}

[System.Serializable]
public class Result
{
    public class AddressComponent
    {
        public string long_name;
        public string short_name;
        public string[] types;
    }

    public class Coordinates
    {
        public float lat;
        public float lng;
    }

    public class Geometry
    {
        public Coordinates location;
        public string location_type;
        public Viewport viewport;
        public bool partial_match;
        public string place_id;
        public string[] types;
    }

    public class Viewport
    {
        public Coordinates northeast;
        public Coordinates southwest;
    }

    public AddressComponent[] address_components;
    public string formatted_address;
    public Geometry geometry;    
}

public class JsonManager : Singleton<JsonManager>
{
    public Data GetData(string json)
    {
        return JsonUtility.FromJson<Data>(json);
    }
}
