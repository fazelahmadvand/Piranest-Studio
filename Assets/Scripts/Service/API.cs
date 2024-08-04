using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Piranest.API
{
    public static class API
    {

        public static IEnumerator DownloadTexture(string url, Action<Texture2D> OnSuccess)
        {
            var req = UnityWebRequestTexture.GetTexture(url);
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Download Texture Failed:{req.error}");
            }
            else
            {
                Texture2D myTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;
                OnSuccess(myTexture);

            }

        }


    }
}
