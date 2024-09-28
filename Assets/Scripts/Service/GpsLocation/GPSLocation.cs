using Piranest.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;

namespace Piranest
{
    public class GPSLocation : Singleton<GPSLocation>
    {

        [SerializeField] private double currentLat, currentLong;

        private event Action OnGetPermission;

        public static bool IsLocationActive => Input.location.isEnabledByUser;
        private void Start()
        {
            OnGetPermission += InitOnGetPermission;
        }

        private void OnDestroy()
        {
            OnGetPermission -= InitOnGetPermission;
        }

        public void Init()
        {
            StartCoroutine(InitGps());
        }

        private void InitOnGetPermission()
        {
            StartCoroutine(InitGps());
        }

        private IEnumerator InitGps()
        {
            // Check if the user has location service enabled.
            if (!IsLocationActive)
            {
                Debug.Log("Location not enabled on device or app does not have permission to access location");

                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    Permission.RequestUserPermission(Permission.FineLocation);
                    yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.FineLocation));
                    OnGetPermission?.Invoke();
                }
                else
                {
#if !UNITY_EDITOR
                    PopUpManager.Instance.Show("Please turn on your location", () =>
                    {
                        OnGetPermission?.Invoke();
                    });
#endif
                }
                yield break;
            }

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
                Debug.Log("Location Success");
            }

        }


        public (bool, int) IsNearLocation(float lat1, float lon1, int distanceMeter)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Input.location.isEnabledByUser) return (false, 0);
            if (Input.location.status != LocationServiceStatus.Running) return (false, 0);
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) return (false, 0);
            var lastData = Input.location.lastData;
            currentLat = lastData.latitude;
            currentLong = lastData.longitude;

            var meter = (int)CalculateDistance(currentLat, currentLong, lat1, lon1);
            bool isNear = meter < distanceMeter;
            return (isNear, meter - distanceMeter);
#elif UNITY_EDITOR
            return (true, 0);
#else
            return (false, 0);
#endif
        }

        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371000; // Earth's radius in meters
            double dLat = (lat2 - lat1) * Mathf.Deg2Rad;
            double dLon = (lon2 - lon1) * Mathf.Deg2Rad;

            double a = Mathf.Sin((float)(dLat / 2)) * Mathf.Sin((float)(dLat / 2)) +
                       Mathf.Cos((float)(lat1 * Mathf.Deg2Rad)) * Mathf.Cos((float)(lat2 * Mathf.Deg2Rad)) *
                       Mathf.Sin((float)(dLon / 2)) * Mathf.Sin((float)(dLon / 2));

            double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));
            double distance = R * c;

            return distance;
        }

    }
}
