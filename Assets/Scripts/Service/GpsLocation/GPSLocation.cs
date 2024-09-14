using System.Collections;
using UnityEngine;

namespace Piranest
{
    public class GPSLocation : Singleton<GPSLocation>
    {

        [SerializeField] private float currentLat, currentLong;


        private IEnumerator Start()
        {
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
                Debug.Log("Location not enabled on device or app does not have permission to access location");

            // Starts the location service.
            Input.location.Start();

            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // If the service didn't initialize in 20 seconds this cancels location service use.
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.LogError("Unable to determine device location");
                yield break;
            }
            else
            {
                // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
                var lastData = Input.location.lastData;
                Debug.Log($"Location: {lastData.latitude} {lastData.longitude} {lastData.altitude} {lastData.horizontalAccuracy} {lastData.timestamp}");
            }

        }


        public (bool, int) IsNearLocation(float lat1, float lon1, int distanceMeter)
        {
#if UNITY_EDITOR
            return (true, 0);
#else
var lastData = Input.location.lastData;
            currentLat = lastData.latitude;
            currentLong = lastData.longitude;

            var meter = CalculateDistance(currentLat, currentLong, lat1, lon1);
            bool isNear = meter < distanceMeter;
            return (isNear, meter);
#endif


        }

        public static int CalculateDistance(float lat1, float lon1, float lat2, float lon2)
        {
            const float R = 6371e3f; // Radius of the Earth in meters

            float φ1 = lat1 * Mathf.Deg2Rad; // φ in radians
            float φ2 = lat2 * Mathf.Deg2Rad; // φ in radians
            float Δφ = (lat2 - lat1) * Mathf.Deg2Rad; // Δφ in radians
            float Δλ = (lon2 - lon1) * Mathf.Deg2Rad; // Δλ in radians

            float a = Mathf.Sin(Δφ / 2) * Mathf.Sin(Δφ / 2) +
                      Mathf.Cos(φ1) * Mathf.Cos(φ2) *
                      Mathf.Sin(Δλ / 2) * Mathf.Sin(Δλ / 2);
            float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

            return (int)Mathf.Round(R * c); // Distance in meters
        }

    }
}
