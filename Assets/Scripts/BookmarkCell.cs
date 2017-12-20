using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookmarkData
{
    public string name;
    public YoutubeData yotubeData;
}

public class BookmarkCell : MonoBehaviour
{
    [SerializeField] private Text _nameLabel;
    [SerializeField] private GameObject _deleteButton;

    public BookmarkData BookmarkData { private set; get; }

    private void Awake()
    {
        _deleteButton.SetActive(false);
    }

    public void UpdateContent(BookmarkData bookmarkData)
    {
        BookmarkData = bookmarkData;
        _nameLabel.text = BookmarkData.name;
    }

    public void OnButtonClick()
    {
        UIManager.Instance.SingleVideoView.Load(BookmarkData.yotubeData);
        UIManager.Instance.BookmarkView.gameObject.SetActive(false);
    }

    public void OnDeleteButtonClick()
    {
        foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
        {
            if (data.id == BookmarkData.yotubeData.id)
            {
                AppManager.Instance.AppData.FavoriteVideos.Remove(data);
                UIManager.Instance.BookmarkView.BookmarkTableView.BookmarkCellList.Remove(this);
                Destroy(gameObject);
                return;
            }
        }
    }

    public void ActiveDeleteButton(bool value)
    {
        _deleteButton.SetActive(value);
    }
}
