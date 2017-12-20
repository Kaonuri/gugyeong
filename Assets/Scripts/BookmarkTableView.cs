using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkTableView : MonoBehaviour
{
    [SerializeField] private GameObject baseCell;
    public List<BookmarkCell> BookmarkCellList = new List<BookmarkCell>();

    private void Awake()
    {
        baseCell.SetActive(false);
    }

    public void Update()
    {
    }

    public void UpdateTableView()
    {
        foreach (BookmarkCell cell in BookmarkCellList)
        {
            Destroy(cell.gameObject);
        }
        BookmarkCellList.Clear();

        foreach (var data in AppManager.Instance.AppData.FavoriteVideos)
        {
            GameObject obj = Instantiate(baseCell, baseCell.transform.parent);
            obj.SetActive(true);

            BookmarkCell bookmarkCell = obj.GetComponent<BookmarkCell>();

            BookmarkData bookmarkData = new BookmarkData();
            bookmarkData.yotubeData = data;
            bookmarkData.name = YoutubeDataManager.Instance.GetPlaceInfo(data).Name;

            bookmarkCell.UpdateContent(bookmarkData);

            BookmarkCellList.Add(bookmarkCell);
        }
    }
}
