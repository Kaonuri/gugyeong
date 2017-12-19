using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class YoutubeSnippet  {

    public string publishedAt;
    public string channelId;
    public string title;
    public string description;
    public YoutubeTumbnails thumbnails;
    public string channelTitle;
    public string[] tags;
    public string categoryId;
}

[Serializable]
public class YoutubeTumbnails
{
    public YoutubeThumbnailData defaultThumbnail;
    public YoutubeThumbnailData mediumThumbnail;
    public YoutubeThumbnailData highThumbnail;
    public YoutubeThumbnailData standardThumbnail;
}

[Serializable]
public class YoutubeThumbnailData
{
    public string url;
    public string width;
    public string height;
}

