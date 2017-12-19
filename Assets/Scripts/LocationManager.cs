using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class LocationManager : MonoSingleton<LocationManager>
{
    [SerializeField] private bool _enableByRequest = true;

    public float LocationUpdateInterval = 0.2f;
    public int MaxWait = 20;

    public LocationInfo LastLocationData { private set; get; }

    public void StartLocationService(Action onComplete = null)
    {
        LocationService service = Input.location;

        if (!_enableByRequest)
        {
            Debug.LogError("Location service is not enabled by requset");
            return;
        }

        if (!service.isEnabledByUser)
        {
            Debug.LogError("Location service is not enabled by user");
            return;
        }

        if (service.status == LocationServiceStatus.Stopped)
        {            
            StartCoroutine(StartLolcationServiceCoroutine(onComplete));
        }
        else
        {
            Debug.LogWarning("Location service can only be started when status is stopped");
        }
    }

    private IEnumerator StartLolcationServiceCoroutine(Action onComplete)
    {
        LocationService service = Input.location;

        service.Start();

        int maxWait = MaxWait;
        while (service.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.LogError("Location service timed out");
            yield break; 
        }

        if (service.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Location service failed");
            yield break;
        }
        else if (service.status == LocationServiceStatus.Running)
        {
            Debug.Log("Location service is running");
            LastLocationData = service.lastData;

            if (onComplete != null)
                onComplete();

            StartCoroutine(RunLocationServiceCoroutine());
        }
        else
        {
            Debug.LogError("Unknown Error!");
        }
    }

    private IEnumerator RunLocationServiceCoroutine()
    {
        LocationService service = Input.location;

        while (service.status == LocationServiceStatus.Running)
        {
            LastLocationData = service.lastData;
//            Debug.Log("Location: " + LastLocationData.latitude + " " + LastLocationData.longitude + " " + LastLocationData.altitude + " " + LastLocationData.horizontalAccuracy + " " + LastLocationData.timestamp);
            yield return new WaitForSeconds(LocationUpdateInterval);
        }
    }
    

    public void StopLocationService()
    {
        LocationService service = Input.location;
        if (service.status == LocationServiceStatus.Stopped)
        {
            Debug.Log("Location service is already stopped");
            return;
        }

        service.Stop();
    }

    public float CalculateDistanceBetweenPlaces(float lat1, float lng1, float lat2, float lng2)
    {
        float r = 6371;        

        float dlat = lat2 - lat1;
        float dlng = lng2 - lng1;

        float a = Mathf.Sin(dlat * Mathf.Deg2Rad / 2f) * Mathf.Sin(dlat * Mathf.Deg2Rad / 2f) +
                                                                   Mathf.Cos(lat1 * Mathf.Deg2Rad) *
                                                                   Mathf.Cos(lat2 * Mathf.Deg2Rad) *
                                                                   Mathf.Sin(dlng * Mathf.Deg2Rad / 2f) *
                                                                   Mathf.Sin(dlng * Mathf.Deg2Rad / 2f);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        float d = r * c;
        return d;
    }
}
