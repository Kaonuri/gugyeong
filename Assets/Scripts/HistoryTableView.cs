using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryTableView : MonoBehaviour
{
    [SerializeField] private GameObject baseCell;
    public List<HistoryCell> HistoryCellList = new List<HistoryCell>();

    private void Awake()
    {
        baseCell.SetActive(false);
    }

    public void UpdateTableView()
    {
        foreach (HistoryCell cell in HistoryCellList)
        {
            Destroy(cell.gameObject);
        }
        HistoryCellList.Clear();

        foreach (var data in AppManager.Instance.History)
        {
            GameObject obj = Instantiate(baseCell, baseCell.transform.parent);
            obj.SetActive(true);

            HistoryCell historyCell = obj.GetComponent<HistoryCell>();

            HistoryData historyData = new HistoryData();
            historyData.yotubeData = data;
            historyData.name = YoutubeDataManager.Instance.GetPlaceInfo(data).Name;

            historyCell.UpdateContent(historyData);

            HistoryCellList.Add(historyCell);
        }
    }
}
