using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class TableViewController<T> : ViewController
{    
    protected List<T> tableData = new List<T>();
    [SerializeField] private RectOffset padding;
    [SerializeField] private float spacingHeight = 4.0f;

    private ScrollRect cachedScrollRect;

    public ScrollRect CachedScrollRect
    {
        get
        {
            if (cachedScrollRect == null)
            {
                cachedScrollRect = GetComponent<ScrollRect>();
            }
            return cachedScrollRect;
        }
    }

    protected virtual void Awake()
    {
    }

    protected virtual float CellHeightAtIndex(int index)
    {
        return 0.0f;
    }

    protected void UpdateContentSize()
    {
        float contentHeight = 0.0f;
        for (int i = 0; i < tableData.Count; i++)
        {
            contentHeight += CellHeightAtIndex(i);
            if (i > 0)
            {
                contentHeight += spacingHeight;
            }
        }

        Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
        sizeDelta.y = padding.top + contentHeight + padding.bottom;
        CachedScrollRect.content.sizeDelta = sizeDelta;
    }

    [SerializeField] private GameObject cellBase;
    private LinkedList<TableViewCell<T>> cells = new LinkedList<TableViewCell<T>>();

    protected virtual void Start()
    {
        cellBase.SetActive(false);
    }

    private TableViewCell<T> CreateCellForIndex(int index)
    {
        GameObject obj = Instantiate(cellBase) as GameObject;
        obj.SetActive(true);
        TableViewCell<T> cell = obj.GetComponent<TableViewCell<T>>();

        Vector3 scale = cell.transform.localScale;
        Vector2 sizeDelta = cell.CachedRectTransform.sizeDelta;

        Vector2 offsetMin = cell.CachedRectTransform.offsetMin;
        Vector2 offsetMax = cell.CachedRectTransform.offsetMax;

        cell.transform.SetParent(cellBase.transform.parent);

        cell.transform.localScale = scale;
        cell.CachedRectTransform.sizeDelta = sizeDelta;
        cell.CachedRectTransform.offsetMin = offsetMin;
        cell.CachedRectTransform.offsetMax = offsetMax;

        UpdateCellForIndex(cell, index);
        cells.AddLast(cell);

        return cell;
    }

    private void UpdateCellForIndex(TableViewCell<T> cell, int index)
    {
        cell.DataIndex = index;

        if (cell.DataIndex >= 0 && cell.DataIndex <= tableData.Count - 1)
        {
            
        }
    }
}
