using System.Collections;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private bool _enableByRequest = true;

    public float LocationUpdateInterval = 0.2f;
    public int MaxWait = 20;

    public LocationInfo LastLocationData { private set; get; }

    public void StartLocationService()
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
            StartCoroutine(StartLolcationServiceCoroutine());
        }
        else
        {
            Debug.LogWarning("Location service can only be started when status is stopped");
        }
    }

    private IEnumerator StartLolcationServiceCoroutine()
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
            Debug.Log("Location services is running");
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
            Debug.Log("Location: " + LastLocationData.latitude + " " + LastLocationData.longitude + " " + LastLocationData.altitude + " " + LastLocationData.horizontalAccuracy + " " + LastLocationData.timestamp);
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
}
