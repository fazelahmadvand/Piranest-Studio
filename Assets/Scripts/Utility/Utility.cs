using System;
using System.Collections;
using UnityEngine;

namespace Piranest
{
    public static class Utility
    {
        public const string SCRIPTABLE_PATH = "Data/";

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

    }
}
