using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MemberInformation
{
    public string nickname;
    public string id;
    public string password;
}


[Serializable]
public class AppData
{    
    public List<YoutubeData> FavoriteVideos;
}
