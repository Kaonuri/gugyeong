using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileView : View
{
    public HistoryTableView HistoryTableView;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        HistoryTableView.UpdateTableView();
    }
}
