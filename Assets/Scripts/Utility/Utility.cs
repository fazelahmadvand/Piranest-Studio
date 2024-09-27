using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace Piranest
{
    public static class Utility
    {
        public const string SCRIPTABLE_PATH = "Data/";
        public const string GOOGLE_MAP_API_KEY = "AIzaSyA-Z74y1-7uuNsidlMZUSYGYjkHWk5nVxs";
        public const int MAP_ZOOM = 15;

        public static IEnumerator DoAfter(float duration, Action OnStart, Action OnEnd)
        {
            OnStart?.Invoke();
            yield return new WaitForSeconds(duration);
            OnEnd?.Invoke();
        }

        public static void OpenGoogleMap(float locationLat, float locationLong)
        {
            string url = $"https://www.google.com/maps/search/?api=1&query={locationLat},{locationLong}";
            Application.OpenURL(url);
        }

        public static string OnePointGoogleMap(float latitude, float longitude, int width, int height)
        {
            return $"https://maps.googleapis.com/maps/api/staticmap?center={latitude},{longitude}&zoom={MAP_ZOOM}&size={width}x{height}&" +
                $"markers=color:red%7Clabel:C%7C{latitude},{longitude}&key={GOOGLE_MAP_API_KEY}";
        }

        /// <summary>
        /// start x is latitude and y is longtitude
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string TwoPointsGoogleMap(Vector2 start, Vector2 end, int width, int height)
        {
            Vector2 center = start + end;
            center /= 2;
            return $"https://maps.googleapis.com/maps/api/staticmap?center={center.x},{center.y}&zoom={MAP_ZOOM}" +
                $"&size={width}x{height}&markers=color:red%7Clabel:S%7C{start.x},{start.y}&markers=color" +
                $":red%7Clabel:E%7C{end.x},{end.y}&key={GOOGLE_MAP_API_KEY}";
        }

        public static void FixFrameRate()
        {
            int hertz = (int)Screen.currentResolution.refreshRateRatio.value;
            Application.targetFrameRate = hertz;
        }

        public static bool HasLocationPermission()
        {
#if UNITY_ANDROID
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation)) return false;
#endif

            return true;
        }

        public static string GenerateRandomString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            StringBuilder result = new(length);
            System.Random random = new();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                result.Append(chars[index]);
            }

            return result.ToString();
        }

    }
}
