using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Piranest.HTTP
{
    public static class API
    {
        public static async Task<Texture2D> DownloadTexture(string url)
        {
            var req = UnityWebRequestTexture.GetTexture(url);
            req.SendWebRequest();
            while (!req.isDone)
            {
                await Task.Delay(10);
            }
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Download Texture Failed:{req.error}");
                return null;
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(req);
                return texture;
            }

        }

        public static IEnumerator Get(string url, Action OnSuccess, Action OnFail)
        {
            var req = UnityWebRequest.Get(url);
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
                OnFail?.Invoke();
            else
                OnSuccess?.Invoke();
        }


        public static IEnumerator DownloadTexture(string url, Action<Texture2D> OnSuccess, Action OnFail = null)
        {
            var req = UnityWebRequestTexture.GetTexture(url);
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Download Texture Failed:{req.error}");
                OnFail?.Invoke();
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(req);
                OnSuccess?.Invoke(texture);
            }

        }

    }
}
