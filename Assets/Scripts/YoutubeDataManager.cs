using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class YoutubeDataManager : MonoSingleton<YoutubeDataManager>
{
    private const string APIKey = "AIzaSyCEr1nTAojBOC8CLJo0tHTjt-45hoRVPv0";
    private const string ChannelID = "UCOO1_93_2Cr5ZN853a91jBw";
    public List<YoutubeData> YoutubeDataList = new List<YoutubeData>();
    public List<PlaceInfo> PlaceInfoList = new List<PlaceInfo>();

    private void Start()
    {
        GetChannelVideos(ChannelID, OnGetChannelVideosComplete);
    }

    private void OnGetChannelVideosComplete(List<YoutubeData> dataList)
    {
        YoutubeDataList = dataList;
        foreach (var data in YoutubeDataList)
        {
            PlaceInfoList.Add(GetPlaceInfo(data));
        }
    }

    public void GetChannelVideos(string channelId, Action<List<YoutubeData>> callback)
    {
        StartCoroutine(GetVideosFromChannel(channelId, callback));
    }

    IEnumerator GetVideosFromChannel(string channelId, Action<List<YoutubeData>> callback = null)
    {
        string nextPageToken = "";
        List<YoutubeData> youtubeDatas = new List<YoutubeData>();

        do
        {
            string url = "https://www.googleapis.com/youtube/v3/search?order=date&type=video&part=snippet&channelId=" + channelId + "&maxResults=50" + "&key=" + APIKey;
            if (nextPageToken != "")
            {
                url = url + "&pageToken=" + nextPageToken;
            }
            WWW call = new WWW(url);
            yield return call;
            Debug.Log(call.url);
            JSONNode result = JSON.Parse(call.text);
            nextPageToken = result["nextPageToken"].Value;
            YoutubeData[] searchResults = new YoutubeData[result["items"].Count];
            for (int itemsCounter = 0; itemsCounter < searchResults.Length; itemsCounter++)
            {
                searchResults[itemsCounter] = new YoutubeData();
                searchResults[itemsCounter].id = result["items"][itemsCounter]["id"]["videoId"];
                SetSnippet(result["items"][itemsCounter]["snippet"], out searchResults[itemsCounter].snippet);
            }
            youtubeDatas.AddRange(searchResults);
        } while (nextPageToken != "");

        Debug.Log("Get Videos From Channel Finish");

        if (callback != null)
            callback.Invoke(youtubeDatas);
    }

    private void SetSnippet(JSONNode resultSnippet, out YoutubeSnippet data)
    {
        data = new YoutubeSnippet();
        data.publishedAt = resultSnippet["publishedAt"];
        data.channelId = resultSnippet["channelId"];
        data.title = resultSnippet["title"];
        data.description = resultSnippet["description"];
        //Thumbnails
        data.thumbnails = new YoutubeTumbnails();
        data.thumbnails.defaultThumbnail = new YoutubeThumbnailData();
        data.thumbnails.defaultThumbnail.url = resultSnippet["thumbnails"]["default"]["url"];
        data.thumbnails.defaultThumbnail.width = resultSnippet["thumbnails"]["default"]["width"];
        data.thumbnails.defaultThumbnail.height = resultSnippet["thumbnails"]["default"]["height"];
        data.thumbnails.mediumThumbnail = new YoutubeThumbnailData();
        data.thumbnails.mediumThumbnail.url = resultSnippet["thumbnails"]["medium"]["url"];
        data.thumbnails.mediumThumbnail.width = resultSnippet["thumbnails"]["medium"]["width"];
        data.thumbnails.mediumThumbnail.height = resultSnippet["thumbnails"]["medium"]["height"];
        data.thumbnails.highThumbnail = new YoutubeThumbnailData();
        data.thumbnails.highThumbnail.url = resultSnippet["thumbnails"]["high"]["url"];
        data.thumbnails.highThumbnail.width = resultSnippet["thumbnails"]["high"]["width"];
        data.thumbnails.highThumbnail.height = resultSnippet["thumbnails"]["high"]["height"];
        data.thumbnails.standardThumbnail = new YoutubeThumbnailData();
        data.thumbnails.standardThumbnail.url = resultSnippet["thumbnails"]["standard"]["url"];
        data.thumbnails.standardThumbnail.width = resultSnippet["thumbnails"]["standard"]["width"];
        data.thumbnails.standardThumbnail.height = resultSnippet["thumbnails"]["standard"]["height"];
        data.channelTitle = resultSnippet["channelTitle"];
        //TAGS
        data.tags = new string[resultSnippet["tags"].Count];
        for (int index = 0; index < data.tags.Length; index++)
        {
            data.tags[index] = resultSnippet["tags"][index];
        }
        data.categoryId = resultSnippet["categoryId"];
    }

    public class PlaceInfo
    {
        public string Name;
        public string Address;
        public float Lat;
        public float Lng;
    }

    public PlaceInfo GetPlaceInfo(YoutubeData data)
    {
        string[] split = data.snippet.description.Split('/');

        PlaceInfo placeInfo = new PlaceInfo();
        placeInfo.Name = split[0];
        placeInfo.Address = split[1];
        placeInfo.Lat = float.Parse(split[2].Split(',')[0]);
        placeInfo.Lng = float.Parse(split[2].Split(',')[1].TrimEnd('.'));
        return placeInfo;
    }
}
