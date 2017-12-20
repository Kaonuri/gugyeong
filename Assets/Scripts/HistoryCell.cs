using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryData
{
    public string name;
    public YoutubeData yotubeData;
}

public class HistoryCell : MonoBehaviour
{
    [SerializeField] private Text _nameLabel;

    public HistoryData HistoryData { private set; get; }

    public void UpdateContent(HistoryData historyData)
    {
        HistoryData = historyData;
        _nameLabel.text = HistoryData.name;
    }

    public void OnButtonClick()
    {
        UIManager.Instance.SingleVideoView.Load(HistoryData.yotubeData);
        UIManager.Instance.MainView.ProfileView.gameObject.SetActive(false);
        UIManager.Instance.MainView.gameObject.SetActive(false);
    }
}
