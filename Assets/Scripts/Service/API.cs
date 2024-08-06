using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Piranest.API
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
                Texture2D myTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;
                return myTexture;
            }

        }


    }
}
