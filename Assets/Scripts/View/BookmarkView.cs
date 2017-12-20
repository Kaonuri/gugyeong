using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkView : View
{
    public BookmarkTableView BookmarkTableView;

    public bool IsEditMode { private set; get; }

    private void Awake()
    {
        IsEditMode = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OnBackButtonClick();
        }
    }

    private void OnEnable()
    {
        BookmarkTableView.UpdateTableView();
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainView.gameObject.SetActive(true);
    }

    public void OnEditButtonClick()
    {
        IsEditMode = !IsEditMode;

        foreach (var cell in BookmarkTableView.BookmarkCellList)
        {
            cell.ActiveDeleteButton(IsEditMode);
        }
    }
}
