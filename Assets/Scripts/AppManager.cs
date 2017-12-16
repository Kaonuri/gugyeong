using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoSingleton<AppManager>
{
    [HideInInspector] public string LastAddress;

    private void Start()
    {
        LocationManager.Instance.StartLocationService();
    }
}
